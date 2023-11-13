
var objectJson = {}

var jsonView = new JSONViewer();

function configureJson() {

    var json = $("#JsonInformation").val();

    if (json != undefined && json != null && json != "") {

        objectJson = JSON.parse(json);

        jsonView.showJSOM(objectJson);

        document.querySelector("#json").appendChild(jsonView.getContainer());
    }
}

$(function () {
    configureJson();
});