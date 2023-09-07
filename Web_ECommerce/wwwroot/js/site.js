﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


var AlertObject = new Object();

AlertObject.ViewAlert = function (type, message) {
    $("JavaScriptAlert").html("");

    // tipo
    // 1 alert-success
    // 2 alert-warning
    // 3 alert-danger

    var AlertClassType = "";

    if (type == 1) {
        AlertClassType = "alert alert-success";
    } else if (type == 2) {
        AlertClassType = "alert alert-warning"
    } else if (type == 3) {
        AlertClassType = "alert alert-danger"
    }

    var AlertDiv = $("<div>", { class: AlertClassType });

}

