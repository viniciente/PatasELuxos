// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Forma 1
// function aumentarFonte() {
//   document.body.style.fontSize =
//     parseFloat(getComputedStyle(document.body).fontSize) + 5 + "px";
// }

// function diminuirFonte() {
//   document.body.style.fontSize =
//     parseFloat(getComputedStyle(document.body).fontSize) - 5 + "px";
// }

//Forma 2
//function aumentarFonte() {
//    document.querySelectorAll("*").forEach(function (el) {
//        const currentSize = parseFloat(getComputedStyle(el).fontSize);
//        el.style.fontSize = currentSize + 2 + "px";
//    });
//}

//function diminuirFonte() {
//    document.querySelectorAll("*").forEach(function (el) {
//        const currentSize = parseFloat(getComputedStyle(el).fontSize);
//        el.style.fontSize = currentSize - 2 + "px";
//    });
//}

let tamanhoFonte = 100; // percentual inicial (100% = tamanho padrão)

function aumentarFonte() {
    if (tamanhoFonte < 200) { // limite máximo de 200%
        tamanhoFonte += 10;
        document.body.style.fontSize = tamanhoFonte + "%";
    }
}

function diminuirFonte() {
    if (tamanhoFonte > 50) { // limite mínimo de 50%
        tamanhoFonte -= 10;
        document.body.style.fontSize = tamanhoFonte + "%";
    }
}
