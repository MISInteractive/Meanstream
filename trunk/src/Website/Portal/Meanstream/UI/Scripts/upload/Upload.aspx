<%@ Page Language="VB" %>
<html><head runat=server></head>

<%
    
    Dim postedFile As HttpPostedFile = Context.Request.Files("Filedata")

    Dim savepath As String = ""
    Dim tempPath As String = ""

    tempPath = Context.Request("folder")
    savepath = Context.Server.MapPath(tempPath)
    Dim filename As String = postedFile.FileName
    If Not System.IO.Directory.Exists(savepath) Then
        System.IO.Directory.CreateDirectory(savepath)
    End If

    postedFile.SaveAs((savepath & "\") + filename)
    Context.Response.Write((tempPath & "/") + filename)
    Context.Response.StatusCode = 200
    

%>
</html>