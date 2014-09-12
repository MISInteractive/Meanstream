
function getPageIDFromHeader() {
    var objHead = document.getElementsByTagName('head');
    var objID = objHead[0].getAttribute('id');
    var sPageID = objID.substring(objID.lastIndexOf('_') + 1);
    return sPageID;
}

function deleteWidget(widgetId) {
    var answer = confirm("Send to Recycle Bin? ");
    var param = getPageIDFromHeader();
    if (answer) {
        Meanstream.Portal.Web.Services.WidgetService.SendToRecycleBin(widgetId);
        setTimeout("redirectToVersion('" + param  + "')", 1000);
    }
}

function redirectToVersion(param) {
    window.location.href = "./Version.aspx?VersionID=" + param;
}

function paneHoverOver(pane) {
    //    var opacity = options.opacity || 70;  
    //    var opaque = (opacity / 100);
    var pane = document.getElementById(pane);

    var node = document.createElement('div');
    node.id = pane.id + '_controlhovernode';
    node.style.backgroundColor = 'blue';
    //    node.style.opacity=opaque;                          
    //    node.style.MozOpacity=opaque;                       
    //    node.style.filter='alpha(opacity='+opacity+')';
    //IE
    node.innerText = ' Drop Control Here';
    //FireFox & Safari
    node.innerHtml = ' Drop Control Here';
    node.style.color = '#000';
    node.style.weight = 'bold';
    node.style.size = '14px';
    node.align = 'center';
    node.valign = 'center';
    node.style.width = '100%';
    node.style.height = '50px';
    //pane.appendChild(node);   
}
function paneHoverOut(pane) {
    //sleep(10);

    //var pane = document.getElementById(pane);
    //var node = document.getElementById(pane.id + '_controlhovernode');
    try {

        //node.parentNode.removeChild(node);
    }
    catch (err) {

    }
}