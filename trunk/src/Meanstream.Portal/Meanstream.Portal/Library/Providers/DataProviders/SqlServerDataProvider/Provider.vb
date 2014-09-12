Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection

Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql

Imports Meanstream.Core.Data
Imports Meanstream.Core.EntityModel

Namespace Meanstream.Core.Providers.SqlServerDataProvider

    Public Class Provider
        Inherits Meanstream.Core.Data.DataProvider


        Public Overrides Function GetById(Of T As {IEntity, New})(ByVal id As Guid) As T

            Dim dataItem As T
            'Dim listItems As T = New T

            Dim ds As DataSet = Me.GetById(id)
            If ds.Tables.Count = 0 Then
                Return dataItem
            End If

            Dim dbReader As IDataReader = ds.Tables(0).CreateDataReader

            Try

                If Not IsNothing(dbReader) Then
                    While dbReader.Read()
                        dataItem = New T
                        Me.FillReader(dataItem, dbReader)
                    End While
                End If

            Catch e As PropertyTypeMismatchException
                Throw New PropertyTypeMismatchException(e.Message)
            Catch e As Exception
                Throw New DataProviderException(e.Message)
            Finally
                dbReader.Close()
            End Try

            Return dataItem

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
            Catch e As PropertyTypeMismatchException
                Throw New PropertyTypeMismatchException(e.Message)
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

            Catch e As PropertyTypeMismatchException
                Throw New PropertyTypeMismatchException(e.Message)
            Catch e As Exception
                Throw New DataProviderException(e.Message)
            Finally

            End Try

            Return listItems

        End Function


        Public Overrides Function Find(Of T As {IEntity, New})(ByVal whereClause As String) As List(Of T)

            Dim dataItem As T
            Dim listItems As List(Of T) = New List(Of T)

            Dim ds As DataSet = Me.Find(GetType(T).FullName, whereClause)
            If ds.Tables.Count = 0 Then
                Return listItems
            End If

            Dim dbReader As IDataReader = ds.Tables(0).CreateDataReader

            Try

                If Not IsNothing(dbReader) Then
                    While dbReader.Read()
                        dataItem = New T
                        Me.FillReader(dataItem, dbReader)
                        listItems.Add(dataItem)
                    End While
                End If

            Catch e As PropertyTypeMismatchException
                Throw New PropertyTypeMismatchException(e.Message)
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

            Catch e As PropertyTypeMismatchException
                Throw New PropertyTypeMismatchException(e.Message)
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
            Catch e As PropertyTypeMismatchException
                Throw New PropertyTypeMismatchException(e.Message)
            Catch e As Exception
                Throw New DataProviderException(e.Message)
            Finally
                dbCommand.Connection.Close()
            End Try

            Return ds

        End Function


        Public Overloads Overrides Function Find(type As String, query As Query.IQuery) As System.Data.DataSet
            Return Me.Find(type, query.ToString)
        End Function


        Public Overloads Overrides Function Find(Of T As {New, EntityModel.IEntity})(query As Query.IQuery) As System.Collections.Generic.List(Of T)
            Return Me.Find(Of T)(query.ToString)
        End Function


        Public Overloads Overrides Function GetKeyValues(type As String, query As Query.IQuery) As System.Collections.Generic.List(Of System.Collections.Generic.Dictionary(Of String, Object))
            Return Me.GetKeyValues(type, query.ToString)
        End Function


        Public Overrides Sub Insert(ByVal entity As Dictionary(Of String, Object))

            If Not entity.ContainsKey("id") And Not entity.ContainsKey("Id") Then
                Throw New ArgumentException("id is required")
            End If

            If Not entity.ContainsKey("type") And Not entity.ContainsKey("Type") Then
                Throw New ArgumentException("type is required")
            End If

            Dim commandInsertItemText = "meanstream_dynamics_InsertItem"
            Dim commandInsertItemValueText = "meanstream_dynamics_InsertItemAttribute"
            Dim commandSyncItemValueText = "meanstream_dynamics_SyncItemAttributes"
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbCommand As Common.DbCommand = Nothing

            Try

                Dim id As Guid = Nothing
                If entity.ContainsKey("Id") Then
                    id = entity.Item("Id")
                Else
                    id = entity.Item("id")
                End If

                Dim type As String = Nothing
                If entity.ContainsKey("Type") Then
                    type = entity.Item("Type")
                Else
                    type = entity.Item("type")
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

                    If key.ToLower <> "id" And key.ToLower <> "type" Then
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

                'sync fields
                dbCommand = db.GetStoredProcCommand(commandSyncItemValueText)
                paramType = New SqlParameter("Type", type)
                dbCommand.Parameters.Add(paramType)
                db.ExecuteNonQuery(dbCommand)
            Catch e As Exception
                Throw New DataProviderException(e.Message)
            Finally
                dbCommand.Connection.Close()
            End Try

        End Sub


        Public Overrides Sub Insert(Of T As IEntity)(ByVal entity As T)

            Dim commandInsertItemText = "meanstream_dynamics_InsertItem"
            Dim commandInsertItemValueText = "meanstream_dynamics_InsertItemAttribute"
            Dim commandSyncItemValueText = "meanstream_dynamics_SyncItemAttributes"
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbCommand As Common.DbCommand = Nothing

            Try
                Dim inputType As Type = entity.GetType
                Dim typeProperties() As PropertyInfo = inputType.GetProperties

                Dim id As Guid = entity.Id 'New Guid(inputType.GetProperty("Id").GetValue(entity, Nothing).ToString)
                Dim type As String = entity.GetType.ToString 'inputType.GetProperty("Type").GetValue(entity, Nothing).ToString

                dbCommand = db.GetStoredProcCommand(commandInsertItemText)
                Dim paramId As SqlParameter = New SqlParameter("Id", id)
                dbCommand.Parameters.Add(paramId)
                Dim paramType As SqlParameter = New SqlParameter("Type", type)
                dbCommand.Parameters.Add(paramType)
                db.ExecuteNonQuery(dbCommand)

                For Each propInfo As PropertyInfo In typeProperties
                    If propInfo.Name <> "Id" And propInfo.Name <> "Type" Then
                        dbCommand = db.GetStoredProcCommand(commandInsertItemValueText)
                        paramId = New SqlParameter("Type", type)
                        dbCommand.Parameters.Add(paramId)
                        Dim paramName As SqlParameter = New SqlParameter("Name", propInfo.Name)
                        dbCommand.Parameters.Add(paramName)
                        Dim paramValue As SqlParameter = Me.GetPropertyValueParameter(entity, propInfo)
                        dbCommand.Parameters.Add(paramValue)

                        db.ExecuteNonQuery(dbCommand)
                    End If
                Next

                'sync fields
                dbCommand = db.GetStoredProcCommand(commandSyncItemValueText)
                paramType = New SqlParameter("Type", type)
                dbCommand.Parameters.Add(paramType)
                db.ExecuteNonQuery(dbCommand)
            Catch e As Exception
                Throw New DataProviderException(e.Message)
            Finally
                dbCommand.Connection.Close()
            End Try

        End Sub


        Public Overrides Sub InsertField(ByVal type As String, ByVal name As String, ByVal defaultValue As Object)
            If String.IsNullOrEmpty(type) Then
                Throw New ArgumentException("type cannot be null or contain spaces")
            End If

            If String.IsNullOrEmpty(name) Then
                Throw New ArgumentException("Name cannot contain spaces")
            End If

            Dim ds As DataSet = Nothing
            Dim commandText = "meanstream_dynamics_InsertItemAttribute"
            Dim parameterNames As String() = {"@Type", "@Name", "@Value"}
            Dim parameterValues As Object() = {type, name, defaultValue}

            Dim commandSyncItemValueText = "meanstream_dynamics_SyncItemAttributes"

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

                'sync fields
                dbCommand = db.GetStoredProcCommand(commandSyncItemValueText)
                Dim paramType As SqlParameter = New SqlParameter("Type", type)
                dbCommand.Parameters.Add(paramType)
                db.ExecuteNonQuery(dbCommand)
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

            If Not entity.ContainsKey("type") And Not entity.ContainsKey("Type") Then
                Throw New ArgumentException("type is required")
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

            Dim type As String = Nothing

            Try
                If entity.ContainsKey("Type") Then
                    type = entity.Item("Type").ToString
                Else
                    type = entity.Item("type").ToString
                End If
            Catch ex As InvalidCastException
                Throw New ArgumentException("a valid type is required")
            End Try

            Dim commandText = "meanstream_dynamics_UpdateItemAttributeValue"
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbCommand As Common.DbCommand = Nothing

            Try

                For Each key As String In entity.Keys
                    If key.Contains(" ") Then
                        Throw New ArgumentException("Name cannot contain spaces")
                    End If

                    If key.ToLower <> "id" And key.ToLower <> "type" Then
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
                    If propInfo.Name <> "Id" And propInfo.Name <> "Type" Then
                        Dim value As SqlParameter = Me.GetPropertyValueParameter(entity, propInfo)
                        If value IsNot Nothing Then
                            dbCommand = db.GetStoredProcCommand(commandText)
                            Dim paramId As SqlParameter = New SqlParameter("Id", id)
                            dbCommand.Parameters.Add(paramId)
                            Dim paramName As SqlParameter = New SqlParameter("Name", propInfo.Name)
                            dbCommand.Parameters.Add(paramName)
                            Dim paramValue As SqlParameter = Me.GetPropertyValueParameter(entity, propInfo)
                            dbCommand.Parameters.Add(paramValue)
                            db.ExecuteNonQuery(dbCommand)
                        End If
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


        Public Overrides Sub DeleteField(ByVal type As String, ByVal name As String)
            If String.IsNullOrEmpty(type) Then
                Throw New ArgumentException("type cannot be null or contain spaces")
            End If

            If name.Trim = "" Then
                Throw New ArgumentException("name cannot be null or contain spaces")
            End If

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
            If String.IsNullOrEmpty(oldType) Then
                Throw New ArgumentException("oldType cannot be null or contain spaces")
            End If

            If String.IsNullOrEmpty(newType) Then
                Throw New ArgumentException("newType cannot be null or contain spaces")
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


        Public Overrides Sub RenameField(ByVal type As String, ByVal oldName As String, ByVal newName As String)
            If String.IsNullOrEmpty(type) Then
                Throw New ArgumentException("type cannot be null or contain spaces")
            End If

            If String.IsNullOrEmpty(newName) Then
                Throw New ArgumentException("newName cannot be null or contain spaces")
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


        Private Sub FillReader(Of T As IEntity)(ByVal entity As T, ByVal reader As System.Data.IDataReader)

            Dim serizableAttribute As SerializableEntityField
            Dim fieldAttribute As EntityField
            Dim attributes() As Attribute

            Dim props() As Reflection.PropertyInfo = entity.GetType.GetProperties

            For Each propInfo As Reflection.PropertyInfo In props
                If propInfo.Name = "Type" Then
                    Continue For
                End If

                'serialized values
                attributes = propInfo.GetCustomAttributes(GetType(SerializableEntityField), True)
                If Not IsNothing(attributes) And attributes.Length = 1 Then
                    serizableAttribute = attributes(0)
                    If Not IsNothing(serizableAttribute) And serizableAttribute.PropertyName.Length > 0 Then
                        Dim serializer As New Serializer(Of Object)
                        Dim serializedObject As String = reader.Item(serizableAttribute.PropertyName)
                        If Not IsDBNull(serializedObject) Then
                            If (propInfo.CanWrite) Then
                                propInfo.SetValue(entity, CTypeDynamic(serializer.Deserialize(serializedObject), propInfo.PropertyType), Nothing)
                            End If
                        End If
                    End If
                End If

                'standard values
                attributes = propInfo.GetCustomAttributes(GetType(EntityField), True)
                If Not IsNothing(attributes) And attributes.Length = 1 Then
                    fieldAttribute = attributes(0)
                    If Not IsNothing(fieldAttribute) And fieldAttribute.PropertyName.Length > 0 Then
                        Dim obj As Object = reader.Item(fieldAttribute.PropertyName)
                        Dim objType As Type = obj.GetType
                        Dim propertyType As Type = propInfo.PropertyType

                        If Not IsDBNull(obj) Then
                            Try
                                If propertyType <> objType Then 'typeOf String
                                    If propertyType.IsPrimitive Then
                                        obj = CTypeDynamic(obj, propertyType)
                                    Else
                                        If objType.GetConstructors.Count > 0 Then

                                            Dim args() As Object = {obj}
                                            Dim types() As Type = {objType}
                                            Dim constr = propertyType.GetConstructor(types)

                                            If constr IsNot Nothing Then
                                                obj = propertyType.InvokeMember("", System.Reflection.BindingFlags.CreateInstance, Nothing, obj, args)
                                            Else
                                                obj = CTypeDynamic(obj, propertyType)
                                            End If

                                        End If
                                    End If
                                End If

                                If (propInfo.CanWrite) Then
                                    propInfo.SetValue(entity, obj, Nothing)
                                End If
                            Catch ex As Exception
                                Throw New PropertyTypeMismatchException("Type mismatch converting " & obj.GetType.Name & " to " & propertyType.Name)
                            End Try
                        End If

                    End If
                End If
            Next

        End Sub


        Private Function GetPropertyValueParameter(Of T As IEntity)(ByVal entity As T, ByVal propInfo As PropertyInfo) As SqlParameter
            Dim inputType As Type = entity.GetType
            Dim serizableAttribute As SerializableEntityField
            Dim fieldAttribute As EntityField
            Dim typeProperties() As PropertyInfo = inputType.GetProperties
            Dim attributes() As Attribute
            Dim value As Object = ""

            'serialized values
            attributes = propInfo.GetCustomAttributes(GetType(SerializableEntityField), True)
            If Not IsNothing(attributes) And attributes.Length = 1 Then
                serizableAttribute = attributes(0)
                If Not IsNothing(serizableAttribute) And serizableAttribute.PropertyName.Length > 0 Then
                    Dim obj As Object = propInfo.GetValue(entity, Nothing)
                    If Not IsDBNull(obj) Then
                        Dim serializer As New Serializer(Of Object)
                        Dim paramValue As SqlParameter = New SqlParameter()
                        paramValue.ParameterName = "Value"
                        If obj Is Nothing Then
                            paramValue.SqlValue = ""
                        Else
                            paramValue.SqlValue = serializer.Serialize(obj)
                        End If
                        Return paramValue
                    End If
                End If
            End If

            'standard values
            attributes = propInfo.GetCustomAttributes(GetType(EntityField), True)
            If Not IsNothing(attributes) And attributes.Length = 1 Then
                fieldAttribute = attributes(0)
                If Not IsNothing(fieldAttribute) And fieldAttribute.PropertyName.Length > 0 Then
                    Dim obj As Object = propInfo.GetValue(entity, Nothing)
                    If Not IsDBNull(obj) Then
                        Dim paramValue As SqlParameter = New SqlParameter()
                        paramValue.ParameterName = "Value"
                        paramValue.SqlDbType = fieldAttribute.DataType
                        If obj Is Nothing Then
                            paramValue.SqlValue = ""
                        Else
                            paramValue.SqlValue = obj
                        End If
                        Return paramValue
                    End If
                End If
            End If

            Return New SqlParameter("Value", "")

        End Function

    End Class

End Namespace