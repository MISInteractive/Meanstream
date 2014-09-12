var startPage = 1;
var endPage = 1;
var startFile = 0;
var endFile = 0;
var totalFiles = 0;
var NumPages = 1;
var uploadify1Obj = false;
var _dialog;
var previewActive = false;
var largeImgPrev;
var isFireFox = false;
var uploadZipArray = [];
var uploadZipFlag = false;
var tiffPath;
var pixlrUrl;
var pixlrTitle;
var leftSplitterWidth;
var pollInterval;
$().ready(function () {
    document.oncontextmenu = function () {
        return false;
    }
    $('#selFolder').val('/' + uploadFolder);
    contentWindowSize();
    $(window).resize(function () {

        setTimeout(contentWindowSize, 1);

    });
    //firefox check. Firefox handles dom properties differently, namely outerText is undefined for our filetree elements
    if (navigator.userAgent.toLowerCase().indexOf('firefox') > -1) {
        isFireFox = true;
    }
    $('#StatusSelFolder').val('/' + uploadFolder);
    getFolderProperties(rootFolder);

    $("#FileExplorer1").splitter({
        type: "v",
        outline: false,
        minLeft: 100, sizeLeft: leftSplitterWidth, minRight: 300,
        resizeToWidth: true,
        accessKey: 'I'
    });
    $('#fileTreeDemo_1').fileTree({ root: rootFolder, expandSpeed: -1, collapseSpeed: -1, script: '/Services/FileManager/directories.aspx?r=1' }, function (file) {
        getFiles(file);
        handleFolderClick(file);
        $("#FileExplorer1_RightPane").addClass("msScroll");

    });
    getFiles(rootFolder);
    $("#FolderString").val(rootFolder);
    $("#preview").addClass("msPreview");
    $("#command1").tooltip('#tooltip');
    $("#command2").tooltip('#tooltip');
    $("#command3").tooltip('#tooltip');
    $("#command4").tooltip('#tooltip');
    $("#command5").tooltip('#tooltip');
    $("#command6").tooltip('#tooltip');
    $("#command7").tooltip('#tooltip');
    $("#command5").bind("click", function () {

        $('#help').dialog({ title: '<span class="spanTitle">Help</span>', height: 650, width: 900 });
    });
    $("#command7").bind("click", function () {

        $('#systemInfo').dialog({ title: '<span class="spanTitle">System Info</span>', height: 400, width: 400, open: function (event, ui) {
            getSystemProperties(rootFolder);
        }
        });
    });
    $("#totalslides").bind("click", function () {

        $('#imageInfo').dialog({ title: '<span class="spanTitle">Image Details</span>', zIndex: 99999999999999, height: 500, width: 600, open: function (event, ui) {
            var obj = new Object();
            _dialog = $('#imageInfo');
            obj.path = tiffPath;

            $.ajax(
                {
                    type: "POST",
                    dataType: "json",
                    data: "{'obj':" + JSON.stringify(obj) + "}",
                    contentType: "application/json",
                    url: "/Services/BoteggaServices.asmx/getImageInfo",
                    success: function (response) {
                        var data = response.d;

                        $('#pDateCreated').text(data.DateCreated);
                        $('#pLastModified').text(data.DateLastModified);
                        $('#pLastAccessed').text(data.DateLastAccessed);
                        $('#pImageName').text(data.Title);
                        $('#pFileSize').text(data.FileSize);
                        $('#pImageType').text(data.FileType);
                        $('#pImageDimensions').text(data.Dimensions);
                        $('#pDownloadLink').attr('href', data.Url);
                        pixlrUrl = data.Url;
                        pixlrTitle = data.Title;

                    }
                }
            );

        }
        });
    });
    $("#command1").bind("click", function () {
        $("#FolderString").val(rootFolder);
        $('#txtNewFolder').val('');
        $('#nfStatus').hide();
        $('#newfolder').dialog({ title: '<span class="spanTitle">Create New Folder</span>' });

    });
    $("#command2").bind("click", function () {

        $('#uStatus').hide();
        $('#btnUploadCancel').show();
        var f = $("#FolderString").val();
        uploadFolder = f.replace(uploadPathFilter, '');
        $("#txtUpload").val(uploadFolder);
        var fInput = $("<input type='file' name='AjaxUploader1_File'  id='AjaxUploader1_File' class='msInputFile' />");
        $('.fileClass').append(fInput);
        $("#upload").dialog({ title: '<span class="spanTitle">Upload New Files</span>', height: 550, width: 550, close: function (event, ui) { destroyUploader(); }, open: function (event, ui) {
            uploadZipFlag = false;
            uploadZipArray.length = 0;
            $('#AjaxUploader1_File').uploadify({
                'uploader': '/scripts/controls/uploader/uploadify.swf',
                'script': '/Services/FileManager/upload.aspx',
                'folder': uploadFolder,
                'cancelImg': '/App_Themes/Meanstream.2011/Uploader/images/msprogressclose.gif',
                'buttonImg': '/App_Themes/Meanstream.2011/Uploader/images/msbtnBrowse.png',
                'width': '60',
                'auto': true,
                'onComplete': function (event, ID, fileObj, response, data) { uploadFileEvent(fileObj); },
                'onAllComplete': function (event, data) { handleUpload(); },
                'method': 'POST',
                'multi': true,
                'onOpen': function (event, ID, fileObj) {
                    $('#AjaxUploader1_FileQueue div.uploadifyQueueItem:first-child').hide();
                },
                'wmode': 'transparent'
            });


        }


        });

    });

    $("#command3").bind("click", function () {
        var file = $("#FolderString").val();
        refreshFolder(file);

    });

    $("#command4").bind("click", function () {
        var f = $("#FolderName").val();
        $('#dfStatus').hide();
        if (f == '') {
            alert("You must select a folder to delete!");
            return false;
        }
        $("#txtDeleteFolder").val(f);

        $('#deletefolder').dialog({ height: 250, width: 300, title: '<span class="spanTitle">Delete Folder</span>' });
    });

    $("#command6").bind("click", function () {  //reload tree
        folder_init();
        $('#fileTreeDemo_1').fileTree({ root: rootFolder, script: '/Services/FileManager/directories.aspx?r=1' }, function (file) {
            getFiles(file);
            handleFolderClick(file);
            $("#FileExplorer1_RightPane").addClass("msScroll");

        });
        getFiles(rootFolder);
    });

    $("#btnFolderCancel").bind("click", function () {
        $('#newfolder').dialog('close');
    });
    $("#btnFolderOK").bind("click", function () {
        var file = $("#FolderString").val();
        handleNewFolder(file);
    });
    $("#btnDeleteCancel").bind("click", function () {
        $('#deletefolder').dialog('close');
    });
    $("#btnDeleteOK").bind("click", function () {
        var file = $("#FolderString").val();
        handleDeleteFolder(file);
    });
    $("#btnRenameCancel").bind("click", function () {
        $('#renamefolder').dialog('close');

    });
    $("#btnRenameOK").bind("click", function () {
        var file = $("#FolderString").val();
        handleRenameFolder(file);
    });
    $("#btnRenameFileCancel").bind("click", function () {

        $('#renamefile').dialog('close');
    });
    $("#btnRenameFileOK").bind("click", function () {
        var file = $("#FileString").val();
        handleRenameFile(file);
    });
    $("#btnDeleteFileCancel").bind("click", function () {
        $('#deletefile').dialog('close');
    });
    $("#btnDeleteFileOK").bind("click", function () {
        var file = $("#FileString").val();
        handleDeleteFile(file);
    });
    $("#btnUploadCancel").bind("click", function () {
        $('#upload').dialog('close');
    });

    $("#btnNext").bind("click", function () {

        if (startPage < endPage) {
            startPage = startPage + 1;
            $('#startPage').text(startPage);
            endFile = endFile + 15;
            if (endFile > totalFiles) {
                endFile = totalFiles;
            }
            $('#endFile').text(endFile);
            startFile = startFile + 15;
            $('#startFile').text(startFile);
            var file = $("#FolderString").val();
            populateFileTreePage(file, startPage)
        }
    });

    $("#btnPrev").bind("click", function () {

        if (startPage > 1) {
            startPage = startPage - 1;
            $('#startPage').text(startPage);
            endFile = endFile - 15;
            if (endFile < 15) {
                endFile = totalFiles;
            }
            $('#endFile').text(endFile);
            startFile = startFile - 15;
            $('#startFile').text(startFile);
            var file = $("#FolderString").val();
            populateFileTreePage(file, startPage)
        }
    });


});                                                               //end document ready

