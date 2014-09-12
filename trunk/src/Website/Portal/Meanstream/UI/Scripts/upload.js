$(document).ready(function () {

$('#AjaxUploader1_File').uploadify({
        'uploader': '/Meanstream/UI/Scripts/uploadify.swf',
        'script': '/Meanstream/UI/Scripts/upload/upload.aspx',
        'folder': '/portals/tmp',
        'cancelImg': '/App_Themes/Meanstream.2011/Uploader/images/msprogressclose.gif',
        'buttonImg': '/App_Themes/Meanstream.2011/Uploader/images/msbtnBrowse.png',
        'width': '60',
        'auto': true,
        'onComplete': function (event, queueID, fileObj, response, data) { handleUpload(event, queueID, fileObj, response, data); },
        'method': 'POST',
        'wmode': 'transparent'
    });

});   