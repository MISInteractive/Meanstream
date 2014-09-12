function msResizeModal(ctrl) {
    var w0 = window.screen.availWidth - 40;
    var h0;
    if (navigator.userAgent.toLowerCase().indexOf('chrome') > -1) {
        h0 = .81 * window.screen.availHeight;
    } else if (navigator.userAgent.toLowerCase().indexOf('safari') > -1) {
    h0=.79 * window.screen.availHeight;
    }
    else if (navigator.userAgent.toLowerCase().indexOf('opera') > -1) {
    h0=.79 * window.screen.availHeight;
    }
    else{
    h0=.75 * window.screen.availHeight;
    }
    
    
    var h10 = h0 + 20;
    var w10 = w0 + 20;
    var h = h0 + 'px';
    var w = w0 + 'px';
    var h1 = h10 + 'px';
    var w1 = w10 + 'px';
    var l = '10px';
    var t = '10px';
    $('#' + ctrl + '_topHeader').css({ 'width': w });
    $('#' + ctrl + '_leftContent').css({ 'height': h });
    $('#' + ctrl + '_mainContent').css({ 'width': w });
    $('#' + ctrl + '_mainContent').css({ 'height': h });
    $('#' + ctrl + '_rightContent').css({ 'height': h });
    $('#' + ctrl + '_footerContent').css({ 'width': w });
    $('#' + ctrl + '_bottomFooter').css({ 'width': w });
    $('#' + ctrl + '_subWin').attr('height', h);
    $('#' + ctrl + '_subWin').attr('width', w);
    _msDiag.container.css({ height: h1,
        width: w1
    });
    _msDiag.container.css({ left: l,
        top: t
    });
}
function msCollapseModal(h0, w0, ctrl) {
    var h10 = h0 + 20;
    var w10 = w0 + 20;
    var h = h0 + 'px';
    var w = w0 + 'px';
    var h1 = h10 + 'px';
    var w1 = w10 + 'px';
    //var l = '10px';
    //var t = '10px';
    $('#' + ctrl + '_topHeader').css({ 'width': w });
    $('#' + ctrl + '_leftContent').css({ 'height': h });
    $('#' + ctrl + '_mainContent').css({ 'width': w });
    $('#' + ctrl + '_mainContent').css({ 'height': h });
    $('#' + ctrl + '_rightContent').css({ 'height': h });
    $('#' + ctrl + '_footerContent').css({ 'width': w });
    $('#' + ctrl + '_bottomFooter').css({ 'width': w });
    $('#' + ctrl + '_subWin').attr('height', h);
    $('#' + ctrl + '_subWin').attr('width', w);
    _msDiag.container.css({ height: h1,
        width: w1
    });
    _msModal.setPosition();
}
function msShowModal(h0, w0, ctrl,url,isModal,popId) {
    var Window1_DOM = ctrl;
    var h10 = h0 + 20;
    var w10 = w0 + 20;
    var h = h0 + 'px';
    var w = w0 + 'px';
    var h1 = h10 + 'px';
    var w1 = w10 + 'px';
    
    $('#' + ctrl + '_topHeader').css({ 'width': w });
    $('#' + ctrl + '_leftContent').css({ 'height': h });
    $('#' + ctrl + '_mainContent').css({ 'width': w });
    $('#' + ctrl + '_mainContent').css({ 'height': h });
    $('#' + ctrl + '_rightContent').css({ 'height': h });
    $('#' + ctrl + '_footerContent').css({ 'width': w });
    $('#' + ctrl + '_bottomFooter').css({ 'width': w });
    $('#' + ctrl + '_subWin').attr('height', h);
    $('#' + ctrl + '_subWin').attr('width', w);
    var wUrl = Window1_DOM + '_url';
    var wSub = Window1_DOM + '_subWin';
    _msWinProg = Window1_DOM + '_progress';
    _msWinUrl = wUrl;
    $('#' + Window1_DOM).modal({ autoResize: 'True', modal: isModal, onShow: function (dialog) {
        if (url != '') {
            var src = url;
            $('#' + wUrl).hide();
            $('#' + wSub).attr('src', src);
            document.getElementById(wSub).setAttribute('onload', "setTimeout(msWindowOnload, 1);");
        }
        else {
            var wContent = Window1_DOM + '_mainContent';
            var childElem = document.getElementById(popId);
            $('#' + wContent).append(childElem);
            $('#' + popId).show();
        }
       
        _msDiag = dialog;
        _msModal = this;
    }
    });
    }
    function msWindowOnload(s) {
        
    $('#' + _msWinProg).hide();
    $('#' + _msWinUrl).show();
}
function msWindowfireEventPostBack(ctrl) {
   
    var obj = document.getElementById(ctrl)
    var v = obj.value;
    v = v + 1;
    obj.value = v;
    if (document.all) {
        obj.fireEvent('onchange');
    }
    else {
        var e = document.createEvent('HTMLEvents');
        e.initEvent('change', false, true);
        obj.dispatchEvent(e);
    }
    
    setTimeout(msCloseWin, 10);
}
function msCloseWin() {
    _msModal.close();
}
function msWindowObject(win) {
    this.windowId=win;
    this.control;
    this.controlArray;
    this.height=200;
    this.width=200;
    this.url='';
    this.isModal = true;
    this.popId = '';
    this.ControlValue = function () {
        var w = this.windowId + '_subWin';
        
        var d = document.getElementById(w);
        var fdoc = d.contentWindow.document;
        var count = fdoc.forms[0].length;
        var i = 0;
        var e = '';
        var eleName;
        for (i = 0; i < count; i++) {
            eleName = fdoc.forms[0].elements[i].id;
            pos = eleName.indexOf(this.control);
            if (pos >= 0) {
                break;
            } else {
                eleName = '';
            }
        }
        if (eleName !== '') {
            e = d.contentWindow.document.getElementById(eleName);
        } else {

        }
        return e;
    }
    this.open = function () {
        var h0 = this.height;
        var w0 = this.width;
        var url = this.url;
        var isModal = this.isModal;
        var popId = this.popId;
        var ctrl = this.windowId;
        msShowModal(h0, w0, ctrl, url, isModal, popId);
    }
    this.append = function (e) {
        var h0 = this.height;
        var w0 = this.width;
        var url = this.url;
        var isModal = false;
        var popId = this.popId;
        var win = this.windowId;
        var winElem = document.getElementById(win);
        var ctrl = win;
        $('#' + e).append(winElem);
        msShowModal(h0, w0, ctrl, url, isModal, popId);
    }
    this.ControlCollectionValues = function () {
        var w = this.windowId + '_subWin';
        var retControlArray = new Array();
        var cArray = this.controlArray;
        var strElements = '';
        var e = '';
        var eleName;
        for (var j = 0; j < cArray.length; j++) {
            

            var d = document.getElementById(w);
            var fdoc = d.contentWindow.document;
            var count = fdoc.forms[0].length;
            var i = 0;

            for (i = 0; i < count; i++) {
                eleName = fdoc.forms[0].elements[i].id;
                pos = eleName.indexOf(cArray[j]);
                if (pos >= 0) {
                    break;
                } else {
                    eleName = '';
                }
            }
            if (eleName !== '') {
                e = d.contentWindow.document.getElementById(eleName);
            } else {

            }
           
            retControlArray[j] = e.value;
            strElements = strElements + e.value;
            var k = j + 1;
            if (k < cArray.length) {
                strElements = strElements + '|';
            }

        } //end for
        //assign strElements to window hidden field *
        var c = this.windowId + '_CollectionValues';
        $('#' + c).val(strElements);
        return retControlArray; //return array of control values
    }
}                                                                                     