function getFiles(file) {
    $('#fileTreeDemo_2').css("visibility", "visible");
    $('#startPage').text('1');


    var obj = new Object();
    obj.src = file;

    $.ajax(
                {
                    type: "POST",
                    dataType: "json",
                    data: "{'obj':" + JSON.stringify(obj) + "}",
                    contentType: "application/json",
                    url: "/Services/BoteggaServices.asmx/GetFilePagingObject",
                    success: function (response) {
                        var sProp = response.d;
                        var p = sProp.split("|");
                        var tFiles = p[0];
                        if (tFiles > 0) {
                            startPage = 1;
                            endPage = p[1];
                            startFile = 1;
                            totalFiles = tFiles;
                            if (totalFiles > 15) {
                                endFile = 15;
                            } else {
                                endFile = totalFiles
                            }



                            $("#endPage").text(endPage);
                            $("#startFile").text('1');
                            $("#endFile").text(endFile);
                            $("#totalFiles").text(totalFiles);
                            populateFileTree(file);
                        } else {
                            startPage = 1;
                            endPage = 1;
                            startFile = 0;
                            endFile = 0;
                            totalFiles = 0;

                            $("#endPage").text('1');
                            $("#startFile").text('0');
                            $("#endFile").text('0');
                            $("#totalFiles").text('0');
                            populateFileTree(file);
                        }


                    }
                }
            );




}
function populateFileTree(file) {
    $('#fileTreeDemo_2').fileTree({ root: file, script: '/Services/FileManager/directories.aspx' }, function (file) {
        handleFileClick(file);
    });
}
function populateFileTreePage(file, page) {
    var scriptString = '/Services/FileManager/directories.aspx?p=' + startPage;
    $('#fileTreeDemo_2').fileTree({ root: file, script: scriptString }, function (file) {
        handleFileClick(file);
    });
}
function uploadFileEvent(fileObj) {

    if (fileObj.type == '.zip') {
        uploadZipArray.push(fileObj.name);
        uploadZipFlag = true;

    }

}
function handleUpload() {
    $("#uStatus").show();
    $("#btnUploadCancel").hide();
    
    var fldPath = root + "/" + uploadFolder;
    _dialog = $('#upload');
    if (uploadZipFlag) {
        setTimeout(closeDialogWithConfirm, 4000);
    } else {
        getFiles(fldPath);

        //destroyUploader();
        setTimeout(closeDialog, 4000);
    }
}
function closeDialog() {

    _dialog.dialog('close');
}
function closeDialogWithConfirm() {

    _dialog.dialog('close');
    $("#uploadConfirm").dialog({
        resizable: false,
        title: '<span class="spanTitle">Unzip Uploaded File(s)</span>',
        height: 200,
        width: 400,
        modal: false,
        buttons: {
            "Yes": function () {
                $(this).dialog("close");
                handleZipExtraction();
            },
            "No": function () {
                $(this).dialog("close");
                var fldPath = root + "/" + uploadFolder;
                getFiles(fldPath);
            }
        }
    });
}
function handleRenameFile(file) {
    $("#rnfStatus").text('Processing...');
    $("#rnfStatus").show();
    var oldname = $("#FolderName").val();
    var newname = $("#txtRenameFile").val();
    if (oldname == newname) {
        alert("Please enter a different file name...");
        return false;
    }
    //need to create new path string; can't do a simple replace because it could replace something other than the end node: e.g, /test22/test2
    //split string, recombine, except for last node; concatenate newname to string to get new path
    var p = file.split("/");
    var count = p.length;
    count = count - 1;

    var newpath = '';
    for (k = 0; k < p.length; k++) {

        if (k < count) {
            newpath = newpath + p[k] + "/"
        }
    }

    newpath = newpath + newname;

    var obj = new Object();
    obj.src = file;
    obj.dest = newpath;
    $.ajax(
                {
                    type: "POST",
                    dataType: "json",
                    data: "{'obj':" + JSON.stringify(obj) + "}",
                    contentType: "application/json",
                    url: "/Services/BoteggaServices.asmx/RenameFile",
                    success: function (response) {
                        var sProp = response.d;
                        if (sProp == 'Success') {
                            $("#rnfStatus").text('File Renamed...');
                            var ffile = $("#FolderString").val();
                            refreshFolder(ffile);
                            _dialog = $('#renamefile');
                            callModalSuccess();
                        }
                        else {
                            alert(sProp);
                            $("#rnfStatus").text('error...');

                        }

                    }
                }
            );

}
function handleRenameFolder(file) {
    $("#rfStatus").text('Processing...');
    $("#rfStatus").show();
    var oldname = $("#FolderName").val();
    var newname = $("#txtRenameFolder").val();
    if (oldname == newname) {
        alert("Please enter a different folder name...");
        return false;
    }
    //need to create new path string; can't do a simple replace because it could replace something other than the end node: e.g, /test22/test2
    //split string, recombine, except for last node; concatenate newname to string to get new path
    var p = file.split("/");
    var count = p.length;
    count = count - 2;

    var newpath = '';
    for (k = 0; k < p.length; k++) {

        if (k < count) {
            newpath = newpath + p[k] + "/"
        }
    }

    newpath = newpath + newname;

    var obj = new Object();
    obj.src = file;
    obj.dest = newpath;
    $.ajax(
                {
                    type: "POST",
                    dataType: "json",
                    data: "{'obj':" + JSON.stringify(obj) + "}",
                    contentType: "application/json",
                    url: "/Services/BoteggaServices.asmx/RenameFolder",
                    success: function (response) {
                        var sProp = response.d;
                        if (sProp == 'Success') {
                            $("#rfStatus").text('Folder Renamed...');

                            $("#FolderString").val(rootFolder);
                            $('#fileTreeDemo_2').css("visibility", "hidden");
                            $('#fileTreeDemo_1').fileTree({ root: rootFolder, script: '/Services/FileManager/directories.aspx?r=1' }, function (file) {
                                getFiles(file);
                                handleFolderClick(file);
                                $("#FileExplorer1_RightPane").addClass("msScroll");

                            });
                            _dialog = $('#renamefolder');
                            callModalSuccess();
                        }
                        else {
                            alert(sProp);
                            $("#rfStatus").text('error...');

                        }

                    }
                }
            );

}
function handleDeleteFolder(file) {
    $("#dfStatus").text('Processing...');
    $("#dfStatus").show();
    var obj = new Object();
    obj.src = file;
    $.ajax(
                {
                    type: "POST",
                    dataType: "json",
                    data: "{'obj':" + JSON.stringify(obj) + "}",
                    contentType: "application/json",
                    url: "/Services/BoteggaServices.asmx/DeleteFolder",
                    success: function (response) {
                        var sProp = response.d;
                        if (sProp == 'Success') {
                            $("#dfStatus").text('Folder Deleted...');
                            $("#FolderName").val('');
                            $("#FolderString").val('');
                            $("a[rel='" + file + "']").parent().remove();
                            _dialog = $('#deletefolder');
                            callModalSuccess();
                        }
                        else {
                            alert(sProp);
                            $("#dfStatus").text('error...');

                        }

                    }
                }
            );
}
function handleDeleteFile(file) {
    $("#dStatus").text('Processing...');
    $("#dStatus").show();
    var obj = new Object();
    obj.src = file;
    $.ajax(
                {
                    type: "POST",
                    dataType: "json",
                    data: "{'obj':" + JSON.stringify(obj) + "}",
                    contentType: "application/json",
                    url: "/Services/BoteggaServices.asmx/DeleteFile",
                    success: function (response) {
                        var sProp = response.d;
                        if (sProp == 'Success') {
                            $("#dStatus").text('File Deleted...');

                            $("a[rel='" + file + "']").parent().parent().remove();

                            _dialog = $('#deletefile');
                            callModalSuccess();
                        }
                        else {
                            alert(sProp);
                            $("#dStatus").text('error...');

                        }

                    }
                }
            );
}
function handleNewFolder(file) {

    if ($("#txtNewFolder").val() == '') {
        alert("Folder Name Required...")
        return false;
    }
    $("#nfStatus").text('Processing...');
    $("#nfStatus").show();
    var obj = new Object();
    if (file == '') {
        file = rootFolder;
    }
    obj.dest = file + $("#txtNewFolder").val();

    $.ajax(
                {
                    type: "POST",
                    dataType: "json",
                    data: "{'obj':" + JSON.stringify(obj) + "}",
                    contentType: "application/json",
                    url: "/Services/BoteggaServices.asmx/CreateFolder",
                    success: function (response) {
                        var sProp = response.d;
                        if (sProp == 'Success') {
                            $("#nfStatus").text('New Folder Created...');

                            if (file == rootFolder) {
                                $('#fileTreeDemo_2').css("visibility", "hidden");
                                $('#fileTreeDemo_1').fileTree({ root: rootFolder, script: '/Services/FileManager/directories.aspx?r=1' }, function (file) {
                                    getFiles(file);
                                    handleFolderClick(file);
                                    $("#FileExplorer1_RightPane").addClass("msScroll");

                                });

                            } else {
                                refreshFolder(file);
                            }
                            _dialog = $('#newfolder');
                            getFolderProperties(file);
                            callModalSuccess();
                        }
                        else {
                            alert(sProp);
                            $("#nfStatus").text('error...');

                        }

                    }
                }
            );

}
function handleFolderClick(file) {
   
    $("#selFolder").val(file.replace(root, ''));
    $("#StatusSelFolder").val(file.replace(root, ''));
    $("#FolderString").val(file);
    $("a").removeClass('msSelected');
    $("a[rel='" + file + "']").addClass('msSelected');
    var f = $("a[rel='" + file + "']")
    if (!isFireFox) {
        $("#FolderName").val(f[0].outerText);
    } else {
        $("#FolderName").val(f[0].text);
    }

    getFolderProperties(file);
    resetPreview();
}
function handleFileClick(file) {
    $("#FileString").val(file);
    var f = $("a[rel='" + file + "']");
    if (isFireFox) {
        $("#ImageString").val(f[0].text);  //firefox fix
       
    } else {
        $("#ImageString").val(f[0].outerText);
    }
    $('#mnFile').hide();
    tiffPath = file;
    previewImage();
}
function handleFolderRightClick(action, el) {

    if (action == 'Open') {
        refreshFolder($(el).attr('rel'));
    }
    else if (action == 'Copy') {
        var f = el;
        $("#mPaste").removeClass("null");
        $("#FolderActionQueue").val('Copy');
        $("#FolderTypeQueue").val('Folder');
        if (!isFireFox) {
            $("#srcQueueName").val(f[0].outerText);
        } else {
            $("#srcQueueName").val(f[0].text);
        }
        $("#srcQueuePath").val($(el).attr('rel'));
    }
    else if (action == 'Cut') {
        var f = el;
        $("#mPaste").removeClass("null");
        $("#FolderActionQueue").val('Cut');
        $("#FolderTypeQueue").val('Folder');
        if (!isFireFox) {
            $("#srcQueueName").val(f[0].outerText);
        } else {
            $("#srcQueueName").val(f[0].text);
        }
        $("#srcQueuePath").val($(el).attr('rel'));
    }
    else if (action == 'New') {

        $("#FolderString").val($(el).attr('rel'));
        
        $('#newfolder').dialog({ title: '<span class="spanTitle">Create New Folder</span>' });
    }
    //####################PASTE##################################################################
    else if (action == 'Paste') {
        var q = $("#FolderActionQueue").val();
        var typ = $("#FolderTypeQueue").val();
        $("#refreshFolder").val($(el).attr('rel'));
        if (q == '') { //nothing queue
            return;
        } else if (q == 'Cut') { //move

            if (typ == 'Folder') { //move folder

                var f = $("#srcQueueName").val();
                var fld = $("#srcQueuePath").val();
                var dest = $(el).attr('rel');

                $("#RemoveFolder").val(fld);  //set values
                $("#destFolder").val(dest);
                $("#srcFolderName").val(f);

                $("#srcQueueName").val(''); //reset queue
                $("#srcQueuePath").val('');

                $("#mPaste").addClass("null");  //visually disable paste
                moveFolder(); //call movefolder
            }
            else {  //move file
                var f = $("#srcQueueName").val();
                var fld = $("#srcQueuePath").val();
                var dest = $(el).attr('rel');
                $("#srcQueueName").val(''); //reset queue
                $("#srcQueuePath").val('');
                $("#mPaste").addClass("null");  //visually disable paste
                $("#refreshFolder").val($(el).attr('rel'))
                moveFile(fld, dest, f);
            }
        } else if (q == 'Copy') {  //copy
            if (typ == 'Folder') { //copy folder
                var f = $("#srcQueueName").val();
                var fld = $("#srcQueuePath").val();
                var dest = $(el).attr('rel');

                $("#RemoveFolder").val(fld);  //set values
                $("#destFolder").val(dest);
                $("#srcFolderName").val(f);

                $("#srcQueueName").val(''); //reset queue
                $("#srcQueuePath").val('');

                $("#mPaste").addClass("null");  //visually disable paste

                copycreateFolder(fld, dest, f);
            }
            else {  //copy file
                var f = $("#srcQueueName").val();
                var fld = $("#srcQueuePath").val();
                var dest = $(el).attr('rel');
                $("#srcQueueName").val(''); //reset queue
                $("#srcQueuePath").val('');
                $("#mPaste").addClass("null");  //visually disable paste
                copyFile(fld, dest, f);
            }
        }


    }
    //######################### END PASTE #########################################################################
    else if (action == 'Delete') {
        var f = el;
        if (!isFireFox) {
            $("#FolderName").val(f[0].outerText);
        } else {
            $("#FolderName").val(f[0].text);
        }
        $("#FolderString").val($(el).attr('rel'));
        if (!isFireFox) {
            $("#txtDeleteFolder").val(f[0].outerText);
        } else {
            $("#txtDeleteFolder").val(f[0].text);
        }
        $('#deletefolder').dialog({ title: '<span class="spanTitle">Delete Folder</span>' });
    }
    else if (action == 'Rename') {
        var f = el;

        if (!isFireFox) {
            $("#txtRenameFolder").val(f[0].outerText);
        } else {
            $("#txtRenameFolder").val(f[0].text);
        }

        $("#FolderString").val($(el).attr('rel'));
        if (!isFireFox) {
            $("#FolderName").val(f[0].outerText);
        } else {
            $("#FolderName").val(f[0].text);
        }

        $('#renamefolder').dialog({ title: '<span class="spanTitle">Rename Folder</span>' });
    }
    else if (action == 'Upload') {
        handleContextUpload(el)
    }
    else if (action == 'Download') {
        handleDownloadZip(el)
    }
}

