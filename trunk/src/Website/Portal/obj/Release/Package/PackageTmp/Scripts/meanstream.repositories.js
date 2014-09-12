
function Repositories() {
    this.createFieldTextBox = function (ele, width, id, css, i) {
        var elem = ele + i;
        var container = $('#' + ele);
        var tb = $('<div id="' + elem + '" class="field"><table border="0" cellspacing="0" cellpadding="0"><tr><td width="100"><strong>Field</strong></td><td><div><input type=text id="' + id + '" class="' + css + '" style="width:' + width + 'px;" /><table width="' + width + '" border="0" cellspacing="0" cellpadding="0"><tr><td background="images/textbox-bg.png">&nbsp;</td><td width="16"><img src="images/textbox-right.png" width="16" height="27"></td></tr></table></div></td><td><span class="tinyLink" id="rmField' + i + '">remove</span></td></tr></table><br></div>');
        container.append(tb);
        var rm = 'rmField' + i;
        $('#' + rm).click(function () {

            $('#' + elem).remove();

        });
    }
    this.createFieldEditTextBox = function (ele, width, id, css, i, field) {
        var elem = ele + i;
        var container = $('#' + ele);
        var tb = $('<div id="' + elem + '" class="field"><table border="0" cellspacing="0" cellpadding="0"><tr><td width="100"><strong>Field</strong></td><td><div><input type=text id="' + id + '" class="' + css + '" style="width:' + width + 'px;" value="' + field + '" /><table width="' + width + '" border="0" cellspacing="0" cellpadding="0"><tr><td background="images/textbox-bg.png">&nbsp;</td><td width="16"><img src="images/textbox-right.png" width="16" height="27"></td></tr></table></div></td><td><span class="tinyLink" id="rmField' + i + '">remove</span></td></tr></table><br></div>');
        container.append(tb);
        var rm = 'rmField' + i;
        $('#' + rm).click(function () {

            $('#' + elem).remove();

        });
    }
    
    
    this.bindLists = function (ele, data) {
        var container = $('#' + ele);
        var s = '';
        var i = 1;
        $.each(data, function (index, obj) {
            var ckId = 'cklist' + i;
            var hfId = 'fldlist' + i;
            s = s + '<table border=0 cellspacing=0 cellpadding=0><tr><td width=22><input type=checkbox id="' + ckId + '" class="listclass" value="' + obj.Id + '" name="' + obj.Name + '"/></td><td>' + obj.Name + '</td></tr></table><br>';
            i++;
        });
        s = '<div>' + s + '</div>';
        var tb = $(s);
        container.append(tb);

    }
}
