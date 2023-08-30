﻿
var ObjectSale = new Object();

ObjectSale.AddCart = function (idProduct) {

    var name = $("#name_" + idProduct).val();
    var quant = $("#quant_" + idProduct).val();

    $.ajax({
        type: "POST",
        url: "/api/AddProductCart",
        dataType: "JSON",
        cache: false,
        async: true,
        data: {
            "id": idProduct, "name": name, "quant": quant
        },
        success: function (data) {  

        }
    });
}

ObjectSale.LoadProducts = function () {
    $.ajax({
        type: "GET",
        url: "/api/ListProductsWithStock",
        dataType: "JSON",
        cache: false,
        async: true,
        success: function (data) {

            var htmlContent = "";

            data.forEach(function (Entitie) {

                htmlContent += "<div class='col-xs-12 col-sm-4 col-md-4 col-lg-4' >"

                var idName = "name_" + Entitie.id;
                var idQuant = "quant_" + Entitie.id;

                htmlContent += "<label id='" + idName + "' > Product: " + Entitie.name + "</label></br>";
                htmlContent += "<label > Price: " + Entitie.price + "</label></br>";

                htmlContent += "Quantity : <input type'number' value='1' id='" + idQuant + "'>";

                htmlContent += "<input type='button' onclick='ObjectSale.AddCart(" + Entitie.id + ")' value ='Buy'> </br> ";

                htmlContent += "</div>";

            });

            $("#DivSale").html(htmlContent);
        }
    });
}

$(function () {
    ObjectSale.LoadProducts();
}); 