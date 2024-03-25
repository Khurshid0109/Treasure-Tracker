const body = document.querySelector('body');
const btn = document.querySelector('.btn');
const icon = document.querySelector('.btn__icon');

//to save the dark mode use the object "local storage".

//function that stores the value true if the dark mode is activated or false if it's not.
function store(value) {
    localStorage.setItem('darkmode', value);
}

//function that indicates if the "darkmode" property exists. It loads the page as we had left it.
function load() {
    const darkmode = localStorage.getItem('darkmode');

    //if the dark mode was never activated
    if (!darkmode) {
        store(false);
        icon.classList.add('fa-sun');
    }
    else if (darkmode == 'true') { //if the dark mode is activated
        body.classList.add('darkmode');
        icon.classList.add('fa-moon');
    }
    else if (darkmode == 'false') { //if the dark mode exists but is disabled
        icon.classList.add('fa-sun');
    }
}


load();

btn.addEventListener('click', () => {

    body.classList.toggle('darkmode');
    icon.classList.add('animated');

    //save true or false
    store(body.classList.contains('darkmode'));

    if (body.classList.contains('darkmode')) {
        icon.classList.remove('fa-sun');
        icon.classList.add('fa-moon');
    }
    else {
        icon.classList.remove('fa-moon');
        icon.classList.add('fa-sun');
    }

    setTimeout(() => {
        icon.classList.remove('animated');
    }, 500)
})


// Language selecter
const dropdown = document.querySelector(".dropdown");
const list = document.querySelector(".list");
const item = document.querySelectorAll(".item");
const selected = document.querySelector(".selected");
const selectedImg = document.querySelector(".selectedImg");

dropdown.addEventListener('click', () => {
    list.classList.toggle('show');
});

item.forEach(elem => {
    elem.addEventListener('click', () => {
        const image = elem.querySelector('img');
        const text = elem.querySelector('.text');

        selectedImg.src = image.src;
        selected.innerHTML = text.innerHTML;

        // Save selected image and text to localStorage
        localStorage.setItem('selectedImage', image.src);
        localStorage.setItem('selectedText', text.innerHTML);
    });
})

// Retrieve selected image and text from localStorage on page load
document.addEventListener('DOMContentLoaded', () => {
    const selectedImage = localStorage.getItem('selectedImage');
    const selectedText = localStorage.getItem('selectedText');

    if (selectedImage && selectedText) {
        selectedImg.src = selectedImage;
        selected.innerHTML = selectedText;
    }
});