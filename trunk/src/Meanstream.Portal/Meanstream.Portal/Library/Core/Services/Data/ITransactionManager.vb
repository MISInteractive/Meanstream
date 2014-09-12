#Region "Using directives"
Imports System.Data
Imports System.Data.Common

Imports System.Diagnostics
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
#End Region

Namespace Meanstream.Portal.Core.Services.Data
    ''' <summary>
    ''' TransactionManager interface
    ''' </summary>
    Public Interface ITransactionManager
        ''' <summary>
        ''' Begins the transaction.
        ''' </summary>
        Sub BeginTransaction()

        ''' <summary>
        ''' Begins the transaction.
        ''' </summary>
        ''' <param name="isolationLevel">The isolation level.</param>
        Sub BeginTransaction(ByVal isolationLevel As IsolationLevel)

        ''' <summary>
        ''' Commits this instance.
        ''' </summary>
        Sub Commit()

        ''' <summary>
        ''' Gets or sets the connection string.
        ''' </summary>
        ''' <value>The connection string.</value>
        Property ConnectionString() As String

        ''' <summary>
        ''' Gets the database.
        ''' </summary>
        ''' <value>The database.</value>
        ReadOnly Property Database() As Database

        ' ''' <summary>
        ' ''' Disposes this instance.
        ' ''' </summary>
        ''Sub Dispose()

        ''' <summary>
        ''' Gets or sets the name of the invariant provider.
        ''' </summary>
        ''' <value>The name of the invariant provider.</value>
        Property InvariantProviderName() As String

        ''' <summary>
        ''' Gets a value indicating whether this instance is open.
        ''' </summary>
        ''' <value><c>true</c> if this instance is open; otherwise, <c>false</c>.</value>
        ReadOnly Property IsOpen() As Boolean

        ''' <summary>
        ''' Rollbacks this instance.
        ''' </summary>
        Sub Rollback()

        ''' <summary>
        ''' Gets the transaction object.
        ''' </summary>
        ''' <value>The transaction object.</value>
        ReadOnly Property TransactionObject() As DbTransaction
    End Interface
End Namespace