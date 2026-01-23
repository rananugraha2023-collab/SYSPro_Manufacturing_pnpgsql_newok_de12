Imports Microsoft.Win32
Imports System.Data
Imports System.Data.Odbc
'Imports System.Data.SqlClient

Public Class WDABase
    Implements IDisposable

    'Class level variables that are available to classes that instantiate me
    Public SQL As String

    Public Connection As OdbcConnection
    Public Command As OdbcCommand
    Public DataAdapter As OdbcDataAdapter
    Public DataReader As OdbcDataReader


    'Public Connection As SqlConnection
    'Public Command As SqlCommand
    'Public DataAdapter As SqlDataAdapter
    'Public DataReader As SqlDataReader

    Private disposed As Boolean = False

    Public Sub New(ByVal Company As String, ByVal Application As String)
        Connection = New OdbcConnection("DSN=hariff10;HOST=192.50.1.44;PORT=30015;DB=Hariff;UID=user1;PWD=u;Connection Timeout = 0")
    End Sub


    Private Overloads Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
                ' TODO: put code to dispose managed resources
                If Not DataReader Is Nothing Then
                    DataReader.Dispose
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
        Catch OleDbExceptionErr As OdbcException
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
                Command = New OdbcCommand(SQL, Connection)
                Command.CommandTimeout = 0
                'Command = New SqlCommand(SQL, Connection)
                'See if this is a stored procedure
                If Not SQL.ToUpper.StartsWith("SELECT ") _
                    And Not SQL.ToUpper.StartsWith("INSERT ") _
                    And Not SQL.ToUpper.StartsWith("UPDATE ") _
                    And Not SQL.ToUpper.StartsWith("DELETE ") Then
                    Command.CommandType = CommandType.StoredProcedure
                End If
            Catch OleDbExceptionErr As OdbcException
                Throw New System.Exception(OleDbExceptionErr.Message, _
                    OleDbExceptionErr.InnerException)
            End Try
        End If
    End Sub

    Public Sub AddParameter(ByVal Name As String, ByVal Value As Object)

        Try
            Command.Parameters.Add(New OdbcParameter(Name, Value))
        Catch OleDbExceptionErr As OdbcException
            Throw New System.Exception(OleDbExceptionErr.Message, _
                OleDbExceptionErr.InnerException)
        End Try
    End Sub

    Public Sub InitializeDataAdapter()
        Try
            DataAdapter = New OdbcDataAdapter
            'DataAdapter = New SqlDataAdapter
            DataAdapter.SelectCommand = Command
        Catch OleDbExceptionErr As OdbcException
            Throw New System.Exception(OleDbExceptionErr.Message, _
            OleDbExceptionErr.InnerException)
        End Try
    End Sub

    Public Sub FillDataSet(ByRef oDataSet As DataSet, ByVal TableName As String)
        Try
            InitializeCommand()
            InitializeDataAdapter()
            DataAdapter.Fill(oDataSet, TableName)
        Catch OleDbExceptionErr As OdbcException
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
        Catch OleDbExceptionErr As OdbcException
            Throw New System.Exception(OleDbExceptionErr.Message, _
                OleDbExceptionErr.InnerException)
        Finally
            Command.Dispose()
            Command = Nothing
            DataAdapter.Dispose()
            DataAdapter = Nothing
        End Try
    End Sub

    Public Function ExecuteStoredProcedure() As Integer
        Try
            OpenConnection()
            ExecuteStoredProcedure = Command.ExecuteNonQuery()
        Catch ExceptionErr As Exception
            Throw New System.Exception(ExceptionErr.Message, _
                ExceptionErr.InnerException)
        Finally
            CloseConnection()
        End Try
    End Function
End Class
