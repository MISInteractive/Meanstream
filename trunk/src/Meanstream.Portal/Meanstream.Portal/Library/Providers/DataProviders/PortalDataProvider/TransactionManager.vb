#Region "Using directives"
Imports System.Data
Imports System.Data.Common

Imports System.Diagnostics
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Meanstream.Portal.Core.Services.Data

#End Region

Namespace Meanstream.Portal.Providers.PortalDataProvider
    ''' <summary>
    ''' TransactionManager is utility class that decorates a <see cref="IDbTransaction"/> instance.
    ''' </summary>
    Public Class TransactionManager
        Implements ITransactionManager

        Implements IDisposable
#Region "Fields"
        Private _database As Database

        Private _connection As DbConnection
        Private _transaction As DbTransaction

        Private _connectionString As String
        Private _invariantProviderName As String
        Private _transactionOpen As Boolean = False

        Private disposed As Boolean
        Private Shared syncRoot As New Object()

#End Region

#Region "Properties"
        ''' <summary>
        '''	Gets or sets the configuration key for database service.
        ''' </summary>
        ''' <remark>Do not change during a transaction.</remark>
        ''' <exception cref="InvalidOperationException">
        ''' If an attempt to set when the connection is currently open.
        ''' </exception>
        Public Property ConnectionString() As String Implements ITransactionManager.ConnectionString
            Get
                Return Me._connectionString
            End Get
            Set(ByVal value As String)
                'make sure transaction is open
                If Me.IsOpen Then
                    Throw New InvalidOperationException("Database cannot be changed during a transaction")
                End If

                Me._connectionString = value
                If Me._connectionString.Length > 0 AndAlso Me._invariantProviderName.Length > 0 Then
                    Me._database = New GenericDatabase(_connectionString, DbProviderFactories.GetFactory(Me._invariantProviderName))
                    Me._connection = Me._database.CreateConnection()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the name of the invariant provider.
        ''' </summary>
        ''' <value>The name of the invariant provider.</value>
        Public Property InvariantProviderName() As String Implements ITransactionManager.InvariantProviderName
            Get
                Return Me._invariantProviderName
            End Get
            Set(ByVal value As String)
                If Me.IsOpen Then
                    Throw New InvalidOperationException("Database cannot be changed during a transaction")
                End If

                Me._invariantProviderName = value
                If Me._connectionString.Length > 0 AndAlso Me._invariantProviderName.Length > 0 Then
                    Me._database = New GenericDatabase(_connectionString, DbProviderFactories.GetFactory(Me._invariantProviderName))
                    Me._connection = Me._database.CreateConnection()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets the <see cref="Database"/> instance.
        ''' </summary>
        ''' <value></value>
        Public ReadOnly Property Database() As Database Implements ITransactionManager.Database
            Get
                Return Me._database
            End Get
        End Property

        ''' <summary>
        '''	Gets the underlying <see cref="DbTransaction"/> object.
        ''' </summary>
        Public ReadOnly Property TransactionObject() As DbTransaction Implements ITransactionManager.TransactionObject
            Get
                Return Me._transaction
            End Get
        End Property

        ''' <summary>
        '''	Gets a value that indicates if a transaction is currently open and operating. 
        ''' </summary>
        ''' <value>Return true if a transaction session is currently open and operating; otherwise false.</value>
        Public ReadOnly Property IsOpen() As Boolean Implements ITransactionManager.IsOpen
            Get
                Return Me._transactionOpen
            End Get
        End Property
#End Region

#Region "Constructors"
        ''' <summary>
        '''	Initializes a new instance of the <see cref="TransactionManager"/> class.
        ''' </summary>
        Friend Sub New()
        End Sub

        ''' <summary>
        '''	Initializes a new instance of the <see cref="TransactionManager"/> class.
        ''' </summary>
        ''' <param name="connectionString">The connection string to the database.</param>
        Public Sub New(ByVal connectionString As String)
            Me.New(connectionString, "System.Data.SqlClient")
        End Sub

        ''' <summary>
        '''	Initializes a new instance of the <see cref="TransactionManager"/> class.
        ''' </summary>
        ''' <param name="connectionString">The connection string to the database.</param>
        ''' <param name="providerInvariantName">Name of the provider invariant.</param>
        Public Sub New(ByVal connectionString As String, ByVal providerInvariantName As String)
            Me._connectionString = connectionString
            Me._invariantProviderName = providerInvariantName
            Me._database = New GenericDatabase(_connectionString, DbProviderFactories.GetFactory(Me._invariantProviderName))
            Me._connection = Me._database.CreateConnection()
        End Sub
#End Region

#Region "Public methods"
        ''' <summary>
        '''	Begins a transaction.
        ''' </summary>
        ''' <remarks>The default <see cref="IsolationLevel"/> mode is ReadCommitted</remarks>
        ''' <exception cref="InvalidOperationException">If a transaction is already open.</exception>
        Public Sub BeginTransaction() Implements ITransactionManager.BeginTransaction
            BeginTransaction(IsolationLevel.ReadCommitted)
        End Sub

        ''' <summary>
        '''	Begins a transaction.
        ''' </summary>
        ''' <param name="isolationLevel">The <see cref="IsolationLevel"/> level of the transaction</param>
        ''' <exception cref="InvalidOperationException">If a transaction is already open.</exception>
        ''' <exception cref="DataException"></exception>
        ''' <exception cref="DbException"></exception>
        Public Sub BeginTransaction(ByVal isolationLevel As IsolationLevel) Implements ITransactionManager.BeginTransaction
            If IsOpen Then
                Throw New InvalidOperationException("Transaction already open.")
            End If

            'Open connection
            Try
                Me._connection.Open()
                Me._transaction = Me._connection.BeginTransaction(isolationLevel)
                Me._transactionOpen = True
            Catch generatedExceptionName As Exception
                ' in the event of an error, close the connection and destroy the transaction object.
                If Me._connection IsNot Nothing Then
                    Me._connection.Close()
                End If

                If Me._transaction IsNot Nothing Then
                    Me._transaction.Dispose()
                End If

                Me._transactionOpen = False
                Throw
            End Try
        End Sub

        ''' <summary>
        '''	Commit the transaction to the datasource.
        ''' </summary>
        ''' <exception cref="InvalidOperationException">If a transaction is not open.</exception>
        Public Sub Commit() Implements ITransactionManager.Commit
            If Not Me.IsOpen Then
                Throw New InvalidOperationException("Transaction needs to begin first.")
            End If

            Try
                ' SqlClient could throw Exception or InvalidOperationException
                Me._transaction.Commit()
            Finally
                'assuming the commit was sucessful.
                Me._connection.Close()
                Me._transaction.Dispose()
                Me._transactionOpen = False
            End Try
        End Sub

        ''' <summary>
        '''	Rollback the transaction.
        ''' </summary>
        ''' <exception cref="InvalidOperationException">If a transaction is not open.</exception>
        Public Sub Rollback() Implements ITransactionManager.Rollback
            If Not Me.IsOpen Then
                Throw New InvalidOperationException("Transaction needs to begin first.")
            End If

            Try
                ' SqlClient could throw Exception or InvalidOperationException
                Me._transaction.Rollback()
            Finally
                Me._connection.Close()
                Me._transaction.Dispose()
                Me._transactionOpen = False
            End Try
        End Sub
#End Region

#Region "IDisposable methods"
        ''' <summary>
        ''' Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            If Not disposed Then
                SyncLock syncRoot
                    disposed = True

                    If Me.IsOpen Then
                        Me.Rollback()
                    End If
                End SyncLock
            End If
        End Sub
#End Region


    End Class
End Namespace