function handleDownloadZip(el) {
    var f = el;
    var obj = new Object();
    obj.src = $(el).attr('rel');
    if (!isFireFox) {
        obj.f = f[0].outerText;
    } else {
        obj.f = f[0].text;
    }
    $.ajax(
                {
                    type: "POST",
                    dataType: "json",
                    data: "{'obj':" + JSON.stringify(obj) + "}",
                    contentType: "application/json",
                    url: "/Services/BoteggaServices.asmx/ZipFolder",
                    success: function (response) {
                        var sProp = response.d;
                        if (sProp == 'Success') {
                            var file = $(el).attr('rel');
                            file = file.replace(root, '')
                            if (!isFireFox) {
                                file = file + f[0].outerText + ".zip";
                            } else {
                                file = file + f[0].text + ".zip";
                            }
                            var url = '/Services/FileManager/Download.aspx?file=' + file;

                            DownloadFile(url);
                        }
                        else {
                            alert(sProp);

                        }

                    }
                }
            );




}

function handleContextUpload(el) {
    var f = $(el).attr('rel');
    //determine context upload folder
    var cxtUploadFolder = f.replace(uploadPathFilter, '')
    $('#uStatus').hide();
    $('#btnUploadCancel').show();
    $("#txtUpload").val(cxtUploadFolder);
    uploadFolder = cxtUploadFolder;
    var fInput = $("<input type='file' name='AjaxUploader1_File'  id='AjaxUploader1_File' class='msInputFile' />");
    $('.fileClass').append(fInput);
    $("#upload").dialog({ title: '<span class="spanTitle">Upload New Files</span>', height: 550, width: 550, close: function (event, ui) { destroyUploader(); }, open: function (event, ui) {
        uploadZipFlag = false;
        uploadZipArray.length = 0;
        $('#AjaxUploader1_File').uploadify({
            'uploader': '/scripts/controls/uploader/uploadify.swf',
            'script': '/Services/FileManager/upload.aspx',
            'folder': uploadFolder,
            'cancelImg': '/App_Themes/Meanstream.2011/Uploader/images/msprogressclose.gif',
            'buttonImg': '/App_Themes/Meanstream.2011/Uploader/images/msbtnBrowse.png',
            'width': '60',
            'auto': true,
            'onComplete': function (event, ID, fileObj, response, data) { uploadFileEvent(fileObj); },
            'onAllComplete': function (event, data) { handleUpload(); },
            'method': 'POST',
            'multi': true,
            'wmode': 'transparent'
        });


    }


    });
}

