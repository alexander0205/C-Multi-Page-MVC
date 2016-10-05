var json_obj = JSON.parse(Get("http://localhost:18499/CustomDataProvider/FileHandlerJpg"));
var objec = [];
$.each(json_obj.files, function (data, value) {
    objec.push({
        "image": value.url,
        "thumb": value.thumbnailUrl,
        "folder": ""
    });
});
document.body.innerHTML = objec;
