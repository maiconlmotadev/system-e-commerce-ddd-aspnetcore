
var ObjectSale = new Object();

ObjectSale.LoadProducts = function () {
    $.ajax({
        type: "GET",
        url: "/api/ListProductsWithStock",
        dataType: "JSON",
        cache: false,
        async: true,
        success: function (data) {

            var htmlContent = "";

            data.forEach(function (entitie) {

                htmlContent += "<div class='col-xs-12 col-sm-4 col-md-4 col-lg-4' >"

                var idName = "name_" + Entitie.id;
                var idQuant = "quant_" + Entitie.id;

                htmlContent += "<label id='" + idName + "' > Product: " + Entitie.name + "</label></br>";
                htmlContent += "<label > Price: " + Entitie.price + "</label></br>";

                htmlContent += "Quantity : <input type'number' value='1' id='" + idQuant + "'>";

            });
        }
    })
}

$(function () {
    ObjectSale.LoadProducts()
}); 