// JScript used for Page Editor

// Pass true to gray out screen, false to ungray  
// options are optional.  This is a JSON object with the following (optional) properties  
// opacity:0-100         
// Lower number = less grayout higher = more of a blackout   
// zindex: #             
// HTML elements with a higher zindex appear on top of the gray out  
// bgcolor: (#xxxxxx)    
// Standard RGB Hex color code  
// grayOut(true, {'zindex':'50', 'bgcolor':'#0000FF', 'opacity':'70'});  
// Because options is JSON opacity/zindex/bgcolor are all optional and can appear  
// in any order.  Pass only the properties you need to set.  
function grayOut(vis, options) {
    var options = options || {};
    var zindex = options.zindex || 50;
    var opacity = options.opacity || 70;
    var opaque = (opacity / 100);
    var bgcolor = options.bgcolor || '#000000';
    var dark = document.getElementById('darkenScreenObject');
    if (!dark) {
        // The dark layer doesn't exist, it's never been created.  So we'll    
        // create it here and apply some basic styles.    
        // If you are getting errors in IE see: http://support.microsoft.com/default.aspx/kb/927917    
        var tbody = document.getElementsByTagName("body")[0];
        var tnode = document.createElement('div');           // Create the layer.        
        tnode.style.position = 'absolute';                 // Position absolutely        
        tnode.style.top = '0px';                           // In the top        
        tnode.style.left = '0px';                          // Left corner of the page        
        tnode.style.overflow = 'hidden';                   // Try to avoid making scroll bars                    
        tnode.style.display = 'none';                      // Start out Hidden        
        tnode.id = 'darkenScreenObject';                   // Name it so we can find it later    
        tbody.appendChild(tnode);                            // Add it to the web page    
        dark = document.getElementById('darkenScreenObject');  // Get the object.  

    }

    if (vis) {    // Calculate the page width and height     
        if (document.body && (document.body.scrollWidth || document.body.scrollHeight)) {
            var pageWidth = document.body.scrollWidth + 'px';
            var pageHeight = document.body.scrollHeight + 'px';
            if (document.body.scrollHeight < '777') {
                pageHeight = '100%';
                pageWidth = '100%';
            }
        } else if (document.body.offsetWidth) {
            var pageWidth = document.body.offsetWidth + 'px';
            var pageHeight = document.body.offsetHeight + 'px';
            
        }
        else {
            var pageWidth = '100%';
            var pageHeight = '100%';
        }

        //set the shader to cover the entire page and make it visible.    
        dark.style.opacity = opaque;
        dark.style.MozOpacity = opaque;
        dark.style.filter = 'alpha(opacity=' + opacity + ')';
        dark.style.zIndex = zindex;
        dark.style.backgroundColor = bgcolor;
        dark.style.width = pageWidth;
        dark.style.height = pageHeight;
        dark.style.display = 'block';
    }
    else {
        dark.style.display = 'none';
    }
}


//Disable all links and buttons on the page 
//Not currently used in the page editor
//Call with the following method
//setTimeout('disableLinks()',3000); 
function disableLinks() { var c = document.links; for (var i = 0; i < c.length; i++) { c[i].title = c[i].href; c[i].href = '#'; } disableSubmitButtons(document.getElementsByTagName('INPUT')); disableSubmitButtons(document.getElementsByTagName('BUTTON')); }
function disableSubmitButtons(c) { for (var i = 0; i < c.length; i++) { if (c[i].type == 'submit') c[i].disabled = true; if (c[i].type == 'image') c[i].disabled = true; } }




//OnDrop Events
//function onDrop(sender, e) {
//    var container = e.get_container();
//    var item = e.get_droppedItem();
//    var position = e.get_position();
//    var widgetId = item.id.split('_')[item.id.split('_').length - 2];
//    var paneId = container.id.split('_')[container.id.split('_').length - 2];
//    Meanstream.Portal.Web.WidgetService.Move(widgetId, paneId, position);
//    $find(document.getElementById(item.id.substring(0, item.id.lastIndexOf('_DragDrop')) + '_ctl00_ctl01_MenuPanel_' + widgetId).getAttribute('HoverMenuExtender')).initialize(); 
//}