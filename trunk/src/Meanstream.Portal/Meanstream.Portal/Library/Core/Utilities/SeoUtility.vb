Imports Microsoft.VisualBasic
Imports System.Text.RegularExpressions

Namespace Meanstream.Portal.Core.Utilities

    Public Class SeoUtilility

        Public Shared Function StripStyles(ByVal Html As String) As String
            ' start by completely removing all unwanted tags 
            Html = Regex.Replace(Html, "<[/]?(font|span|xml|del|ins|[ovwxp]:\w+)[^>]*?>", "", RegexOptions.IgnoreCase)
            ' then run another pass over the html (twice), removing unwanted attributes 
            Html = Regex.Replace(Html, "<([^>]*)(?:class|lang|style|size|face|[ovwxp]:\w+)=(?:'[^']*'|""[^""]*""|[^\s>]+)([^>]*)>", "<$1$2>", RegexOptions.IgnoreCase)
            Html = Regex.Replace(Html, "<([^>]*)(?:class|lang|style|size|face|[ovwxp]:\w+)=(?:'[^']*'|""[^""]*""|[^\s>]+)([^>]*)>", "<$1$2>", RegexOptions.IgnoreCase)
            Return Html
        End Function

        Public Shared Function StripHtml(ByVal Phrase As String) As String
            Return System.Text.RegularExpressions.Regex.Replace(Phrase, "<[^>]*>", " ")
        End Function

        Public Shared Function FormatFilename(ByVal Phrase As String) As String
            Phrase = StripHtml(Phrase)
            Dim VoidedChar As String = ":;~!@#$%^*<()+=`',.?>/\\\"
            Dim ret As String() = Phrase.Split(VoidedChar.ToCharArray)
            Phrase = String.Concat(ret)
            Phrase = Phrase.Replace("&", "")
            Phrase = Phrase.Replace("""", "")
            Phrase = Phrase.Replace(" ", "-")
            Return Phrase.Replace("--", "-")
        End Function

        Public Shared Function FormatForURL(ByVal Phrase As String) As String
            Phrase = Phrase.Replace("&", "&amp;")
            Return Phrase
        End Function

        Public Shared Function FormatForMeta(ByVal Phrase As String) As String
            Phrase = Phrase.Replace("&", "")
            Phrase = Phrase.Replace("&amp;", "")
            Phrase = Phrase.Replace("&amp;amp;", "")
            Phrase = Phrase.Replace("amp;", "")
            Return Phrase
        End Function

    End Class

End Namespace