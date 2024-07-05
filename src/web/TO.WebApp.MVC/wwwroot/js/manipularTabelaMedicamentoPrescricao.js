
$(document).ready(function () {

    // Função para adicionar uma nova linha na tabela
    $("#adicionarItem").on("click", function () {
        let qtdeLinha = $("#tabelaMedicamento tbody .trNovaLinha").length;
        let idNovaLinha = `ItensNovos[${qtdeLinha}].MedicamentoId`;
        let qtdeNovaLinha = `ItensNovos[${qtdeLinha}].Quantidade`;

        let novaLinha = $("<tr class='trNovaLinha'>");

        novaLinha.append($("<td>")
            .append($("#Medicamentos").clone()
                .attr('id', idNovaLinha)
                .attr('name', idNovaLinha)
                .attr("data-id-inicio", "ItensNovos")
                .attr("data-id-final", "MedicamentoId")
                .show()));

        novaLinha.append($("<td>")
            .append($("#Quantidades").clone()
                .attr('id', qtdeNovaLinha)
                .attr('name', qtdeNovaLinha)
                .attr("data-id-inicio", "ItensNovos")
                .attr("data-id-final", "Quantidade")
                .show()));

        novaLinha.append($("<td style='width: 0%;'>")
            .append($('<button type="button" class="btn btn-danger removerItem"> <i class="fas fa-trash-alt"></i></button>')));

        $("#tabelaMedicamento tbody").append(novaLinha);
    });

    // Remover uma linha e atualizar os ids e names
    $(document).on('click', '.removerItem', function () {
        $(this).closest('tr').find('.trNovaLinha td').rules('remove');
        $(this).closest('tr').remove();
        atualizarIdsENames();
    });

    // Atualiza os ids e names das linhas restantes
    function atualizarIdsENames() {
        $('#tabelaMedicamento tbody .trNovaLinha').each(function (index, tr) {
            $(tr).find('input, select').each(function () {
                var idBase = $(this).attr('data-id-inicio');
                var idFinal = $(this).attr('data-id-final');

                $(this).attr('id', idBase + index);
                $(this).attr('name', `${idBase}[${index}].${idFinal}`);
            });
        });
    }

    //Atualiza a quantidade de medicação
    $(document).on('change', ".qtdMedicamento", function (evt) {
        let quantidade = $(this).val();
        let medicamentoId = $(this).data("medicamentoid");
        let prescricaoId = $("#Id").val();
        let url = $(this).data("url");

        $.ajax({
            method: "POST",
            url: url,
            data: { prescricaoId, medicamentoId, quantidade }
        })
            .done(() => {
                alertRetorno(tipoAlert.Success, "Quantidade medicamento atualizada com sucesso.");
            })
            .fail(() => {
                alertRetorno(tipoAlert.Error);
            });
    });

    //Remove o item no banco de dados
    $(document).on('click', '.removerItemTabela', function (evt) {
        evt.preventDefault();

        const btn = $(this);
        let prescricaoId = $("#Id").val();
        let url = $(this).data("url");
        let medicamentoId = $(this).data("id");

        $.ajax({
            method: "POST",
            url: url,
            data: { prescricaoId: prescricaoId, medicamentoId:medicamentoId }
        })
            .done(function (msg) {
                $(btn).closest('tr').remove();
                alertRetorno(tipoAlert.Success, "Medicamento removido com sucesso.");
            })
            .fail(() => {
                alertRetorno(tipoAlert.Error);
            });
    });
});