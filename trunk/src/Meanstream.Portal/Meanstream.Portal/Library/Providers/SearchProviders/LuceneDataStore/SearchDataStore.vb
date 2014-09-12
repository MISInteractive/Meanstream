
Namespace Meanstream.Portal.Providers.LuceneDataStore
    Public Class SearchDataStore
        Inherits Meanstream.Portal.Core.Services.Search.DataProvider

        Public Overrides Sub Index(ByVal document As Meanstream.Portal.Core.Services.Search.Document)
            Dim workingDir As String = Me.Directory

            If workingDir.Trim = "" Then
                Throw New ApplicationException("Directory Path is required")
            End If

            If workingDir.Trim.StartsWith("~/") Then
                workingDir = System.Web.HttpContext.Current.Server.MapPath(workingDir)
            End If

            If document.Fields.Count = 0 Then
                Throw New ArgumentException("Document requires fields to index")
            End If

            Dim Analyzer As Lucene.Net.Analysis.Analyzer = New Lucene.Net.Analysis.Standard.StandardAnalyzer
            Dim Directory As Lucene.Net.Store.Directory = Lucene.Net.Store.FSDirectory.GetDirectory(workingDir)
            Dim IndexWriter As Lucene.Net.Index.IndexWriter = Nothing

            If System.IO.Directory.GetFiles(workingDir).Length > 0 Then
                IndexWriter = New Lucene.Net.Index.IndexWriter(Directory, Analyzer, False)
            Else
                IndexWriter = New Lucene.Net.Index.IndexWriter(Directory, Analyzer, True)
            End If

            IndexWriter.SetMaxFieldLength(Me.MaxFieldLength)

            Dim LuceneDocument As Lucene.Net.Documents.Document = New Lucene.Net.Documents.Document()

            For Each Field As Meanstream.Portal.Core.Services.Search.Field In document.Fields

                Dim Store As Lucene.Net.Documents.Field.Store = Lucene.Net.Documents.Field.Store.NO
                If Field.Store Then
                    Store = Lucene.Net.Documents.Field.Store.YES
                End If

                Dim Index As Lucene.Net.Documents.Field.Index = Lucene.Net.Documents.Field.Index.NO
                If Field.Index Then
                    Index = Lucene.Net.Documents.Field.Index.TOKENIZED
                End If

                LuceneDocument.Add(New Lucene.Net.Documents.Field(Field.Name, Field.Value, Lucene.Net.Documents.Field.Store.YES, Lucene.Net.Documents.Field.Index.TOKENIZED))
            Next

            IndexWriter.AddDocument(LuceneDocument)
            IndexWriter.Optimize()
            IndexWriter.Close()
            Directory.Close()
        End Sub

        Public Overrides Sub Index(ByVal documents As List(Of Meanstream.Portal.Core.Services.Search.Document))
            Dim workingDir As String = Me.Directory

            If workingDir.Trim = "" Then
                Throw New ApplicationException("Directory Path is required")
            End If

            If workingDir.Trim.StartsWith("~/") Then
                workingDir = System.Web.HttpContext.Current.Server.MapPath(workingDir)
            End If

            'we want to refresh the current index
            Me.DeleteIndex()

            Dim Analyzer As Lucene.Net.Analysis.Analyzer = New Lucene.Net.Analysis.Standard.StandardAnalyzer
            Dim Directory As Lucene.Net.Store.Directory = Lucene.Net.Store.FSDirectory.GetDirectory(workingDir)
            Dim IndexWriter As Lucene.Net.Index.IndexWriter = Nothing

            If System.IO.Directory.GetFiles(workingDir).Length > 0 Then
                IndexWriter = New Lucene.Net.Index.IndexWriter(Directory, Analyzer, False)
            Else
                IndexWriter = New Lucene.Net.Index.IndexWriter(Directory, Analyzer, True)
            End If

            IndexWriter.SetMaxFieldLength(Me.MaxFieldLength)

            For Each document As Core.Services.Search.Document In documents
                If document.Fields.Count = 0 Then
                    Continue For
                End If

                Dim LuceneDocument As Lucene.Net.Documents.Document = New Lucene.Net.Documents.Document()

                For Each Field As Meanstream.Portal.Core.Services.Search.Field In document.Fields
                    Dim Store As Lucene.Net.Documents.Field.Store = Lucene.Net.Documents.Field.Store.NO
                    If Field.Store Then
                        Store = Lucene.Net.Documents.Field.Store.YES
                    End If
                    Dim Index As Lucene.Net.Documents.Field.Index = Lucene.Net.Documents.Field.Index.NO
                    If Field.Index Then
                        Index = Lucene.Net.Documents.Field.Index.TOKENIZED
                    End If
                    LuceneDocument.Add(New Lucene.Net.Documents.Field(Field.Name, Field.Value, Lucene.Net.Documents.Field.Store.YES, Lucene.Net.Documents.Field.Index.TOKENIZED))
                Next

                IndexWriter.AddDocument(LuceneDocument)
            Next

            IndexWriter.Optimize()
            IndexWriter.Close()
            Directory.Close()
        End Sub

        Public Overrides Function Search(ByVal fieldName As String, ByVal keyword As String) As List(Of Meanstream.Portal.Core.Services.Search.Document)
            Dim workingDir As String = Me.Directory
            Dim Documents As List(Of Meanstream.Portal.Core.Services.Search.Document) = New List(Of Meanstream.Portal.Core.Services.Search.Document)

            If workingDir.Trim = "" Then
                Throw New ApplicationException("Directory Path is required")
            End If
            If workingDir.Trim.StartsWith("~/") Then
                workingDir = System.Web.HttpContext.Current.Server.MapPath(Me.Directory)

                If Not System.IO.Directory.Exists(workingDir) Then
                    Return Documents
                End If
            End If

            If fieldName.Trim = "" Then
                Throw New ArgumentException("fieldName is required")
            End If

            If keyword.Trim = "" Then
                Throw New ArgumentException("keyword is required")
            End If

            'Return all documents
            If keyword.Trim = "*" Then
                Return GetAll()
            End If

            Dim Analyzer As Lucene.Net.Analysis.Analyzer = New Lucene.Net.Analysis.Standard.StandardAnalyzer
            Dim Directory As Lucene.Net.Store.Directory = Lucene.Net.Store.FSDirectory.GetDirectory(workingDir)
            Dim IndexSearcher As Lucene.Net.Search.IndexSearcher = New Lucene.Net.Search.IndexSearcher(Directory)
            Dim QueryParser As Lucene.Net.QueryParsers.QueryParser = New Lucene.Net.QueryParsers.QueryParser(fieldName, Analyzer)

            Dim Query As Lucene.Net.Search.Query = QueryParser.Parse(keyword)

            Dim Hits As Lucene.Net.Search.Hits = IndexSearcher.Search(Query)

            If Hits IsNot Nothing And Hits.Length > 0 Then
                For Index As Integer = 0 To Hits.Length - 1
                    Dim Document As Meanstream.Portal.Core.Services.Search.Document = New Meanstream.Portal.Core.Services.Search.Document

                    Dim LuceneDocument As Lucene.Net.Documents.Document = Hits.Doc(Index)

                    For Each LuceneField As Lucene.Net.Documents.Field In LuceneDocument.GetFields
                        Document.Add(New Meanstream.Portal.Core.Services.Search.Field(LuceneField.Name, LuceneField.StringValue, LuceneField.IsStored(), LuceneField.IsIndexed()))
                    Next

                    Documents.Add(Document)

                    Index = Index + 1
                Next
            End If

            IndexSearcher.Close()
            Directory.Close()

            Return Documents
        End Function

        Public Overrides Function GetAll() As List(Of Meanstream.Portal.Core.Services.Search.Document)
            Dim workingDir As String = Me.Directory
            Dim Documents As List(Of Meanstream.Portal.Core.Services.Search.Document) = New List(Of Meanstream.Portal.Core.Services.Search.Document)

            If workingDir.Trim = "" Then
                Throw New ApplicationException("Directory Path is required")
            End If

            If workingDir.Trim.StartsWith("~/") Then
                workingDir = System.Web.HttpContext.Current.Server.MapPath(Me.Directory)

                If Not System.IO.Directory.Exists(workingDir) Then
                    Return Documents
                End If
            End If

            Dim Analyzer As Lucene.Net.Analysis.Analyzer = New Lucene.Net.Analysis.Standard.StandardAnalyzer
            Dim Directory As Lucene.Net.Store.Directory = Lucene.Net.Store.FSDirectory.GetDirectory(workingDir)
            Dim IndexReader As Lucene.Net.Index.IndexReader = Lucene.Net.Index.IndexReader.Open(Directory)

            For index As Integer = 0 To IndexReader.MaxDoc - 1
                Dim Document As Meanstream.Portal.Core.Services.Search.Document = New Meanstream.Portal.Core.Services.Search.Document
                Dim LuceneDocument As Lucene.Net.Documents.Document = IndexReader.Document(index)
                For Each LuceneField As Lucene.Net.Documents.Field In LuceneDocument.GetFields
                    Document.Add(New Meanstream.Portal.Core.Services.Search.Field(LuceneField.Name, LuceneField.StringValue, LuceneField.IsStored(), LuceneField.IsIndexed()))
                Next
                Documents.Add(Document)
            Next

            Directory.Close()
            IndexReader.Close()

            Return Documents
        End Function

        Public Overrides Sub RemoveFromIndex(ByVal url As String)
            Dim workingDir As String = Me.Directory
            If workingDir.Trim = "" Then
                Throw New ApplicationException("Directory Path is required")
            End If
            If workingDir.Trim.StartsWith("~/") Then
                workingDir = System.Web.HttpContext.Current.Server.MapPath(workingDir)
            End If
            Dim Analyzer As Lucene.Net.Analysis.Analyzer = New Lucene.Net.Analysis.Standard.StandardAnalyzer
            Dim Directory As Lucene.Net.Store.Directory = Lucene.Net.Store.FSDirectory.GetDirectory(workingDir)
            Dim IndexReader As Lucene.Net.Index.IndexReader = Lucene.Net.Index.IndexReader.Open(Directory)
            IndexReader.DeleteDocuments(New Lucene.Net.Index.Term("url", url))
            IndexReader.Close()
            Directory.Close()
        End Sub

        Public Overrides Sub DeleteIndex()
            Dim workingDir As String = Me.Directory

            If workingDir.Trim = "" Then
                Throw New ApplicationException("Directory Path is required")
            End If
            If workingDir.Trim.StartsWith("~/") Then
                workingDir = System.Web.HttpContext.Current.Server.MapPath(workingDir)
                If Not System.IO.Directory.Exists(workingDir) Then
                    Return
                End If
            End If

            Dim Analyzer As Lucene.Net.Analysis.Analyzer = New Lucene.Net.Analysis.Standard.StandardAnalyzer
            Dim Directory As Lucene.Net.Store.Directory = Lucene.Net.Store.FSDirectory.GetDirectory(workingDir)

            Dim IndexReader As Lucene.Net.Index.IndexReader = Lucene.Net.Index.IndexReader.Open(Directory)
            For Index As Integer = 0 To IndexReader.MaxDoc - 1
                IndexReader.DeleteDocument(Index)
            Next
            IndexReader.Close()

            For Each file As String In System.IO.Directory.GetFiles(workingDir)
                Directory.DeleteFile(file)
            Next

            Directory.Close()

            System.IO.Directory.Delete(workingDir)
        End Sub
    End Class
End Namespace

