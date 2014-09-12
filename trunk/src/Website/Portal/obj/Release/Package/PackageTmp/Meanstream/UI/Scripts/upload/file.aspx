<%@ Page Language="VB" %>



<%
    Dim count As Integer = Context.Request.Files.Count
    count = count - 1
    Dim i As Integer = 0
    For i = 0 To count
        Dim file As HttpPostedFile = Context.Request.Files(i)
        Dim fileName As String = System.IO.Path.GetFileName(file.FileName)
        Dim physicalPath As String = System.IO.Path.Combine(Server.MapPath("~/Portals/tmp"), fileName)

        file.SaveAs(physicalPath)
    Next
   
    Context.Response.Write("")
   
    

%>