function handleUploadContextComplete(file) {

    $("#uStatus").show();
    $("#btnUploadCancel").hide();
    $("#refreshFolder").val(file);
    refresh();
    _dialog = $('#upload');
    setTimeout(closeDialog, 4000);
}

function handleFileRightClick(action, el) {

    var file = $(el).attr('rel');
    if (action == 'Download') {

        file = file.replace(root, '')

        var url = '/Services/FileManager/Download.aspx?file=' + file;
        DownloadFile(url);
    }
    else if (action == 'Copy') {
        var f = el;
        $("#mPaste").removeClass("null");
        $("#FolderActionQueue").val('Copy');
        $("#FolderTypeQueue").val('File');
        if (!isFireFox) {
            $("#srcQueueName").val(f[0].outerText);
        } else {
            $("#srcQueueName").val(f[0].text);
        }
        $("#srcQueuePath").val($(el).attr('rel'));
    }
    else if (action == 'Cut') {
        var f = el;
        $("#mPaste").removeClass("null");
        $("#FolderActionQueue").val('Cut');
        $("#FolderTypeQueue").val('File');
        if (!isFireFox) {
            $("#srcQueueName").val(f[0].outerText);
        } else {
            $("#srcQueueName").val(f[0].text);
        }
        $("#srcQueuePath").val($(el).attr('rel'));
    }
    else if (action == 'Rename') {
        var f = el;
        if (!isFireFox) {
            $("#txtRenameFile").val(f[0].outerText);
        } else {
            $("#txtRenameFile").val(f[0].text);
        }
        $("#FileString").val($(el).attr('rel'));
        if (!isFireFox) {
            $("#FolderName").val(f[0].outerText);
        } else {
            $("#FolderName").val(f[0].text);
        }
        $('#rnfStatus').hide();
        $('#renamefile').dialog({ title: '<span class="spanTitle">Rename File</span>' });
    }
    else if (action == 'Delete') {
        var f = el;
        if (!isFireFox) {
            $("#txtDeleteFile").val(f[0].outerText);
        } else {
            $("#txtDeleteFile").val(f[0].text);
        }
        $("#FileString").val($(el).attr('rel'));
        $('#dStatus').hide();
        $('#deletefile').dialog({ title: '<span class="spanTitle">Delete File</span>' });
    }
    else if (action == 'Preview') {
        var f = el;

        if (!isFireFox) {
            var previewFile = f[0].outerText;
        } else {
            var previewFile = f[0].text;
        }
        var filePath = $(el).attr('rel');
        var src = urlRoot + filePath.replace(root, '');
        tiffPath = filePath;
        var ext = previewFile.substring(previewFile.lastIndexOf('.') + 1);
        if (/^(doc|docx|xls|xlsx|ppt|pptx|pps|odt|ods|odp|sxw|sxc|sxi|wpd|pdf|rtf|txt|html|csv|tsv)$/.test(ext)) {
            documentPreview(previewFile, src);
            $('#document-controls-wrapper').show();
            $('.ajaxLoader').show();
            $(document).keydown(function (e) {
                if (e.keyCode == 27) {
                    $("#documentContainer").remove();
                    $('#document-controls-wrapper').hide();
                    $('.ajaxLoader').hide();
                }
            });
        }
        if (/^(jpg|tif|gif|png|jpeg)$/.test(ext)) {
            if (ext == 'tif') {
                var nfile = filePath.replace(root, '');
                nfile = nfile.replace(previewFile, '');
                var img = previewFile.replace('.tif', '.jpg');
                nfile = nfile + 'TFF_JPGS\\' + img;
                src = urlRoot + nfile;
            } else {

            }
            $('.slidenumber').text(previewFile);
            largeImgPrev = src;
            handlePreview();
        }

    }
    else if (action == 'Edit') {
        var f = el;
        var filePath = $(el).attr('rel');
        var src = urlRoot + filePath.replace(root, '');
        if (!isFireFox) {
            var imgFile = f[0].outerText;
        } else {
            var imgFile = f[0].text;
        }
        var ext = imgFile.substring(imgFile.lastIndexOf('.') + 1);
        if (/^(jpg|tif|gif|png|jpeg)$/.test(ext)) {
            pixlr.overlay.show({ image: src, title: imgFile, service: 'editor', exit: pixlrCloseUrl, target: pixlrSaveUrl })
            createCookie("imgStatus", "Edit", 0);
            createCookie("imgPath", filePath, 0);
            pollInterval = setInterval(pollCookie, 2000);
            $(document).keydown(function (e) {
                if (e.keyCode == 27) {
                    $("#pixlrDiv").remove();
                    $("#pixlriDiv").remove();
                    clearInterval(pollInterval);
                    deleteCookie('imgStatus');
                    deleteCookie('imgPath');
                }
            });
        }
    }
    else if (action == 'Info') {
        var f = el;
        var filePath = $(el).attr('rel');
        $('#fileInfo').dialog({ title: '<span class="spanTitle">File Details</span>', zIndex: 9999999999999999, height: 400, width: 600, open: function (event, ui) {
            var obj = new Object();
            _dialog = $('#imageInfo');
            obj.path = filePath;
            $.ajax(
                {
                    type: "POST",
                    dataType: "json",
                    data: "{'obj':" + JSON.stringify(obj) + "}",
                    contentType: "application/json",
                    url: "/Services/BoteggaServices.asmx/getFileInfo",
                    success: function (response) {
                        var data = response.d;
                        $('#fDateCreated').text(data.DateCreated);
                        $('#fLastModified').text(data.DateLastModified);
                        $('#fLastAccessed').text(data.DateLastAccessed);
                        $('#fFileName').text(data.Title);
                        $('#fFileSize').text(data.FileSize);
                        $('#fFileType').text(data.FileType);
                        $('#fFileDimensions').text(data.Dimensions);

                    }
                }
            );
        }

        });
    }
}
function refreshFolder(file) {

    if ($("a[rel='" + file + "']").parent().hasClass('expanded')) {
        $("a[rel='" + file + "']").parent().removeClass('expanded').addClass('collapsed');
    }


    if (file == rootFolder) {
        getFiles(file);
    } else {
        $("a[rel='" + file + "']").click();
    }
}
function refresh() {

    var file = $("#refreshFolder").val();

    if ($("a[rel='" + file + "']").parent().hasClass('expanded')) {
        $("a[rel='" + file + "']").parent().removeClass('expanded').addClass('collapsed');
    }
    $("a[rel='" + file + "']").click();

}
function handleDropHover(event, ui, obj) {
    var mobj = ui;
    var t = obj;
    var tt = '1';

    var file = t[0].rel;
    $("a[rel='" + file + "']").removeClass('msSelected');

}
function handleDropOut(event, ui, obj) {
    var t = obj;
    var file = t[0].rel;
    var sFile = $("#FolderString").val();
    if (file == sFile) {
        $("a[rel='" + file + "']").addClass('msSelected');
    }
}
function handleDrop(event, ui, obj) {
    var mobj = ui;
    var t = obj;
    var tt = '1';

    var file = obj[0].rel;

    var s;

    if ($("a[rel='" + ui.draggable[0].rel + "']").parent().hasClass('directory')) {

        $("#RemoveFolder").val(ui.draggable[0].rel);
        $("#destFolder").val(file);
        if (!isFireFox) {
            $("#srcFolderName").val(ui.draggable[0].outerText);
        } else {
            $("#srcFolderName").val(ui.draggable[0].text);
        }
        setTimeout(moveFolder, 1);

    }
    else {
        //move file
        var fle;
        if (navigator.userAgent.toLowerCase().indexOf('firefox') > -1) {
            fle = ui.draggable[0].text;
        } else {
            fle = ui.draggable[0].outerText;
        }

        moveFile(ui.draggable[0].rel, file, fle)
    }
    $("#refreshFolder").val(file);



}
function stoperror() {
    return true
}
function folder_init() {
    $("#RemoveFolder").val('');
    $("#destFolder").val('');
    $("#FolderString").val(rootFolder);
    $("#refreshFolder").val('');
    $("#FileString").val('');
    $("#ImageString").val('');
    $("#FolderName").val('');
    $("#srcFolderName").val('');
    $("#FolderTypeQueue").val('');
    $("#FolderActionQueue").val('');
    $("#srcQueuePath").val('');
    $("#srcQueueName").val('');

}

