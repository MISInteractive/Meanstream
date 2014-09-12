// (c) 2010 CodePlex Foundation
(function(){var b="ExtendedBase";function a(){var b="undefined",f="populating",e="populated",d="dispose",c="initialize",a=null,g=this,h=Sys.version;if(!h&&!Sys._versionChecked){Sys._versionChecked=true;throw new Error("AjaxControlToolkit requires ASP.NET Ajax 4.0 scripts. Ensure the correct version of the scripts are referenced. If you are using an ASP.NET ScriptManager, switch to the ToolkitScriptManager in AjaxControlToolkit.dll.");}Type.registerNamespace("Sys.Extended.UI");Sys.Extended.UI.BehaviorBase=function(c){var b=this;Sys.Extended.UI.BehaviorBase.initializeBase(b,[c]);b._clientStateFieldID=a;b._pageRequestManager=a;b._partialUpdateBeginRequestHandler=a;b._partialUpdateEndRequestHandler=a};Sys.Extended.UI.BehaviorBase.prototype={initialize:function(){Sys.Extended.UI.BehaviorBase.callBaseMethod(this,c)},dispose:function(){var b=this;Sys.Extended.UI.BehaviorBase.callBaseMethod(b,d);if(b._pageRequestManager){if(b._partialUpdateBeginRequestHandler){b._pageRequestManager.remove_beginRequest(b._partialUpdateBeginRequestHandler);b._partialUpdateBeginRequestHandler=a}if(b._partialUpdateEndRequestHandler){b._pageRequestManager.remove_endRequest(b._partialUpdateEndRequestHandler);b._partialUpdateEndRequestHandler=a}b._pageRequestManager=a}},get_ClientStateFieldID:function(){return this._clientStateFieldID},set_ClientStateFieldID:function(a){if(this._clientStateFieldID!=a){this._clientStateFieldID=a;this.raisePropertyChanged("ClientStateFieldID")}},get_ClientState:function(){if(this._clientStateFieldID){var b=document.getElementById(this._clientStateFieldID);if(b)return b.value}return a},set_ClientState:function(b){if(this._clientStateFieldID){var a=document.getElementById(this._clientStateFieldID);if(a)a.value=b}},registerPartialUpdateEvents:function(){var a=this;if(Sys&&Sys.WebForms&&Sys.WebForms.PageRequestManager){a._pageRequestManager=Sys.WebForms.PageRequestManager.getInstance();if(a._pageRequestManager){a._partialUpdateBeginRequestHandler=Function.createDelegate(a,a._partialUpdateBeginRequest);a._pageRequestManager.add_beginRequest(a._partialUpdateBeginRequestHandler);a._partialUpdateEndRequestHandler=Function.createDelegate(a,a._partialUpdateEndRequest);a._pageRequestManager.add_endRequest(a._partialUpdateEndRequestHandler)}}},_partialUpdateBeginRequest:function(){},_partialUpdateEndRequest:function(){}};Sys.Extended.UI.BehaviorBase.registerClass("Sys.Extended.UI.BehaviorBase",Sys.UI.Behavior);Sys.Extended.UI.DynamicPopulateBehaviorBase=function(c){var b=this;Sys.Extended.UI.DynamicPopulateBehaviorBase.initializeBase(b,[c]);b._DynamicControlID=a;b._DynamicContextKey=a;b._DynamicServicePath=a;b._DynamicServiceMethod=a;b._cacheDynamicResults=false;b._dynamicPopulateBehavior=a;b._populatingHandler=a;b._populatedHandler=a};Sys.Extended.UI.DynamicPopulateBehaviorBase.prototype={initialize:function(){var a=this;Sys.Extended.UI.DynamicPopulateBehaviorBase.callBaseMethod(a,c);a._populatingHandler=Function.createDelegate(a,a._onPopulating);a._populatedHandler=Function.createDelegate(a,a._onPopulated)},dispose:function(){var b=this;if(b._populatedHandler){b._dynamicPopulateBehavior&&b._dynamicPopulateBehavior.remove_populated(b._populatedHandler);b._populatedHandler=a}if(b._populatingHandler){b._dynamicPopulateBehavior&&b._dynamicPopulateBehavior.remove_populating(b._populatingHandler);b._populatingHandler=a}if(b._dynamicPopulateBehavior){b._dynamicPopulateBehavior.dispose();b._dynamicPopulateBehavior=a}Sys.Extended.UI.DynamicPopulateBehaviorBase.callBaseMethod(b,d)},populate:function(c){var b=this;if(b._dynamicPopulateBehavior&&b._dynamicPopulateBehavior.get_element()!=$get(b._DynamicControlID)){b._dynamicPopulateBehavior.dispose();b._dynamicPopulateBehavior=a}if(!b._dynamicPopulateBehavior&&b._DynamicControlID&&b._DynamicServiceMethod){b._dynamicPopulateBehavior=$create(Sys.Extended.UI.DynamicPopulateBehavior,{id:b.get_id()+"_DynamicPopulateBehavior",ContextKey:b._DynamicContextKey,ServicePath:b._DynamicServicePath,ServiceMethod:b._DynamicServiceMethod,cacheDynamicResults:b._cacheDynamicResults},a,a,$get(b._DynamicControlID));b._dynamicPopulateBehavior.add_populating(b._populatingHandler);b._dynamicPopulateBehavior.add_populated(b._populatedHandler)}b._dynamicPopulateBehavior&&b._dynamicPopulateBehavior.populate(c?c:b._DynamicContextKey)},_onPopulating:function(b,a){this.raisePopulating(a)},_onPopulated:function(b,a){this.raisePopulated(a)},get_dynamicControlID:function(){return this._DynamicControlID},get_DynamicControlID:g.get_dynamicControlID,set_dynamicControlID:function(b){var a=this;if(a._DynamicControlID!=b){a._DynamicControlID=b;a.raisePropertyChanged("dynamicControlID");a.raisePropertyChanged("DynamicControlID")}},set_DynamicControlID:g.set_dynamicControlID,get_dynamicContextKey:function(){return this._DynamicContextKey},get_DynamicContextKey:g.get_dynamicContextKey,set_dynamicContextKey:function(b){var a=this;if(a._DynamicContextKey!=b){a._DynamicContextKey=b;a.raisePropertyChanged("dynamicContextKey");a.raisePropertyChanged("DynamicContextKey")}},set_DynamicContextKey:g.set_dynamicContextKey,get_dynamicServicePath:function(){return this._DynamicServicePath},get_DynamicServicePath:g.get_dynamicServicePath,set_dynamicServicePath:function(b){var a=this;if(a._DynamicServicePath!=b){a._DynamicServicePath=b;a.raisePropertyChanged("dynamicServicePath");a.raisePropertyChanged("DynamicServicePath")}},set_DynamicServicePath:g.set_dynamicServicePath,get_dynamicServiceMethod:function(){return this._DynamicServiceMethod},get_DynamicServiceMethod:g.get_dynamicServiceMethod,set_dynamicServiceMethod:function(b){var a=this;if(a._DynamicServiceMethod!=b){a._DynamicServiceMethod=b;a.raisePropertyChanged("dynamicServiceMethod");a.raisePropertyChanged("DynamicServiceMethod")}},set_DynamicServiceMethod:g.set_dynamicServiceMethod,get_cacheDynamicResults:function(){return this._cacheDynamicResults},set_cacheDynamicResults:function(a){if(this._cacheDynamicResults!=a){this._cacheDynamicResults=a;this.raisePropertyChanged("cacheDynamicResults")}},add_populated:function(a){this.get_events().addHandler(e,a)},remove_populated:function(a){this.get_events().removeHandler(e,a)},raisePopulated:function(b){var a=this.get_events().getHandler(e);a&&a(this,b)},add_populating:function(a){this.get_events().addHandler(f,a)},remove_populating:function(a){this.get_events().removeHandler(f,a)},raisePopulating:function(b){var a=this.get_events().getHandler(f);a&&a(this,b)}};Sys.Extended.UI.DynamicPopulateBehaviorBase.registerClass("Sys.Extended.UI.DynamicPopulateBehaviorBase",Sys.Extended.UI.BehaviorBase);Sys.Extended.UI.ControlBase=function(c){var b=this;Sys.Extended.UI.ControlBase.initializeBase(b,[c]);b._clientStateField=a;b._callbackTarget=a;b._onsubmit$delegate=Function.createDelegate(b,b._onsubmit);b._oncomplete$delegate=Function.createDelegate(b,b._oncomplete);b._onerror$delegate=Function.createDelegate(b,b._onerror)};Sys.Extended.UI.ControlBase.__doPostBack=function(c,b){if(!Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack())for(var a=0;a<Sys.Extended.UI.ControlBase.onsubmitCollection.length;a++)Sys.Extended.UI.ControlBase.onsubmitCollection[a]();Function.createDelegate(window,Sys.Extended.UI.ControlBase.__doPostBackSaved)(c,b)};Sys.Extended.UI.ControlBase.prototype={initialize:function(){var d=this;Sys.Extended.UI.ControlBase.callBaseMethod(d,c);d._clientStateField&&d.loadClientState(d._clientStateField.value);if(typeof Sys.WebForms!==b&&typeof Sys.WebForms.PageRequestManager!==b){Array.add(Sys.WebForms.PageRequestManager.getInstance()._onSubmitStatements,d._onsubmit$delegate);if(Sys.Extended.UI.ControlBase.__doPostBackSaved==a||typeof Sys.Extended.UI.ControlBase.__doPostBackSaved==b){Sys.Extended.UI.ControlBase.__doPostBackSaved=window.__doPostBack;window.__doPostBack=Sys.Extended.UI.ControlBase.__doPostBack;Sys.Extended.UI.ControlBase.onsubmitCollection=[]}Array.add(Sys.Extended.UI.ControlBase.onsubmitCollection,d._onsubmit$delegate)}else $addHandler(document.forms[0],"submit",d._onsubmit$delegate)},dispose:function(){var a=this;if(typeof Sys.WebForms!==b&&typeof Sys.WebForms.PageRequestManager!==b){Array.remove(Sys.Extended.UI.ControlBase.onsubmitCollection,a._onsubmit$delegate);Array.remove(Sys.WebForms.PageRequestManager.getInstance()._onSubmitStatements,a._onsubmit$delegate)}else $removeHandler(document.forms[0],"submit",a._onsubmit$delegate);Sys.Extended.UI.ControlBase.callBaseMethod(a,d)},findElement:function(a){return $get(this.get_id()+"_"+a.split(":").join("_"))},get_clientStateField:function(){return this._clientStateField},set_clientStateField:function(b){var a=this;if(a.get_isInitialized())throw Error.invalidOperation(Sys.Extended.UI.Resources.ExtenderBase_CannotSetClientStateField);if(a._clientStateField!=b){a._clientStateField=b;a.raisePropertyChanged("clientStateField")}},loadClientState:function(){},saveClientState:function(){return a},_invoke:function(i,f,j){var c=this;if(!c._callbackTarget)throw Error.invalidOperation(Sys.Extended.UI.Resources.ExtenderBase_ControlNotRegisteredForCallbacks);if(typeof WebForm_DoCallback===b)throw Error.invalidOperation(Sys.Extended.UI.Resources.ExtenderBase_PageNotRegisteredForCallbacks);for(var g=[],d=0;d<f.length;d++)g[d]=f[d];var e=c.saveClientState();if(e!=a&&!String.isInstanceOfType(e))throw Error.invalidOperation(Sys.Extended.UI.Resources.ExtenderBase_InvalidClientStateType);var h=Sys.Serialization.JavaScriptSerializer.serialize({name:i,args:g,state:c.saveClientState()});WebForm_DoCallback(c._callbackTarget,h,c._oncomplete$delegate,j,c._onerror$delegate,true)},_oncomplete:function(a,b){a=Sys.Serialization.JavaScriptSerializer.deserialize(a);if(a.error)throw Error.create(a.error);this.loadClientState(a.state);b(a.result)},_onerror:function(a){throw Error.create(a);},_onsubmit:function(){if(this._clientStateField)this._clientStateField.value=this.saveClientState();return true}};Sys.Extended.UI.ControlBase.registerClass("Sys.Extended.UI.ControlBase",Sys.UI.Control)}if(window.Sys&&Sys.loader)Sys.loader.registerScript(b,["ComponentModel","Serialization"],a);else a()})();Type.registerNamespace("Sys.Extended.UI");Sys.Extended.UI.Resources={AlwaysVisible_ElementRequired:"必须为 Sys.Extended.UI.AlwaysVisibleControlBehavior 指定一个元素",Animation_CannotNestSequence:"Sys.Extended.UI.Animation.ParallelAnimation 不能内含 Sys.Extended.UI.Animation.SequenceAnimation",Animation_ChildrenNotAllowed:"Sys.Extended.UI.Animation.createAnimation 无法加入一个不是派生自 Sys.Extended.UI.Animation.ParentAnimation 且类型为 {0} 的子动画",Animation_InvalidBaseType:"Sys.Extended.UI.Animation.registerAnimation 只能注册那些继承自 Sys.Extended.UI.Animation.Animation 的类型",Animation_InvalidColor:"标记名称 Color 必须是 7 个字符的 16 进位字符串（例如：#246ACF），不能是 {0}",Animation_MissingAnimationName:"Sys.Extended.UI.Animation.createAnimation 必须持有一个 AnimationName 属性的对象",Animation_NoDynamicPropertyFound:"Sys.Extended.UI.Animation.createAnimation 找不到相对应的  {0} 或 {1} 属性",Animation_NoPropertyFound:"Sys.Extended.UI.Animation.createAnimation 找不到相对应的 {0} 属性",Animation_TargetNotFound:"Sys.Extended.UI.Animation.Animation.set_animationTarget 需要一个 Sys.UI.DomElement 或 Sys.UI.Control 类的控件  ID。找不到相对应的  {0} 之元素或控件",Animation_UknownAnimationName:"Sys.Extended.UI.Animation.createAnimation 找不到名称为 {0} 的动画",Calendar_Today:"今天:  {0}",CascadingDropDown_MethodError:"[方法错误 {0}]",CascadingDropDown_MethodTimeout:"[方法逾时]",CascadingDropDown_NoParentElement:"无法找到父元素 {0}",CollapsiblePanel_NoControlID:"无法找到元素 {0}",Common_DateTime_InvalidFormat:"格式无效",Common_DateTime_InvalidTimeSpan:"{0} 的 TimeSpan 格式无效",Common_InvalidBorderWidthUnit:"单位类型 {0} 对 parseBorderWidth 而言无效",Common_InvalidPaddingUnit:"单位类型 {0} 对 parsePadding 而言无效",Common_UnitHasNoDigits:"没有数字",DynamicPopulate_WebServiceError:"无法调用 Web 服务：{0}",DynamicPopulate_WebServiceTimeout:"调用 Web 服务超时",ExtenderBase_CannotSetClientStateField:"只能在初始化之前设定 clientStateField",ExtenderBase_ControlNotRegisteredForCallbacks:"这个控件尚未注册，无法提供回调",ExtenderBase_InvalidClientStateType:"saveClientState 必须返回 String 类型的值",ExtenderBase_PageNotRegisteredForCallbacks:"这个页面尚未注册，无法提供回调",HTMLEditor_toolbar_button_FixedBackColor_title:"Background color",HTMLEditor_toolbar_button_BackColorClear_title:"Clear background color",HTMLEditor_toolbar_button_Bold_title:"Bold",HTMLEditor_toolbar_button_BulletedList_title:"Bulleted List",HTMLEditor_toolbar_button_Copy_title:"Copy",HTMLEditor_toolbar_button_Cut_title:"Cut",HTMLEditor_toolbar_button_DecreaseIndent_title:"Decrease Indent",HTMLEditor_toolbar_button_FontName_defaultValue:"default",HTMLEditor_toolbar_button_FontSize_defaultValue:"default",HTMLEditor_toolbar_button_DesignMode_title:"Design mode",HTMLEditor_toolbar_button_FontName_label:"Font",HTMLEditor_toolbar_button_FixedForeColor_title:"Foreground color",HTMLEditor_toolbar_button_ForeColorClear_title:"Clear foreground color",HTMLEditor_toolbar_button_HtmlMode_title:"HTML text",HTMLEditor_toolbar_button_IncreaseIndent_title:"Increase Indent",HTMLEditor_toolbar_button_InsertHR_title:"Insert horizontal rule",HTMLEditor_toolbar_button_InsertLink_title:"Insert/Edit URL link",HTMLEditor_toolbar_button_InsertLink_message_EmptyURL:"URL can not be empty",HTMLEditor_toolbar_button_Italic_title:"Italic",HTMLEditor_toolbar_button_JustifyCenter_title:"Justify Center",HTMLEditor_toolbar_button_JustifyFull_title:"Justify",HTMLEditor_toolbar_button_JustifyLeft_title:"Justify Left",HTMLEditor_toolbar_button_JustifyRight_title:"Justify Right",HTMLEditor_toolbar_button_Ltr_title:"Left to right direction",HTMLEditor_toolbar_button_OnPasteFromMSWord:"Pasting from MS Word is switched on. Just now: {0}",HTMLEditor_toolbar_button_OnPastePlainText:"Plain text pasting is switched on. Just now: {0}",HTMLEditor_toolbar_button_OrderedList_title:"Ordered List",HTMLEditor_toolbar_button_Paragraph_title:"Make Paragraph",HTMLEditor_toolbar_button_Paste_title:"Paste",HTMLEditor_toolbar_button_PasteText_title:"Paste Plain Text",HTMLEditor_toolbar_button_PasteWord_title:"Paste from MS Word (with cleanup)",HTMLEditor_toolbar_popup_LinkProperties_button_Cancel:"Cancel",HTMLEditor_toolbar_popup_LinkProperties_button_OK:"OK",HTMLEditor_toolbar_popup_LinkProperties_field_URL:"URL",HTMLEditor_toolbar_popup_LinkProperties_field_Target:"Target",HTMLEditor_toolbar_popup_LinkProperties_field_Target_New:"New window",HTMLEditor_toolbar_popup_LinkProperties_field_Target_Current:"Current window",HTMLEditor_toolbar_popup_LinkProperties_field_Target_Parent:"Parent window",HTMLEditor_toolbar_popup_LinkProperties_field_Target_Top:"Top window",HTMLEditor_toolbar_button_PreviewMode_title:"Preview",HTMLEditor_toolbar_button_Redo_title:"Redo",HTMLEditor_toolbar_button_RemoveAlignment_title:"Remove Alignment",HTMLEditor_toolbar_button_RemoveLink_title:"Remove Link",HTMLEditor_toolbar_button_RemoveStyles_title:"Remove styles",HTMLEditor_toolbar_button_Rtl_title:"Right to left direction",HTMLEditor_toolbar_button_FontSize_label:"Size",HTMLEditor_toolbar_button_StrikeThrough_title:"Strike through",HTMLEditor_toolbar_button_SubScript_title:"Sub script",HTMLEditor_toolbar_button_SuperScript_title:"Super script",HTMLEditor_toolbar_button_Underline_title:"Underline",HTMLEditor_toolbar_button_Undo_title:"Undo",HTMLEditor_toolbar_button_Use_verb:"Use {0}",ListSearch_DefaultPrompt:"请键入以便搜寻",PasswordStrength_DefaultStrengthDescriptions:"没有;很弱;弱;差;差强人意;尚可;普通;好;很好;非常好;臻于完美！",PasswordStrength_GetHelpRequirements:"取得密码强度的要求说明",PasswordStrength_InvalidStrengthDescriptions:"所指定的密码强度文字内容个数无效",PasswordStrength_InvalidStrengthDescriptionStyles:"密码强度文字说明的样式表，必须符合文字内容之个数",PasswordStrength_InvalidWeightingRatios:"密码强度的权重比例必须有 4 种",PasswordStrength_RemainingCharacters:"还需要 {0} 个字符",PasswordStrength_RemainingLowerCase:"{0} more lower case characters",PasswordStrength_RemainingMixedCase:"大小写混合",PasswordStrength_RemainingNumbers:"还需要 {0} 个数字",PasswordStrength_RemainingSymbols:"还需要 {0} 个符号",PasswordStrength_RemainingUpperCase:"{0} more upper case characters",PasswordStrength_Satisfied:"密码强度已经足够",PasswordStrength_StrengthPrompt:"强度：",PopupControl_NoDefaultProperty:"类型 {1} 的 控件 {0} 不支持预设属性",PopupExtender_NoParentElement:"无法找到父元素 {0}",Rating_CallbackError:"发生未处理的异常状况：\\r\\n{0}",ReorderList_DropWatcherBehavior_CallbackError:"无法重新排列，请参考下面的说明：\\r\\n\\r\\n{0}",ReorderList_DropWatcherBehavior_NoChild:"无法找到 ID 为 {0} 的子清单",ResizableControlBehavior_CannotChangeProperty:"不支持对 {0} 的变更",ResizableControlBehavior_InvalidHandler:"{0} 处理例程不是函数、函数名称、或是函数文字",RTE_BackgroundColor:"Background Color",RTE_BarColor:"Bar Color",RTE_Bold:"Bold",RTE_Border:"Border",RTE_BorderColor:"Border Color",RTE_Cancel:"Cancel",RTE_CellColor:"Cell Color",RTE_CellPadding:"Cell Padding",RTE_CellSpacing:"Cell Spacing",RTE_ClearFormatting:"Clear Formatting",RTE_Columns:"Columns",RTE_Copy:"Copy",RTE_Create:"Create",RTE_Cut:"Cut",RTE_Font:"Font",RTE_FontColor:"Font Color",RTE_Heading:"Heading",RTE_Hyperlink:"Hyperlink",RTE_Indent:"Indent",RTE_InsertHorizontalRule:"Insert Horizontal Rule",RTE_InsertImage:"Insert Image",RTE_InsertTable:"Insert Table",RTE_Inserttexthere:"Insert text here",RTE_Italic:"Italic",RTE_JustifyCenter:"Justify Center",RTE_JustifyFull:"Justify Full",RTE_JustifyLeft:"Justify Left",RTE_JustifyRight:"Justify Right",RTE_LabelColor:"Label Color",RTE_Labels:"Labels",RTE_Legend:"Legend",RTE_Normal:"Normal",RTE_OK:"OK",RTE_OrderedList:"Ordered List",RTE_Outdent:"Outdent",RTE_Paragraph:"Paragraph",RTE_Paste:"Paste",RTE_PreviewHTML:"Preview HTML",RTE_Redo:"Redo",RTE_Rows:"Rows",RTE_Size:"Size",RTE_Underline:"Underline",RTE_Undo:"Undo",RTE_UnorderedList:"Unordered List",RTE_Values:"Values",RTE_ViewEditor:"View Editor",RTE_ViewSource:"View Source",RTE_ViewValues:"View Values",Shared_BrowserSecurityPreventsPaste:"您的浏览器安全性设定，不允许执行自动粘贴的操作。请改用键盘快捷键 Ctrl + V。",Slider_NoSizeProvided:"请在 Slider 的 CSS Class 中，设定高度与宽度属性的有效值",Tabs_ActiveTabArgumentOutOfRange:"参数不是索引标签 (Tab) 集合的成员",Tabs_OwnerExpected:"于初始化之前，必须设定拥有者",Tabs_PropertySetAfterInitialization:"于初始化之后，无法变更 {0}",Tabs_PropertySetBeforeInitialization:"于初始化之前，无法变更 {0}",ValidatorCallout_DefaultErrorMessage:"这个控件无效",MultiHandleSlider_CssHeightWidthRequired:"You must specify a CSS width and height for all handle styles as well as the rail.",AsyncFileUpload_InternalErrorMessage:"The AsyncFileUpload control has encountered an error with the uploader in this page. Please refresh the page and try again.",AsyncFileUpload_UnhandledException:"Unhandled Exception",AsyncFileUpload_ConfirmToSeeErrorPage:"Do you want to see the response page?",AsyncFileUpload_ServerResponseError:"Server Response Error",AsyncFileUpload_UnknownServerError:"Unknown Server error",AsyncFileUpload_UploadingProblem:"The requested file uploading problem."};