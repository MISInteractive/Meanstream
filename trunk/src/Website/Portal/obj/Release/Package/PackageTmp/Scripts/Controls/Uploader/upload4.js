$(document).ready(function () {

    $('#AjaxUploader4_File').uploadify({
        'uploader': '/Meanstream/UI/Scripts/uploadify.swf',
        'script': '/Meanstream/UI/Scripts/upload/upload.aspx',
        'folder': '/portal/images/tmp',
        'cancelImg': '/App_Themes/Meanstream.Black/Uploader/images/msprogressclose.gif',
        'buttonImg': '/App_Themes/Meanstream.Black/Uploader/images/msbtnBrowse.png',
        'width': '60',
        'auto': true,
        'onComplete': function (event, queueID, fileObj, response, data) { handleFinishUpload(event, queueID, fileObj, response, data); },
        'method': 'POST',
        'wmode': 'transparent'
    });

});   