var OxO122a=["nodeName","INPUT","TEXTAREA","BUTTON","IMG","SELECT","TABLE","position","style","absolute","relative","|H1|H2|H3|H4|H5|H6|P|PRE|LI|TD|DIV|BLOCKQUOTE|DT|DD|TABLE|HR|IMG|","|","body","document","allanchors","anchor_name","editor","window","name","value","[[ValidName]]","options","length","anchors","OPTION","text","#","images","className","cetempAnchor","anchorname","","--\x3E"," ","trim","prototype"];function Element_IsBlockControl(element){var name=element[OxO122a[0]];if(name==OxO122a[1]){return true;} ;if(name==OxO122a[2]){return true;} ;if(name==OxO122a[3]){return true;} ;if(name==OxO122a[4]){return true;} ;if(name==OxO122a[5]){return true;} ;if(name==OxO122a[6]){return true;} ;var Ox126=element[OxO122a[8]][OxO122a[7]];if(Ox126==OxO122a[9]||Ox126==OxO122a[10]){return true;} ;return false;} ;function Element_CUtil_IsBlock(Ox36f){var Ox370=OxO122a[11];return (Ox36f!=null)&&(Ox370.indexOf(OxO122a[12]+Ox36f[OxO122a[0]]+OxO122a[12])!=-1);} ;function Window_SelectElement(Ox1a8,element){if(Browser_UseIESelection()){if(Element_IsBlockControl(element)){var Ox31=Ox1a8[OxO122a[14]][OxO122a[13]].createControlRange();Ox31.add(element);Ox31.select();} else {var Ox228=Ox1a8[OxO122a[14]][OxO122a[13]].createTextRange();Ox228.moveToElementText(element);Ox228.select();} ;} else {var Ox228=Ox1a8[OxO122a[14]].createRange();try{Ox228.selectNode(element);} catch(x){Ox228.selectNodeContents(element);} ;var Ox136=Ox1a8.getSelection();Ox136.removeAllRanges();Ox136.addRange(Ox228);} ;} ;var allanchors=Window_GetElement(window,OxO122a[15],true);var anchor_name=Window_GetElement(window,OxO122a[16],true);var obj=Window_GetDialogArguments(window);var editor=obj[OxO122a[17]];var editwin=obj[OxO122a[18]];var editdoc=obj[OxO122a[14]];var name=obj[OxO122a[19]];function insert_link(){var Ox375=anchor_name[OxO122a[20]];var Ox376=/[^a-z\d]/i;Ox375=Ox375.trim();if(Ox376.test(Ox375)){alert(OxO122a[21]);} else {Window_SetDialogReturnValue(window,Ox375);Window_CloseDialog(window);} ;} ;function updateList(){while(allanchors[OxO122a[22]][OxO122a[23]]!=0){allanchors[OxO122a[22]].remove(allanchors.options(0));} ;if(Browser_IsWinIE()){for(var i=0;i<editdoc[OxO122a[24]][OxO122a[23]];i++){var Ox378=document.createElement(OxO122a[25]);Ox378[OxO122a[26]]=OxO122a[27]+editdoc[OxO122a[24]][i][OxO122a[19]];Ox378[OxO122a[20]]=editdoc[OxO122a[24]][i][OxO122a[19]];allanchors[OxO122a[22]].add(Ox378);} ;} else {var Ox379=editdoc[OxO122a[28]];if(Ox379){for(var Ox25=0;Ox25<Ox379[OxO122a[23]];Ox25++){var img=Ox379[Ox25];if(img[OxO122a[29]]==OxO122a[30]){var Ox378=document.createElement(OxO122a[25]);Ox378[OxO122a[26]]=OxO122a[27]+img.getAttribute(OxO122a[31]);Ox378[OxO122a[20]]=img.getAttribute(OxO122a[31]);allanchors[OxO122a[22]].add(Ox378);} ;} ;} ;} ;} ;function selectAnchor(Ox37b){editor.FocusDocument();for(var i=0;i<editdoc[OxO122a[24]][OxO122a[23]];i++){if(editdoc[OxO122a[24]][i][OxO122a[19]]==Ox37b){anchor_name[OxO122a[20]]=Ox37b;Window_SelectElement(editwin,editdoc[OxO122a[24]][i]);} ;} ;} ;if(name&&name!=OxO122a[32]){name=name.replace(/[\s]*<!--[\s\S]*?-->[\s]*/g,OxO122a[32]);name=name.replace(OxO122a[33],OxO122a[34]);anchor_name[OxO122a[20]]=name;} ;updateList();String[OxO122a[36]][OxO122a[35]]=function (){return this.replace(/^\s*/,OxO122a[32]).replace(/\s*$/,OxO122a[32]);} ;