function previewImage() {
    var file = $("#FileString").val();
    var Image = $("#ImageString").val();
    
    var ext = Image.split('.').pop().toLowerCase();
    var allow = new Array('gif', 'png', 'jpg', 'jpeg', 'tif');
    if (jQuery.inArray(ext, allow) == -1) {

       
        noPreview();
    }
    else {
        $("#preview").removeClass("msPreview");
        $("#preview").removeClass("msNoPreview");
        $("#preview").addClass("msClickable");
        previewActive = true;
        var src;
        if (ext != 'tif') {
            src = urlRoot + file.replace(root, '');
        } else {
            var nfile = file.replace(root, '');
            nfile = nfile.replace(Image, '');
            var img = Image.replace('.tif', '.jpg');
            nfile = nfile + 'TFF_JPGS\\' + img;
            src = urlRoot + nfile;
        }
        largeImgPrev = src;
        $('.slidenumber').text(Image);
        $("#showImage").attr('src', src);
        $("#showImage").show();
    }

}
function moveFile(src, dest, f) {
    var msg = 'Moving ' + f + ' to folder: ' + dest.replace(root, '');
    callBlockUI(msg);
    var obj = new Object();
    obj.src = src;
    obj.dest = dest + f;
    
    $.ajax(
                {
                    type: "POST",
                    dataType: "json",
                    data: "{'obj':" + JSON.stringify(obj) + "}",
                    contentType: "application/json",
                    url: "/Services/BoteggaServices.asmx/MoveFile",
                    success: function (response) {
                        var sProp = response.d;
                        if (sProp == 'Success') {
                            setTimeout(refresh, 1);
                            getFolderProperties(dest);
                            callSuccess();
                        }
                        else {
                            alert(sProp);
                            $.unblockUI();
                        }

                    }
                }
            );
}

