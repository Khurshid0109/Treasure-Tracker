const body = document.querySelector("body"),
      modeToggle = body.querySelector(".mode-toggle");
      sidebar = body.querySelector("nav");
      sidebarToggle = body.querySelector(".sidebar-toggle");

let getMode = localStorage.getItem("mode");
if(getMode && getMode ==="dark"){
    body.classList.toggle("dark");
}

let getStatus = localStorage.getItem("status");
if(getStatus && getStatus ==="close"){
    sidebar.classList.toggle("close");
}

modeToggle.addEventListener("click", () =>{
    body.classList.toggle("dark");
    if(body.classList.contains("dark")){
        localStorage.setItem("mode", "dark");
    }else{
        localStorage.setItem("mode", "light");
    }
});

sidebarToggle.addEventListener("click", () => {
    sidebar.classList.toggle("close");
    if(sidebar.classList.contains("close")){
        localStorage.setItem("status", "close");
    }else{
        localStorage.setItem("status", "open");
    }
})



$(document).ready(function () {
    // Fetch categories list via AJAX
    $.ajax({
        url: '/UserPage/GetCategories',
        type: 'GET',
        success: function (data) {
            var select = $('#category');
            console.log("ajax invoked");
            $.each(data, function (index, category) {
                select.append($('<option></option>').val(category.id).text(category.name));
            });
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error fetching categories:', errorThrown);
        }
    });

    // Handle category selection
    $('#category').change(function () {
        var categoryId = $(this).val();
        $('#categoryId').val(categoryId);
    });
});
