// Hamburger
const hamburger = document.querySelector('.navbar-toggler'),
    detailMenu = document.querySelector('.detail-nav'),
    body = document.querySelector('body'),
    bgHamburger = document.querySelector('.hamburger-background')

// open hamburger
hamburger.addEventListener('click', () => {
        detailMenu.classList.toggle('open');
        bgHamburger.classList.remove('d-none');
        body.style.overflowY = 'hidden';
    })
    // closed hamburger
bgHamburger.addEventListener('click', () => {
    detailMenu.classList.remove('open');
    bgHamburger.classList.add('d-none');
    body.style.overflowY = 'auto';
})