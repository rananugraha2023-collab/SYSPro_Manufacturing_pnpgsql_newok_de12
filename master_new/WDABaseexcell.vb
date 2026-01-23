Imports Microsoft.Win32
Imports System.Data
Imports System.Data.SqlClient
Imports npgsql
Imports System.Data.OleDb

Public Class WDABaseexcell
    Implements IDisposable

    'Class level variables that are available to classes that instantiate me
    Public SQL As String

    Dim Connection As System.Data.OleDb.OleDbConnection
    Dim Command As System.Data.OleDb.OleDbCommand
    Public DataAdapter As OleDb.OleDbDataAdapter
    Public DataReader As OleDb.OleDbDataReader
    Public trans As OleDb.OleDbTransaction

    Private disposed As Boolean = False

    Public Sub New(ByVal Company As String, ByVal Application As String)
        'Connection = New nPgSqlConnection("Server=192.50.1.3;Port=5432;User Id=postgres;Password=syspronuri;Database=sdm")
        'Connection = New nPgSqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=Allahuakbar;Database=sdm")

        'Connection = New System.Data.OleDb.OleDbConnection _
        '   ("provider=Microsoft.Jet.OLEDB.4.0;Data Source='D:\Hendrik_Job\syspro\slip_gaji.xls'; " + _
        '    "Extended Properties=Excel 8.0;")

        Connection = New System.Data.OleDb.OleDbConnection _
          ("provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ClsVar._FileName + "; " + _
           "Extended Properties=Excel 8.0;")

        'Connection = New nPgSqlConnection("Server=" + master_new.ClsVar.sServerName + "; " + _
        '                                " Port=5432;" + _
        '                                " User Id=postgres;" + _
        '                                " Password=syspronuri;" + _
        '                                " Database=sdm")
    End Sub

    Private Overloads Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
                ' TODO: put code to dispose managed resources
                If Not DataReader Is Nothing Then
                    DataReader.Dispose()
                    DataReader = Nothing
                End If
                If Not DataAdapter Is Nothing Then
                    DataAdapter.Dispose()
                    DataAdapter = Nothing
                End If
                If Not Command Is Nothing Then
                    Command.Dispose()
                    Command = Nothing
                End If
                If Not Connection Is Nothing Then
                    Connection.Close()
                    Connection.Dispose()
                    Connection = Nothing
                End If
            End If

            ' TODO: put code to free unmanaged resources here
        End If
        Me.disposed = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(False)
        MyBase.Finalize()
    End Sub
#End Region

    Public Sub OpenConnection()
        Try
            Connection.Open()
        Catch OleDbExceptionErr As OleDbException
            Throw New System.Exception(OleDbExceptionErr.Message, _
                OleDbExceptionErr.InnerException)
        Catch InvalidOperationExceptionErr As InvalidOperationException
            Throw New System.Exception(InvalidOperationExceptionErr.Message, _
                InvalidOperationExceptionErr.InnerException)
        End Try
    End Sub

    Public Sub CloseConnection()
        Connection.Close()
    End Sub

    Public Sub InitializeCommand()
        If Command Is Nothing Then
            Try
                Command = New OleDbCommand(SQL, Connection)
                Command.CommandTimeout = 0
                'See if this is a stored procedure
                If Not SQL.ToUpper.StartsWith("SELECT ") _
                    And Not SQL.ToUpper.StartsWith("INSERT ") _
                    And Not SQL.ToUpper.StartsWith("UPDATE ") _
                    And Not SQL.ToUpper.StartsWith("DELETE ") _
                    And Not SQL.ToUpper.StartsWith("DROP ") _
                    And Not SQL.ToUpper.StartsWith("ALTER ") _
                    And Not SQL.ToUpper.StartsWith("CREATE ") Then
                    Command.CommandType = CommandType.StoredProcedure
                End If
            Catch OleDbExceptionErr As OleDbException
                Throw New System.Exception(OleDbExceptionErr.Message, _
                    OleDbExceptionErr.InnerException)
            End Try
        End If
    End Sub

    Public Sub AddParameter(ByVal Name As String, ByVal Value As Object, ByVal size As Integer)
        Try
            'Command.Parameters.Add(New nnPgSqlParameter(Name, SqlDbType.VarChar, 8000))
            Command.Parameters.Add(New OleDbParameter(Name, OleDbType.VarChar, 8000))
            Command.Parameters(Name).Value = Value
            'Command.Parameters(Name).Value = Value
        Catch OleDbExceptionErr As OleDbException
            Throw New System.Exception(OleDbExceptionErr.Message, _
                OleDbExceptionErr.InnerException)
        End Try
    End Sub

    Public Sub AddParameter(ByVal Name As String, ByVal Value As Object)
        Try
            'Command.Parameters.Add(New nnPgSqlParameter(Name, Value))
            Command.Parameters.Add(New OleDbParameter(Name, Value))
        Catch OleDbExceptionErr As OleDbException
            Throw New System.Exception(OleDbExceptionErr.Message, _
                OleDbExceptionErr.InnerException)
        End Try
    End Sub

    Public Sub InitializeDataAdapter()
        Try
            DataAdapter = New OleDbDataAdapter
            DataAdapter.SelectCommand = Command
        Catch OleDbExceptionErr As OleDbException
            Throw New System.Exception(OleDbExceptionErr.Message, _
            OleDbExceptionErr.InnerException)
        End Try
    End Sub

    Public Sub FillDataSet(ByRef oDataSet As DataSet, ByVal TableName As String)
        Try
            InitializeCommand()
            InitializeDataAdapter()
            DataAdapter.Fill(oDataSet, TableName)
        Catch OleDbExceptionErr As OleDbException 'SqlException
            Throw New System.Exception(OleDbExceptionErr.Message, _
                OleDbExceptionErr.InnerException)
        Finally
            Command.Dispose()
            Command = Nothing
            DataAdapter.Dispose()
            DataAdapter = Nothing
        End Try
    End Sub

    Public Sub FillDataTable(ByRef oDataTable As DataTable)
        Try
            InitializeCommand()
            InitializeDataAdapter()
            DataAdapter.Fill(oDataTable)
        Catch OleDbExceptionErr As OleDbException
            Throw New System.Exception(OleDbExceptionErr.Message, _
                OleDbExceptionErr.InnerException)
        Finally
            Command.Dispose()
            Command = Nothing
            DataAdapter.Dispose()
            DataAdapter = Nothing
        End Try
    End Sub

    'Public Function ExecuteStoredProcedure() As Integer
    '    Try
    '        OpenConnection()
    '        ExecuteStoredProcedure = Command.ExecuteNonQuery
    '        'Catch ExceptionErr As Exception
    '    Catch ExceptionErr As nPgSqlException
    '        Throw New System.Exception(ExceptionErr.Message, _
    '            ExceptionErr.InnerException)
    '    Finally
    '        CloseConnection()
    '    End Try
    'End Function
    Public Function ExecuteStoredProcedure() As Integer
        Try
            OpenConnection()
            ExecuteStoredProcedure = Command.ExecuteNonQuery
            'Catch ExceptionErr As Exception
        Catch ExceptionErr As OleDb.OleDbException
            Throw New System.Exception(ExceptionErr.Message, _
                ExceptionErr.InnerException)
        Finally
            CloseConnection()
        End Try
    End Function

End Class
