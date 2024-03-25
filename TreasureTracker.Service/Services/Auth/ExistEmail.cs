using System.Net;
using System.Net.Mail;
using TreasureTracker.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using TreasureTracker.Domain.Entities;
using Microsoft.Extensions.Configuration;
using TreasureTracker.Domain.IRepositories;
using TreasureTracker.Service.Interfaces.Auth;
using TreasureTracker.Service.Helpers.Exceptions;
using TreasureTracker.Service.Interfaces.UserCodes;
using TreasureTracker.Service.DTOs.Auth;
using TreasureTracker.Service.Helpers;

namespace TreasureTracker.Service.Services.Auth;
public class ExistEmail:IExistEmail
{
    private readonly IRepository<User> _repository;
    private readonly IUserCodeService _userCodeService;
    private readonly IRepository<UserCode> _codeRepository;
    private readonly IConfiguration _configuration;

    public ExistEmail(IRepository<User> repository,
                     IConfiguration configuration,
                     IUserCodeService userCodeService,
                     IRepository<UserCode> codeRepository)
    {
        _repository = repository;
        _configuration = configuration.GetSection("Email");
        _userCodeService = userCodeService;
        _codeRepository = codeRepository;
    }

    public async Task<ExistEmailEnum> EmailExist(string email)
    {
        var user = await _repository
            .GetAllAsync()
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user is null)
            return ExistEmailEnum.EmailNotFound;

        if (user.IsVerified)
            return ExistEmailEnum.EmailFound;

        var resend = await ResendCodeAsync(email);

        if (!resend)
            throw new TTrackerException(403, "Birozdan keyinroq qayta urinib ko'ring!");

        return ExistEmailEnum.EmailNotChecked;
    }

    public async Task<bool> VerifyCodeAsync(VerificationPostModel model)
    {
        var userCodeAny = await _codeRepository
            .GetAllAsync()
            .IgnoreQueryFilters()
            .Include(u => u.User)
            .AnyAsync(c => c.User.Email.ToLower() == model.Email.ToLower() && c.ExpireDate > DateTime.UtcNow && c.Code == model.Code);

        if (userCodeAny)
        {
            var user = await _repository
                .GetAllAsync()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(u => u.Email.ToLower() == model.Email.ToLower());

            if (user is null)
                return false;

            if (!user.IsVerified)
            {
                user.IsVerified = true;
                await _repository.UpdateAasync(user);
                await _repository.SaveChangesAsync();
            }
            return true;
        }

        return false;
    }

    public async Task<bool> ResendCodeAsync(string email)
    {
        var userCodeAny = await _codeRepository.GetAllAsync()
            .AnyAsync(c => c.User.Email == email && c.ExpireDate > DateTime.UtcNow);

        if (userCodeAny)
            return false;

        var user = await _repository
            .GetAllAsync()
            .Where(u => u.Email == email)
            .Select(u => new { u.Id })
            .FirstOrDefaultAsync();

        if (user is null)
            return false;

        var randomNumber = new Random().Next(100000, 999999);

        var message = new Message()
        {
            Subject = "Bu kodni boshqalarga bermang!",
            To = email,
            Body = $"Sizning tasdiqlash kodingiz: {randomNumber}"
        };


        var userCode = new UserCode()
        {
            Code = randomNumber.ToString(),
            UserId = user.Id,
            ExpireDate = DateTime.UtcNow.AddMinutes(3)
        };

        _ = await _userCodeService.CreateAsync(userCode);
        await this.SendMessage(message);

        return true;
    }

    public Task SendMessage(Message message)
    {
        var _smtpModel = new
        {
            Host = _configuration["Host"]!,
            Email = (string)_configuration["EmailAddress"]!,
            Port = 587,
            AppPassword = _configuration["Password"]!
        };

        using (MailMessage mm = new MailMessage(_smtpModel.Email, message.To))
        {
            mm.Subject = message.Subject;
            mm.Body = message.Body;
            mm.IsBodyHtml = false;
            using (System.Net.Mail.SmtpClient smtp = new SmtpClient())
            {
                smtp.Host = _smtpModel.Host;
                smtp.Port = _smtpModel.Port;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                NetworkCredential NetworkCred = new NetworkCredential(_smtpModel.Email, _smtpModel.AppPassword);
                smtp.Credentials = NetworkCred;
                smtp.Send(mm);
            }
        }

        return Task.CompletedTask;
    }
}
