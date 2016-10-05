/*
$('#tree').treeview({
    data: getTree(),
    text: "titulo1",
    enableLinks: true
});*/

// String remplaze para crear las url correctamente
String.prototype.replaceLatinChar = function () {
    return output = this.replace(/á|é|í|ó|ú|ñ|ä|ë|ï|ö|ü/ig, function (str, offset, s) {
        var str = str == "á" ? "a" : str == "é" ? "e" : str == "í" ? "i" : str == "ó" ? "o" : str == "ú" ? "u" : str == "ñ" ? "n" : str;
        str = str == "Á" ? "A" : str == "É" ? "E" : str == "Í" ? "I" : str == "Ó" ? "O" : str == "Ú" ? "U" : str == "Ñ" ? "N" : str;
        str = str == "Á" ? "A" : str == "É" ? "E" : str == "Í" ? "I" : str == "Ó" ? "O" : str == "Ú" ? "U" : str == "Ñ" ? "N" : str;
        str = str == "ä" ? "a" : str == "ë" ? "e" : str == "ï" ? "i" : str == "ö" ? "o" : str == "ü" ? "u" : str;
        str = str == "Ä" ? "A" : str == "Ë" ? "E" : str == "Ï" ? "I" : str == "Ö" ? "O" : str == "Ü" ? "U" : str;
        return (str);
    });
}

$(function () {
    $('.decimalMoney').maskMoney();
    
    // Funcion para los input generar codigo enlace 
    var textOutput =    $('.textOutput');
    var textInput = $('.textInput');

    textInput.keyup(function () {
        var val = $(this).val();
        var valOupt = val.replaceLatinChar();
        valOupt = valOupt.replace(/ /g, "-");
       
        textOutput.val(valOupt);
    });
    //Final

});
$("#example-basic").treetable({ expandable: true });

$("#example-basic tbody").on("mousedown", "tr", function () {
    $(".selected").not(this).removeClass("selected");
    $(this).toggleClass("selected");
});



$('.mediaColorBox').colorbox({ width: "80%", height: "80%" ,iframe:true});

function callImagepIcker(clase) {
    $("#" + clase).html($('.' + clase + ' :selected').length);

    $("." + clase).imagepicker();
    $('.' + clase).change(function () {
        $("#" + clase).html($('.' + clase + ' :selected').length);
    });
}

$.getJSON(filesSelectUrl, function (list) {
    $.each(list, function (_idx, item) {
        var selet = "";
        console.log(filesSelectJson.indexOf(item.id) == 0 || filesSelectJson.indexOf(item.id) > 0);
        if (filesSelectJson.indexOf(item.id) == 0 || filesSelectJson.indexOf(item.id) > 0)
            var selet = "selected";

        if (item.type == "application/pdf") {
            $('.image-picker-pdf').append($('<option value="' + item.id + '" data-img-css="height:100px" data-img-src="' + item.thumbnail_url + '" ' + selet + '>' + item.name + '</option>'));
        } else if (item.type == "video") {
            $('.image-picker-video').append($('<option value="' + item.id + '" data-img-css="height:100px" data-img-src="' + item.thumbnail_url + '" ' + selet + '>' + item.name + '</option>'));
        } else {
            $('.image-picker-img').append($('<option value="' + item.id + '" data-img-css="height:100px" data-img-src="' + item.thumbnail_url + '" ' + selet + '>' + item.name + '</option>'));

        }
    });
    callImagepIcker("image-picker-img");
    callImagepIcker("image-picker-pdf");
    callImagepIcker("image-picker-video");


});
$(document).ready(function () {
    $(".mask").inputmask("decimal", { radixPoint: '.', digits: 2 });
    $("#tags").inputosaurus({
        width: '500px'
    });
});

