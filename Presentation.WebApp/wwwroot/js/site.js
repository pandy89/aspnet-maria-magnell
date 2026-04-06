
/* FAQ Accordion */
const questions = document.querySelectorAll('.question');

questions.forEach((question) =>
    question.addEventListener('click', () => {
        if (question.parentNode.classList.contains('active')) {
            question.parentNode.classList.toggle('active');
        } else {
            questions.forEach((q) => q.parentNode.classList.remove('active'));
            question.parentNode.classList.add('active');
        }
    })
);

/* Menus */

// Menu dropdown JS
document.addEventListener("DOMContentLoaded", () => {
    const toggles = document.querySelectorAll(".dropdown-toggle");

    toggles.forEach(toggle => {
        toggle.addEventListener("click", (e) => {
            const parent = toggle.closest(".menu-item");

            // Close other open dropdowns
            document.querySelectorAll(".menu-item--open").forEach(item => {
                if (item !== parent) {
                    item.classList.remove("menu-item--open");
                }
            });

            // Toggle current
            parent.classList.toggle("menu-item--open");
        });
    });

    // Click outside = close
    document.addEventListener("click", (e) => {
        if (!e.target.closest(".menu-item--has-dropdown")) {
            document.querySelectorAll(".menu-item--open").forEach(item => {
                item.classList.remove("menu-item--open");
            });
        }
    });
});


// I got help from chatgpt and i ask for a common mobilemenu.
// Mobile menu
document.addEventListener("DOMContentLoaded", () => {
    const button = document.getElementById("mobile-menu-button");
    const menu = document.querySelector(button.dataset.target);
    const overlay = document.getElementById("mobile-menu-overlay");

    const toggleMenu = () => {
        menu.classList.toggle("is-open");
        overlay.classList.toggle("is-open");
    };

    button.addEventListener("click", toggleMenu);
});

document.addEventListener("DOMContentLoaded", () => {
    const flyout = document.querySelector("#mobile-menu-flyout");

    flyout.addEventListener("click", (e) => {
        const toggle = e.target.closest(".menu-item--has-dropdown > .menu-item-link");

        if (!toggle) return;

        e.preventDefault();

        const parent = toggle.parentElement;

        parent.classList.toggle("menu-item--open");
    });
});


// Clone menu from desktop to the mobilemenu
document.addEventListener("DOMContentLoaded", () => {
    const flyout = document.querySelector("#mobile-menu-flyout .mobile-nav");

    const mainMenu = document.querySelector(".main-menu .menu").cloneNode(true);
    const accountMenu = document.querySelector(".account-menu .menu").cloneNode(true);

    const mainWrapper = document.createElement("div");
    const accountWrapper = document.createElement("div");

    mainWrapper.appendChild(mainMenu);
    accountWrapper.appendChild(accountMenu);

    flyout.appendChild(mainWrapper);
    flyout.appendChild(accountWrapper);
});