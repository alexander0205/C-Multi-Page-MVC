// Register a template definition set named "default".
var allText;
function readTextFile(file) {
    var rawFile = new XMLHttpRequest();
    rawFile.open("GET", file, false);
    rawFile.onreadystatechange = function () {
        if (rawFile.readyState === 4) {
            if (rawFile.status === 200 || rawFile.status == 0) {
                 allText = rawFile.responseText;
                return allText;
            }
        }
    }
    rawFile.send(null);
}
 readTextFile("/Scripts/ckeditor/templates/template1.html");

CKEDITOR.addTemplates('default',
{
    // The name of the subfolder that contains the preview images of the templates.
  //  imagesPath: CKEDITOR.getUrl(CKEDITOR.plugins.getPath('templates') + 'templates/images/'),

    // Template definitions.
    templates:
		[
			{
			    title: 'Pagina Basica',
			    image: 'template1.gif',
			    description: 'Description of My Template 1.',
			    html: allText
			},
			{
			    title: 'Formularios',
			    html:
					'<h3>Template 2</h3>' +
					'<p>Type your text here.</p>'
			}
		]
});