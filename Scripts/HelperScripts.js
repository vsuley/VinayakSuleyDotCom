var XMLHttpRequestObject = false;
if (window.XMLHttpRequest) {
    XMLHttpRequestObject = new XMLHttpRequest();
} else if (window.ActiveXObject) {
    XMLHttpRequestObject = new ActiveXObject("Microsoft.XMLHTTP");
}

function getData(datasource, divId) {
    if (XMLHttpRequestObject) {
        var obj = document.getElementById(divId);
        XMLHttpRequestObject.open("GET", datasource);

        XMLHttpRequestObject.onreadystatechange = function () {
            obj.innerHTML = XMLHttpRequestObject.responseText;
        }

        XMLHttpRequestObject.send(null);
    }
}

function displayBlurMessage(divId) {
    var obj = document.getElementById(divId);
    obj.innerHTML = "what are you doing hun";
}
