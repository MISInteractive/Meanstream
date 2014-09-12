function MSComboBoxObject(t,v,control,isAutoPostBack) {
 var strObj = control + '_Input';
 var obj = document.getElementById(strObj);
 obj.value=t;
 var strObjHidden = control + '_Hidden';
 var objHidden = document.getElementById(strObjHidden);
 var fnAnimate=control + '_animateBox';
 eval(fnAnimate + '();');
 var hiddenValue=objHidden.value;


 if(isAutoPostBack){
        if (hiddenValue!=v){ //if selected combo value has changed
            if (document.all){ //if IE, use fireevent to trigger postback
                obj.fireEvent('onchange');
                } 
                else{  //if IE, use dispatchEvent to fire postback from client
                var e = document.createEvent('HTMLEvents');
                e.initEvent('change', false, true);
                obj.dispatchEvent(e);
                } 
                }
 }
objHidden.value=v;
}
function _initMSCombo() {
//_intialize;
}
function msComboAnimate(control){
//$('#' + control).animate({
//height: 'toggle'
    //});
   
if ($('#' + control).css('visibility') == 'hidden') {
$('#' + control).css('visibility', 'visible');
} else {
 $('#' + control).css('visibility', 'hidden');
}
}
function msComboKeyBoardEnter(ctrl) {
    var val = $('#' + ctrl + '_KbVal').val();
    $('#' + ctrl + '_Hidden').val(val);

    var control = ctrl + '_Items';
    $('#' + control).css('visibility', 'hidden');
    var oldValue = $('#' + ctrl + '_KbValChange').val();
    var fnChg = $('#' + ctrl + '_ClientFunction').val();
    if ((val != oldValue) && (fnChg != '')) {
        $('#' + ctrl + '_KbValChange').val(val);
        document.getElementById(ctrl + '_KbValChange').onchange();
    }



}
function msComboKeyBoardFind(ctrl, obj, key) {
    var cbIndex = obj.Index;
    var arr = [];
    //get indices of matching li items
    $('#' + ctrl + '_Items li').each(function (index) {
        var text = $(this).text();
        if (text.length > 0 && text.charAt(0).toLowerCase() == key.toLowerCase()) {

            arr.push(index);
        }
    });
    var count = 0;
    try {
        count = arr.length;
    }
    catch (err) {
        count = 0;
    }
    if (count > 0) {
        var scrollPaneHeight = obj.ScrollPaneHeight;
        var itemHeight = obj.ItemHeight;
        var scrollIndex = obj.ScrollIndex;
        if (count == 1) {
            if (cbIndex != arr[0]) {
                obj.Index = arr[0];
                var li = $('#' + ctrl + '_Items li').eq(arr[0]);
                $('.msCombo_Items_hover').removeClass("msCombo_Items_hover");
                var offset = li.offset().top - $('#' + ctrl + '_Items').offset().top;
                li.addClass("msCombo_Items_hover");
                var v = li.text();
                $('#' + ctrl + '_Input').val(v);

                var liItems = $('#' + ctrl + '_Items li').get();
                var li2 = liItems[obj.Index];
                var val = li2.getAttribute('value');
                $('#' + ctrl + '_KbVal').val(val);

                if (arr[0] > cbIndex) {
                    if (offset > scrollPaneHeight) {

                        var offsetIndex = arr[0] - scrollIndex + 1;
                        var offsetHeight = offsetIndex * (itemHeight);
                        $('#' + ctrl + '_Items').scrollTop(offsetHeight);

                    }
                } else {

                    var scrollerOffset = obj.Index * (itemHeight);
                    $('#' + ctrl + '_Items').scrollTop(scrollerOffset);


                }

            } //end if cbIndex!=arr[0]
        } else {  //else for count==1
            var arrIndex = $.inArray(cbIndex, arr);
            var maxIndex = count - 1;
            var boolFlag = 0;
            if (arrIndex == -1) {
                //current index not in array
                //loop through array; go to first index greater than current index
                for (var k = 0; k < count; k++) {
                    var ind = arr[k];
                    if (ind > cbIndex) {
                        boolFlag = 1;
                        obj.Index = arr[k];
                        var li = $('#' + ctrl + '_Items li').eq(arr[k]);
                        var offset = li.offset().top - $('#' + ctrl + '_Items').offset().top;
                        $('.msCombo_Items_hover').removeClass("msCombo_Items_hover");
                        li.addClass("msCombo_Items_hover");
                        var v = li.text();
                        $('#' + ctrl + '_Input').val(v);
                        var liItems = $('#' + ctrl + '_Items li').get();
                        var li2 = liItems[obj.Index];
                        var val = li2.getAttribute('value');
                        $('#' + ctrl + '_KbVal').val(val);

                        if (offset > scrollPaneHeight) {

                            var offsetIndex = arr[k] - scrollIndex + 1;
                            var offsetHeight = offsetIndex * (itemHeight);
                            $('#' + ctrl + '_Items').scrollTop(offsetHeight);

                        }
                        break;
                    }
                }
                if (boolFlag == 0) {
                    //no index greater than current index; goto first index in array
                    obj.Index = arr[0];
                    var li = $('#' + ctrl + '_Items li').eq(arr[0]);
                    var offset = li.offset().top - $('#' + ctrl + '_Items').offset().top;
                    $('.msCombo_Items_hover').removeClass("msCombo_Items_hover");
                    li.addClass("msCombo_Items_hover");
                    var v = li.text();
                    $('#' + ctrl + '_Input').val(v);
                    var liItems = $('#' + ctrl + '_Items li').get();
                    var li2 = liItems[obj.Index];
                    var val = li2.getAttribute('value');
                    $('#' + ctrl + '_KbVal').val(val);

                    var scrollerOffset = obj.Index * (itemHeight);

                    $('#' + ctrl + '_Items').scrollTop(scrollerOffset);

                }
            } else {
                //goto next index in array;
                //alert(arrIndex);
                if (arrIndex < maxIndex) {
                    arrIndex++;
                } else {
                    arrIndex = 0;  //start over
                }
                //alert(arrIndex);
                var newIndex = arr[arrIndex];
                obj.Index = newIndex;
                var li = $('#' + ctrl + '_Items li').eq(newIndex);
                var offset = li.offset().top - $('#' + ctrl + '_Items').offset().top;
                $('.msCombo_Items_hover').removeClass("msCombo_Items_hover");
                li.addClass("msCombo_Items_hover");
                var v = li.text();
                $('#' + ctrl + '_Input').val(v);
                if (newIndex > cbIndex) {
                    if (offset > scrollPaneHeight) {

                        var offsetIndex = newIndex - scrollIndex + 1;
                        var offsetHeight = offsetIndex * (itemHeight);
                        $('#' + ctrl + '_Items').scrollTop(offsetHeight);

                    }
                } else {

                    var scrollerOffset = obj.Index * (itemHeight);
                    $('#' + ctrl + '_Items').scrollTop(scrollerOffset);
                }


            }  //end if-else arrIndex == -1
        } //end if-else count==1

    } //end if count > 0
}  //end function
function msComboKeyBoardDownArrow(ctrl, obj) {
    var cbIndex = obj.Index;
    $('#' + ctrl + '_Items li').each(function (index) {


        if (index > cbIndex) {

            $('.msCombo_Items_hover').removeClass("msCombo_Items_hover");
            obj.Index = index;
            $(this).addClass("msCombo_Items_hover");
            var scrollPaneHeight = obj.ScrollPaneHeight;
            var offset = $(this).offset().top - $('#' + ctrl + '_Items').offset().top;
            var itemHeight = obj.ItemHeight;
            var scrollIndex = obj.ScrollIndex;
            var v = $(this).text();
            var val = (this).getAttribute('value');
            $('#' + ctrl + '_Input').val(v);
            $('#' + ctrl + '_KbVal').val(val);

            if (offset > scrollPaneHeight) {

                var offsetIndex = index - scrollIndex + 1;
                var offsetHeight = offsetIndex * (itemHeight);
                $('#' + ctrl + '_Items').scrollTop(offsetHeight);

            }
            return false;
        }

    });

}
function msComboSetLiIndex(ctrl, obj, li) {
    //var liIext = (li).innerHTML;
    var liValue = (li).getAttribute('value');
    $('#' + ctrl + '_Items li').each(function (index) {
        //var v = $(this).text();
        var val = (this).getAttribute('value');

        if (val == liValue) {
            obj.Index = index;

            return false;
        }


    });
   
}
function msComboKeyBoardUpArrow(ctrl, obj) {
    var cbIndex = obj.Index;
    var liItems = $('#' + ctrl + '_Items li').get();
    var count = liItems.length;
    count--;
    jQuery.fn.reverse = [].reverse;
    $('#' + ctrl + '_Items li').reverse().each(function (i) {

        var index = count - i;
        if (index < cbIndex) {

            $('.msCombo_Items_hover').removeClass("msCombo_Items_hover");
            obj.Index = index;

            $(this).addClass("msCombo_Items_hover");
            var v = $(this).text();
            $('#' + ctrl + '_Input').val(v);
            var val = (this).getAttribute('value');
            $('#' + ctrl + '_KbVal').val(val);
            var scrollPaneHeight = obj.ScrollPaneHeight;
            var offset = $(this).offset().top - $('#' + ctrl + '_Items').offset().top;
            var itemHeight = obj.ItemHeight;
            var scrollIndex = obj.ScrollIndex;
            if (offset < 1) {
                var scrollerOffset = $('#' + ctrl + '_Items').scrollTop();
                var newOffset = scrollerOffset - itemHeight;
                $('#' + ctrl + '_Items').scrollTop(newOffset);

            }

            return false;
        }

    });

}
function msComboKeyboardInit(ctrl, obj) {
    $('.msCombo_Items_hover').removeClass("msCombo_Items_hover");
    var itemHeight = $('#' + ctrl + '_Items li').height();
    obj.ItemHeight = itemHeight;
    var scrollPaneHeight = $('#' + ctrl + '_Items').height();
    var scrollIndex = parseInt(scrollPaneHeight / itemHeight);
    obj.ScrollIndex = scrollIndex;
    var adjHeight = scrollIndex * itemHeight;
    obj.ScrollPaneHeight = adjHeight;
    $('#' + ctrl + '_Items').css('height', adjHeight + 'px');

    //set initial value
    var initValue = $('#' + ctrl + '_Hidden').val();
    var initText = $('#' + ctrl + '_Input').val();

    var setFlag = 0;
    var k = 0;
    var initThis;
    $('#' + ctrl + '_Items li').each(function (index) {
        if (k == 0) {
            initThis = this;
        }
        k++;
        var v = $(this).text();
        var val = (this).getAttribute('value');
        var offset = $(this).offset().top - $('#' + ctrl + '_Items').offset().top;

        if (val == initValue) {
            $(this).addClass("msCombo_Items_hover");
            if (offset > scrollPaneHeight) {

                var offsetIndex = index - scrollIndex + 1;
                var offsetHeight = offsetIndex * (itemHeight);
                $('#' + ctrl + '_Items').scrollTop(offsetHeight);

            }
            obj.Index = index;
            setFlag = 1;
            return false;

        } else if (v == initText) {

            $(this).addClass("msCombo_Items_hover");
            if (offset > scrollPaneHeight) {

                var offsetIndex = index - scrollIndex + 1;
                var offsetHeight = offsetIndex * (itemHeight);
                $('#' + ctrl + '_Items').scrollTop(offsetHeight);

            }
            obj.Index = index;
            setFlag = 1;
            return false;
        }

    });

    if ((setFlag == 0) && (k != 0)) {
       
        $(initThis).addClass("msCombo_Items_hover");
    }

}
function msComboSelect(input,hidden,kbval,a,isAutoPostBack){
var ComboHidden= document.getElementById(hidden);
var ComboInput = document.getElementById(input);
var ComboKB = document.getElementById(kbval);
var hiddenValue=ComboHidden.value;
var x=a.innerHTML;
ComboInput.value=a.innerHTML;
     if(isAutoPostBack){

            
         if (hiddenValue!=a.attributes[1].nodeValue){ //if selected combo value has changed...
                if (document.all){ //if IE, use fireevent to trigger postback
                ComboInput.fireEvent('onchange');
                }
                else{ //if IE, use dispatchEvent to fire postback from client
                var e = document.createEvent('HTMLEvents');
                e.initEvent('change', false, true);
                ComboInput.dispatchEvent(e);
                }
                }
      }
//ComboHidden.value=a.value;
//ComboHidden.value = a.attributes[1].nodeValue;
//ComboHidden.value = a.childNodes[0].nodeValue;
    //ComboHidden.value = $(a).attr('value');
var tmp = a.getAttribute('value');
ComboHidden.value = tmp;
ComboKB.value = tmp;
//var tmp2 = ComboHidden.value;
//alert(tmp2);
var tmp3;
}
function msClientComboSelect(input, hidden, a) {
    //alert(a.childNodes[0].nodeValue);
    //$('#' + input).val(a.innerHTML);
    //$('#' + hidden).val(a.childNodes[0].nodeValue);
    $(input).val(a.innerHTML);
    $(hidden).val(a.childNodes[0].nodeValue);
}
function msComboBox(control) {
    this.ComboId = control;
    this.dataSource;

    this.clear = function () {
        var combo = this.ComboId;
        $('#' + combo + '_Items li').each(function (i, item) {
           $(item).remove();
        })

        $('#' + combo + '_Hidden').val('');

    }

    this.removeInput = function () {
        var combo = this.ComboId;
        $('#' + combo + '_Input').val('');
    }

    this.dataBind = function () {
        var combo = this.ComboId;
        var selEvent = $('#' + combo + '_ClientFunction').val();
        var comboArray = [];
        comboArray = this.dataSource;
        $.each(comboArray, function (index, obj) {
            $('#' + combo + '_Items').append(
                  
                  "<li value='" + obj.Value + "' onclick='" + combo + "_lSelect(this);" + selEvent + "'>" + obj.Text + "</li>"
                  
              );
        });

    }
    this.clientDataBind = function () {
        var combo = this.ComboId;
        var ctrl = combo + '_Input';
        var ctrl2 = combo + '_Hidden';

        var comboArray = [];
        comboArray = this.dataSource;
        $.each(comboArray, function (index, obj) {
            $('#' + combo + '_Items').append(

                  "<li value=" + obj.Value + " onclick='msClientComboSelect(" + ctrl + "," + ctrl2 + ",this)'>" + obj.Text + "</li>"

              );
        });

    }
    this.selectedValue = function () {
        try {
            var combo = this.ComboId + '_Hidden';
            var gSel = document.getElementById(combo);
            var selVal = gSel.value;
            return selVal;
        }
        catch (err) {
            return ('undefined');
        }

    }

    this.removeItemIndex = function (index) {
        var combo = this.ComboId;
        $('#' + combo + '_Items li').each(function (i, item) {
            if (i == index) {
                $(item).remove();
            }
        })

    }
    this.removeItemText = function (text) {
        var combo = this.ComboId;
        $('#' + combo + '_Items li').each(function (i, item) {
            var t = item.childNodes[0].nodeValue;
            if (t == text) {
                $(item).remove();
            }
        })

    }
    this.removeItemValue = function (v) {
        var combo = this.ComboId;
        $('#' + combo + '_Items li').each(function (i, item) {
            var t = item.value;
            if (t == v) {
                $(item).remove();
            }
        })

    }
    this.setDefaultValue = function (obj) {
        var combo = this.ComboId;
        $('#' + combo + '_Input').val(obj.Text);
        $('#' + combo + '_Hidden').val(obj.Value);

    }
    this.setSelectedValue = function (obj) {
        var combo = this.ComboId;
        $('#' + combo + '_Input').val(obj.Text);
        $('#' + combo + '_Hidden').val(obj.Value);

    }
}
            