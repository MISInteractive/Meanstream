<%@ Page Language="C#" Inherits="CuteEditor.EditorUtilityPage" %>
<script runat="server">
string GetDialogQueryString;
override protected void OnInit(EventArgs args)
{
	if(Context.Request.QueryString["Dialog"]=="Standard")
	{	
	if(Context.Request.QueryString["IsFrame"]==null)
	{
		string FrameSrc="colorpicker_basic.aspx?IsFrame=1&"+Request.ServerVariables["QUERY_STRING"];
		CuteEditor.CEU.WriteDialogOuterFrame(Context,"[[MoreColors]]",FrameSrc);
		Context.Response.End();
	}
	}
	string s="";
	if(Context.Request.QueryString["Dialog"]=="Standard")	
		s="&Dialog=Standard";
	
	GetDialogQueryString="Theme="+Context.Request.QueryString["Theme"]+s;	
	base.OnInit(args);
}
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server">
		<meta http-equiv="Page-Enter" content="blendTrans(Duration=0.1)" />
		<meta http-equiv="Page-Exit" content="blendTrans(Duration=0.1)" />
		<script type="text/javascript" src="Load.ashx?type=dialogscript&verfix=1006&file=DialogHead.js"></script>
		<script type="text/javascript" src="Load.ashx?type=dialogscript&verfix=1006&file=Dialog_ColorPicker.js"></script>
		<link href='Load.ashx?type=themecss&file=dialog.css&theme=[[_Theme_]]' type="text/css"
			rel="stylesheet" />
		<style type="text/css">
			.colorcell
			{
				width:16px;
				height:17px;
				cursor:hand;
			}
			.colordiv,.customdiv
			{
				border:solid 1px #808080;
				width:16px;
				height:17px;
				font-size:1px;
			}
			#ajaxdiv{padding:10px;margin:0;text-align:center; background:#eeeeee;}
		</style>
		<title>[[NamedColors]]</title>
		<script>
								
		var OxObd2d=["Green","#008000","Lime","#00FF00","Teal","#008080","Aqua","#00FFFF","Navy","#000080","Blue","#0000FF","Purple","#800080","Fuchsia","#FF00FF","Maroon","#800000","Red","#FF0000","Olive","#808000","Yellow","#FFFF00","White","#FFFFFF","Silver","#C0C0C0","Gray","#808080","Black","#000000","DarkOliveGreen","#556B2F","DarkGreen","#006400","DarkSlateGray","#2F4F4F","SlateGray","#708090","DarkBlue","#00008B","MidnightBlue","#191970","Indigo","#4B0082","DarkMagenta","#8B008B","Brown","#A52A2A","DarkRed","#8B0000","Sienna","#A0522D","SaddleBrown","#8B4513","DarkGoldenrod","#B8860B","Beige","#F5F5DC","HoneyDew","#F0FFF0","DimGray","#696969","OliveDrab","#6B8E23","ForestGreen","#228B22","DarkCyan","#008B8B","LightSlateGray","#778899","MediumBlue","#0000CD","DarkSlateBlue","#483D8B","DarkViolet","#9400D3","MediumVioletRed","#C71585","IndianRed","#CD5C5C","Firebrick","#B22222","Chocolate","#D2691E","Peru","#CD853F","Goldenrod","#DAA520","LightGoldenrodYellow","#FAFAD2","MintCream","#F5FFFA","DarkGray","#A9A9A9","YellowGreen","#9ACD32","SeaGreen","#2E8B57","CadetBlue","#5F9EA0","SteelBlue","#4682B4","RoyalBlue","#4169E1","BlueViolet","#8A2BE2","DarkOrchid","#9932CC","DeepPink","#FF1493","RosyBrown","#BC8F8F","Crimson","#DC143C","DarkOrange","#FF8C00","BurlyWood","#DEB887","DarkKhaki","#BDB76B","LightYellow","#FFFFE0","Azure","#F0FFFF","LightGray","#D3D3D3","LawnGreen","#7CFC00","MediumSeaGreen","#3CB371","LightSeaGreen","#20B2AA","DeepSkyBlue","#00BFFF","DodgerBlue","#1E90FF","SlateBlue","#6A5ACD","MediumOrchid","#BA55D3","PaleVioletRed","#DB7093","Salmon","#FA8072","OrangeRed","#FF4500","SandyBrown","#F4A460","Tan","#D2B48C","Gold","#FFD700","Ivory","#FFFFF0","GhostWhite","#F8F8FF","Gainsboro","#DCDCDC","Chartreuse","#7FFF00","LimeGreen","#32CD32","MediumAquamarine","#66CDAA","DarkTurquoise","#00CED1","CornflowerBlue","#6495ED","MediumSlateBlue","#7B68EE","Orchid","#DA70D6","HotPink","#FF69B4","LightCoral","#F08080","Tomato","#FF6347","Orange","#FFA500","Bisque","#FFE4C4","Khaki","#F0E68C","Cornsilk","#FFF8DC","Linen","#FAF0E6","WhiteSmoke","#F5F5F5","GreenYellow","#ADFF2F","DarkSeaGreen","#8FBC8B","Turquoise","#40E0D0","MediumTurquoise","#48D1CC","SkyBlue","#87CEEB","MediumPurple","#9370DB","Violet","#EE82EE","LightPink","#FFB6C1","DarkSalmon","#E9967A","Coral","#FF7F50","NavajoWhite","#FFDEAD","BlanchedAlmond","#FFEBCD","PaleGoldenrod","#EEE8AA","Oldlace","#FDF5E6","Seashell","#FFF5EE","PaleGreen","#98FB98","SpringGreen","#00FF7F","Aquamarine","#7FFFD4","PowderBlue","#B0E0E6","LightSkyBlue","#87CEFA","LightSteelBlue","#B0C4DE","Plum","#DDA0DD","Pink","#FFC0CB","LightSalmon","#FFA07A","Wheat","#F5DEB3","Moccasin","#FFE4B5","AntiqueWhite","#FAEBD7","LemonChiffon","#FFFACD","FloralWhite","#FFFAF0","Snow","#FFFAFA","AliceBlue","#F0F8FF","LightGreen","#90EE90","MediumSpringGreen","#00FA9A","PaleTurquoise","#AFEEEE","LightCyan","#E0FFFF","LightBlue","#ADD8E6","Lavender","#E6E6FA","Thistle","#D8BFD8","MistyRose","#FFE4E1","Peachpuff","#FFDAB9","PapayaWhip","#FFEFD5"];var colorlist=[{n:OxObd2d[0],h:OxObd2d[1]},{n:OxObd2d[2],h:OxObd2d[3]},{n:OxObd2d[4],h:OxObd2d[5]},{n:OxObd2d[6],h:OxObd2d[7]},{n:OxObd2d[8],h:OxObd2d[9]},{n:OxObd2d[10],h:OxObd2d[11]},{n:OxObd2d[12],h:OxObd2d[13]},{n:OxObd2d[14],h:OxObd2d[15]},{n:OxObd2d[16],h:OxObd2d[17]},{n:OxObd2d[18],h:OxObd2d[19]},{n:OxObd2d[20],h:OxObd2d[21]},{n:OxObd2d[22],h:OxObd2d[23]},{n:OxObd2d[24],h:OxObd2d[25]},{n:OxObd2d[26],h:OxObd2d[27]},{n:OxObd2d[28],h:OxObd2d[29]},{n:OxObd2d[30],h:OxObd2d[31]}];var colormore=[{n:OxObd2d[32],h:OxObd2d[33]},{n:OxObd2d[34],h:OxObd2d[35]},{n:OxObd2d[36],h:OxObd2d[37]},{n:OxObd2d[38],h:OxObd2d[39]},{n:OxObd2d[40],h:OxObd2d[41]},{n:OxObd2d[42],h:OxObd2d[43]},{n:OxObd2d[44],h:OxObd2d[45]},{n:OxObd2d[46],h:OxObd2d[47]},{n:OxObd2d[48],h:OxObd2d[49]},{n:OxObd2d[50],h:OxObd2d[51]},{n:OxObd2d[52],h:OxObd2d[53]},{n:OxObd2d[54],h:OxObd2d[55]},{n:OxObd2d[56],h:OxObd2d[57]},{n:OxObd2d[58],h:OxObd2d[59]},{n:OxObd2d[60],h:OxObd2d[61]},{n:OxObd2d[62],h:OxObd2d[63]},{n:OxObd2d[64],h:OxObd2d[65]},{n:OxObd2d[66],h:OxObd2d[67]},{n:OxObd2d[68],h:OxObd2d[69]},{n:OxObd2d[70],h:OxObd2d[71]},{n:OxObd2d[72],h:OxObd2d[73]},{n:OxObd2d[74],h:OxObd2d[75]},{n:OxObd2d[76],h:OxObd2d[77]},{n:OxObd2d[78],h:OxObd2d[79]},{n:OxObd2d[80],h:OxObd2d[81]},{n:OxObd2d[82],h:OxObd2d[83]},{n:OxObd2d[84],h:OxObd2d[85]},{n:OxObd2d[86],h:OxObd2d[87]},{n:OxObd2d[88],h:OxObd2d[89]},{n:OxObd2d[90],h:OxObd2d[91]},{n:OxObd2d[92],h:OxObd2d[93]},{n:OxObd2d[94],h:OxObd2d[95]},{n:OxObd2d[96],h:OxObd2d[97]},{n:OxObd2d[98],h:OxObd2d[99]},{n:OxObd2d[100],h:OxObd2d[101]},{n:OxObd2d[102],h:OxObd2d[103]},{n:OxObd2d[104],h:OxObd2d[105]},{n:OxObd2d[106],h:OxObd2d[107]},{n:OxObd2d[108],h:OxObd2d[109]},{n:OxObd2d[110],h:OxObd2d[111]},{n:OxObd2d[112],h:OxObd2d[113]},{n:OxObd2d[114],h:OxObd2d[115]},{n:OxObd2d[116],h:OxObd2d[117]},{n:OxObd2d[118],h:OxObd2d[119]},{n:OxObd2d[120],h:OxObd2d[121]},{n:OxObd2d[122],h:OxObd2d[123]},{n:OxObd2d[124],h:OxObd2d[125]},{n:OxObd2d[126],h:OxObd2d[127]},{n:OxObd2d[128],h:OxObd2d[129]},{n:OxObd2d[130],h:OxObd2d[131]},{n:OxObd2d[132],h:OxObd2d[133]},{n:OxObd2d[134],h:OxObd2d[135]},{n:OxObd2d[136],h:OxObd2d[137]},{n:OxObd2d[138],h:OxObd2d[139]},{n:OxObd2d[140],h:OxObd2d[141]},{n:OxObd2d[142],h:OxObd2d[143]},{n:OxObd2d[144],h:OxObd2d[145]},{n:OxObd2d[146],h:OxObd2d[147]},{n:OxObd2d[148],h:OxObd2d[149]},{n:OxObd2d[150],h:OxObd2d[151]},{n:OxObd2d[152],h:OxObd2d[153]},{n:OxObd2d[154],h:OxObd2d[155]},{n:OxObd2d[156],h:OxObd2d[157]},{n:OxObd2d[158],h:OxObd2d[159]},{n:OxObd2d[160],h:OxObd2d[161]},{n:OxObd2d[162],h:OxObd2d[163]},{n:OxObd2d[164],h:OxObd2d[165]},{n:OxObd2d[166],h:OxObd2d[167]},{n:OxObd2d[168],h:OxObd2d[169]},{n:OxObd2d[170],h:OxObd2d[171]},{n:OxObd2d[172],h:OxObd2d[173]},{n:OxObd2d[174],h:OxObd2d[175]},{n:OxObd2d[176],h:OxObd2d[177]},{n:OxObd2d[178],h:OxObd2d[179]},{n:OxObd2d[180],h:OxObd2d[181]},{n:OxObd2d[182],h:OxObd2d[183]},{n:OxObd2d[184],h:OxObd2d[185]},{n:OxObd2d[186],h:OxObd2d[187]},{n:OxObd2d[188],h:OxObd2d[189]},{n:OxObd2d[190],h:OxObd2d[191]},{n:OxObd2d[192],h:OxObd2d[193]},{n:OxObd2d[194],h:OxObd2d[195]},{n:OxObd2d[196],h:OxObd2d[197]},{n:OxObd2d[198],h:OxObd2d[199]},{n:OxObd2d[200],h:OxObd2d[201]},{n:OxObd2d[202],h:OxObd2d[203]},{n:OxObd2d[204],h:OxObd2d[205]},{n:OxObd2d[206],h:OxObd2d[207]},{n:OxObd2d[208],h:OxObd2d[209]},{n:OxObd2d[210],h:OxObd2d[211]},{n:OxObd2d[212],h:OxObd2d[213]},{n:OxObd2d[214],h:OxObd2d[215]},{n:OxObd2d[216],h:OxObd2d[217]},{n:OxObd2d[218],h:OxObd2d[219]},{n:OxObd2d[220],h:OxObd2d[221]},{n:OxObd2d[156],h:OxObd2d[157]},{n:OxObd2d[222],h:OxObd2d[223]},{n:OxObd2d[224],h:OxObd2d[225]},{n:OxObd2d[226],h:OxObd2d[227]},{n:OxObd2d[228],h:OxObd2d[229]},{n:OxObd2d[230],h:OxObd2d[231]},{n:OxObd2d[232],h:OxObd2d[233]},{n:OxObd2d[234],h:OxObd2d[235]},{n:OxObd2d[236],h:OxObd2d[237]},{n:OxObd2d[238],h:OxObd2d[239]},{n:OxObd2d[240],h:OxObd2d[241]},{n:OxObd2d[242],h:OxObd2d[243]},{n:OxObd2d[244],h:OxObd2d[245]},{n:OxObd2d[246],h:OxObd2d[247]},{n:OxObd2d[248],h:OxObd2d[249]},{n:OxObd2d[250],h:OxObd2d[251]},{n:OxObd2d[252],h:OxObd2d[253]},{n:OxObd2d[254],h:OxObd2d[255]},{n:OxObd2d[256],h:OxObd2d[257]},{n:OxObd2d[258],h:OxObd2d[259]},{n:OxObd2d[260],h:OxObd2d[261]},{n:OxObd2d[262],h:OxObd2d[263]},{n:OxObd2d[264],h:OxObd2d[265]},{n:OxObd2d[266],h:OxObd2d[267]},{n:OxObd2d[268],h:OxObd2d[269]},{n:OxObd2d[270],h:OxObd2d[271]},{n:OxObd2d[272],h:OxObd2d[273]}];
		
		</script>
	</head>
	<body>
		<div id="ajaxdiv">
			<div class="tab-pane-control tab-pane" id="tabPane1">
				<div class="tab-row">
					<h2 class="tab">
						<a tabindex="-1" href='colorpicker.aspx?<%=GetDialogQueryString%>'>
							<span style="white-space:nowrap;">
								[[WebPalette]]
							</span>
						</a>
					</h2>
					<h2 class="tab selected">
							<a tabindex="-1" href='colorpicker_basic.aspx?<%=GetDialogQueryString%>'>
								<span style="white-space:nowrap;">
									[[NamedColors]]
								</span>
							</a>
					</h2>
					<h2 class="tab">
							<a tabindex="-1" href='colorpicker_more.aspx?<%=GetDialogQueryString%>'>
								<span style="white-space:nowrap;">
									[[CustomColor]]
								</span>
							</a>
					</h2>
				</div>
				<div class="tab-page">			
					<table class="colortable" align="center">
						<tr>
							<td colspan="16" height="16"><p align="left">Basic:
								</p>
							</td>
						</tr>
						<tr>
							<script>
								var OxO2e4a=["length","\x3Ctd class=\x27colorcell\x27\x3E\x3Cdiv class=\x27colordiv\x27 style=\x27background-color:","\x27 title=\x27"," ","\x27 cname=\x27","\x27 cvalue=\x27","\x27\x3E\x3C/div\x3E\x3C/td\x3E",""];var arr=[];for(var i=0;i<colorlist[OxO2e4a[0]];i++){arr.push(OxO2e4a[1]);arr.push(colorlist[i].n);arr.push(OxO2e4a[2]);arr.push(colorlist[i].n);arr.push(OxO2e4a[3]);arr.push(colorlist[i].h);arr.push(OxO2e4a[4]);arr.push(colorlist[i].n);arr.push(OxO2e4a[5]);arr.push(colorlist[i].h);arr.push(OxO2e4a[6]);} ;document.write(arr.join(OxO2e4a[7]));
							</script>
						</tr>
						<tr>
							<td colspan="16" height="12"><p align="left"></p>
							</td>
						</tr>
						<tr>
							<td colspan="16"><p align="left">Additional:
								</p>
							</td>
						</tr>
						<script>
							var OxO6110=["length","\x3Ctr\x3E","\x3Ctd class=\x27colorcell\x27\x3E\x3Cdiv class=\x27colordiv\x27 style=\x27background-color:","\x27 title=\x27"," ","\x27 cname=\x27","\x27 cvalue=\x27","\x27\x3E\x3C/div\x3E\x3C/td\x3E","\x3C/tr\x3E",""];var arr=[];for(var i=0;i<colormore[OxO6110[0]];i++){if(i%16==0){arr.push(OxO6110[1]);} ;arr.push(OxO6110[2]);arr.push(colormore[i].n);arr.push(OxO6110[3]);arr.push(colormore[i].n);arr.push(OxO6110[4]);arr.push(colormore[i].h);arr.push(OxO6110[5]);arr.push(colormore[i].n);arr.push(OxO6110[6]);arr.push(colormore[i].h);arr.push(OxO6110[7]);if(i%16==15){arr.push(OxO6110[8]);} ;} ;if(colormore%16>0){arr.push(OxO6110[8]);} ;document.write(arr.join(OxO6110[9]));
						</script>
						<tr>
							<td colspan="16" height="8">
							</td>
						</tr>
						<tr>
							<td colspan="16" height="12">
								<input checked id="CheckboxColorNames" style="width: 16px; height: 20px" type="checkbox">
								<span style="width: 118px;">Use color names</span>
							</td>
						</tr>
						<tr>
							<td colspan="16" height="12">
							</td>
						</tr>
						<tr>
							<td colspan="16" valign="middle" height="24">
							<span style="height:24px;width:50px;vertical-align:middle;">Color : </span>&nbsp;
							<input type="text" id="divpreview" size="7" maxlength="7" style="width:180px;height:24px;border:#a0a0a0 1px solid; Padding:4;"/>
					
							</td>
						</tr>
				</table>
			</div>
		</div>
		<div id="container-bottom">
			<input type="button" id="buttonok" value="[[OK]]" class="formbutton" style="width:70px"	onclick="do_insert();" /> 
			&nbsp;&nbsp;&nbsp;&nbsp; 
			<input type="button" id="buttoncancel" value="[[Cancel]]" class="formbutton" style="width:70px"	onclick="do_Close();" />	
		</div>
	</div>
	</body>
</html>

