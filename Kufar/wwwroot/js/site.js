// Write your JavaScript code.

$(function () {
    $("#Countryid").change(function () {
        var url = '@Url.Content("~/")' + "Advertisements/GetStateById";
        var ddlsource = "#Countryid";
        $.getJSON(url, { id: $(ddlsource).val() }, function (data) {
            var items = '';
            $("#stateid").empty();
            $.each(data, function (i, row) {
                items += "<option value='" + row.value + "'>" + row.text + "</option>";
            });
            $("#stateid").html(items);
        });

    });
});