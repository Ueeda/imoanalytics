$(document).ready(function () {
    $('.js-telefone').focusout(function () {
        var phone, element;
        element = $(this);
        element.unmask();
        phone = element.val().replace(/\D/g, '');
        if (phone.length > 10) {
            element.mask("(99) 99999-999?9");
        } else {
            element.mask("(99) 9999-9999?9");
        }
    }).trigger('focusout');

    $('.js-cpf').mask('999.999.999-99', { reverse: true });
    $(".js-cep").mask('99999-999');
    $(".js-data").mask('99/99/9999');
    $(".js-horario").mask('99:99');
    $(".js-dinheiro").maskMoney({
        decimal: ",",
        thousands: ""
    });

    $('[data-toggle="tooltip"]').tooltip();
});