Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Namespace Caracole.Portal
    Public Class Util

        Public Shared Function ConvertDataReaderToDataSet(ByVal reader As SqlDataReader) As DataSet
            Dim dataSet As New DataSet
            Do
                ' Create new data table
                Dim schemaTable As DataTable = reader.GetSchemaTable()
                Dim dataTable As New DataTable

                If Not (schemaTable Is Nothing) Then
                    ' A query returning records was executed
                    Dim i As Integer
                    For i = 0 To schemaTable.Rows.Count - 1
                        Dim dataRow As DataRow = schemaTable.Rows(i)
                        ' Create a column name that is unique in the data table
                        Dim columnName As String = CStr(dataRow("ColumnName")) '+ "<C" + i + "/>";
                        ' Add the column definition to the data table
                        Dim column As New DataColumn(columnName, CType(dataRow("DataType"), Type))
                        dataTable.Columns.Add(column)
                    Next i

                    dataSet.Tables.Add(dataTable)

                    ' Fill the data table we just created
                    While reader.Read()
                        Dim dataRow As DataRow = dataTable.NewRow()

                        'Dim i As Integer
                        For i = 0 To reader.FieldCount - 1
                            dataRow(i) = reader.GetValue(i)
                        Next i
                        dataTable.Rows.Add(dataRow)
                    End While
                Else
                    ' No records were returned
                    Dim column As New DataColumn("RowsAffected")
                    dataTable.Columns.Add(column)
                    dataSet.Tables.Add(dataTable)
                    Dim dataRow As DataRow = dataTable.NewRow()
                    dataRow(0) = reader.RecordsAffected
                    dataTable.Rows.Add(dataRow)
                End If
            Loop While reader.NextResult()
            Return dataSet
        End Function 'convertDataReaderToDataSet
    End Class

End Namespace