function getFolderProperties(src) {
    var obj = new Object();
    obj.src = src;
    $.ajax(
                {
                    type: "POST",
                    dataType: "json",
                    data: "{'obj':" + JSON.stringify(obj) + "}",
                    contentType: "application/json",
                    url: "/Services/BoteggaServices.asmx/GetFolderProperties",
                    success: function (response) {
                        var sProp = response.d;
                        var p = sProp.split("|");
                        $("#tFiles").text(p[0]);
                        $("#tFolders").text(p[1]);
                        $('#diskSpace').text(p[2]);

                    }
                }
            );
}
function getSystemProperties(src) {
    var obj = new Object();
    obj.src = src;
    $.ajax(
                {
                    type: "POST",
                    dataType: "json",
                    data: "{'obj':" + JSON.stringify(obj) + "}",
                    contentType: "application/json",
                    url: "/Services/BoteggaServices.asmx/GetAbsoluteFolderProperties",
                    success: function (response) {
                        var sProp = response.d;
                        var p = sProp.split("|");
                        $("#sTotalFiles").text(p[0]);
                        $("#sTotalFolders").text(p[1]);
                        $('#sDiskSpace').text(p[2]);

                    }
                }
            );
}
function copyFile(src, dest, f) {
    var msg = 'Copying ' + f + ' to folder: ' + dest.replace(root, '');
    callBlockUI(msg);
    var obj = new Object();
    obj.src = src;
    obj.dest = dest + f;

    $.ajax(
                {
                    type: "POST",
                    dataType: "json",
                    data: "{'obj':" + JSON.stringify(obj) + "}",
                    contentType: "application/json",
                    url: "/Services/BoteggaServices.asmx/CopyFile",
                    success: function (response) {
                        var sProp = response.d;
                        if (sProp == 'Success') {

                            setTimeout(refresh, 1);
                            getFolderProperties(dest);
                            callSuccess();
                        }
                        else {
                            alert(sProp);
                            $.unblockUI();
                        }

                    }
                }
            );
}
function moveFolder() {
    var fld = $("#RemoveFolder").val();
    var dest = $("#destFolder").val();
    var f = $("#srcFolderName").val();
    var msg = 'Moving ' + f + ' to folder: ' + dest.replace(root, '');
    callBlockUI(msg);
    var obj = new Object();
    obj.src = fld;
    obj.dest = dest + f;
    $.ajax(
                {
                    type: "POST",
                    dataType: "json",
                    data: "{'obj':" + JSON.stringify(obj) + "}",
                    contentType: "application/json",
                    url: "/Services/BoteggaServices.asmx/MoveFolder",
                    success: function (response) {
                        var sProp = response.d;
                        if (sProp == 'Success') {
                            $("a[rel='" + fld + "']").parent().remove();
                            setTimeout(refresh, 1);
                            getFolderProperties(dest);
                            callSuccess();
                        }
                        else {
                            alert(sProp);
                            $.unblockUI();
                        }

                    }
                }
            );





}

