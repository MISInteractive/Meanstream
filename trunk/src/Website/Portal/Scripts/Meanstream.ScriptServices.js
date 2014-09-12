function registerNamespace(ns) {
    var nsParts = ns.split(".");
    var root = window;

    for (var i = 0; i < nsParts.length; i++) {
        if (typeof root[nsParts[i]] == "undefined")
            root[nsParts[i]] = new Object();

        root = root[nsParts[i]];
    }
}

registerNamespace("Meanstream.UI.ScriptServices");
Meanstream.UI.ScriptServices = function () {

   
    this.createBlockUI = function (control, msg) {
        $().ready(function () {
            if ($('#' + control)[0]) { //dom already exists
                var lbl = control + 'Status';
              
                $('#' + lbl).text(msg);
                $.blockUI({ message: $('#' + control) });
            } else { //dom does not exist
                var lbl = control + 'Status';
               
                //create the dom 
                var body = $(document.body);
                var div = $('<div id="' + control + '" class="msblockUIBG"><div class="msblockUISpinner"></div><div style="margin-top:10px;"><span class="msblockUIStatus" id="' + lbl + '"></span></div></div>');
                body.append(div);

                $('#' + lbl).text(msg);
                $.blockUI({ message: $('#' + control) });
            }
        });

    }
    this.reBlockUI = function (control, msg) {
        var lbl = control + 'Status';
        $.unblockUI({ fadeOut: 0 });
        $('#' + lbl).text(msg);
        $.blockUI({ message: $('#' + control) });

    }
    this.unblockUI = function () {
        $.unblockUI({ fadeOut: 0 });
    }

}