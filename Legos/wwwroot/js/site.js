// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

(function cookie_accept() {
    var button = document.querySelector("#cookieConsent button[data-cookie-string]");
    button.addEventListener("click", function (event) {
        document.cookie = button.dataset.cookieString;
    }, false);
})();