function copyFolder(src, dest, f) {

    var msg = 'Copying ' + f + ' to folder: ' + dest.replace(root, '');
    callBlockUI(msg);
    var obj = new Object();
    obj.src = src;
    obj.dest = dest + f;

    $.ajax(
                {
                    type: "POST",
                    dataType: "json",
                    data: "{'obj':" + JSON.stringify(obj) + "}",
                    contentType: "application/json",
                    url: "/Services/BoteggaServices.asmx/CopyFolder",
                    success: function (response) {
                        var sProp = response.d;
                        if (sProp == 'Success') {

                            setTimeout(refresh, 1);
                            getFolderProperties(dest);
                            callSuccess();
                        }
                        else {
                            alert(sProp);
                            $.unblockUI();
                        }

                    }
                }
            );



}
function copycreateFolder(src, dest, f) {

    var obj = new Object();
    obj.dest = dest + f;
    $.ajax(
                {
                    type: "POST",
                    dataType: "json",
                    data: "{'obj':" + JSON.stringify(obj) + "}",
                    contentType: "application/json",
                    url: "/Services/BoteggaServices.asmx/CreateFolder",
                    success: function (response) {
                        var sProp = response.d;
                        if (sProp == 'Success') {
                            copyFolder(src, dest, f);
                        }
                        else {
                            alert(sProp);
                            return false;
                        }

                    }
                }
            );
}



function DownloadFile(url) {


    var dn = new AjaxDownload(url);
    dn.EnableTrace(true);
    //fires before download, 
    dn.add_onBeginDownload(BeginDownload);
    dn.add_onEndDownload(EndDownload);
    dn.add_onError(DownloadError);
    dn.Download();

}
function BeginDownload() {
    var uMsg = '<span class=msMsgTitle>Processing Download Request...</span><br><br>'
   
    $.blockUI({ message: uMsg, fadeIn: 700, centerX: true, centerY: false, css: {
        border: '3px solid #fff',
        padding: '15px',
        height: '200px',
        backgroundColor: '#000',
        '-webkit-border-radius': '10px',
        '-moz-border-radius': '10px',
        opacity: .6,
        top: '200px',
        color: '#fff'
    }
    });
}

function EndDownload() {
    $.unblockUI();
}
function DownloadError() {
    var errMsg = AjaxDownload.ErrorMessage;
    var errCk = $.cookie('downloaderror');

    if (errCk) {
        errMsg += ", Error from server = " + errCk;
    }
    alert(errMsg);
}
function callBlockUI(msg) {

    var uMsg = '<span class=msMsgTitle>Processing Request...</span><br><br><span class=msMsg>' + msg + '</span>'

    $.blockUI({ message: uMsg, fadeIn: 700, centerX: true, centerY: false, css: {
        border: '3px solid #fff',
        padding: '15px',
        height: '200px',
        backgroundColor: '#000',
        '-webkit-border-radius': '10px',
        '-moz-border-radius': '10px',
        opacity: .6,
        top: '200px',
        color: '#fff'
    }
    });
}
function callModalSuccess() {
    setTimeout(closeModal, 3000);
}
function closeModal() {

    _dialog.dialog('close');
}
function destroyUploader() {

    $("#AjaxUploader1_File").unbind("uploadifySelect");
    $('#AjaxUploader1_FileQueue').remove();
    swfobject.removeSWF('#AjaxUploader1_FileUploader');
    $('#AjaxUploader1_File').remove();
}
function callSuccess() {
    setTimeout(callSuccessBlockUI, 3000);
}
function callSuccessBlockUI() {
    $.unblockUI({ fadeOut: 0 });
    var uMsg = '<span class=msMsgTitleSuccess>Operation Completed Succesfully...</span><br><br>'

    $.blockUI({ message: uMsg, centerX: true, centerY: false, fadeIn: 0, css: {
        border: '3px solid #fff',
        padding: '15px',
        height: '200px',
        backgroundColor: '#8A5102',
        '-webkit-border-radius': '10px',
        '-moz-border-radius': '10px',
        opacity: .7,
        top: '200px',
        color: '#000'
    }
    });
    setTimeout(endBlock, 2000);
}
function endBlock() {
    $.unblockUI();
}
function previewClick() {
    if (previewActive != true) {
        return;
    }
    handlePreview();
  
}
function handlePreview() {
    $.backstretch(largeImgPrev);
    $('#controls-wrapper').show();
    $(document).keydown(function (e) {
        if (e.keyCode == 27) {
            $("#backstretch").remove();
            $('#controls-wrapper').hide();
        }
    });

}
function generatePreview(data) {

  
}

