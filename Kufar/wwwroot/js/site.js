// Write your JavaScript code.

function GetCity(_stateId) {
    var procemessage = "<option value='0'> Please wait...</option>";
    $("#ddlcity").html(procemessage).show();
    var url = "/Home/GetCityByCountryId/";

    $.ajax({
        url: url,
        data: { countryid: countryid },
        cache: false,
        type: "POST",
        success: function (data) {
            var markup = "<option value='0'>Select City</option>";
            for (var x = 0; x < data.length; x++) {
                markup += "<option value=" + data[x].value + ">" + data[x].text + "</option>";
            }
            $("#ddlcity").html(markup).show();
        },
    });

}