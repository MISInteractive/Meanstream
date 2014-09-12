function msShowModal(ctrl, h0, w0, url) {
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
    $('#' + Window1_DOM).modal({ autoResize: 'True', onShow: function (dialog) {
        var src = url;
        $('#' + wUrl).hide();

        $('#' + wSub).attr('src', src);
        alert(src);
        if (document.getElementById(wSub).attachEvent) {
            document.getElementById(wSub).attachEvent("onload", msWindowOnload);
        } else {
            document.getElementById(wSub).setAttribute('onload', "setTimeout(msWindowOnload, 1);");
        }
        _msDiag = dialog;
        _msModal = this;

    }
    });
}
function msWindowOnload() {
   
    $('#' + _msWinProg).hide();
    
}