function resetPreview() {
    $("#showImage").hide();
    $("#preview").removeClass("msClickable");
    $("#preview").removeClass("msNoPreview");
    $("#preview").addClass("msPreview");
    previewActive = false;
}
function noPreview() {
    $("#showImage").hide();
    $("#preview").removeClass("msPreview");
    $("#preview").removeClass("msClickable");
    $("#preview").addClass("msNoPreview");
    previewActive = false;
}
function documentPreview(fle, url) {
    var container = $("<div />").attr("id", "documentContainer")
                                            .css({ left: 0, top: 0, position: "fixed", overflow: "hidden", zIndex: 999999, margin: 0, padding: 0, height: "100%", width: "100%", backgroundColor: "#fff" });

    alert(url);
    var uSrc = 'http://viewer.zoho.com/docs/urlview.do?embed=true&url=' + encodeURIComponent(url);
    var ht = $(document).height();
    ht = ht - 62;

    var doc = $("<div><iframe src='" + uSrc + "' width=100% height=" + ht + " onload='hideLoader()'></iframe>");
    $(container).append(doc);
    if ($("body #documentContainer").length == 0) {
        $("body").append(container);
    }
    $('#docDownload').attr('href', url);
}
function closeDocPreview() {
    $("#documentContainer").remove();
    $('#document-controls-wrapper').hide();

}
function closeImgPreview() {
    $("#backstretch").remove();
    $('#controls-wrapper').hide();
    _dialog.dialog('close');
}
function handleZipExtraction() {
    var fldPath = root + "/" + uploadFolder;
    var zipfiles = '';
    for (var i = 0; i < uploadZipArray.length; i++) {
        zipfiles = uploadZipArray[i] + '|';
    }
    callBlockUI("Unzipping Uploaded Files...Please Wait");
    var obj = new Object();
    obj.files = zipfiles;
    obj.path = fldPath;
    $.ajax(
                {
                    type: "POST",
                    dataType: "json",
                    data: "{'obj':" + JSON.stringify(obj) + "}",
                    contentType: "application/json",
                    url: "/Services/BoteggaServices.asmx/UnZipFiles",
                    success: function (response) {
                        var sProp = response.d;
                        if (sProp == 'Success') {
                            callSuccessBlockUI();
                            getFiles(fldPath);
                        }
                        else {
                            alert(sProp);
                            endBlock();
                        }

                    }
                }
            );
}

function callPixlr() {
    closeImgPreview();
    var filePath = $("#FileString").val();
    pixlr.overlay.show({ image: pixlrUrl, title: pixlrTitle, service: 'editor', exit: pixlrCloseUrl, target: pixlrSaveUrl })
    createCookie("imgStatus", "Edit", 0);
    createCookie("imgPath", filePath, 0);
    pollInterval = setInterval(pollCookie, 2000);
    $(document).keydown(function (e) {
        if (e.keyCode == 27) {
            $("#pixlrDiv").remove();
            $("#pixlriDiv").remove();
            clearInterval(pollInterval);
            deleteCookie('imgStatus');
            deleteCookie('imgPath');
        }
    });

}
function contentWindowSize() {
    var docWidth = $(window).width() - 40;
    var outerTableWidth = parseInt(.75 * docWidth);
    var msCointWidth = (outerTableWidth - 10);
    var selFolderWidth = (outerTableWidth - 15);
    var msFilesBarWidth = (outerTableWidth * .7);
    var sInfoWidth = parseInt(outerTableWidth * .60);
    leftSplitterWidth = parseInt(outerTableWidth * .30);
    var docHeight = $(window).height() - 40;
    var outerTableHeight = parseInt(.75 * docHeight);
    var fileTree2Height = parseInt(outerTableHeight - 93);
    var msStatusHeight = parseInt(outerTableHeight - 176);
    var loaderLeft = parseInt(outerTableWidth * .50);
    loaderLeft = loaderLeft + 200;
    $('#outerTable').css('width', outerTableWidth + 5 + 'px');
    $('.msExplorerBar').css('width', outerTableWidth + 'px');
    $('#msCoint').css('width', msCointWidth + 'px');
    $('#selFolder').css('width', selFolderWidth + 'px');
    $('#FileExplorer1').css('width', outerTableWidth + 'px');
    $('.msFileBar2').attr('width', msFilesBarWidth + 'px');
    $('#sInfo').attr('width', sInfoWidth + 'px');
    $('#FileExplorer1').css('height', outerTableHeight + 'px');
    $('#fileTreeDemo_2').css('height', fileTree2Height + 'px');
    $('.msStatus').css('height', msStatusHeight + 'px');
    $('.ajaxLoader').css('left', loaderLeft + 'px');
}
function hideLoader() {
    $('.ajaxLoader').hide();
}
function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";
    document.cookie = name + "=" + value + expires + "; path=/";
}

function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function deleteCookie(name) {
    createCookie(name, "", -1);
}
function pollCookie() {
    var c = readCookie('imgStatus');
    if (c == 'Completed') {
        clearInterval(pollInterval);
        deleteCookie('imgStatus');
        deleteCookie('imgPath');
        $("#pixlrDiv").remove();
        $("#pixlriDiv").remove();
    }
}