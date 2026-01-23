Imports Microsoft.Win32
Imports System.Data
Imports System.Data.SqlClient
Imports npgsql

Public Class WDABasepgsql_server
    Implements IDisposable

    'Class level variables that are available to classes that instantiate me
    Public SQL As String

    Public Connection As nPgSqlConnection
    Public Command As nPgSqlCommand
    Public DataAdapter As nPgSqlDataAdapter
    Public DataReader As NpgsqlDataReader
    Public trans As nPgSqlTransaction

    Private disposed As Boolean = False

    Public Sub New(ByVal Company As String, ByVal Application As String)
        'Declare variables
        'Dim objReg As RegistryKey

        ''Key size must be 128 bits to 192 bits in increments of 64 bits
        'Dim bytKey() As Byte = System.Text.Encoding.UTF8.GetBytes( _
        '    "G~v!x@Z#c$a%C^b&h*K(e)K_")
        'Dim bytIV() As Byte = System.Text.Encoding.UTF8.GetBytes( _
        '    "rgY^p$b%")
        'Dsn=ProgressODBC;uid=syssetiadi;pwd=syspronuri
        'Connection = New OdbcConnection("DSN=ProgressODBC;HOST=localhost;PORT=50280;DB=Hariff;UID=syssetiadi;PWD=syspronuri")
        'Dim string_s As String
        'string_s = System.Net.Dns.GetHostName
        'string_s = string_s + "\sqlexpress"
        'Connection = New SqlConnection( _
        '        "Data Source = " & "10.1.1.254" & ";" & _
        '        "Database = mfgpro" & ";" & _
        '        "User ID = syssetiadi" & ";" & _
        '        "Password = syspro" & ";" & _
        '        "Connection Timeout = 0" & ";")

        'Connection = New nPgSqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=dbadmin;Database=sdm;unicode=true")
        Connection = New nPgSqlConnection("Server=192.50.1.3;Port=5432;User Id=postgres;Password=syspronuri;Database=sdm;unicode=true")

        ' providerName="CoreLab.PostgreSql" />
        'Connection = New SqlConnection( _
        '        "Data Source = " & "localhost" & ";" & _
        '        "Database = mfgpro " & ";" & _
        '        "User ID = syssetiadi" & ";" & _
        '        "Password = syspro" & ";")


        'Using objCrypto As New WDACrypto(bytKey, bytIV)
        '    Try
        '        'Open the registry key
        '        'objReg = Registry.LocalMachine.OpenSubKey("SOFTWARE\" & _
        '        '   "syspro" & "\" & "JakartaBan" & "\Database", False)

        '        'Build the SQL connection string from the registry values
        '        'and initialize the Connection object
        '        'Connection = New SqlConnection( _
        '        '"Data Source=" & objReg.GetValue("Server") & ";" & _
        '        '"Database=" & objReg.GetValue("Database") & ";" & _
        '        '"User ID=" & objCrypto.Decrypt( _
        '        '    objReg.GetValue("Login")) & ";" & _
        '        '"Password=" & objCrypto.Decrypt( _
        '        '    objReg.GetValue("Password")) & ";")
        '        Connection = New SqlConnection( _
        '        "Data Source = syspro" & ";" & _
        '        "Database = JakartaBan-AVG " & ";" & _
        '        "User ID = syssetiadi" & ";" & _
        '        "Password = syspro" & ";")

        '    Catch ExceptionErr As Exception
        '        Throw New System.Exception(ExceptionErr.Message, _
        '            ExceptionErr.InnerException)
        '    Finally
        '        'Clean up
        '        objReg = Nothing
        '    End Try
        'End Using
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
        Catch OleDbExceptionErr As SqlException
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
                Command = New nPgSqlCommand(SQL, Connection)
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
            Catch OleDbExceptionErr As SqlException
                Throw New System.Exception(OleDbExceptionErr.Message, _
                    OleDbExceptionErr.InnerException)
            End Try
        End If
    End Sub

    Public Sub AddParameter(ByVal Name As String, ByVal Value As Object, ByVal size As Integer)
        Try
            Command.Parameters.Add(New NpgsqlParameter(Name, NpgsqlTypes.NpgsqlDbType.Varchar, 8000))
            Command.Parameters(Name).Value = Value
        Catch OleDbExceptionErr As SqlException
            Throw New System.Exception(OleDbExceptionErr.Message, _
                OleDbExceptionErr.InnerException)
        End Try
    End Sub

    Public Sub AddParameter(ByVal Name As String, ByVal Value As Object)
        Try
            Command.Parameters.Add(New nPgSqlParameter(Name, Value))
        Catch OleDbExceptionErr As SqlException
            Throw New System.Exception(OleDbExceptionErr.Message, _
                OleDbExceptionErr.InnerException)
        End Try
    End Sub

    Public Sub InitializeDataAdapter()
        Try
            DataAdapter = New nPgSqlDataAdapter
            DataAdapter.SelectCommand = Command
        Catch OleDbExceptionErr As SqlException
            Throw New System.Exception(OleDbExceptionErr.Message, _
            OleDbExceptionErr.InnerException)
        End Try
    End Sub

    Public Sub FillDataSet(ByRef oDataSet As DataSet, ByVal TableName As String)
        Try
            InitializeCommand()
            InitializeDataAdapter()
            DataAdapter.Fill(oDataSet, TableName)
        Catch OleDbExceptionErr As SqlException
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
        Catch OleDbExceptionErr As SqlException
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
            ExecuteStoredProcedure = Command.ExecuteNonQuery
            'Catch ExceptionErr As Exception
        Catch ExceptionErr As nPgSqlException
            Throw New System.Exception(ExceptionErr.Message, _
                ExceptionErr.InnerException)
        Finally
            CloseConnection()
        End Try
    End Function
End Class
