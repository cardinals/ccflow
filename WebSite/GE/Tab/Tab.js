function tabChange(obj, id,styleID) {
    var arrayli = obj.parentNode.getElementsByTagName("li"); //��ȡli����
    var arrayul = document.getElementById(id).getElementsByTagName("ul"); //��ȡul����
    for (i = 0; i < arrayul.length; i++) {
        if (obj == arrayli[i]) {
            arrayli[i].className = styleID + "_cli";
            arrayul[i].className = "";
        }
        else {
            arrayli[i].className = "";
            arrayul[i].className = styleID + "_hidden";
        }
    }
}