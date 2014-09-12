Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection

Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql

Imports Meanstream.Portal.Core.Services.Data

Namespace Meanstream.Portal.Providers.PortalDataProvider

    Public Class DataProvider
        Inherits Meanstream.Portal.Core.Services.Data.DataProvider

        Public Overrides Function GetById(Of T As {IEntity, New})(ByVal id As Guid) As List(Of T)

            Dim dataItem As T
            Dim listItems As List(Of T) = New List(Of T)

            Dim ds As DataSet = Me.GetById(id)
            If ds.Tables.Count = 0 Then
                Return listItems
            End If

            Dim dbReader As IDataReader = ds.Tables(0).CreateDataReader

            Try
                If Not IsNothing(dbReader) Then
                    While dbReader.Read()
                        dataItem = New T
                        dataItem.Fill(dbReader)
                        listItems.Add(dataItem)
                    End While
                End If
            Catch e As Exception
                Throw New DataProviderException(e.Message)
            Finally
                dbReader.Close()
            End Try

            Return listItems
        End Function

        Public Overrides Function GetById(ByVal id As Guid) As DataSet

            Dim ds As DataSet = Nothing
            Dim commandText = "meanstream_dynamics_GetItemById"
            Dim parameterNames As String() = {"@Id"}
            Dim parameterValues As Object() = {id}

            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbCommand As Common.DbCommand = db.GetStoredProcCommand(commandText)
            Dim param As System.Data.SqlClient.SqlParameter

            If Not IsNothing(parameterNames) And parameterNames.Length > 0 Then
                For i As Integer = 0 To parameterNames.Count - 1
                    param = New System.Data.SqlClient.SqlParameter
                    param.ParameterName = parameterNames(i)
                    param.Direction = ParameterDirection.Input
                    param.Value = parameterValues(i)
                    dbCommand.Parameters.Add(param)
                Next
            End If

            Try
                ds = db.ExecuteDataSet(dbCommand)
            Catch e As Exception
                Throw New DataProviderException(e.Message)
            Finally
                dbCommand.Connection.Close()
            End Try

            Return ds
        End Function

        Public Overrides Function GetKeyValuesById(ByVal id As Guid) As List(Of Dictionary(Of String, Object))

            Dim dataItem As Dictionary(Of String, Object)
            Dim listItems As List(Of Dictionary(Of String, Object)) = New List(Of Dictionary(Of String, Object))
            Try
                Dim ds As DataSet = Me.GetById(id)
                If ds.Tables.Count = 0 Then
                    Return listItems
                End If

                Dim dt As DataTable = ds.Tables(0)
                Dim columns As Integer = dt.Columns.Count - 1
                Dim index As Integer = 0
                For Each row As DataRow In dt.Rows
                    dataItem = New Dictionary(Of String, Object)
                    For index = 0 To columns
                        dataItem.Add(dt.Columns(index).ColumnName, row(dt.Columns(index).ColumnName))
                    Next
                    listItems.Add(dataItem)
                Next
            Catch e As Exception
                Throw New DataProviderException(e.Message)
            Finally

            End Try

            Return listItems
        End Function

        Public Overrides Function Find(Of T As {IEntity, New})(ByVal type As String, ByVal whereClause As String) As List(Of T)

            Dim dataItem As T
            Dim listItems As List(Of T) = New List(Of T)

            Dim ds As DataSet = Me.Find(type, whereClause)
            If ds.Tables.Count = 0 Then
                Return listItems
            End If

            Dim dbReader As IDataReader = ds.Tables(0).CreateDataReader

            Try
                If Not IsNothing(dbReader) Then
                    While dbReader.Read()
                        dataItem = New T
                        dataItem.Fill(dbReader)
                        listItems.Add(dataItem)
                    End While
                End If
            Catch e As Exception
                Throw New DataProviderException(e.Message)
            Finally
                dbReader.Close()
            End Try

            Return listItems
        End Function

        Public Overrides Function GetKeyValues(ByVal type As String, ByVal whereClause As String) As List(Of Dictionary(Of String, Object))

            Dim dataItem As Dictionary(Of String, Object)
            Dim listItems As List(Of Dictionary(Of String, Object)) = New List(Of Dictionary(Of String, Object))
            Try
                Dim ds As DataSet = Me.Find(type, whereClause)
                If ds.Tables.Count = 0 Then
                    Return listItems
                End If

                Dim dt As DataTable = ds.Tables(0)
                Dim columns As Integer = dt.Columns.Count - 1
                Dim index As Integer = 0
                For Each row As DataRow In dt.Rows
                    dataItem = New Dictionary(Of String, Object)
                    For index = 0 To columns
                        dataItem.Add(dt.Columns(index).ColumnName, row(dt.Columns(index).ColumnName))
                    Next
                    listItems.Add(dataItem)
                Next
            Catch e As Exception
                Throw New DataProviderException(e.Message)
            Finally

            End Try

            Return listItems
        End Function

        Public Overrides Function Find(ByVal type As String, ByVal whereClause As String) As DataSet

            Dim ds As DataSet = Nothing
            Dim commandText = "meanstream_dynamics_GetItems"
            Dim parameterNames As String() = {"@Type", "@WhereClause"}
            Dim parameterValues As Object() = {type, whereClause}

            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbCommand As Common.DbCommand = db.GetStoredProcCommand(commandText)
            Dim param As System.Data.SqlClient.SqlParameter

            If Not IsNothing(parameterNames) And parameterNames.Length > 0 Then
                For i As Integer = 0 To parameterNames.Count - 1
                    param = New System.Data.SqlClient.SqlParameter
                    param.ParameterName = parameterNames(i)
                    param.Direction = ParameterDirection.Input
                    param.Value = parameterValues(i)
                    dbCommand.Parameters.Add(param)
                Next
            End If

            Try
                ds = db.ExecuteDataSet(dbCommand)
            Catch e As Exception
                Throw New DataProviderException(e.Message)
            Finally
                dbCommand.Connection.Close()
            End Try

            Return ds

        End Function

        Public Overrides Sub Insert(ByVal type As String, ByVal entity As Dictionary(Of String, Object))

            If Not entity.ContainsKey("id") And Not entity.ContainsKey("Id") Then
                Throw New ArgumentException("id is required")
            End If

            Dim commandInsertItemText = "meanstream_dynamics_InsertItem"
            Dim commandInsertItemValueText = "meanstream_dynamics_InsertItemAttribute"
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbCommand As Common.DbCommand = Nothing

            Try
                Dim id As Guid = Nothing
                If entity.ContainsKey("Id") Then
                    id = entity.Item("Id")
                Else
                    id = entity.Item("id")
                End If

                dbCommand = db.GetStoredProcCommand(commandInsertItemText)
                Dim paramId As SqlParameter = New SqlParameter("Id", id)
                dbCommand.Parameters.Add(paramId)
                Dim paramType As SqlParameter = New SqlParameter("Type", type)
                dbCommand.Parameters.Add(paramType)
                db.ExecuteNonQuery(dbCommand)

                For Each key As String In entity.Keys
                    If key.Contains(" ") Then
                        Throw New ArgumentException("Name cannot contain spaces")
                    End If

                    If key.ToLower <> "id" Then
                        dbCommand = db.GetStoredProcCommand(commandInsertItemValueText)
                        paramId = New SqlParameter("Type", type)
                        dbCommand.Parameters.Add(paramId)
                        Dim paramName As SqlParameter = New SqlParameter("Name", key)
                        dbCommand.Parameters.Add(paramName)
                        Dim paramValue As SqlParameter = New SqlParameter("Value", entity.Item(key))
                        dbCommand.Parameters.Add(paramValue)

                        db.ExecuteNonQuery(dbCommand)
                    End If
                Next
            Catch e As Exception
                Throw New DataProviderException(e.Message)
            Finally
                dbCommand.Connection.Close()
            End Try

        End Sub

        Public Overrides Sub Insert(Of T As IEntity)(ByVal type As String, ByVal entity As T)

            Dim commandInsertItemText = "meanstream_dynamics_InsertItem"
            Dim commandInsertItemValueText = "meanstream_dynamics_InsertItemAttribute"
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbCommand As Common.DbCommand = Nothing

            Try
                Dim inputType As Type = entity.GetType
                Dim typeProperties() As PropertyInfo = inputType.GetProperties

                Dim id As Guid = New Guid(inputType.GetProperty("Id").GetValue(entity, Nothing).ToString)

                dbCommand = db.GetStoredProcCommand(commandInsertItemText)
                Dim paramId As SqlParameter = New SqlParameter("Id", id)
                dbCommand.Parameters.Add(paramId)
                Dim paramType As SqlParameter = New SqlParameter("Type", type)
                dbCommand.Parameters.Add(paramType)
                db.ExecuteNonQuery(dbCommand)

                For Each propInfo As PropertyInfo In typeProperties
                    If propInfo.Name <> "Id" Then
                        dbCommand = db.GetStoredProcCommand(commandInsertItemValueText)
                        paramId = New SqlParameter("Type", type)
                        dbCommand.Parameters.Add(paramId)
                        Dim paramName As SqlParameter = New SqlParameter("Name", propInfo.Name)
                        dbCommand.Parameters.Add(paramName)
                        Dim paramValue As SqlParameter = New SqlParameter("Value", propInfo.GetValue(entity, Nothing))
                        dbCommand.Parameters.Add(paramValue)

                        db.ExecuteNonQuery(dbCommand)
                    End If
                Next
            Catch e As Exception
                Throw New DataProviderException(e.Message)
            Finally
                dbCommand.Connection.Close()
            End Try

        End Sub

        Public Overrides Sub InsertColumn(ByVal type As String, ByVal name As String, ByVal defaultValue As String)

            If name.Contains(" ") Then
                Throw New ArgumentException("Name cannot contain spaces")
            End If

            Dim ds As DataSet = Nothing
            Dim commandText = "meanstream_dynamics_InsertItemAttribute"
            Dim parameterNames As String() = {"@Type", "@Name", "@Value"}
            Dim parameterValues As Object() = {type, name, defaultValue}

            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbCommand As Common.DbCommand = db.GetStoredProcCommand(commandText)
            Dim param As System.Data.SqlClient.SqlParameter

            If Not IsNothing(parameterNames) And parameterNames.Length > 0 Then
                For i As Integer = 0 To parameterNames.Count - 1
                    param = New System.Data.SqlClient.SqlParameter
                    param.ParameterName = parameterNames(i)
                    param.Direction = ParameterDirection.Input
                    param.Value = parameterValues(i)
                    dbCommand.Parameters.Add(param)
                Next
            End If

            Try
                ds = db.ExecuteDataSet(dbCommand)
            Catch e As Exception
                Throw New DataProviderException(e.Message)
            Finally
                dbCommand.Connection.Close()
            End Try
        End Sub

        Public Overrides Sub Update(ByVal entity As Dictionary(Of String, Object))

            If Not entity.ContainsKey("id") And Not entity.ContainsKey("Id") Then
                Throw New ArgumentException("id is required")
            End If

            Dim id As Guid = Nothing

            Try
                If entity.ContainsKey("Id") Then
                    id = New Guid(entity.Item("Id").ToString)
                Else
                    id = New Guid(entity.Item("id").ToString)
                End If
            Catch ex As InvalidCastException
                Throw New ArgumentException("a valid id (Guid) is required")
            End Try

            Dim commandText = "meanstream_dynamics_UpdateItemAttributeValue"
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbCommand As Common.DbCommand = Nothing

            Try

                For Each key As String In entity.Keys
                    If key.Contains(" ") Then
                        Throw New ArgumentException("Name cannot contain spaces")
                    End If

                    If key.ToLower <> "id" Then
                        dbCommand = db.GetStoredProcCommand(commandText)
                        Dim paramId As SqlParameter = New SqlParameter("Id", id)
                        dbCommand.Parameters.Add(paramId)
                        Dim paramName As SqlParameter = New SqlParameter("Name", key)
                        dbCommand.Parameters.Add(paramName)
                        Dim paramValue As SqlParameter = New SqlParameter("Value", entity.Item(key))
                        dbCommand.Parameters.Add(paramValue)

                        db.ExecuteNonQuery(dbCommand)
                    End If
                Next
            Catch e As Exception
                Throw New DataProviderException(e.Message)
            Finally
                dbCommand.Connection.Close()
            End Try

        End Sub

        Public Overrides Sub Update(Of T As IEntity)(ByVal entity As T)

            Dim inputType As Type = entity.GetType
            Dim typeProperties() As PropertyInfo = inputType.GetProperties

            Dim id As Guid = New Guid(inputType.GetProperty("Id").GetValue(entity, Nothing).ToString)

            Dim commandText = "meanstream_dynamics_UpdateItemAttributeValue"
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbCommand As Common.DbCommand = Nothing

            Try

                For Each propInfo As PropertyInfo In typeProperties
                    If propInfo.Name <> "Id" Then
                        dbCommand = db.GetStoredProcCommand(commandText)
                        Dim paramId As SqlParameter = New SqlParameter("Id", id)
                        dbCommand.Parameters.Add(paramId)
                        Dim paramName As SqlParameter = New SqlParameter("Name", propInfo.Name)
                        dbCommand.Parameters.Add(paramName)
                        Dim paramValue As SqlParameter = New SqlParameter("Value", propInfo.GetValue(entity, Nothing))
                        dbCommand.Parameters.Add(paramValue)

                        db.ExecuteNonQuery(dbCommand)
                    End If
                Next
            Catch e As Exception
                Throw New DataProviderException(e.Message)
            Finally
                dbCommand.Connection.Close()
            End Try

        End Sub

        Public Overrides Sub Delete(ByVal id As Guid)

            Dim ds As DataSet = Nothing
            Dim commandText = "meanstream_dynamics_DeleteItem"
            Dim parameterNames As String() = {"@Id"}
            Dim parameterValues As Object() = {id}

            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbCommand As Common.DbCommand = db.GetStoredProcCommand(commandText)
            Dim param As System.Data.SqlClient.SqlParameter

            If Not IsNothing(parameterNames) And parameterNames.Length > 0 Then
                For i As Integer = 0 To parameterNames.Count - 1
                    param = New System.Data.SqlClient.SqlParameter
                    param.ParameterName = parameterNames(i)
                    param.Direction = ParameterDirection.Input
                    param.Value = parameterValues(i)
                    dbCommand.Parameters.Add(param)
                Next
            End If

            Try
                ds = db.ExecuteDataSet(dbCommand)
            Catch e As Exception
                Throw New DataProviderException(e.Message)
            Finally
                dbCommand.Connection.Close()
            End Try
        End Sub

        Public Overrides Sub DeleteColumn(ByVal type As String, ByVal name As String)

            Dim ds As DataSet = Nothing
            Dim commandText = "meanstream_dynamics_DeleteItemAttribute"
            Dim parameterNames As String() = {"@Type", "@Name"}
            Dim parameterValues As Object() = {type, name}

            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbCommand As Common.DbCommand = db.GetStoredProcCommand(commandText)
            Dim param As System.Data.SqlClient.SqlParameter

            If Not IsNothing(parameterNames) And parameterNames.Length > 0 Then
                For i As Integer = 0 To parameterNames.Count - 1
                    param = New System.Data.SqlClient.SqlParameter
                    param.ParameterName = parameterNames(i)
                    param.Direction = ParameterDirection.Input
                    param.Value = parameterValues(i)
                    dbCommand.Parameters.Add(param)
                Next
            End If

            Try
                ds = db.ExecuteDataSet(dbCommand)
            Catch e As Exception
                Throw New DataProviderException(e.Message)
            Finally
                dbCommand.Connection.Close()
            End Try
        End Sub

        Public Overrides Sub Rename(ByVal oldType As String, ByVal newType As String)

            If newType.Contains(" ") Then
                Throw New ArgumentException("newType cannot contain spaces")
            End If

            Dim ds As DataSet = Nothing
            Dim commandText = "meanstream_dynamics_UpdateItemType"
            Dim parameterNames As String() = {"@OldType", "@NewType"}
            Dim parameterValues As Object() = {oldType, newType}

            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbCommand As Common.DbCommand = db.GetStoredProcCommand(commandText)
            Dim param As System.Data.SqlClient.SqlParameter

            If Not IsNothing(parameterNames) And parameterNames.Length > 0 Then
                For i As Integer = 0 To parameterNames.Count - 1
                    param = New System.Data.SqlClient.SqlParameter
                    param.ParameterName = parameterNames(i)
                    param.Direction = ParameterDirection.Input
                    param.Value = parameterValues(i)
                    dbCommand.Parameters.Add(param)
                Next
            End If

            Try
                ds = db.ExecuteDataSet(dbCommand)
            Catch e As Exception
                Throw New DataProviderException(e.Message)
            Finally
                dbCommand.Connection.Close()
            End Try
        End Sub

        Public Overrides Sub RenameColumn(ByVal type As String, ByVal oldName As String, ByVal newName As String)

            If newName.Contains(" ") Then
                Throw New ArgumentException("newName cannot contain spaces")
            End If

            Dim ds As DataSet = Nothing
            Dim commandText = "meanstream_dynamics_UpdateItemAttributeName"
            Dim parameterNames As String() = {"@Type", "@OldName", "@NewName"}
            Dim parameterValues As Object() = {type, oldName, newName}

            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbCommand As Common.DbCommand = db.GetStoredProcCommand(commandText)
            Dim param As System.Data.SqlClient.SqlParameter

            If Not IsNothing(parameterNames) And parameterNames.Length > 0 Then
                For i As Integer = 0 To parameterNames.Count - 1
                    param = New System.Data.SqlClient.SqlParameter
                    param.ParameterName = parameterNames(i)
                    param.Direction = ParameterDirection.Input
                    param.Value = parameterValues(i)
                    dbCommand.Parameters.Add(param)
                Next
            End If

            Try
                ds = db.ExecuteDataSet(dbCommand)
            Catch e As Exception
                Throw New DataProviderException(e.Message)
            Finally
                dbCommand.Connection.Close()
            End Try
        End Sub

        Private Sub FillParameters(Of T As IEntity)(ByVal InputClass As T, ByRef CurrentCommand As Common.DbCommand)

            Dim inputType As Type = InputClass.GetType
            Dim storedProcAttribute As StoredProcParameter
            Dim typeProperties() As PropertyInfo = inputType.GetProperties
            Dim attributes() As Attribute

            For Each parameter As SqlParameter In CurrentCommand.Parameters

                For Each propInfo As PropertyInfo In typeProperties
                    attributes = propInfo.GetCustomAttributes(GetType(StoredProcParameter), True)
                    If Not IsNothing(attributes) And attributes.Length = 1 Then
                        storedProcAttribute = attributes(0)
                        If Not IsNothing(storedProcAttribute) And storedProcAttribute.StoredProcParameter.Length > 0 Then
                            If storedProcAttribute.StoredProcParameter = parameter.ParameterName Then
                                parameter.Value = propInfo.GetValue(InputClass, Nothing)
                                Exit For
                            End If
                        End If
                    End If
                Next
            Next

        End Sub
    End Class
End Namespace