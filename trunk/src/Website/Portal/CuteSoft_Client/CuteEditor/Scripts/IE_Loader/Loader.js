var OxOb999=["undefined","Microsoft.XMLHTTP","readyState","onreadystatechange","","document","length","element \x27","\x27 not found","returnValue","preventDefault","cancelBubble","stopPropagation","onchange","oninitialized","command","commandui","commandvalue","oncommand","string","_fireEventFunction","event","parentNode","_IsCuteEditor","True","readOnly","_IsRichDropDown","null","value","selectedIndex","nodeName","TR","cells","display","style","nextSibling","innerHTML","\x3Cimg src=\x22","/Load.ashx?type=image\x26file=t-minus.gif\x22\x3E","onclick","CuteEditor_CollapseTreeDropDownItem(this,\x22","\x22)","none","/Load.ashx?type=image\x26file=t-plus.gif\x22\x3E","CuteEditor_ExpandTreeDropDownItem(this,\x22","//TODO: event not found? throw error ?","all","UNSELECTABLE","on","tabIndex","-1","contentWindow","contentDocument","parentWindow","id","frames","frameElement","//TODO:frame contentWindow not found?","head","script","language","javascript","type","text/javascript","src","caller","arguments","parent","top","opener","srcElement","target","//TODO: srcElement not found? throw error ?","fromElement","relatedTarget","toElement","keyCode","clientX","clientY","offsetX","offsetY","button","ctrlKey","altKey","shiftKey","CuteEditor_GetEditor(this).ExecImageCommand(this.getAttribute(\x27Command\x27),this.getAttribute(\x27CommandUI\x27),this.getAttribute(\x27CommandArgument\x27),this)","CuteEditor_GetEditor(this).PostBack(this.getAttribute(\x27Command\x27))","this.onmouseout();CuteEditor_GetEditor(this).DropMenu(this.getAttribute(\x27Group\x27),this)","ResourceDir","Theme","/Load.ashx?type=theme\x26theme=","\x26file=all.png","/Images/blank2020.gif","IMG","alt","title","Command","Group","ThemeIndex","width","20px","height","backgroundImage","url(",")","backgroundPosition","0 -","px","className","separator","CuteEditorButton","onmouseover","CuteEditor_ButtonCommandOver(this)","onmouseout","CuteEditor_ButtonCommandOut(this)","onmousedown","CuteEditor_ButtonCommandDown(this)","onmouseup","CuteEditor_ButtonCommandUp(this)","oncontextmenu","ondragstart","PostBack","ondblclick","_ToolBarID","_CodeViewToolBarID","_FrameID"," CuteEditorFrame"," CuteEditorToolbar","buttonInitialized","isover","CuteEditorButtonOver","CuteEditorButtonDown","CuteEditorDown","border","solid 1px #0A246A","backgroundColor","#b6bdd2","padding","1px","solid 1px #f5f5f4","inset 1px","IsCommandDisabled","CuteEditorButtonDisabled","IsCommandActive","CuteEditorButtonActive","(","href","location",",DanaInfo=",",","+","scriptProperties","GetScriptProperty","/Load.ashx?type=scripts\x26file=IE_Implementation\x26culture=","Culture","/Load.ashx?type=scripts\x26file=EditorExtension\x26culture=","CuteEditorImplementation","function","POST","\x26getModified=1\x26_temp=","loadScript","status","responseText","GET","\x26modified=","Failed to load impl code!","block","contentEditable","body","cursor","InitializeCode","no-drop","CuteEditorInitialize"];function CreateXMLHttpRequest(){try{if( typeof (XMLHttpRequest)!=OxOb999[0]){return  new XMLHttpRequest();} ;if( typeof (ActiveXObject)!=OxOb999[0]){return  new ActiveXObject(OxOb999[1]);} ;} catch(x){return null;} ;} ;function LoadXMLAsync(Oxa5d,Ox288,Ox235,Oxa5e){var Oxe7=CreateXMLHttpRequest();function Oxa5f(){if(Oxe7[OxOb999[2]]!=4){return ;} ;Oxe7[OxOb999[3]]= new Function();var Ox290=Oxe7;Oxe7=null;Ox235(Ox290);} ;Oxe7[OxOb999[3]]=Oxa5f;Oxe7.open(Oxa5d,Ox288,true);Oxe7.send(Oxa5e||OxOb999[4]);} ;function Window_GetElement(Ox1a8,Ox9a,Ox240){var Ox29=Ox1a8[OxOb999[5]].getElementById(Ox9a);if(Ox29){return Ox29;} ;var Ox31=Ox1a8[OxOb999[5]].getElementsByName(Ox9a);if(Ox31[OxOb999[6]]>0){return Ox31.item(0);} ;if(Ox240){throw ( new Error(OxOb999[7]+Ox9a+OxOb999[8]));} ;return null;} ;function Event_PreventDefault(Ox245){Ox245=Event_GetEvent(Ox245);Ox245[OxOb999[9]]=false;if(Ox245[OxOb999[10]]){Ox245.preventDefault();} ;} ;function Event_CancelBubble(Ox245){Ox245=Event_GetEvent(Ox245);Ox245[OxOb999[11]]=true;if(Ox245[OxOb999[12]]){Ox245.stopPropagation();} ;return false;} ;function Event_CancelEvent(Ox245){Ox245=Event_GetEvent(Ox245);Event_PreventDefault(Ox245);return Event_CancelBubble(Ox245);} ;function CuteEditor_AddMainMenuItems(Ox66a){} ;function CuteEditor_AddDropMenuItems(Ox66a,Ox671){} ;function CuteEditor_AddTagMenuItems(Ox66a,Ox673){} ;function CuteEditor_AddVerbMenuItems(Ox66a,Ox673){} ;function CuteEditor_OnInitialized(editor){} ;function CuteEditor_OnCommand(editor,Ox4d,Ox4e,Ox4f){} ;function CuteEditor_OnChange(editor){} ;function CuteEditor_FilterCode(editor,Ox26b){return Ox26b;} ;function CuteEditor_FilterHTML(editor,Ox283){return Ox283;} ;function CuteEditor_FireChange(editor){window.CuteEditor_OnChange(editor);CuteEditor_FireEvent(editor,OxOb999[13],null);} ;function CuteEditor_FireInitialized(editor){window.CuteEditor_OnInitialized(editor);CuteEditor_FireEvent(editor,OxOb999[14],null);} ;function CuteEditor_FireCommand(editor,Ox4d,Ox4e,Ox4f){var Ox13e=window.CuteEditor_OnCommand(editor,Ox4d,Ox4e,Ox4f);if(Ox13e==true){return true;} ;var Ox67c={};Ox67c[OxOb999[15]]=Ox4d;Ox67c[OxOb999[16]]=Ox4e;Ox67c[OxOb999[17]]=Ox4f;Ox67c[OxOb999[9]]=true;CuteEditor_FireEvent(editor,OxOb999[18],Ox67c);if(Ox67c[OxOb999[9]]==false){return true;} ;} ;function CuteEditor_FireEvent(editor,Ox67e,Ox67f){if(Ox67f==null){Ox67f={};} ;var Ox680=editor.getAttribute(Ox67e);if(Ox680){if( typeof (Ox680)==OxOb999[19]){editor[OxOb999[20]]= new Function(OxOb999[21],Ox680);} else {editor[OxOb999[20]]=Ox680;} ;editor._fireEventFunction(Ox67f);} ;} ;function CuteEditor_GetEditor(element){for(var Ox43=element;Ox43!=null;Ox43=Ox43[OxOb999[22]]){if(Ox43.getAttribute(OxOb999[23])==OxOb999[24]){return Ox43;} ;} ;return null;} ;function CuteEditor_DropDownCommand(element,Oxa61){var editor=CuteEditor_GetEditor(element);if(editor[OxOb999[25]]){return ;} ;if(element.getAttribute(OxOb999[26])==OxOb999[24]){var Ox142=element.GetValue();if(Ox142==OxOb999[27]){Ox142=OxOb999[4];} ;var Ox201=element.GetText();if(Ox201==OxOb999[27]){Ox201=OxOb999[4];} ;element.SetSelectedIndex(0);editor.ExecCommand(Oxa61,false,Ox142,Ox201);} else {if(!editor[OxOb999[25]]&&element[OxOb999[28]]){var Ox142=element[OxOb999[28]];if(Ox142==OxOb999[27]){Ox142=OxOb999[4];} ;element[OxOb999[29]]=0;editor.ExecCommand(Oxa61,false,Ox142,Ox201);} else {element[OxOb999[29]]=0;} ;} ;editor.FocusDocument();} ;function CuteEditor_ExpandTreeDropDownItem(src,Ox740){var Oxba=null;while(src!=null){if(src[OxOb999[30]]==OxOb999[31]){Oxba=src;break ;} ;src=src[OxOb999[22]];} ;var Ox1e4=Oxba[OxOb999[32]].item(0);Oxba[OxOb999[35]][OxOb999[34]][OxOb999[33]]=OxOb999[4];Ox1e4[OxOb999[36]]=OxOb999[37]+Ox740+OxOb999[38];Oxba[OxOb999[39]]= new Function(OxOb999[40]+Ox740+OxOb999[41]);} ;function CuteEditor_CollapseTreeDropDownItem(src,Ox740){var Oxba=null;while(src!=null){if(src[OxOb999[30]]==OxOb999[31]){Oxba=src;break ;} ;src=src[OxOb999[22]];} ;var Ox1e4=Oxba[OxOb999[32]].item(0);Oxba[OxOb999[35]][OxOb999[34]][OxOb999[33]]=OxOb999[42];Ox1e4[OxOb999[36]]=OxOb999[37]+Ox740+OxOb999[43];Oxba[OxOb999[39]]= new Function(OxOb999[44]+Ox740+OxOb999[41]);} ;function Event_GetEvent(Ox245){Ox245=Event_FindEvent(Ox245);if(Ox245==null){Debug_Todo(OxOb999[45]);} ;return Ox245;} ;function Element_GetAllElements(p){var arr=[];for(var i=0;i<p[OxOb999[46]][OxOb999[6]];i++){arr.push(p[OxOb999[46]].item(i));} ;return arr;} ;function Element_SetUnselectable(element){element.setAttribute(OxOb999[47],OxOb999[48]);element.setAttribute(OxOb999[49],OxOb999[50]);var arr=Element_GetAllElements(element);var len=arr[OxOb999[6]];if(!len){return ;} ;for(var i=0;i<len;i++){arr[i].setAttribute(OxOb999[47],OxOb999[48]);arr[i].setAttribute(OxOb999[49],OxOb999[50]);} ;} ;function Frame_GetContentWindow(Ox349){if(Ox349[OxOb999[51]]){return Ox349[OxOb999[51]];} ;if(Ox349[OxOb999[52]]){if(Ox349[OxOb999[52]][OxOb999[53]]){return Ox349[OxOb999[52]][OxOb999[53]];} ;} ;var Ox1a8;if(Ox349[OxOb999[54]]){Ox1a8=window[OxOb999[55]][Ox349[OxOb999[54]]];if(Ox1a8){return Ox1a8;} ;} ;var len=window[OxOb999[55]][OxOb999[6]];for(var i=0;i<len;i++){Ox1a8=window[OxOb999[55]][i];if(Ox1a8[OxOb999[56]]==Ox349){return Ox1a8;} ;if(Ox1a8[OxOb999[5]]==Ox349[OxOb999[52]]){return Ox1a8;} ;} ;Debug_Todo(OxOb999[57]);} ;function Array_IndexOf(arr,Ox247){for(var i=0;i<arr[OxOb999[6]];i++){if(arr[i]==Ox247){return i;} ;} ;return -1;} ;function Array_Contains(arr,Ox247){return Array_IndexOf(arr,Ox247)!=-1;} ;function clearArray(Ox24a){for(var i=0;i<Ox24a[OxOb999[6]];i++){Ox24a[i]=null;} ;} ;function Event_FindEvent(Ox245){if(Ox245&&Ox245[OxOb999[10]]){return Ox245;} ;if(window[OxOb999[21]]){return window[OxOb999[21]];} ;return Event_FindEvent_FindEventFromWindows();} ;function include(Oxc9,Ox288){var Ox289=document.getElementsByTagName(OxOb999[58]).item(0);var Ox28a=document.getElementById(Oxc9);if(Ox28a){Ox289.removeChild(Ox28a);} ;var Ox28b=document.createElement(OxOb999[59]);Ox28b.setAttribute(OxOb999[60],OxOb999[61]);Ox28b.setAttribute(OxOb999[62],OxOb999[63]);Ox28b.setAttribute(OxOb999[64],Ox288);Ox28b.setAttribute(OxOb999[54],Oxc9);Ox289.appendChild(Ox28b);} ;function Event_FindEvent_FindEventFromCallers(){var Ox18f=Event_GetEvent[OxOb999[65]];for(var i=0;i<100;i++){if(!Ox18f){break ;} ;var Ox245=Ox18f[OxOb999[66]][0];if(Ox245&&Ox245[OxOb999[10]]){return Ox245;} ;Ox18f=Ox18f[OxOb999[65]];} ;} ;function Event_FindEvent_FindEventFromWindows(){var arr=[];return Ox24e(window);function Ox24e(Ox1a8){if(Ox1a8==null){return null;} ;if(Ox1a8[OxOb999[21]]){return Ox1a8[OxOb999[21]];} ;if(Array_Contains(arr,Ox1a8)){return null;} ;arr.push(Ox1a8);var Ox24f=[];if(Ox1a8[OxOb999[67]]!=Ox1a8){Ox24f.push(Ox1a8.parent);} ;if(Ox1a8[OxOb999[68]]!=Ox1a8[OxOb999[67]]){Ox24f.push(Ox1a8.top);} ;if(Ox1a8[OxOb999[69]]){Ox24f.push(Ox1a8.opener);} ;for(var i=0;i<Ox1a8[OxOb999[55]][OxOb999[6]];i++){Ox24f.push(Ox1a8[OxOb999[55]][i]);} ;for(var i=0;i<Ox24f[OxOb999[6]];i++){try{var Ox245=Ox24e(Ox24f[i]);if(Ox245){return Ox245;} ;} catch(x){} ;} ;return null;} ;} ;function Event_GetSrcElement(Ox245){Ox245=Event_GetEvent(Ox245);if(Ox245[OxOb999[70]]){return Ox245[OxOb999[70]];} ;if(Ox245[OxOb999[71]]){return Ox245[OxOb999[71]];} ;Debug_Todo(OxOb999[72]);return null;} ;function Event_GetFromElement(Ox245){Ox245=Event_GetEvent(Ox245);if(Ox245[OxOb999[73]]){return Ox245[OxOb999[73]];} ;if(Ox245[OxOb999[74]]){return Ox245[OxOb999[74]];} ;return null;} ;function Event_GetToElement(Ox245){Ox245=Event_GetEvent(Ox245);if(Ox245[OxOb999[75]]){return Ox245[OxOb999[75]];} ;if(Ox245[OxOb999[74]]){return Ox245[OxOb999[74]];} ;return null;} ;function Event_GetKeyCode(Ox245){Ox245=Event_GetEvent(Ox245);return Ox245[OxOb999[76]];} ;function Event_GetClientX(Ox245){Ox245=Event_GetEvent(Ox245);return Ox245[OxOb999[77]];} ;function Event_GetClientY(Ox245){Ox245=Event_GetEvent(Ox245);return Ox245[OxOb999[78]];} ;function Event_GetOffsetX(Ox245){Ox245=Event_GetEvent(Ox245);return Ox245[OxOb999[79]];} ;function Event_GetOffsetY(Ox245){Ox245=Event_GetEvent(Ox245);return Ox245[OxOb999[80]];} ;function Event_IsLeftButton(Ox245){Ox245=Event_GetEvent(Ox245);return Ox245[OxOb999[81]]==1;} ;function Event_IsCtrlKey(Ox245){Ox245=Event_GetEvent(Ox245);return Ox245[OxOb999[82]];} ;function Event_IsAltKey(Ox245){Ox245=Event_GetEvent(Ox245);return Ox245[OxOb999[83]];} ;function Event_IsShiftKey(Ox245){Ox245=Event_GetEvent(Ox245);return Ox245[OxOb999[84]];} ;function CuteEditor_BasicInitialize(editor){var Ox709= new Function(OxOb999[85]);var Oxa65= new Function(OxOb999[86]);var Oxa66= new Function(OxOb999[87]);var Oxa67=editor.GetScriptProperty(OxOb999[88]);var Oxa68=editor.GetScriptProperty(OxOb999[89]);var Oxa69=Oxa67+OxOb999[90]+Oxa68+OxOb999[91];var Oxa6a=Oxa67+OxOb999[92];var images=editor.getElementsByTagName(OxOb999[93]);var len=images[OxOb999[6]];for(var i=0;i<len;i++){var img=images[i];if(img.getAttribute(OxOb999[94])&&!img.getAttribute(OxOb999[95])){img.setAttribute(OxOb999[95],img.getAttribute(OxOb999[94]));} ;var Ox135=img.getAttribute(OxOb999[96]);var Ox671=img.getAttribute(OxOb999[97]);if(!(Ox135||Ox671)){continue ;} ;var Oxa6b=img.getAttribute(OxOb999[98]);if(parseInt(Oxa6b)>=0){img[OxOb999[34]][OxOb999[99]]=OxOb999[100];img[OxOb999[34]][OxOb999[101]]=OxOb999[100];img[OxOb999[64]]=Oxa6a;img[OxOb999[34]][OxOb999[102]]=OxOb999[103]+Oxa69+OxOb999[104];img[OxOb999[34]][OxOb999[105]]=OxOb999[106]+(Oxa6b*20)+OxOb999[107];img[OxOb999[34]][OxOb999[33]]=OxOb999[4];} ;if(img[OxOb999[108]]!=OxOb999[109]){img[OxOb999[108]]=OxOb999[110];img[OxOb999[111]]= new Function(OxOb999[112]);img[OxOb999[113]]= new Function(OxOb999[114]);img[OxOb999[115]]= new Function(OxOb999[116]);img[OxOb999[117]]= new Function(OxOb999[118]);} ;if(!img[OxOb999[119]]){img[OxOb999[119]]=Event_CancelEvent;} ;if(!img[OxOb999[120]]){img[OxOb999[120]]=Event_CancelEvent;} ;if(Ox135){var Ox18f=img.getAttribute(OxOb999[121])==OxOb999[24]?Oxa65:Ox709;if(img[OxOb999[39]]==null){img[OxOb999[39]]=Ox18f;} ;if(img[OxOb999[122]]==null){img[OxOb999[122]]=Ox18f;} ;} else {if(Ox671){if(img[OxOb999[39]]==null){img[OxOb999[39]]=Oxa66;} ;} ;} ;} ;var Ox776=Window_GetElement(window,editor.GetScriptProperty(OxOb999[123]),true);var Ox777=Window_GetElement(window,editor.GetScriptProperty(OxOb999[124]),true);var Ox772=Window_GetElement(window,editor.GetScriptProperty(OxOb999[125]),true);Ox772[OxOb999[108]]+=OxOb999[126];Ox776[OxOb999[108]]+=OxOb999[127];Ox777[OxOb999[108]]+=OxOb999[127];Element_SetUnselectable(Ox776);Element_SetUnselectable(Ox777);} ;function CuteEditor_ButtonOver(element){if(!element[OxOb999[128]]){element[OxOb999[119]]=Event_CancelEvent;element[OxOb999[113]]=CuteEditor_ButtonOut;element[OxOb999[115]]=CuteEditor_ButtonDown;element[OxOb999[117]]=CuteEditor_ButtonUp;Element_SetUnselectable(element);element[OxOb999[128]]=true;} ;element[OxOb999[129]]=true;element[OxOb999[108]]=OxOb999[130];} ;function CuteEditor_ButtonOut(){var element=this;element[OxOb999[108]]=OxOb999[110];element[OxOb999[129]]=false;} ;function CuteEditor_ButtonDown(){if(!Event_IsLeftButton()){return ;} ;var element=this;element[OxOb999[108]]=OxOb999[131];} ;function CuteEditor_ButtonUp(){if(!Event_IsLeftButton()){return ;} ;var element=this;if(element[OxOb999[129]]){element[OxOb999[108]]=OxOb999[130];} else {element[OxOb999[108]]=OxOb999[132];} ;} ;function CuteEditor_ColorPicker_ButtonOver(element){if(!element[OxOb999[128]]){element[OxOb999[119]]=Event_CancelEvent;element[OxOb999[113]]=CuteEditor_ColorPicker_ButtonOut;element[OxOb999[115]]=CuteEditor_ColorPicker_ButtonDown;Element_SetUnselectable(element);element[OxOb999[128]]=true;} ;element[OxOb999[129]]=true;element[OxOb999[34]][OxOb999[133]]=OxOb999[134];element[OxOb999[34]][OxOb999[135]]=OxOb999[136];element[OxOb999[34]][OxOb999[137]]=OxOb999[138];} ;function CuteEditor_ColorPicker_ButtonOut(){var element=this;element[OxOb999[129]]=false;element[OxOb999[34]][OxOb999[133]]=OxOb999[139];element[OxOb999[34]][OxOb999[135]]=OxOb999[4];element[OxOb999[34]][OxOb999[137]]=OxOb999[138];} ;function CuteEditor_ColorPicker_ButtonDown(){var element=this;element[OxOb999[34]][OxOb999[133]]=OxOb999[140];element[OxOb999[34]][OxOb999[135]]=OxOb999[4];element[OxOb999[34]][OxOb999[137]]=OxOb999[138];} ;function CuteEditor_ButtonCommandOver(element){element[OxOb999[129]]=true;if(element[OxOb999[141]]){element[OxOb999[108]]=OxOb999[142];} else {element[OxOb999[108]]=OxOb999[130];} ;} ;function CuteEditor_ButtonCommandOut(element){element[OxOb999[129]]=false;if(element[OxOb999[143]]){element[OxOb999[108]]=OxOb999[144];} else {if(element[OxOb999[141]]){element[OxOb999[108]]=OxOb999[142];} else {element[OxOb999[108]]=OxOb999[110];} ;} ;} ;function CuteEditor_ButtonCommandDown(element){if(!Event_IsLeftButton()){return ;} ;element[OxOb999[108]]=OxOb999[131];} ;function CuteEditor_ButtonCommandUp(element){if(!Event_IsLeftButton()){return ;} ;if(element[OxOb999[141]]){element[OxOb999[108]]=OxOb999[142];return ;} ;if(element[OxOb999[129]]){element[OxOb999[108]]=OxOb999[130];} else {if(element[OxOb999[143]]){element[OxOb999[108]]=OxOb999[144];} else {element[OxOb999[108]]=OxOb999[110];} ;} ;} ;var CuteEditorGlobalFunctions=[CuteEditor_GetEditor,CuteEditor_ButtonOver,CuteEditor_ButtonOut,CuteEditor_ButtonDown,CuteEditor_ButtonUp,CuteEditor_ColorPicker_ButtonOver,CuteEditor_ColorPicker_ButtonOut,CuteEditor_ColorPicker_ButtonDown,CuteEditor_ButtonCommandOver,CuteEditor_ButtonCommandOut,CuteEditor_ButtonCommandDown,CuteEditor_ButtonCommandUp,CuteEditor_DropDownCommand,CuteEditor_ExpandTreeDropDownItem,CuteEditor_CollapseTreeDropDownItem,CuteEditor_OnInitialized,CuteEditor_OnCommand,CuteEditor_OnChange,CuteEditor_AddVerbMenuItems,CuteEditor_AddTagMenuItems,CuteEditor_AddMainMenuItems,CuteEditor_AddDropMenuItems,CuteEditor_FilterCode,CuteEditor_FilterHTML];function SetupCuteEditorGlobalFunctions(){for(var i=0;i<CuteEditorGlobalFunctions[OxOb999[6]];i++){var Ox18f=CuteEditorGlobalFunctions[i];var name=Ox18f+OxOb999[4];name=name.substr(8,name.indexOf(OxOb999[145])-8).replace(/\s/g,OxOb999[4]);if(!window[name]){window[name]=Ox18f;} ;} ;} ;SetupCuteEditorGlobalFunctions();var __danainfo=null;var danaurl=window[OxOb999[147]][OxOb999[146]];var danapos=danaurl.indexOf(OxOb999[148]);if(danapos!=-1){var pluspos1=danaurl.indexOf(OxOb999[149],danapos+10);var pluspos2=danaurl.indexOf(OxOb999[150],danapos+10);if(pluspos1!=-1&&pluspos1<pluspos2){pluspos2=pluspos1;} ;__danainfo=danaurl.substring(danapos,pluspos2)+OxOb999[150];} ;function CuteEditor_GetScriptProperty(name){var Ox142=this[OxOb999[151]][name];if(Ox142&&__danainfo){if(name==OxOb999[88]){return Ox142+__danainfo;} ;var Ox382=this[OxOb999[151]][OxOb999[88]];if(Ox142.substr(0,Ox382.length)==Ox382){return Ox382+__danainfo+Ox142.substring(Ox382.length);} ;} ;return Ox142;} ;function CuteEditor_SetScriptProperty(name,Ox142){if(Ox142==null){this[OxOb999[151]][name]=null;} else {this[OxOb999[151]][name]=String(Ox142);} ;} ;function CuteEditorInitialize(Oxa78,Oxa79){var editor=Window_GetElement(window,Oxa78,true);editor[OxOb999[151]]=Oxa79;editor[OxOb999[152]]=CuteEditor_GetScriptProperty;var Ox772=Window_GetElement(window,editor.GetScriptProperty(OxOb999[125]),true);var editwin=Frame_GetContentWindow(Ox772);var editdoc=editwin[OxOb999[5]];var Oxa7a=false;var Oxa7b;var Oxa7c=false;var Oxa7d=editor.GetScriptProperty(OxOb999[88])+OxOb999[153]+editor.GetScriptProperty(OxOb999[154]);var Oxb05=editor.GetScriptProperty(OxOb999[88])+OxOb999[155]+editor.GetScriptProperty(OxOb999[154]);function Oxa7e(){if( typeof (window[OxOb999[156]])==OxOb999[157]){return ;} ;try{LoadXMLAsync(OxOb999[158],Oxa7d+OxOb999[159]+ new Date().getTime(),Oxa7f);} catch(x){include(OxOb999[160],Oxa7d);var Oxb06=setInterval(function (){if(window[OxOb999[156]]){clearInterval(Oxb06);if(Oxa7a){Oxa81();} ;} ;} ,100);} ;} ;function Oxa7f(Ox290){var Ox889= new Date().getTime();if(Ox290[OxOb999[161]]!=200){} else {Ox889=Ox290[OxOb999[162]];} ;LoadXMLAsync(OxOb999[163],Oxa7d+OxOb999[164]+Ox889,Oxa80);} ;function Oxa80(Ox290){if(Ox290[OxOb999[161]]!=200){alert(OxOb999[165]);return ;} ;CuteEditorInstallScriptCode(Ox290.responseText,OxOb999[156]);if(Oxa7a){Oxa81();} ;} ;function Oxa81(){if(Oxa7c){return ;} ;for(var Ox183=editor;Ox183&&Ox183[OxOb999[34]];Ox183=Ox183[OxOb999[22]]){if(Ox183[OxOb999[34]][OxOb999[33]]==OxOb999[42]){setTimeout(Oxa81,100);return ;} ;} ;Oxa7c=true;Ox772[OxOb999[34]][OxOb999[33]]=OxOb999[166];editdoc[OxOb999[168]][OxOb999[167]]=true;window.CuteEditorImplementation(editor);try{editor[OxOb999[34]][OxOb999[169]]=OxOb999[4];} catch(x){} ;try{editdoc[OxOb999[168]][OxOb999[34]][OxOb999[169]]=OxOb999[4];} catch(x){} ;var Oxa82=editor.GetScriptProperty(OxOb999[170]);if(Oxa82){editor.Eval(Oxa82);} ;} ;function Oxa83(){if(!window[OxOb999[5]][OxOb999[168]].contains(editor)){return ;} ;try{Ox772=Window_GetElement(window,editor.GetScriptProperty(OxOb999[125]),true);editwin=Frame_GetContentWindow(Ox772);editdoc=editwin[OxOb999[5]];x=editdoc[OxOb999[168]];} catch(x){setTimeout(Oxa83,100);return ;} ;if(!editdoc[OxOb999[168]]){setTimeout(Oxa83,100);return ;} ;if(!Oxa7a){Oxa7a=true;setTimeout(Oxa83,100);return ;} ;if( typeof (window[OxOb999[156]])==OxOb999[157]){Oxa81();} else {try{editdoc[OxOb999[168]][OxOb999[34]][OxOb999[169]]=OxOb999[171];} catch(x){} ;} ;} ;CuteEditor_BasicInitialize(editor);Oxa7e();Oxa83();} ;function CuteEditorInstallScriptCode(Ox9c3,Ox9c4){eval(Ox9c3);window[Ox9c4]=eval(Ox9c4);} ;window[OxOb999[172]]=CuteEditorInitialize;