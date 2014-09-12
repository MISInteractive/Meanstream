'------------------------------------------------------------------------------
' <copyright file="SqlConnectionHelper.cs" company="Microsoft">
' Copyright (c) Microsoft Corporation. All rights reserved.
' </copyright>
'------------------------------------------------------------------------------


Imports System.Web
Imports System
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Collections.Specialized
Imports System.Web.Util
Imports System.Web.Hosting
Imports System.Web.Configuration
Imports System.Security.Permissions
Imports System.IO
Imports System.Web.Management
Imports System.Threading
Imports System.Configuration.Provider
Imports System.Diagnostics
Imports System.Data
Namespace Meanstream.Portal.Providers.AspNetMembershipProvider

    ''' <devdoc>
    ''' </devdoc>
    Friend Module SqlConnectionHelper
        Sub New()
        End Sub
        Friend Const s_strUpperDataDirWithToken As String = "|DATADIRECTORY|"
        Private s_lock As New Object()

        ''' <devdoc>
        ''' </devdoc>
        Friend Function GetConnection(ByVal connectionString As String, ByVal revertImpersonation As Boolean) As SqlConnectionHolder
            Dim strTempConnection As String = connectionString.ToUpperInvariant()
            'Commented out for source code release.
            'if (strTempConnection.Contains(s_strUpperDataDirWithToken))
            ' EnsureSqlExpressDBFile( connectionString );

            Dim holder As New SqlConnectionHolder(connectionString)
            Dim closeConn As Boolean = True
            Try
                Try
                    holder.Open(Nothing, revertImpersonation)
                    closeConn = False
                Finally
                    If closeConn Then
                        holder.Close()
                        holder = Nothing
                    End If
                End Try
            Catch
                Throw
            End Try
            Return holder
        End Function

        ''' <devdoc>
        ''' </devdoc>
        Friend Function GetConnectionString(ByVal specifiedConnectionString As String, ByVal lookupConnectionString As Boolean, ByVal appLevel As Boolean) As String
            If specifiedConnectionString Is Nothing OrElse specifiedConnectionString.Length < 1 Then
                Return Nothing
            End If

            Dim connectionString As String = Nothing


            ' Step 1: Check <connectionStrings> config section for this connection string
            If lookupConnectionString Then
                Dim connObj As ConnectionStringSettings = ConfigurationManager.ConnectionStrings(specifiedConnectionString)
                If connObj IsNot Nothing Then
                    connectionString = connObj.ConnectionString
                End If

                If connectionString Is Nothing Then
                    Return Nothing
                End If
            Else
                connectionString = specifiedConnectionString
            End If

            Return connectionString
        End Function
    End Module

    '''///////////////////////////////////////////////////////////////////
    '''///////////////////////////////////////////////////////////////////
    '''///////////////////////////////////////////////////////////////////
    Friend NotInheritable Class SqlConnectionHolder
        Friend _Connection As SqlConnection
        Private _Opened As Boolean

        Friend ReadOnly Property Connection() As SqlConnection
            Get
                Return _Connection
            End Get
        End Property

        '''///////////////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////////////
        Friend Sub New(ByVal connectionString As String)
            Try
                _Connection = New SqlConnection(connectionString)
            Catch e As ArgumentException
                Throw New ArgumentException(SR.GetString(SR.SqlError_Connection_String), "connectionString", e)
            End Try
        End Sub

        '''///////////////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////////////
        Friend Sub Open(ByVal context As HttpContext, ByVal revertImpersonate As Boolean)
            If _Opened Then
                Exit Sub
            End If
            ' Already opened
            If revertImpersonate Then
                Using HostingEnvironment.Impersonate()
                    Connection.Open()
                End Using
            Else
                Connection.Open()
            End If

            ' Open worked!
            _Opened = True
        End Sub

        '''///////////////////////////////////////////////////////////////////////////
        '''///////////////////////////////////////////////////////////////////////////
        Friend Sub Close()
            If Not _Opened Then
                Exit Sub
                ' Not open!
            End If
            ' Close connection
            Connection.Close()
            _Opened = False
        End Sub
    End Class
End Namespace