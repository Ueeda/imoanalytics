$(document).ready(function () {
    $(".js-telefone").inputmask("mask", { "mask": "(99) 99999-9999" });
    $(".js-cpf").inputmask("mask", { "mask": "999.999.999-99" }, { reverse: true }, { removeMaskOnSubmit: true });
    $(".js-cep").inputmask("mask", { "mask": "99999-999" });
    $(".js-data").inputmask("mask", { "mask": "99/99/9999" });
    $(".js-horario").inputmask("mask", { "mask": "99:99" });
});

$('form').submit(function () {
    $(".js-cpf").unmask();
});
