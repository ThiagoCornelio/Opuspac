const tipoAlert = {
    Success: "success",
    Error: "error",
    Warning: "warning"
}

function alertRetorno(tipo, mensagem) {

    //Obter o botão
    let alert = $(`.toast-${tipo}`);
    let bsAlert = new bootstrap.Toast(alert);

    //Trocar a mensagem
    let mensagemAlert;
    let possuiMensagem = mensagem && mensagem.length > 0


    switch (tipo) {
        case tipoAlert.Success:
            mensagemAlert = possuiMensagem > 0 ? mensagem : "Sucesso.";
            break;

        case tipoAlert.Error:
            mensagemAlert = possuiMensagem > 0 ? mensagem : "Alguma coisa deu errado. Tente novamente ou contate nosso time de suporte.";
            break;

        case tipoAlert.Warning:
            mensagemAlert = possuiMensagem > 0 ? mensagem : "Deseja realmente realizar a ação ?";
            break;
        default:
    }

    $(`.toast-body-${tipo}`).text(mensagemAlert);

    bsAlert.show();
}
