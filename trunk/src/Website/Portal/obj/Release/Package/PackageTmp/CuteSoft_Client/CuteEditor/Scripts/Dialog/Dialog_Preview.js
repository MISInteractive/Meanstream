var OxO50a6=["idSource","inc_width","inc_height","onload","availWidth","screen","window","availHeight","contentWindow","outerHTML","documentElement","text/html","replace","onresize","dialogWidth","innerWidth","clientWidth","body","dialogHeight","innerHeight","clientHeight","value","contentDocument","document"];var editor=Window_GetDialogArguments(window);var idSource=Window_GetElement(window,OxO50a6[0],true);var inc_width=Window_GetElement(window,OxO50a6[1],true);var inc_height=Window_GetElement(window,OxO50a6[2],true);var ParentW;var ParentH;window[OxO50a6[3]]=function window_onload(){ParentW=top[OxO50a6[6]][OxO50a6[5]][OxO50a6[4]];ParentH=top[OxO50a6[6]][OxO50a6[5]][OxO50a6[7]];var iframe=idSource[OxO50a6[8]];var editdoc=editor.GetDocument();var Oxf5;if(Browser_IsWinIE()){Oxf5=editdoc[OxO50a6[10]][OxO50a6[9]];} else {Oxf5=outerHTML(editdoc.documentElement);} ;var Ox46e=Frame_GetContentDocument(iframe);Ox46e.open(OxO50a6[11],OxO50a6[12]);Ox46e.write(Oxf5);Ox46e.close();ShowSizeInfo();} ;window[OxO50a6[13]]=ShowSizeInfo;function ShowSizeInfo(){var Oxe1,Ox2d;if(window[OxO50a6[14]]){Oxe1=window[OxO50a6[14]];} else {if(window[OxO50a6[15]]){Oxe1=window[OxO50a6[15]];} else {if(document[OxO50a6[10]]&&document[OxO50a6[10]][OxO50a6[16]]){Oxe1=document[OxO50a6[10]][OxO50a6[16]];} else {if(document[OxO50a6[17]]){Oxe1=document[OxO50a6[17]][OxO50a6[16]];} ;} ;} ;} ;if(window[OxO50a6[18]]){Ox2d=window[OxO50a6[18]];} else {if(window[OxO50a6[19]]){Ox2d=window[OxO50a6[19]];} else {if(document[OxO50a6[10]]&&document[OxO50a6[10]][OxO50a6[20]]){Ox2d=document[OxO50a6[10]][OxO50a6[20]];} else {if(document[OxO50a6[17]]){Ox2d=document[OxO50a6[17]][OxO50a6[20]];} ;} ;} ;} ;inc_width[OxO50a6[21]]=Oxe1;inc_height[OxO50a6[21]]=Ox2d;} ;function do_Close(){Window_CloseDialog(window);} ;function Frame_GetContentDocument(Ox349){if(Ox349[OxO50a6[22]]){return Ox349[OxO50a6[22]];} ;return Ox349[OxO50a6[23]];} ;