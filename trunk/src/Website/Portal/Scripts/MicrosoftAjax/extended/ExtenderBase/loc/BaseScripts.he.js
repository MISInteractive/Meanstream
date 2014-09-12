// (c) 2010 CodePlex Foundation
(function(){var b="ExtendedBase";function a(){var b="undefined",f="populating",e="populated",d="dispose",c="initialize",a=null,g=this,h=Sys.version;if(!h&&!Sys._versionChecked){Sys._versionChecked=true;throw new Error("AjaxControlToolkit requires ASP.NET Ajax 4.0 scripts. Ensure the correct version of the scripts are referenced. If you are using an ASP.NET ScriptManager, switch to the ToolkitScriptManager in AjaxControlToolkit.dll.");}Type.registerNamespace("Sys.Extended.UI");Sys.Extended.UI.BehaviorBase=function(c){var b=this;Sys.Extended.UI.BehaviorBase.initializeBase(b,[c]);b._clientStateFieldID=a;b._pageRequestManager=a;b._partialUpdateBeginRequestHandler=a;b._partialUpdateEndRequestHandler=a};Sys.Extended.UI.BehaviorBase.prototype={initialize:function(){Sys.Extended.UI.BehaviorBase.callBaseMethod(this,c)},dispose:function(){var b=this;Sys.Extended.UI.BehaviorBase.callBaseMethod(b,d);if(b._pageRequestManager){if(b._partialUpdateBeginRequestHandler){b._pageRequestManager.remove_beginRequest(b._partialUpdateBeginRequestHandler);b._partialUpdateBeginRequestHandler=a}if(b._partialUpdateEndRequestHandler){b._pageRequestManager.remove_endRequest(b._partialUpdateEndRequestHandler);b._partialUpdateEndRequestHandler=a}b._pageRequestManager=a}},get_ClientStateFieldID:function(){return this._clientStateFieldID},set_ClientStateFieldID:function(a){if(this._clientStateFieldID!=a){this._clientStateFieldID=a;this.raisePropertyChanged("ClientStateFieldID")}},get_ClientState:function(){if(this._clientStateFieldID){var b=document.getElementById(this._clientStateFieldID);if(b)return b.value}return a},set_ClientState:function(b){if(this._clientStateFieldID){var a=document.getElementById(this._clientStateFieldID);if(a)a.value=b}},registerPartialUpdateEvents:function(){var a=this;if(Sys&&Sys.WebForms&&Sys.WebForms.PageRequestManager){a._pageRequestManager=Sys.WebForms.PageRequestManager.getInstance();if(a._pageRequestManager){a._partialUpdateBeginRequestHandler=Function.createDelegate(a,a._partialUpdateBeginRequest);a._pageRequestManager.add_beginRequest(a._partialUpdateBeginRequestHandler);a._partialUpdateEndRequestHandler=Function.createDelegate(a,a._partialUpdateEndRequest);a._pageRequestManager.add_endRequest(a._partialUpdateEndRequestHandler)}}},_partialUpdateBeginRequest:function(){},_partialUpdateEndRequest:function(){}};Sys.Extended.UI.BehaviorBase.registerClass("Sys.Extended.UI.BehaviorBase",Sys.UI.Behavior);Sys.Extended.UI.DynamicPopulateBehaviorBase=function(c){var b=this;Sys.Extended.UI.DynamicPopulateBehaviorBase.initializeBase(b,[c]);b._DynamicControlID=a;b._DynamicContextKey=a;b._DynamicServicePath=a;b._DynamicServiceMethod=a;b._cacheDynamicResults=false;b._dynamicPopulateBehavior=a;b._populatingHandler=a;b._populatedHandler=a};Sys.Extended.UI.DynamicPopulateBehaviorBase.prototype={initialize:function(){var a=this;Sys.Extended.UI.DynamicPopulateBehaviorBase.callBaseMethod(a,c);a._populatingHandler=Function.createDelegate(a,a._onPopulating);a._populatedHandler=Function.createDelegate(a,a._onPopulated)},dispose:function(){var b=this;if(b._populatedHandler){b._dynamicPopulateBehavior&&b._dynamicPopulateBehavior.remove_populated(b._populatedHandler);b._populatedHandler=a}if(b._populatingHandler){b._dynamicPopulateBehavior&&b._dynamicPopulateBehavior.remove_populating(b._populatingHandler);b._populatingHandler=a}if(b._dynamicPopulateBehavior){b._dynamicPopulateBehavior.dispose();b._dynamicPopulateBehavior=a}Sys.Extended.UI.DynamicPopulateBehaviorBase.callBaseMethod(b,d)},populate:function(c){var b=this;if(b._dynamicPopulateBehavior&&b._dynamicPopulateBehavior.get_element()!=$get(b._DynamicControlID)){b._dynamicPopulateBehavior.dispose();b._dynamicPopulateBehavior=a}if(!b._dynamicPopulateBehavior&&b._DynamicControlID&&b._DynamicServiceMethod){b._dynamicPopulateBehavior=$create(Sys.Extended.UI.DynamicPopulateBehavior,{id:b.get_id()+"_DynamicPopulateBehavior",ContextKey:b._DynamicContextKey,ServicePath:b._DynamicServicePath,ServiceMethod:b._DynamicServiceMethod,cacheDynamicResults:b._cacheDynamicResults},a,a,$get(b._DynamicControlID));b._dynamicPopulateBehavior.add_populating(b._populatingHandler);b._dynamicPopulateBehavior.add_populated(b._populatedHandler)}b._dynamicPopulateBehavior&&b._dynamicPopulateBehavior.populate(c?c:b._DynamicContextKey)},_onPopulating:function(b,a){this.raisePopulating(a)},_onPopulated:function(b,a){this.raisePopulated(a)},get_dynamicControlID:function(){return this._DynamicControlID},get_DynamicControlID:g.get_dynamicControlID,set_dynamicControlID:function(b){var a=this;if(a._DynamicControlID!=b){a._DynamicControlID=b;a.raisePropertyChanged("dynamicControlID");a.raisePropertyChanged("DynamicControlID")}},set_DynamicControlID:g.set_dynamicControlID,get_dynamicContextKey:function(){return this._DynamicContextKey},get_DynamicContextKey:g.get_dynamicContextKey,set_dynamicContextKey:function(b){var a=this;if(a._DynamicContextKey!=b){a._DynamicContextKey=b;a.raisePropertyChanged("dynamicContextKey");a.raisePropertyChanged("DynamicContextKey")}},set_DynamicContextKey:g.set_dynamicContextKey,get_dynamicServicePath:function(){return this._DynamicServicePath},get_DynamicServicePath:g.get_dynamicServicePath,set_dynamicServicePath:function(b){var a=this;if(a._DynamicServicePath!=b){a._DynamicServicePath=b;a.raisePropertyChanged("dynamicServicePath");a.raisePropertyChanged("DynamicServicePath")}},set_DynamicServicePath:g.set_dynamicServicePath,get_dynamicServiceMethod:function(){return this._DynamicServiceMethod},get_DynamicServiceMethod:g.get_dynamicServiceMethod,set_dynamicServiceMethod:function(b){var a=this;if(a._DynamicServiceMethod!=b){a._DynamicServiceMethod=b;a.raisePropertyChanged("dynamicServiceMethod");a.raisePropertyChanged("DynamicServiceMethod")}},set_DynamicServiceMethod:g.set_dynamicServiceMethod,get_cacheDynamicResults:function(){return this._cacheDynamicResults},set_cacheDynamicResults:function(a){if(this._cacheDynamicResults!=a){this._cacheDynamicResults=a;this.raisePropertyChanged("cacheDynamicResults")}},add_populated:function(a){this.get_events().addHandler(e,a)},remove_populated:function(a){this.get_events().removeHandler(e,a)},raisePopulated:function(b){var a=this.get_events().getHandler(e);a&&a(this,b)},add_populating:function(a){this.get_events().addHandler(f,a)},remove_populating:function(a){this.get_events().removeHandler(f,a)},raisePopulating:function(b){var a=this.get_events().getHandler(f);a&&a(this,b)}};Sys.Extended.UI.DynamicPopulateBehaviorBase.registerClass("Sys.Extended.UI.DynamicPopulateBehaviorBase",Sys.Extended.UI.BehaviorBase);Sys.Extended.UI.ControlBase=function(c){var b=this;Sys.Extended.UI.ControlBase.initializeBase(b,[c]);b._clientStateField=a;b._callbackTarget=a;b._onsubmit$delegate=Function.createDelegate(b,b._onsubmit);b._oncomplete$delegate=Function.createDelegate(b,b._oncomplete);b._onerror$delegate=Function.createDelegate(b,b._onerror)};Sys.Extended.UI.ControlBase.__doPostBack=function(c,b){if(!Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack())for(var a=0;a<Sys.Extended.UI.ControlBase.onsubmitCollection.length;a++)Sys.Extended.UI.ControlBase.onsubmitCollection[a]();Function.createDelegate(window,Sys.Extended.UI.ControlBase.__doPostBackSaved)(c,b)};Sys.Extended.UI.ControlBase.prototype={initialize:function(){var d=this;Sys.Extended.UI.ControlBase.callBaseMethod(d,c);d._clientStateField&&d.loadClientState(d._clientStateField.value);if(typeof Sys.WebForms!==b&&typeof Sys.WebForms.PageRequestManager!==b){Array.add(Sys.WebForms.PageRequestManager.getInstance()._onSubmitStatements,d._onsubmit$delegate);if(Sys.Extended.UI.ControlBase.__doPostBackSaved==a||typeof Sys.Extended.UI.ControlBase.__doPostBackSaved==b){Sys.Extended.UI.ControlBase.__doPostBackSaved=window.__doPostBack;window.__doPostBack=Sys.Extended.UI.ControlBase.__doPostBack;Sys.Extended.UI.ControlBase.onsubmitCollection=[]}Array.add(Sys.Extended.UI.ControlBase.onsubmitCollection,d._onsubmit$delegate)}else $addHandler(document.forms[0],"submit",d._onsubmit$delegate)},dispose:function(){var a=this;if(typeof Sys.WebForms!==b&&typeof Sys.WebForms.PageRequestManager!==b){Array.remove(Sys.Extended.UI.ControlBase.onsubmitCollection,a._onsubmit$delegate);Array.remove(Sys.WebForms.PageRequestManager.getInstance()._onSubmitStatements,a._onsubmit$delegate)}else $removeHandler(document.forms[0],"submit",a._onsubmit$delegate);Sys.Extended.UI.ControlBase.callBaseMethod(a,d)},findElement:function(a){return $get(this.get_id()+"_"+a.split(":").join("_"))},get_clientStateField:function(){return this._clientStateField},set_clientStateField:function(b){var a=this;if(a.get_isInitialized())throw Error.invalidOperation(Sys.Extended.UI.Resources.ExtenderBase_CannotSetClientStateField);if(a._clientStateField!=b){a._clientStateField=b;a.raisePropertyChanged("clientStateField")}},loadClientState:function(){},saveClientState:function(){return a},_invoke:function(i,f,j){var c=this;if(!c._callbackTarget)throw Error.invalidOperation(Sys.Extended.UI.Resources.ExtenderBase_ControlNotRegisteredForCallbacks);if(typeof WebForm_DoCallback===b)throw Error.invalidOperation(Sys.Extended.UI.Resources.ExtenderBase_PageNotRegisteredForCallbacks);for(var g=[],d=0;d<f.length;d++)g[d]=f[d];var e=c.saveClientState();if(e!=a&&!String.isInstanceOfType(e))throw Error.invalidOperation(Sys.Extended.UI.Resources.ExtenderBase_InvalidClientStateType);var h=Sys.Serialization.JavaScriptSerializer.serialize({name:i,args:g,state:c.saveClientState()});WebForm_DoCallback(c._callbackTarget,h,c._oncomplete$delegate,j,c._onerror$delegate,true)},_oncomplete:function(a,b){a=Sys.Serialization.JavaScriptSerializer.deserialize(a);if(a.error)throw Error.create(a.error);this.loadClientState(a.state);b(a.result)},_onerror:function(a){throw Error.create(a);},_onsubmit:function(){if(this._clientStateField)this._clientStateField.value=this.saveClientState();return true}};Sys.Extended.UI.ControlBase.registerClass("Sys.Extended.UI.ControlBase",Sys.UI.Control)}if(window.Sys&&Sys.loader)Sys.loader.registerScript(b,["ComponentModel","Serialization"],a);else a()})();Type.registerNamespace("Sys.Extended.UI");Sys.Extended.UI.Resources={AlwaysVisible_ElementRequired:"Sys.Extended.UI.AlwaysVisibleControlBehavior must have an element",Animation_CannotNestSequence:"Sys.Extended.UI.Animation.SequenceAnimation cannot be nested inside Sys.Extended.UI.Animation.ParallelAnimation",Animation_ChildrenNotAllowed:'Sys.Extended.UI.Animation.createAnimation cannot add child animations to type "{0}" that does not derive from Sys.Extended.UI.Animation.ParentAnimation',Animation_InvalidBaseType:"Sys.Extended.UI.Animation.registerAnimation can only register types that inherit from Sys.Extended.UI.Animation.Animation",Animation_InvalidColor:'Color must be a 7-character hex representation (e.g. #246ACF), not "{0}"',Animation_MissingAnimationName:"Sys.Extended.UI.Animation.createAnimation requires an object with an AnimationName property",Animation_NoDynamicPropertyFound:'Sys.Extended.UI.Animation.createAnimation found no property corresponding to "{0}" or "{1}"',Animation_NoPropertyFound:'Sys.Extended.UI.Animation.createAnimation found no property corresponding to "{0}"',Animation_TargetNotFound:'Sys.Extended.UI.Animation.Animation.set_animationTarget requires the ID of a Sys.UI.DomElement or Sys.UI.Control.  No element or control could be found corresponding to "{0}"',Animation_UknownAnimationName:'Sys.Extended.UI.Animation.createAnimation could not find an Animation corresponding to the name "{0}"',Calendar_Today:"היום: {0}",CascadingDropDown_MethodError:"[Method error {0}]",CascadingDropDown_MethodTimeout:"[Method timeout]",CascadingDropDown_NoParentElement:'Failed to find parent element "{0}"',CollapsiblePanel_NoControlID:'Failed to find element "{0}"',Common_DateTime_InvalidFormat:"Invalid format",Common_DateTime_InvalidTimeSpan:'"{0}" is not a valid TimeSpan format',Common_InvalidBorderWidthUnit:'A unit type of "{0}"\' is invalid for parseBorderWidth',Common_InvalidPaddingUnit:'A unit type of "{0}" is invalid for parsePadding',Common_UnitHasNoDigits:"No digits",DynamicPopulate_WebServiceError:"Web Service call failed: {0}",DynamicPopulate_WebServiceTimeout:"Web service call timed out",ExtenderBase_CannotSetClientStateField:"clientStateField can only be set before initialization",ExtenderBase_ControlNotRegisteredForCallbacks:"This Control has not been registered for callbacks",ExtenderBase_InvalidClientStateType:"saveClientState must return a value of type String",ExtenderBase_PageNotRegisteredForCallbacks:"This Page has not been registered for callbacks",HTMLEditor_toolbar_button_FixedBackColor_title:"Background color",HTMLEditor_toolbar_button_BackColorClear_title:"Clear background color",HTMLEditor_toolbar_button_Bold_title:"Bold",HTMLEditor_toolbar_button_BulletedList_title:"Bulleted List",HTMLEditor_toolbar_button_Copy_title:"Copy",HTMLEditor_toolbar_button_Cut_title:"Cut",HTMLEditor_toolbar_button_DecreaseIndent_title:"Decrease Indent",HTMLEditor_toolbar_button_FontName_defaultValue:"default",HTMLEditor_toolbar_button_FontSize_defaultValue:"default",HTMLEditor_toolbar_button_DesignMode_title:"Design mode",HTMLEditor_toolbar_button_FontName_label:"Font",HTMLEditor_toolbar_button_FixedForeColor_title:"Foreground color",HTMLEditor_toolbar_button_ForeColorClear_title:"Clear foreground color",HTMLEditor_toolbar_button_HtmlMode_title:"HTML text",HTMLEditor_toolbar_button_IncreaseIndent_title:"Increase Indent",HTMLEditor_toolbar_button_InsertHR_title:"Insert horizontal rule",HTMLEditor_toolbar_button_InsertLink_title:"Insert/Edit URL link",HTMLEditor_toolbar_button_InsertLink_message_EmptyURL:"URL can not be empty",HTMLEditor_toolbar_button_Italic_title:"Italic",HTMLEditor_toolbar_button_JustifyCenter_title:"Justify Center",HTMLEditor_toolbar_button_JustifyFull_title:"Justify",HTMLEditor_toolbar_button_JustifyLeft_title:"Justify Left",HTMLEditor_toolbar_button_JustifyRight_title:"Justify Right",HTMLEditor_toolbar_button_Ltr_title:"Left to right direction",HTMLEditor_toolbar_button_OnPasteFromMSWord:"Pasting from MS Word is switched on. Just now: {0}",HTMLEditor_toolbar_button_OnPastePlainText:"Plain text pasting is switched on. Just now: {0}",HTMLEditor_toolbar_button_OrderedList_title:"Ordered List",HTMLEditor_toolbar_button_Paragraph_title:"Make Paragraph",HTMLEditor_toolbar_button_Paste_title:"Paste",HTMLEditor_toolbar_button_PasteText_title:"Paste Plain Text",HTMLEditor_toolbar_button_PasteWord_title:"Paste from MS Word (with cleanup)",HTMLEditor_toolbar_popup_LinkProperties_button_Cancel:"Cancel",HTMLEditor_toolbar_popup_LinkProperties_button_OK:"OK",HTMLEditor_toolbar_popup_LinkProperties_field_URL:"URL",HTMLEditor_toolbar_popup_LinkProperties_field_Target:"Target",HTMLEditor_toolbar_popup_LinkProperties_field_Target_New:"New window",HTMLEditor_toolbar_popup_LinkProperties_field_Target_Current:"Current window",HTMLEditor_toolbar_popup_LinkProperties_field_Target_Parent:"Parent window",HTMLEditor_toolbar_popup_LinkProperties_field_Target_Top:"Top window",HTMLEditor_toolbar_button_PreviewMode_title:"Preview",HTMLEditor_toolbar_button_Redo_title:"Redo",HTMLEditor_toolbar_button_RemoveAlignment_title:"Remove Alignment",HTMLEditor_toolbar_button_RemoveLink_title:"Remove Link",HTMLEditor_toolbar_button_RemoveStyles_title:"Remove styles",HTMLEditor_toolbar_button_Rtl_title:"Right to left direction",HTMLEditor_toolbar_button_FontSize_label:"Size",HTMLEditor_toolbar_button_StrikeThrough_title:"Strike through",HTMLEditor_toolbar_button_SubScript_title:"Sub script",HTMLEditor_toolbar_button_SuperScript_title:"Super script",HTMLEditor_toolbar_button_Underline_title:"Underline",HTMLEditor_toolbar_button_Undo_title:"Undo",HTMLEditor_toolbar_button_Use_verb:"Use {0}",ListSearch_DefaultPrompt:"Type to search",PasswordStrength_DefaultStrengthDescriptions:"NonExistent;Very Weak;Weak;Poor;Almost OK;Barely Acceptable;Average;Good;Strong;Excellent;Unbreakable!",PasswordStrength_GetHelpRequirements:"Get help on password requirements",PasswordStrength_InvalidStrengthDescriptions:"Invalid number of text strength descriptions specified",PasswordStrength_InvalidStrengthDescriptionStyles:"Text Strength description style classes must match the number of text descriptions.",PasswordStrength_InvalidWeightingRatios:"Strength Weighting ratios must have 4 elements",PasswordStrength_RemainingCharacters:"{0} more characters",PasswordStrength_RemainingLowerCase:"{0} more lower case characters",PasswordStrength_RemainingMixedCase:"Mixed case characters",PasswordStrength_RemainingNumbers:"{0} more numbers",PasswordStrength_RemainingSymbols:"{0} symbol characters",PasswordStrength_RemainingUpperCase:"{0} more upper case characters",PasswordStrength_Satisfied:"Nothing more required",PasswordStrength_StrengthPrompt:"Strength: ",PopupControl_NoDefaultProperty:'No default property supported for control "{0}" of type "{1}"',PopupExtender_NoParentElement:'Couldn\'t find parent element "{0}"',Rating_CallbackError:"An unhandled exception has occurred:\\r\\n{0}",ReorderList_DropWatcherBehavior_CallbackError:"Reorder failed, see details below.\\r\\n\\r\\n{0}",ReorderList_DropWatcherBehavior_NoChild:'Could not find child of list with id "{0}"',ResizableControlBehavior_CannotChangeProperty:"Changes to {0} not supported",ResizableControlBehavior_InvalidHandler:"{0} handler not a function, function name, or function text",RTE_BackgroundColor:"Background Color",RTE_BarColor:"Bar Color",RTE_Bold:"Bold",RTE_Border:"Border",RTE_BorderColor:"Border Color",RTE_Cancel:"Cancel",RTE_CellColor:"Cell Color",RTE_CellPadding:"Cell Padding",RTE_CellSpacing:"Cell Spacing",RTE_ClearFormatting:"Clear Formatting",RTE_Columns:"Columns",RTE_Copy:"Copy",RTE_Create:"Create",RTE_Cut:"Cut",RTE_Font:"Font",RTE_FontColor:"Font Color",RTE_Heading:"Heading",RTE_Hyperlink:"Hyperlink",RTE_Indent:"Indent",RTE_InsertHorizontalRule:"Insert Horizontal Rule",RTE_InsertImage:"Insert Image",RTE_InsertTable:"Insert Table",RTE_Inserttexthere:"Insert text here",RTE_Italic:"Italic",RTE_JustifyCenter:"Justify Center",RTE_JustifyFull:"Justify Full",RTE_JustifyLeft:"Justify Left",RTE_JustifyRight:"Justify Right",RTE_LabelColor:"Label Color",RTE_Labels:"Labels",RTE_Legend:"Legend",RTE_Normal:"Normal",RTE_OK:"OK",RTE_OrderedList:"Ordered List",RTE_Outdent:"Outdent",RTE_Paragraph:"Paragraph",RTE_Paste:"Paste",RTE_PreviewHTML:"Preview HTML",RTE_Redo:"Redo",RTE_Rows:"Rows",RTE_Size:"Size",RTE_Underline:"Underline",RTE_Undo:"Undo",RTE_UnorderedList:"Unordered List",RTE_Values:"Values",RTE_ViewEditor:"View Editor",RTE_ViewSource:"View Source",RTE_ViewValues:"View Values",Shared_BrowserSecurityPreventsPaste:"Your browser security settings don't permit the automatic execution of paste operations. Please use the keyboard shortcut Ctrl+V instead.",Slider_NoSizeProvided:"Please set valid values for the height and width attributes in the slider's CSS classes",Tabs_ActiveTabArgumentOutOfRange:"Argument is not a member of the tabs collection",Tabs_OwnerExpected:"owner must be set before initialize",Tabs_PropertySetAfterInitialization:"{0} cannot be changed after initialization",Tabs_PropertySetBeforeInitialization:"{0} cannot be changed before initialization",ValidatorCallout_DefaultErrorMessage:"This control is invalid",MultiHandleSlider_CssHeightWidthRequired:"You must specify a CSS width and height for all handle styles as well as the rail.",AsyncFileUpload_InternalErrorMessage:"The AsyncFileUpload control has encountered an error with the uploader in this page. Please refresh the page and try again.",AsyncFileUpload_UnhandledException:"Unhandled Exception",AsyncFileUpload_ConfirmToSeeErrorPage:"Do you want to see the response page?",AsyncFileUpload_ServerResponseError:"Server Response Error",AsyncFileUpload_UnknownServerError:"Unknown Server error",AsyncFileUpload_UploadingProblem:"The requested file uploading problem."};