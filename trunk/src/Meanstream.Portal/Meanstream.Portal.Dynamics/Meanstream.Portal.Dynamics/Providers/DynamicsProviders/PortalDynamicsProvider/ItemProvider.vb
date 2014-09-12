Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection

Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql

Imports Meanstream.Portal.Core.Services.Dynamics

Namespace Meanstream.Portal.Providers.PortalDynamicsProvider

    Public Class ItemProvider


        Public Function GetById(Of T As {IEntity, New})(ByVal id As Guid) As T

            Dim dataItem As T = New T
            Try
                Dim dbReader As IDataReader = Me.GetById(id).Tables(0).CreateDataReader
                If Not IsNothing(dbReader) Then
                    dbReader.Read()
                    dataItem.Fill(dbReader)
                End If
            Catch e As Exception
                Throw New DynamicsException(e.Message)
            Finally

            End Try

            Return dataItem
        End Function

        Public Function GetById(ByVal id As Guid) As DataSet

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
                Throw New DynamicsException(e.Message)
            Finally
                dbCommand.Connection.Close()
            End Try

            Return ds
        End Function

        Public Function Find(Of T As {IEntity, New})(ByVal type As String, ByVal whereClause As String) As List(Of T)

            Dim dataItem As T
            Dim listItems As List(Of T) = New List(Of T)
            Try
                Dim dbReader As IDataReader = Me.Find(type, whereClause).Tables(0).CreateDataReader
                If Not IsNothing(dbReader) Then
                    While dbReader.Read()
                        dataItem = New T
                        dataItem.Fill(dbReader)
                        listItems.Add(dataItem)
                    End While
                End If
            Catch e As Exception
                Throw New DynamicsException(e.Message)
            Finally

            End Try

            Return listItems
        End Function

        Public Function GetKeyValues(ByVal type As String, ByVal whereClause As String) As List(Of Dictionary(Of String, Object))

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
                        dataItem.Add(dt.Columns(index).ColumnName.ToLower, row(dt.Columns(index).ColumnName))
                    Next
                    listItems.Add(dataItem)
                Next
            Catch e As Exception
                Throw New DynamicsException(e.Message)
            Finally

            End Try

            Return listItems
        End Function

        Public Function Find(ByVal type As String, ByVal whereClause As String) As DataSet

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
                Throw New DynamicsException(e.Message)
            Finally
                dbCommand.Connection.Close()
            End Try

            Return ds

        End Function

        Public Sub Insert(ByVal type As String, ByVal entity As Dictionary(Of String, Object))

            Dim commandInsertItemText = "meanstream_dynamics_InsertItem"
            Dim commandInsertItemValueText = "meanstream_dynamics_InsertItemAttribute"
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbCommand As Common.DbCommand = Nothing

            If entity.Item("Id") Is Nothing Then
                Throw New ArgumentException("id is required")
            End If

            Try
                Dim id As Guid = entity.Item("Id")

                dbCommand = db.GetStoredProcCommand(commandInsertItemText)
                Dim paramId As SqlParameter = New SqlParameter("Id", id)
                dbCommand.Parameters.Add(paramId)
                Dim paramType As SqlParameter = New SqlParameter("Type", type)
                dbCommand.Parameters.Add(paramType)
                db.ExecuteNonQuery(dbCommand)

                For Each key As String In entity.Keys
                    If key.ToLower <> "id" Then
                        dbCommand = db.GetStoredProcCommand(commandInsertItemValueText)
                        paramId = New SqlParameter("Id", id)
                        dbCommand.Parameters.Add(paramId)
                        paramType = New SqlParameter("Type", type)
                        dbCommand.Parameters.Add(paramType)
                        Dim paramName As SqlParameter = New SqlParameter("Name", key)
                        dbCommand.Parameters.Add(paramName)
                        Dim paramValue As SqlParameter = New SqlParameter("Value", entity.Item(key))
                        dbCommand.Parameters.Add(paramValue)

                        db.ExecuteNonQuery(dbCommand)
                    End If
                Next
            Catch e As Exception
                Throw New DynamicsException(e.Message)
            Finally
                dbCommand.Connection.Close()
            End Try

        End Sub

        Public Sub Insert(Of T As IEntity)(ByVal type As String, ByVal entity As T)

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
                        paramId = New SqlParameter("Id", id)
                        dbCommand.Parameters.Add(paramId)
                        paramType = New SqlParameter("Type", type)
                        dbCommand.Parameters.Add(paramType)
                        Dim paramName As SqlParameter = New SqlParameter("Name", propInfo.Name)
                        dbCommand.Parameters.Add(paramName)
                        Dim paramValue As SqlParameter = New SqlParameter("Value", propInfo.GetValue(entity, Nothing))
                        dbCommand.Parameters.Add(paramValue)

                        db.ExecuteNonQuery(dbCommand)
                    End If
                Next
            Catch e As Exception
                Throw New DynamicsException(e.Message)
            Finally
                dbCommand.Connection.Close()
            End Try

        End Sub

        Public Sub Update(ByVal entity As Dictionary(Of String, Object))

            Dim commandText = "meanstream_dynamics_UpdateItemAttributeValue"
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbCommand As Common.DbCommand = Nothing

            If entity.Item("Id") Is Nothing Then
                Throw New ArgumentException("id is required")
            End If

            Try
                Dim id As Guid = entity.Item("Id")

                For Each key As String In entity.Keys
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
                Throw New DynamicsException(e.Message)
            Finally
                dbCommand.Connection.Close()
            End Try

        End Sub

        Public Sub Update(Of T As IEntity)(ByVal entity As T)

            Dim commandText = "meanstream_dynamics_UpdateItemAttributeValue"
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbCommand As Common.DbCommand = Nothing

            Try
                Dim inputType As Type = entity.GetType
                Dim typeProperties() As PropertyInfo = inputType.GetProperties

                Dim id As Guid = New Guid(inputType.GetProperty("Id").GetValue(entity, Nothing).ToString)

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
                Throw New DynamicsException(e.Message)
            Finally
                dbCommand.Connection.Close()
            End Try

        End Sub

        Public Sub Delete(ByVal id As Guid)

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
                Throw New DynamicsException(e.Message)
            Finally
                dbCommand.Connection.Close()
            End Try
        End Sub

        Public Sub DeleteColumn(ByVal type As String, ByVal name As String)

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
                Throw New DynamicsException(e.Message)
            Finally
                dbCommand.Connection.Close()
            End Try
        End Sub

        Public Sub Rename(ByVal oldType As String, ByVal newType As String)

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
                Throw New DynamicsException(e.Message)
            Finally
                dbCommand.Connection.Close()
            End Try
        End Sub

        Public Sub RenameColumn(ByVal type As String, ByVal oldName As String, ByVal newName As String)
            Dim ds As DataSet = Nothing
            Dim commandText = "meanstream_dynamics_UpdateItemAttributeType"
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
                Throw New DynamicsException(e.Message)
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