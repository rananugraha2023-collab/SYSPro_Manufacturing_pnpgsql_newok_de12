<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptCalculateCost
    Inherits DevExpress.XtraReports.UI.XtraReport

    'XtraReport overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Designer
    'It can be modified using the Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptCalculateCost))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand
        Me.OdbcSelectCommand1 = New System.Data.Odbc.OdbcCommand
        Me.OdbcConnection1 = New System.Data.Odbc.OdbcConnection
        Me.OdbcDataAdapter1 = New System.Data.Odbc.OdbcDataAdapter
        'Me.DsCalculateCost1 = New sygma_solution_system.DsCalculateCost
        Me.cf_opsi_isi = New DevExpress.XtraReports.UI.CalculatedField
        Me.cf_opsi_sb = New DevExpress.XtraReports.UI.CalculatedField
        Me.cf_opsi_cv = New DevExpress.XtraReports.UI.CalculatedField
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand
        Me.XrSubreport2 = New DevExpress.XtraReports.UI.XRSubreport
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrSubreport1 = New DevExpress.XtraReports.UI.XRSubreport
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrTable2 = New DevExpress.XtraReports.UI.XRTable
        Me.XrTableRow6 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell16 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell17 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell18 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell31 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell36 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell41 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell46 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell51 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell56 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow7 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell19 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell20 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell21 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell32 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell37 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell42 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell47 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell52 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell57 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow8 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell22 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell23 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell24 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell33 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell38 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell43 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell48 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell53 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell58 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow9 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell25 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell26 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell27 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell34 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell39 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell44 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell49 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell54 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell59 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow10 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell28 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell29 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell30 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell35 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell40 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell45 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell50 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell55 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell60 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow11 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell61 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell62 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell63 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell64 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell65 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell66 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell67 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell68 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell69 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow13 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell79 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell80 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell81 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell82 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell83 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell84 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell85 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell86 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell87 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow12 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell70 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell71 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell72 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell73 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell74 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell75 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell76 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell77 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell78 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow14 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell88 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell89 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell90 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell91 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell92 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell93 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell94 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell95 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell96 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow15 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell97 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell98 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell99 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell100 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell101 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell102 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell103 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell104 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell105 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow16 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell106 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell107 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell108 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell109 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell110 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell111 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell112 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell113 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell114 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow17 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell115 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell116 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell117 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell118 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell119 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell120 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell121 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell122 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell123 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow18 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell124 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell125 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell126 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell127 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell128 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell129 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell130 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell131 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell132 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow19 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell133 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell134 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell135 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell136 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell137 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell138 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell139 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell140 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell141 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow21 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell151 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell152 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell153 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell154 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell155 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell156 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell157 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell158 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell159 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow20 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell142 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell143 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell144 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell145 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell146 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell147 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell148 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell149 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell150 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow22 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell160 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell161 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell162 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell163 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell164 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell165 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell166 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell167 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell168 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow23 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell169 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell170 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell171 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell172 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell173 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell174 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell175 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell176 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell177 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow24 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell178 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell179 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell180 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell181 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell182 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell183 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell184 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell185 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell186 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow25 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell187 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell188 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell189 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell190 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell191 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell192 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell193 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell194 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell195 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow26 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell196 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell197 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell198 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell199 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell200 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell201 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell202 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell203 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell204 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow28 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell214 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell215 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell216 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell217 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell218 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell219 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell220 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell221 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell222 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow27 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell205 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell206 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell207 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell208 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell209 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell210 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell211 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell212 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell213 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow29 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell223 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell224 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell225 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell226 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell227 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell228 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell229 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell230 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell231 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow30 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell232 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell233 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell234 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell235 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell236 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell237 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell238 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell239 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell240 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow31 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell241 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell242 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell243 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell244 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell245 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell246 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell247 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell248 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell249 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow32 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell250 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell251 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell252 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell253 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell254 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell255 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell256 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell257 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell258 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrTable1 = New DevExpress.XtraReports.UI.XRTable
        Me.XrTableRow5 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell13 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell14 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell15 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow1 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell1 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell2 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell3 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow2 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell4 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell5 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell6 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow3 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell7 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell8 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell9 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow4 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell10 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell11 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell12 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrSubreport3 = New DevExpress.XtraReports.UI.XRSubreport
        Me.cf_total = New DevExpress.XtraReports.UI.CalculatedField
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrSubreport4 = New DevExpress.XtraReports.UI.XRSubreport
        Me.XrLabel8 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrTable3 = New DevExpress.XtraReports.UI.XRTable
        Me.XrTableRow33 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell259 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell260 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell261 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow34 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell262 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell263 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell264 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow35 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell265 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell266 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell267 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow36 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell268 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell269 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell270 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow37 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell271 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell272 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell273 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell274 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell275 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow38 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell276 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell277 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell278 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow39 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell279 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell280 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell281 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell282 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell283 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell284 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell285 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow40 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell286 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell291 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell292 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrLabel9 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo
        Me.XrPageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo
        'CType(Me.DsCalculateCost1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Dpi = 254.0!
        Me.Detail.Height = 0
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'PageHeader
        '
        Me.PageHeader.Dpi = 254.0!
        Me.PageHeader.Height = 0
        Me.PageHeader.Name = "PageHeader"
        Me.PageHeader.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel9, Me.XrPageInfo1, Me.XrPageInfo2})
        Me.PageFooter.Dpi = 254.0!
        Me.PageFooter.Height = 48
        Me.PageFooter.Name = "PageFooter"
        Me.PageFooter.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'OdbcSelectCommand1
        '
        Me.OdbcSelectCommand1.CommandText = resources.GetString("OdbcSelectCommand1.CommandText")
        Me.OdbcSelectCommand1.Connection = Me.OdbcConnection1
        '
        'OdbcConnection1
        '
        Me.OdbcConnection1.ConnectionString = "Dsn=syspro_seg"
        '
        'OdbcDataAdapter1
        '
        Me.OdbcDataAdapter1.SelectCommand = Me.OdbcSelectCommand1
        Me.OdbcDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "Table", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("calc_oid", "calc_oid"), New System.Data.Common.DataColumnMapping("calc_code", "calc_code"), New System.Data.Common.DataColumnMapping("calc_en_id", "calc_en_id"), New System.Data.Common.DataColumnMapping("en_desc", "en_desc"), New System.Data.Common.DataColumnMapping("calc_date", "calc_date"), New System.Data.Common.DataColumnMapping("calc_judul", "calc_judul"), New System.Data.Common.DataColumnMapping("calc_jns_buku", "calc_jns_buku"), New System.Data.Common.DataColumnMapping("calc_remarks", "calc_remarks"), New System.Data.Common.DataColumnMapping("calc_oplah", "calc_oplah"), New System.Data.Common.DataColumnMapping("calc_ukuran", "calc_ukuran"), New System.Data.Common.DataColumnMapping("calc_biaya_produksi", "calc_biaya_produksi"), New System.Data.Common.DataColumnMapping("calc_biaya_per_buku", "calc_biaya_per_buku"), New System.Data.Common.DataColumnMapping("calc_panjang", "calc_panjang"), New System.Data.Common.DataColumnMapping("calc_lebar", "calc_lebar"), New System.Data.Common.DataColumnMapping("calc_berat", "calc_berat"), New System.Data.Common.DataColumnMapping("calc_isi_jml", "calc_isi_jml"), New System.Data.Common.DataColumnMapping("calc_isi_opsi", "calc_isi_opsi"), New System.Data.Common.DataColumnMapping("calc_isi_bahan", "calc_isi_bahan"), New System.Data.Common.DataColumnMapping("calc_isi_bhn_qty", "calc_isi_bhn_qty"), New System.Data.Common.DataColumnMapping("calc_isi_panjang", "calc_isi_panjang"), New System.Data.Common.DataColumnMapping("calc_isi_lebar", "calc_isi_lebar"), New System.Data.Common.DataColumnMapping("calc_isi_jml_warna", "calc_isi_jml_warna"), New System.Data.Common.DataColumnMapping("calc_isi_insheet", "calc_isi_insheet"), New System.Data.Common.DataColumnMapping("calc_isi_naik_cetak", "calc_isi_naik_cetak"), New System.Data.Common.DataColumnMapping("calc_jns_mesin", "calc_jns_mesin"), New System.Data.Common.DataColumnMapping("calc_isi_kbthn_bahan", "calc_isi_kbthn_bahan"), New System.Data.Common.DataColumnMapping("calc_isi_nilai", "calc_isi_nilai"), New System.Data.Common.DataColumnMapping("calc_isi_outsource_opsi", "calc_isi_outsource_opsi"), New System.Data.Common.DataColumnMapping("calc_isi_outsource_nilai", "calc_isi_outsource_nilai"), New System.Data.Common.DataColumnMapping("calc_sb_jml_design", "calc_sb_jml_design"), New System.Data.Common.DataColumnMapping("calc_sb_opsi", "calc_sb_opsi"), New System.Data.Common.DataColumnMapping("calc_sb_panjang", "calc_sb_panjang"), New System.Data.Common.DataColumnMapping("calc_sb_lebar", "calc_sb_lebar"), New System.Data.Common.DataColumnMapping("calc_sb_jns_bahan", "calc_sb_jns_bahan"), New System.Data.Common.DataColumnMapping("calc_sb_bahan_qty", "calc_sb_bahan_qty"), New System.Data.Common.DataColumnMapping("calc_sb_bhn_panjang", "calc_sb_bhn_panjang"), New System.Data.Common.DataColumnMapping("calc_sb_bhn_lebar", "calc_sb_bhn_lebar"), New System.Data.Common.DataColumnMapping("calc_sb_jml_warna", "calc_sb_jml_warna"), New System.Data.Common.DataColumnMapping("calc_sb_insheet", "calc_sb_insheet"), New System.Data.Common.DataColumnMapping("calc_sb_naik_cetak", "calc_sb_naik_cetak"), New System.Data.Common.DataColumnMapping("calc_sb_berat", "calc_sb_berat"), New System.Data.Common.DataColumnMapping("calc_sb_bhn_qty", "calc_sb_bhn_qty"), New System.Data.Common.DataColumnMapping("calc_sb_jns_mesin", "calc_sb_jns_mesin"), New System.Data.Common.DataColumnMapping("calc_sb_nilai", "calc_sb_nilai"), New System.Data.Common.DataColumnMapping("calc_sb_outsource_opsi", "calc_sb_outsource_opsi"), New System.Data.Common.DataColumnMapping("calc_sb_outsource_nilai", "calc_sb_outsource_nilai"), New System.Data.Common.DataColumnMapping("calc_cv_jml_design", "calc_cv_jml_design"), New System.Data.Common.DataColumnMapping("calc_cv_opsi", "calc_cv_opsi"), New System.Data.Common.DataColumnMapping("calc_cv_panjang", "calc_cv_panjang"), New System.Data.Common.DataColumnMapping("calc_cv_lebar", "calc_cv_lebar"), New System.Data.Common.DataColumnMapping("calc_cv_jns_bahan", "calc_cv_jns_bahan"), New System.Data.Common.DataColumnMapping("calc_cv_bahan_qty", "calc_cv_bahan_qty"), New System.Data.Common.DataColumnMapping("calc_cv_bhn_panjang", "calc_cv_bhn_panjang"), New System.Data.Common.DataColumnMapping("calc_cv_bhn_lebar", "calc_cv_bhn_lebar"), New System.Data.Common.DataColumnMapping("calc_cv_jml_warna", "calc_cv_jml_warna"), New System.Data.Common.DataColumnMapping("calc_cv_insheet", "calc_cv_insheet"), New System.Data.Common.DataColumnMapping("calc_cv_naik_cetak", "calc_cv_naik_cetak"), New System.Data.Common.DataColumnMapping("calc_cv_berat", "calc_cv_berat"), New System.Data.Common.DataColumnMapping("calc_cv_kbthn_bhn_qty", "calc_cv_kbthn_bhn_qty"), New System.Data.Common.DataColumnMapping("calc_cv_jns_mesin", "calc_cv_jns_mesin"), New System.Data.Common.DataColumnMapping("calc_cv_nilai", "calc_cv_nilai"), New System.Data.Common.DataColumnMapping("calc_cv_outsource_opsi", "calc_cv_outsource_opsi"), New System.Data.Common.DataColumnMapping("calc_cv_outsource_nilai", "calc_cv_outsource_nilai"), New System.Data.Common.DataColumnMapping("calc_kr_jns_bahan", "calc_kr_jns_bahan"), New System.Data.Common.DataColumnMapping("calc_kr_jns_bhn_qty", "calc_kr_jns_bhn_qty"), New System.Data.Common.DataColumnMapping("calc_kr_opsi", "calc_kr_opsi"), New System.Data.Common.DataColumnMapping("calc_kr_bhn_panjang", "calc_kr_bhn_panjang"), New System.Data.Common.DataColumnMapping("calc_kr_bhn_lebar", "calc_kr_bhn_lebar"), New System.Data.Common.DataColumnMapping("calc_kr_insheet", "calc_kr_insheet"), New System.Data.Common.DataColumnMapping("calc_kr_bhn_qty", "calc_kr_bhn_qty"), New System.Data.Common.DataColumnMapping("calc_kr_nilai", "calc_kr_nilai"), New System.Data.Common.DataColumnMapping("calc_kr_outsource_opsi", "calc_kr_outsource_opsi"), New System.Data.Common.DataColumnMapping("calc_kr_outsource_nilai", "calc_kr_outsource_nilai"), New System.Data.Common.DataColumnMapping("calc_pra_opsi", "calc_pra_opsi"), New System.Data.Common.DataColumnMapping("calc_opsi_isi", "calc_opsi_isi"), New System.Data.Common.DataColumnMapping("calc_biaya_produksi2", "calc_biaya_produksi2"), New System.Data.Common.DataColumnMapping("calc_margin", "calc_margin"), New System.Data.Common.DataColumnMapping("calc_nilai_margin", "calc_nilai_margin"), New System.Data.Common.DataColumnMapping("calc_biaya_produksi_nilai_margin", "calc_biaya_produksi_nilai_margin"), New System.Data.Common.DataColumnMapping("calc_ppn", "calc_ppn"), New System.Data.Common.DataColumnMapping("calc_nilai_ppn", "calc_nilai_ppn"), New System.Data.Common.DataColumnMapping("calc_biaya_produksi_nilai_margin_nilai_ppn", "calc_biaya_produksi_nilai_margin_nilai_ppn"), New System.Data.Common.DataColumnMapping("calc_biaya_cetak_pcs", "calc_biaya_cetak_pcs"), New System.Data.Common.DataColumnMapping("calc_harga_jaket", "calc_harga_jaket"), New System.Data.Common.DataColumnMapping("calc_margin_jaket", "calc_margin_jaket"), New System.Data.Common.DataColumnMapping("calc_nilai_jaket", "calc_nilai_jaket"), New System.Data.Common.DataColumnMapping("calc_biaya_cetak_final", "calc_biaya_cetak_final"), New System.Data.Common.DataColumnMapping("calc_add_by", "calc_add_by"), New System.Data.Common.DataColumnMapping("calc_add_date", "calc_add_date"), New System.Data.Common.DataColumnMapping("calc_upd_by", "calc_upd_by"), New System.Data.Common.DataColumnMapping("calc_upd_date", "calc_upd_date")})})
        '
        'DsCalculateCost1
        '
        'Me.DsCalculateCost1.DataSetName = "DsCalculateCost"
        'Me.DsCalculateCost1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'cf_opsi_isi
        '
        Me.cf_opsi_isi.DataMember = "Table"
        'Me.cf_opsi_isi.DataSource = Me.DsCalculateCost1
        Me.cf_opsi_isi.Expression = "Iif([calc_isi_opsi]  == 1, 'Y' ,'T' )"
        Me.cf_opsi_isi.Name = "cf_opsi_isi"
        '
        'cf_opsi_sb
        '
        Me.cf_opsi_sb.DataMember = "Table"
        'Me.cf_opsi_sb.DataSource = Me.DsCalculateCost1
        Me.cf_opsi_sb.Expression = "Iif([calc_sb_opsi]  == 1, 'Y' ,'T' )"
        Me.cf_opsi_sb.Name = "cf_opsi_sb"
        '
        'cf_opsi_cv
        '
        Me.cf_opsi_cv.DataMember = "Table"
        'Me.cf_opsi_cv.DataSource = Me.DsCalculateCost1
        Me.cf_opsi_cv.Expression = "Iif([calc_cv_opsi]  == 1, 'Y' ,'T' )"
        Me.cf_opsi_cv.Name = "cf_opsi_cv"
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrSubreport2, Me.XrLabel4, Me.XrSubreport1, Me.XrLabel3, Me.XrTable2, Me.XrLabel2, Me.XrLabel1, Me.XrTable1, Me.XrLabel5, Me.XrSubreport3, Me.XrLabel6, Me.XrLabel7, Me.XrSubreport4, Me.XrLabel8, Me.XrTable3})
        Me.ReportHeader.Dpi = 254.0!
        Me.ReportHeader.Height = 2797
        Me.ReportHeader.Name = "ReportHeader"
        '
        'XrSubreport2
        '
        Me.XrSubreport2.Dpi = 254.0!
        Me.XrSubreport2.Location = New System.Drawing.Point(0, 2096)
        Me.XrSubreport2.Name = "XrSubreport2"
        Me.XrSubreport2.Size = New System.Drawing.Size(1894, 21)
        '
        'XrLabel4
        '
        Me.XrLabel4.Dpi = 254.0!
        Me.XrLabel4.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel4.Location = New System.Drawing.Point(455, 2043)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel4.Size = New System.Drawing.Size(984, 42)
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.StylePriority.UseTextAlignment = False
        Me.XrLabel4.Text = "CETAK"
        Me.XrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrSubreport1
        '
        Me.XrSubreport1.Dpi = 254.0!
        Me.XrSubreport1.Location = New System.Drawing.Point(0, 1979)
        Me.XrSubreport1.Name = "XrSubreport1"
        Me.XrSubreport1.Size = New System.Drawing.Size(1894, 21)
        '
        'XrLabel3
        '
        Me.XrLabel3.Dpi = 254.0!
        Me.XrLabel3.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel3.Location = New System.Drawing.Point(455, 1926)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel3.Size = New System.Drawing.Size(984, 42)
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.StylePriority.UseTextAlignment = False
        Me.XrLabel3.Text = "PRACETAK"
        Me.XrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrTable2
        '
        Me.XrTable2.Dpi = 254.0!
        Me.XrTable2.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTable2.Location = New System.Drawing.Point(0, 445)
        Me.XrTable2.Name = "XrTable2"
        Me.XrTable2.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow6, Me.XrTableRow7, Me.XrTableRow8, Me.XrTableRow9, Me.XrTableRow10, Me.XrTableRow11, Me.XrTableRow13, Me.XrTableRow12, Me.XrTableRow14, Me.XrTableRow15, Me.XrTableRow16, Me.XrTableRow17, Me.XrTableRow18, Me.XrTableRow19, Me.XrTableRow21, Me.XrTableRow20, Me.XrTableRow22, Me.XrTableRow23, Me.XrTableRow24, Me.XrTableRow25, Me.XrTableRow26, Me.XrTableRow28, Me.XrTableRow27, Me.XrTableRow29, Me.XrTableRow30, Me.XrTableRow31, Me.XrTableRow32})
        Me.XrTable2.Size = New System.Drawing.Size(1873, 1342)
        Me.XrTable2.StylePriority.UseFont = False
        '
        'XrTableRow6
        '
        Me.XrTableRow6.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell16, Me.XrTableCell17, Me.XrTableCell18, Me.XrTableCell31, Me.XrTableCell36, Me.XrTableCell41, Me.XrTableCell46, Me.XrTableCell51, Me.XrTableCell56})
        Me.XrTableRow6.Dpi = 254.0!
        Me.XrTableRow6.Name = "XrTableRow6"
        Me.XrTableRow6.Weight = 0.79245283018867929
        '
        'XrTableCell16
        '
        Me.XrTableCell16.Dpi = 254.0!
        Me.XrTableCell16.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCell16.Name = "XrTableCell16"
        Me.XrTableCell16.StylePriority.UseFont = False
        Me.XrTableCell16.Text = "Spesifikasi Umum"
        Me.XrTableCell16.Weight = 0.49571962216054355
        '
        'XrTableCell17
        '
        Me.XrTableCell17.Dpi = 254.0!
        Me.XrTableCell17.Name = "XrTableCell17"
        Me.XrTableCell17.StylePriority.UseTextAlignment = False
        Me.XrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell17.Weight = 0.079791868633959345
        '
        'XrTableCell18
        '
        Me.XrTableCell18.Dpi = 254.0!
        Me.XrTableCell18.Name = "XrTableCell18"
        Me.XrTableCell18.Weight = 0.32147920794879936
        '
        'XrTableCell31
        '
        Me.XrTableCell31.Dpi = 254.0!
        Me.XrTableCell31.Name = "XrTableCell31"
        Me.XrTableCell31.Weight = 0.29286409763295734
        '
        'XrTableCell36
        '
        Me.XrTableCell36.Dpi = 254.0!
        Me.XrTableCell36.Name = "XrTableCell36"
        Me.XrTableCell36.Weight = 0.4384827400607183
        '
        'XrTableCell41
        '
        Me.XrTableCell41.Dpi = 254.0!
        Me.XrTableCell41.Name = "XrTableCell41"
        Me.XrTableCell41.Weight = 0.40292485668647893
        '
        'XrTableCell46
        '
        Me.XrTableCell46.Dpi = 254.0!
        Me.XrTableCell46.Name = "XrTableCell46"
        Me.XrTableCell46.Weight = 0.29043844208201086
        '
        'XrTableCell51
        '
        Me.XrTableCell51.Dpi = 254.0!
        Me.XrTableCell51.Name = "XrTableCell51"
        Me.XrTableCell51.Weight = 0.288695764992034
        '
        'XrTableCell56
        '
        Me.XrTableCell56.Dpi = 254.0!
        Me.XrTableCell56.Name = "XrTableCell56"
        Me.XrTableCell56.StylePriority.UseTextAlignment = False
        Me.XrTableCell56.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell56.Weight = 0.38960339980249858
        '
        'XrTableRow7
        '
        Me.XrTableRow7.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
        Me.XrTableRow7.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell19, Me.XrTableCell20, Me.XrTableCell21, Me.XrTableCell32, Me.XrTableCell37, Me.XrTableCell42, Me.XrTableCell47, Me.XrTableCell52, Me.XrTableCell57})
        Me.XrTableRow7.Dpi = 254.0!
        Me.XrTableRow7.Name = "XrTableRow7"
        Me.XrTableRow7.StylePriority.UseBorders = False
        Me.XrTableRow7.Weight = 0.94339622641509435
        '
        'XrTableCell19
        '
        Me.XrTableCell19.Dpi = 254.0!
        Me.XrTableCell19.Name = "XrTableCell19"
        Me.XrTableCell19.Text = "Ukuran Buku"
        Me.XrTableCell19.Weight = 0.49571962216054355
        '
        'XrTableCell20
        '
        Me.XrTableCell20.Dpi = 254.0!
        Me.XrTableCell20.Name = "XrTableCell20"
        Me.XrTableCell20.StylePriority.UseTextAlignment = False
        Me.XrTableCell20.Text = ":"
        Me.XrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell20.Weight = 0.079791868633959345
        '
        'XrTableCell21
        '
        Me.XrTableCell21.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_panjang", "")})
        Me.XrTableCell21.Dpi = 254.0!
        Me.XrTableCell21.Name = "XrTableCell21"
        Me.XrTableCell21.Text = "XrTableCell3"
        Me.XrTableCell21.Weight = 0.32147920794879936
        '
        'XrTableCell32
        '
        Me.XrTableCell32.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_lebar", "")})
        Me.XrTableCell32.Dpi = 254.0!
        Me.XrTableCell32.Name = "XrTableCell32"
        Me.XrTableCell32.Text = "XrTableCell32"
        Me.XrTableCell32.Weight = 0.29286409763295734
        '
        'XrTableCell37
        '
        Me.XrTableCell37.Dpi = 254.0!
        Me.XrTableCell37.Name = "XrTableCell37"
        Me.XrTableCell37.Text = "Cm (tertutup)"
        Me.XrTableCell37.Weight = 0.4384827400607183
        '
        'XrTableCell42
        '
        Me.XrTableCell42.Dpi = 254.0!
        Me.XrTableCell42.Name = "XrTableCell42"
        Me.XrTableCell42.Text = "Berat 1 Buku"
        Me.XrTableCell42.Weight = 0.40292485668647893
        '
        'XrTableCell47
        '
        Me.XrTableCell47.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_berat", "")})
        Me.XrTableCell47.Dpi = 254.0!
        Me.XrTableCell47.Name = "XrTableCell47"
        Me.XrTableCell47.Text = "XrTableCell47"
        Me.XrTableCell47.Weight = 0.29043844208201086
        '
        'XrTableCell52
        '
        Me.XrTableCell52.Dpi = 254.0!
        Me.XrTableCell52.Name = "XrTableCell52"
        Me.XrTableCell52.Text = "Gram"
        Me.XrTableCell52.Weight = 0.288695764992034
        '
        'XrTableCell57
        '
        Me.XrTableCell57.Dpi = 254.0!
        Me.XrTableCell57.Name = "XrTableCell57"
        Me.XrTableCell57.StylePriority.UseTextAlignment = False
        Me.XrTableCell57.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell57.Weight = 0.38960339980249858
        '
        'XrTableRow8
        '
        Me.XrTableRow8.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell22, Me.XrTableCell23, Me.XrTableCell24, Me.XrTableCell33, Me.XrTableCell38, Me.XrTableCell43, Me.XrTableCell48, Me.XrTableCell53, Me.XrTableCell58})
        Me.XrTableRow8.Dpi = 254.0!
        Me.XrTableRow8.Name = "XrTableRow8"
        Me.XrTableRow8.Weight = 0.94339622641509435
        '
        'XrTableCell22
        '
        Me.XrTableCell22.Dpi = 254.0!
        Me.XrTableCell22.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCell22.Name = "XrTableCell22"
        Me.XrTableCell22.StylePriority.UseFont = False
        Me.XrTableCell22.Text = "Isi"
        Me.XrTableCell22.Weight = 0.49571962216054355
        '
        'XrTableCell23
        '
        Me.XrTableCell23.Dpi = 254.0!
        Me.XrTableCell23.Name = "XrTableCell23"
        Me.XrTableCell23.StylePriority.UseTextAlignment = False
        Me.XrTableCell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell23.Weight = 0.079791868633959345
        '
        'XrTableCell24
        '
        Me.XrTableCell24.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.cf_opsi_isi", "")})
        Me.XrTableCell24.Dpi = 254.0!
        Me.XrTableCell24.Name = "XrTableCell24"
        Me.XrTableCell24.Weight = 0.32147920794879936
        '
        'XrTableCell33
        '
        Me.XrTableCell33.Dpi = 254.0!
        Me.XrTableCell33.Name = "XrTableCell33"
        Me.XrTableCell33.Weight = 0.29286409763295734
        '
        'XrTableCell38
        '
        Me.XrTableCell38.Dpi = 254.0!
        Me.XrTableCell38.Name = "XrTableCell38"
        Me.XrTableCell38.Weight = 0.4384827400607183
        '
        'XrTableCell43
        '
        Me.XrTableCell43.Dpi = 254.0!
        Me.XrTableCell43.Name = "XrTableCell43"
        Me.XrTableCell43.Weight = 0.40292485668647893
        '
        'XrTableCell48
        '
        Me.XrTableCell48.Dpi = 254.0!
        Me.XrTableCell48.Name = "XrTableCell48"
        Me.XrTableCell48.Weight = 0.29043844208201086
        '
        'XrTableCell53
        '
        Me.XrTableCell53.Dpi = 254.0!
        Me.XrTableCell53.Name = "XrTableCell53"
        Me.XrTableCell53.Weight = 0.288695764992034
        '
        'XrTableCell58
        '
        Me.XrTableCell58.Dpi = 254.0!
        Me.XrTableCell58.Name = "XrTableCell58"
        Me.XrTableCell58.StylePriority.UseTextAlignment = False
        Me.XrTableCell58.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell58.Weight = 0.38960339980249858
        '
        'XrTableRow9
        '
        Me.XrTableRow9.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell25, Me.XrTableCell26, Me.XrTableCell27, Me.XrTableCell34, Me.XrTableCell39, Me.XrTableCell44, Me.XrTableCell49, Me.XrTableCell54, Me.XrTableCell59})
        Me.XrTableRow9.Dpi = 254.0!
        Me.XrTableRow9.Name = "XrTableRow9"
        Me.XrTableRow9.Weight = 0.94339622641509424
        '
        'XrTableCell25
        '
        Me.XrTableCell25.Dpi = 254.0!
        Me.XrTableCell25.Name = "XrTableCell25"
        Me.XrTableCell25.Text = "Jumlah Halaman"
        Me.XrTableCell25.Weight = 0.49571962216054355
        '
        'XrTableCell26
        '
        Me.XrTableCell26.Dpi = 254.0!
        Me.XrTableCell26.Name = "XrTableCell26"
        Me.XrTableCell26.StylePriority.UseTextAlignment = False
        Me.XrTableCell26.Text = ":"
        Me.XrTableCell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell26.Weight = 0.079791868633959345
        '
        'XrTableCell27
        '
        Me.XrTableCell27.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_isi_jml", "")})
        Me.XrTableCell27.Dpi = 254.0!
        Me.XrTableCell27.Name = "XrTableCell27"
        Me.XrTableCell27.Text = "XrTableCell9"
        Me.XrTableCell27.Weight = 0.32147920794879936
        '
        'XrTableCell34
        '
        Me.XrTableCell34.Dpi = 254.0!
        Me.XrTableCell34.Name = "XrTableCell34"
        Me.XrTableCell34.Text = "Halaman"
        Me.XrTableCell34.Weight = 0.29286409763295734
        '
        'XrTableCell39
        '
        Me.XrTableCell39.Dpi = 254.0!
        Me.XrTableCell39.Name = "XrTableCell39"
        Me.XrTableCell39.Weight = 0.4384827400607183
        '
        'XrTableCell44
        '
        Me.XrTableCell44.Dpi = 254.0!
        Me.XrTableCell44.Name = "XrTableCell44"
        Me.XrTableCell44.Weight = 0.40292485668647893
        '
        'XrTableCell49
        '
        Me.XrTableCell49.Dpi = 254.0!
        Me.XrTableCell49.Name = "XrTableCell49"
        Me.XrTableCell49.Weight = 0.29043844208201086
        '
        'XrTableCell54
        '
        Me.XrTableCell54.Dpi = 254.0!
        Me.XrTableCell54.Name = "XrTableCell54"
        Me.XrTableCell54.Weight = 0.288695764992034
        '
        'XrTableCell59
        '
        Me.XrTableCell59.Dpi = 254.0!
        Me.XrTableCell59.Name = "XrTableCell59"
        Me.XrTableCell59.StylePriority.UseTextAlignment = False
        Me.XrTableCell59.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell59.Weight = 0.38960339980249858
        '
        'XrTableRow10
        '
        Me.XrTableRow10.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell28, Me.XrTableCell29, Me.XrTableCell30, Me.XrTableCell35, Me.XrTableCell40, Me.XrTableCell45, Me.XrTableCell50, Me.XrTableCell55, Me.XrTableCell60})
        Me.XrTableRow10.Dpi = 254.0!
        Me.XrTableRow10.Name = "XrTableRow10"
        Me.XrTableRow10.Weight = 0.94339622641509424
        '
        'XrTableCell28
        '
        Me.XrTableCell28.Dpi = 254.0!
        Me.XrTableCell28.Name = "XrTableCell28"
        Me.XrTableCell28.StylePriority.UseTextAlignment = False
        Me.XrTableCell28.Text = "Bahan"
        Me.XrTableCell28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell28.Weight = 0.49571962216054355
        '
        'XrTableCell29
        '
        Me.XrTableCell29.Dpi = 254.0!
        Me.XrTableCell29.Name = "XrTableCell29"
        Me.XrTableCell29.StylePriority.UseTextAlignment = False
        Me.XrTableCell29.Text = ":"
        Me.XrTableCell29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell29.Weight = 0.079791868633959345
        '
        'XrTableCell30
        '
        Me.XrTableCell30.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_isi_bahan", "")})
        Me.XrTableCell30.Dpi = 254.0!
        Me.XrTableCell30.Name = "XrTableCell30"
        Me.XrTableCell30.Text = "XrTableCell12"
        Me.XrTableCell30.Weight = 0.32147920794879936
        '
        'XrTableCell35
        '
        Me.XrTableCell35.Dpi = 254.0!
        Me.XrTableCell35.Name = "XrTableCell35"
        Me.XrTableCell35.Weight = 0.29286409763295734
        '
        'XrTableCell40
        '
        Me.XrTableCell40.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_isi_kbthn_bahan", "")})
        Me.XrTableCell40.Dpi = 254.0!
        Me.XrTableCell40.Name = "XrTableCell40"
        Me.XrTableCell40.Text = "XrTableCell40"
        Me.XrTableCell40.Weight = 0.4384827400607183
        '
        'XrTableCell45
        '
        Me.XrTableCell45.Dpi = 254.0!
        Me.XrTableCell45.Name = "XrTableCell45"
        Me.XrTableCell45.Text = "Gram/m2"
        Me.XrTableCell45.Weight = 0.40292485668647893
        '
        'XrTableCell50
        '
        Me.XrTableCell50.Dpi = 254.0!
        Me.XrTableCell50.Name = "XrTableCell50"
        Me.XrTableCell50.Weight = 0.29043844208201086
        '
        'XrTableCell55
        '
        Me.XrTableCell55.Dpi = 254.0!
        Me.XrTableCell55.Name = "XrTableCell55"
        Me.XrTableCell55.Text = "Harga"
        Me.XrTableCell55.Weight = 0.288695764992034
        '
        'XrTableCell60
        '
        Me.XrTableCell60.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_isi_nilai", "{0:n2}")})
        Me.XrTableCell60.Dpi = 254.0!
        Me.XrTableCell60.Name = "XrTableCell60"
        Me.XrTableCell60.StylePriority.UseTextAlignment = False
        Me.XrTableCell60.Text = "XrTableCell60"
        Me.XrTableCell60.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell60.Weight = 0.38960339980249858
        '
        'XrTableRow11
        '
        Me.XrTableRow11.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell61, Me.XrTableCell62, Me.XrTableCell63, Me.XrTableCell64, Me.XrTableCell65, Me.XrTableCell66, Me.XrTableCell67, Me.XrTableCell68, Me.XrTableCell69})
        Me.XrTableRow11.Dpi = 254.0!
        Me.XrTableRow11.Name = "XrTableRow11"
        Me.XrTableRow11.Weight = 0.94339622641509446
        '
        'XrTableCell61
        '
        Me.XrTableCell61.Dpi = 254.0!
        Me.XrTableCell61.Name = "XrTableCell61"
        Me.XrTableCell61.StylePriority.UseTextAlignment = False
        Me.XrTableCell61.Text = "Warna"
        Me.XrTableCell61.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell61.Weight = 0.49571962216054355
        '
        'XrTableCell62
        '
        Me.XrTableCell62.Dpi = 254.0!
        Me.XrTableCell62.Name = "XrTableCell62"
        Me.XrTableCell62.StylePriority.UseTextAlignment = False
        Me.XrTableCell62.Text = ":"
        Me.XrTableCell62.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell62.Weight = 0.079791868633959345
        '
        'XrTableCell63
        '
        Me.XrTableCell63.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_isi_jml_warna", "")})
        Me.XrTableCell63.Dpi = 254.0!
        Me.XrTableCell63.Name = "XrTableCell63"
        Me.XrTableCell63.Text = "XrTableCell63"
        Me.XrTableCell63.Weight = 0.32147920794879936
        '
        'XrTableCell64
        '
        Me.XrTableCell64.Dpi = 254.0!
        Me.XrTableCell64.Name = "XrTableCell64"
        Me.XrTableCell64.Weight = 0.29286409763295734
        '
        'XrTableCell65
        '
        Me.XrTableCell65.Dpi = 254.0!
        Me.XrTableCell65.Name = "XrTableCell65"
        Me.XrTableCell65.Text = "Ukuran"
        Me.XrTableCell65.Weight = 0.4384827400607183
        '
        'XrTableCell66
        '
        Me.XrTableCell66.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_isi_panjang", "{0:n2}")})
        Me.XrTableCell66.Dpi = 254.0!
        Me.XrTableCell66.Name = "XrTableCell66"
        Me.XrTableCell66.Text = "XrTableCell66"
        Me.XrTableCell66.Weight = 0.40292485668647893
        '
        'XrTableCell67
        '
        Me.XrTableCell67.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_isi_lebar", "")})
        Me.XrTableCell67.Dpi = 254.0!
        Me.XrTableCell67.Name = "XrTableCell67"
        Me.XrTableCell67.Text = "XrTableCell67"
        Me.XrTableCell67.Weight = 0.29043844208201086
        '
        'XrTableCell68
        '
        Me.XrTableCell68.Dpi = 254.0!
        Me.XrTableCell68.Name = "XrTableCell68"
        Me.XrTableCell68.Text = "Cm"
        Me.XrTableCell68.Weight = 0.288695764992034
        '
        'XrTableCell69
        '
        Me.XrTableCell69.Dpi = 254.0!
        Me.XrTableCell69.Name = "XrTableCell69"
        Me.XrTableCell69.StylePriority.UseTextAlignment = False
        Me.XrTableCell69.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell69.Weight = 0.38960339980249858
        '
        'XrTableRow13
        '
        Me.XrTableRow13.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell79, Me.XrTableCell80, Me.XrTableCell81, Me.XrTableCell82, Me.XrTableCell83, Me.XrTableCell84, Me.XrTableCell85, Me.XrTableCell86, Me.XrTableCell87})
        Me.XrTableRow13.Dpi = 254.0!
        Me.XrTableRow13.Name = "XrTableRow13"
        Me.XrTableRow13.Weight = 0.94339622641509424
        '
        'XrTableCell79
        '
        Me.XrTableCell79.Dpi = 254.0!
        Me.XrTableCell79.Name = "XrTableCell79"
        Me.XrTableCell79.Text = "Bahan + Insheet" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.XrTableCell79.Weight = 0.49571962216054355
        '
        'XrTableCell80
        '
        Me.XrTableCell80.Dpi = 254.0!
        Me.XrTableCell80.Name = "XrTableCell80"
        Me.XrTableCell80.StylePriority.UseTextAlignment = False
        Me.XrTableCell80.Text = ":"
        Me.XrTableCell80.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell80.Weight = 0.079791868633959345
        '
        'XrTableCell81
        '
        Me.XrTableCell81.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_isi_insheet", "")})
        Me.XrTableCell81.Dpi = 254.0!
        Me.XrTableCell81.Name = "XrTableCell81"
        Me.XrTableCell81.Text = "XrTableCell81"
        Me.XrTableCell81.Weight = 0.32147920794879936
        '
        'XrTableCell82
        '
        Me.XrTableCell82.Dpi = 254.0!
        Me.XrTableCell82.Name = "XrTableCell82"
        Me.XrTableCell82.Text = "%"
        Me.XrTableCell82.Weight = 0.29286409763295734
        '
        'XrTableCell83
        '
        Me.XrTableCell83.Dpi = 254.0!
        Me.XrTableCell83.Name = "XrTableCell83"
        Me.XrTableCell83.Text = "Kebutuhan Bhn"
        Me.XrTableCell83.Weight = 0.4384827400607183
        '
        'XrTableCell84
        '
        Me.XrTableCell84.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_isi_kbthn_bahan", "{0:n2}")})
        Me.XrTableCell84.Dpi = 254.0!
        Me.XrTableCell84.Name = "XrTableCell84"
        Me.XrTableCell84.Text = "XrTableCell84"
        Me.XrTableCell84.Weight = 0.40292485668647893
        '
        'XrTableCell85
        '
        Me.XrTableCell85.Dpi = 254.0!
        Me.XrTableCell85.Name = "XrTableCell85"
        Me.XrTableCell85.Text = "Rim"
        Me.XrTableCell85.Weight = 0.29043844208201086
        '
        'XrTableCell86
        '
        Me.XrTableCell86.Dpi = 254.0!
        Me.XrTableCell86.Name = "XrTableCell86"
        Me.XrTableCell86.Weight = 0.288695764992034
        '
        'XrTableCell87
        '
        Me.XrTableCell87.Dpi = 254.0!
        Me.XrTableCell87.Name = "XrTableCell87"
        Me.XrTableCell87.StylePriority.UseTextAlignment = False
        Me.XrTableCell87.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell87.Weight = 0.38960339980249858
        '
        'XrTableRow12
        '
        Me.XrTableRow12.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrTableRow12.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell70, Me.XrTableCell71, Me.XrTableCell72, Me.XrTableCell73, Me.XrTableCell74, Me.XrTableCell75, Me.XrTableCell76, Me.XrTableCell77, Me.XrTableCell78})
        Me.XrTableRow12.Dpi = 254.0!
        Me.XrTableRow12.Name = "XrTableRow12"
        Me.XrTableRow12.StylePriority.UseBorders = False
        Me.XrTableRow12.Weight = 0.94339622641509446
        '
        'XrTableCell70
        '
        Me.XrTableCell70.Dpi = 254.0!
        Me.XrTableCell70.Name = "XrTableCell70"
        Me.XrTableCell70.Text = "Naik Cetak"
        Me.XrTableCell70.Weight = 0.49571962216054355
        '
        'XrTableCell71
        '
        Me.XrTableCell71.Dpi = 254.0!
        Me.XrTableCell71.Name = "XrTableCell71"
        Me.XrTableCell71.StylePriority.UseTextAlignment = False
        Me.XrTableCell71.Text = ":"
        Me.XrTableCell71.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell71.Weight = 0.079791868633959345
        '
        'XrTableCell72
        '
        Me.XrTableCell72.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_isi_naik_cetak", "")})
        Me.XrTableCell72.Dpi = 254.0!
        Me.XrTableCell72.Name = "XrTableCell72"
        Me.XrTableCell72.Text = "XrTableCell72"
        Me.XrTableCell72.Weight = 0.32147920794879936
        '
        'XrTableCell73
        '
        Me.XrTableCell73.Dpi = 254.0!
        Me.XrTableCell73.Name = "XrTableCell73"
        Me.XrTableCell73.Weight = 0.29286409763295734
        '
        'XrTableCell74
        '
        Me.XrTableCell74.Dpi = 254.0!
        Me.XrTableCell74.Name = "XrTableCell74"
        Me.XrTableCell74.Text = "Jenis Mesin"
        Me.XrTableCell74.Weight = 0.4384827400607183
        '
        'XrTableCell75
        '
        Me.XrTableCell75.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_jns_mesin", "")})
        Me.XrTableCell75.Dpi = 254.0!
        Me.XrTableCell75.Name = "XrTableCell75"
        Me.XrTableCell75.Text = "XrTableCell75"
        Me.XrTableCell75.Weight = 0.40292485668647893
        '
        'XrTableCell76
        '
        Me.XrTableCell76.Dpi = 254.0!
        Me.XrTableCell76.Name = "XrTableCell76"
        Me.XrTableCell76.Weight = 0.29043844208201086
        '
        'XrTableCell77
        '
        Me.XrTableCell77.Dpi = 254.0!
        Me.XrTableCell77.Name = "XrTableCell77"
        Me.XrTableCell77.Weight = 0.288695764992034
        '
        'XrTableCell78
        '
        Me.XrTableCell78.Dpi = 254.0!
        Me.XrTableCell78.Name = "XrTableCell78"
        Me.XrTableCell78.StylePriority.UseTextAlignment = False
        Me.XrTableCell78.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell78.Weight = 0.38960339980249858
        '
        'XrTableRow14
        '
        Me.XrTableRow14.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
        Me.XrTableRow14.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell88, Me.XrTableCell89, Me.XrTableCell90, Me.XrTableCell91, Me.XrTableCell92, Me.XrTableCell93, Me.XrTableCell94, Me.XrTableCell95, Me.XrTableCell96})
        Me.XrTableRow14.Dpi = 254.0!
        Me.XrTableRow14.Name = "XrTableRow14"
        Me.XrTableRow14.StylePriority.UseBorders = False
        Me.XrTableRow14.Weight = 0.94339622641509424
        '
        'XrTableCell88
        '
        Me.XrTableCell88.Dpi = 254.0!
        Me.XrTableCell88.Name = "XrTableCell88"
        Me.XrTableCell88.Weight = 0.49571962216054355
        '
        'XrTableCell89
        '
        Me.XrTableCell89.Dpi = 254.0!
        Me.XrTableCell89.Name = "XrTableCell89"
        Me.XrTableCell89.Weight = 0.079791868633959345
        '
        'XrTableCell90
        '
        Me.XrTableCell90.Dpi = 254.0!
        Me.XrTableCell90.Name = "XrTableCell90"
        Me.XrTableCell90.Weight = 0.32147920794879936
        '
        'XrTableCell91
        '
        Me.XrTableCell91.Dpi = 254.0!
        Me.XrTableCell91.Name = "XrTableCell91"
        Me.XrTableCell91.Weight = 0.29286409763295734
        '
        'XrTableCell92
        '
        Me.XrTableCell92.Dpi = 254.0!
        Me.XrTableCell92.Name = "XrTableCell92"
        Me.XrTableCell92.Weight = 0.4384827400607183
        '
        'XrTableCell93
        '
        Me.XrTableCell93.Dpi = 254.0!
        Me.XrTableCell93.Name = "XrTableCell93"
        Me.XrTableCell93.Weight = 0.40292485668647893
        '
        'XrTableCell94
        '
        Me.XrTableCell94.Dpi = 254.0!
        Me.XrTableCell94.Name = "XrTableCell94"
        Me.XrTableCell94.Weight = 0.29043844208201086
        '
        'XrTableCell95
        '
        Me.XrTableCell95.Dpi = 254.0!
        Me.XrTableCell95.Name = "XrTableCell95"
        Me.XrTableCell95.Text = "Outsource"
        Me.XrTableCell95.Weight = 0.288695764992034
        '
        'XrTableCell96
        '
        Me.XrTableCell96.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_isi_outsource_nilai", "{0:n2}")})
        Me.XrTableCell96.Dpi = 254.0!
        Me.XrTableCell96.Name = "XrTableCell96"
        Me.XrTableCell96.StylePriority.UseTextAlignment = False
        Me.XrTableCell96.Text = "XrTableCell96"
        Me.XrTableCell96.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell96.Weight = 0.38960339980249858
        '
        'XrTableRow15
        '
        Me.XrTableRow15.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell97, Me.XrTableCell98, Me.XrTableCell99, Me.XrTableCell100, Me.XrTableCell101, Me.XrTableCell102, Me.XrTableCell103, Me.XrTableCell104, Me.XrTableCell105})
        Me.XrTableRow15.Dpi = 254.0!
        Me.XrTableRow15.Name = "XrTableRow15"
        Me.XrTableRow15.Weight = 0.94339622641509446
        '
        'XrTableCell97
        '
        Me.XrTableCell97.Dpi = 254.0!
        Me.XrTableCell97.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCell97.Name = "XrTableCell97"
        Me.XrTableCell97.StylePriority.UseFont = False
        Me.XrTableCell97.Text = "Skipblade"
        Me.XrTableCell97.Weight = 0.49571962216054355
        '
        'XrTableCell98
        '
        Me.XrTableCell98.Dpi = 254.0!
        Me.XrTableCell98.Name = "XrTableCell98"
        Me.XrTableCell98.Weight = 0.079791868633959345
        '
        'XrTableCell99
        '
        Me.XrTableCell99.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.cf_opsi_sb", "")})
        Me.XrTableCell99.Dpi = 254.0!
        Me.XrTableCell99.Name = "XrTableCell99"
        Me.XrTableCell99.Weight = 0.32147920794879936
        '
        'XrTableCell100
        '
        Me.XrTableCell100.Dpi = 254.0!
        Me.XrTableCell100.Name = "XrTableCell100"
        Me.XrTableCell100.Weight = 0.29286409763295734
        '
        'XrTableCell101
        '
        Me.XrTableCell101.Dpi = 254.0!
        Me.XrTableCell101.Name = "XrTableCell101"
        Me.XrTableCell101.Weight = 0.4384827400607183
        '
        'XrTableCell102
        '
        Me.XrTableCell102.Dpi = 254.0!
        Me.XrTableCell102.Name = "XrTableCell102"
        Me.XrTableCell102.Weight = 0.40292485668647893
        '
        'XrTableCell103
        '
        Me.XrTableCell103.Dpi = 254.0!
        Me.XrTableCell103.Name = "XrTableCell103"
        Me.XrTableCell103.Weight = 0.29043844208201086
        '
        'XrTableCell104
        '
        Me.XrTableCell104.Dpi = 254.0!
        Me.XrTableCell104.Name = "XrTableCell104"
        Me.XrTableCell104.Weight = 0.288695764992034
        '
        'XrTableCell105
        '
        Me.XrTableCell105.Dpi = 254.0!
        Me.XrTableCell105.Name = "XrTableCell105"
        Me.XrTableCell105.Weight = 0.38960339980249858
        '
        'XrTableRow16
        '
        Me.XrTableRow16.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell106, Me.XrTableCell107, Me.XrTableCell108, Me.XrTableCell109, Me.XrTableCell110, Me.XrTableCell111, Me.XrTableCell112, Me.XrTableCell113, Me.XrTableCell114})
        Me.XrTableRow16.Dpi = 254.0!
        Me.XrTableRow16.Name = "XrTableRow16"
        Me.XrTableRow16.Weight = 0.94339622641509413
        '
        'XrTableCell106
        '
        Me.XrTableCell106.Dpi = 254.0!
        Me.XrTableCell106.Name = "XrTableCell106"
        Me.XrTableCell106.Text = "Jumlah Design"
        Me.XrTableCell106.Weight = 0.49571962216054355
        '
        'XrTableCell107
        '
        Me.XrTableCell107.Dpi = 254.0!
        Me.XrTableCell107.Name = "XrTableCell107"
        Me.XrTableCell107.StylePriority.UseTextAlignment = False
        Me.XrTableCell107.Text = ":"
        Me.XrTableCell107.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell107.Weight = 0.079791868633959345
        '
        'XrTableCell108
        '
        Me.XrTableCell108.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_sb_jml_design", "")})
        Me.XrTableCell108.Dpi = 254.0!
        Me.XrTableCell108.Name = "XrTableCell108"
        Me.XrTableCell108.Text = "XrTableCell108"
        Me.XrTableCell108.Weight = 0.32147920794879936
        '
        'XrTableCell109
        '
        Me.XrTableCell109.Dpi = 254.0!
        Me.XrTableCell109.Name = "XrTableCell109"
        Me.XrTableCell109.Text = "Uk Terbuka"
        Me.XrTableCell109.Weight = 0.29286409763295734
        '
        'XrTableCell110
        '
        Me.XrTableCell110.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_sb_panjang", "")})
        Me.XrTableCell110.Dpi = 254.0!
        Me.XrTableCell110.Name = "XrTableCell110"
        Me.XrTableCell110.Weight = 0.4384827400607183
        '
        'XrTableCell111
        '
        Me.XrTableCell111.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_sb_lebar", "")})
        Me.XrTableCell111.Dpi = 254.0!
        Me.XrTableCell111.Name = "XrTableCell111"
        Me.XrTableCell111.Text = "XrTableCell111"
        Me.XrTableCell111.Weight = 0.40292485668647893
        '
        'XrTableCell112
        '
        Me.XrTableCell112.Dpi = 254.0!
        Me.XrTableCell112.Name = "XrTableCell112"
        Me.XrTableCell112.Text = "Berat"
        Me.XrTableCell112.Weight = 0.29043844208201086
        '
        'XrTableCell113
        '
        Me.XrTableCell113.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_sb_berat", "")})
        Me.XrTableCell113.Dpi = 254.0!
        Me.XrTableCell113.Name = "XrTableCell113"
        Me.XrTableCell113.Text = "XrTableCell113"
        Me.XrTableCell113.Weight = 0.288695764992034
        '
        'XrTableCell114
        '
        Me.XrTableCell114.Dpi = 254.0!
        Me.XrTableCell114.Name = "XrTableCell114"
        Me.XrTableCell114.Weight = 0.38960339980249858
        '
        'XrTableRow17
        '
        Me.XrTableRow17.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell115, Me.XrTableCell116, Me.XrTableCell117, Me.XrTableCell118, Me.XrTableCell119, Me.XrTableCell120, Me.XrTableCell121, Me.XrTableCell122, Me.XrTableCell123})
        Me.XrTableRow17.Dpi = 254.0!
        Me.XrTableRow17.Name = "XrTableRow17"
        Me.XrTableRow17.Weight = 0.94339622641509435
        '
        'XrTableCell115
        '
        Me.XrTableCell115.Dpi = 254.0!
        Me.XrTableCell115.Name = "XrTableCell115"
        Me.XrTableCell115.StylePriority.UseTextAlignment = False
        Me.XrTableCell115.Text = "Bahan"
        Me.XrTableCell115.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell115.Weight = 0.49571962216054355
        '
        'XrTableCell116
        '
        Me.XrTableCell116.Dpi = 254.0!
        Me.XrTableCell116.Name = "XrTableCell116"
        Me.XrTableCell116.StylePriority.UseTextAlignment = False
        Me.XrTableCell116.Text = ":"
        Me.XrTableCell116.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell116.Weight = 0.079791868633959345
        '
        'XrTableCell117
        '
        Me.XrTableCell117.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_sb_jns_bahan", "")})
        Me.XrTableCell117.Dpi = 254.0!
        Me.XrTableCell117.Name = "XrTableCell117"
        Me.XrTableCell117.Text = "XrTableCell117"
        Me.XrTableCell117.Weight = 0.32147920794879936
        '
        'XrTableCell118
        '
        Me.XrTableCell118.Dpi = 254.0!
        Me.XrTableCell118.Name = "XrTableCell118"
        Me.XrTableCell118.Weight = 0.29286409763295734
        '
        'XrTableCell119
        '
        Me.XrTableCell119.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_sb_bahan_qty", "{0:n2}")})
        Me.XrTableCell119.Dpi = 254.0!
        Me.XrTableCell119.Name = "XrTableCell119"
        Me.XrTableCell119.Text = "XrTableCell119"
        Me.XrTableCell119.Weight = 0.4384827400607183
        '
        'XrTableCell120
        '
        Me.XrTableCell120.Dpi = 254.0!
        Me.XrTableCell120.Name = "XrTableCell120"
        Me.XrTableCell120.Text = "Gram/m2"
        Me.XrTableCell120.Weight = 0.40292485668647893
        '
        'XrTableCell121
        '
        Me.XrTableCell121.Dpi = 254.0!
        Me.XrTableCell121.Name = "XrTableCell121"
        Me.XrTableCell121.Weight = 0.29043844208201086
        '
        'XrTableCell122
        '
        Me.XrTableCell122.Dpi = 254.0!
        Me.XrTableCell122.Name = "XrTableCell122"
        Me.XrTableCell122.Text = "Harga"
        Me.XrTableCell122.Weight = 0.288695764992034
        '
        'XrTableCell123
        '
        Me.XrTableCell123.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_sb_nilai", "{0:n2}")})
        Me.XrTableCell123.Dpi = 254.0!
        Me.XrTableCell123.Name = "XrTableCell123"
        Me.XrTableCell123.StylePriority.UseTextAlignment = False
        Me.XrTableCell123.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell123.Weight = 0.38960339980249858
        '
        'XrTableRow18
        '
        Me.XrTableRow18.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell124, Me.XrTableCell125, Me.XrTableCell126, Me.XrTableCell127, Me.XrTableCell128, Me.XrTableCell129, Me.XrTableCell130, Me.XrTableCell131, Me.XrTableCell132})
        Me.XrTableRow18.Dpi = 254.0!
        Me.XrTableRow18.Name = "XrTableRow18"
        Me.XrTableRow18.Weight = 0.94339622641509435
        '
        'XrTableCell124
        '
        Me.XrTableCell124.Dpi = 254.0!
        Me.XrTableCell124.Name = "XrTableCell124"
        Me.XrTableCell124.StylePriority.UseTextAlignment = False
        Me.XrTableCell124.Text = "Warna"
        Me.XrTableCell124.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell124.Weight = 0.49571962216054355
        '
        'XrTableCell125
        '
        Me.XrTableCell125.Dpi = 254.0!
        Me.XrTableCell125.Name = "XrTableCell125"
        Me.XrTableCell125.StylePriority.UseTextAlignment = False
        Me.XrTableCell125.Text = ":"
        Me.XrTableCell125.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell125.Weight = 0.079791868633959345
        '
        'XrTableCell126
        '
        Me.XrTableCell126.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_sb_jml_warna", "")})
        Me.XrTableCell126.Dpi = 254.0!
        Me.XrTableCell126.Name = "XrTableCell126"
        Me.XrTableCell126.Weight = 0.32147920794879936
        '
        'XrTableCell127
        '
        Me.XrTableCell127.Dpi = 254.0!
        Me.XrTableCell127.Name = "XrTableCell127"
        Me.XrTableCell127.Weight = 0.29286409763295734
        '
        'XrTableCell128
        '
        Me.XrTableCell128.Dpi = 254.0!
        Me.XrTableCell128.Name = "XrTableCell128"
        Me.XrTableCell128.Text = "Ukuran"
        Me.XrTableCell128.Weight = 0.4384827400607183
        '
        'XrTableCell129
        '
        Me.XrTableCell129.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_sb_bhn_panjang", "")})
        Me.XrTableCell129.Dpi = 254.0!
        Me.XrTableCell129.Name = "XrTableCell129"
        Me.XrTableCell129.Text = "XrTableCell129"
        Me.XrTableCell129.Weight = 0.40292485668647893
        '
        'XrTableCell130
        '
        Me.XrTableCell130.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_sb_bhn_lebar", "")})
        Me.XrTableCell130.Dpi = 254.0!
        Me.XrTableCell130.Name = "XrTableCell130"
        Me.XrTableCell130.Text = "XrTableCell130"
        Me.XrTableCell130.Weight = 0.29043844208201086
        '
        'XrTableCell131
        '
        Me.XrTableCell131.Dpi = 254.0!
        Me.XrTableCell131.Name = "XrTableCell131"
        Me.XrTableCell131.Weight = 0.288695764992034
        '
        'XrTableCell132
        '
        Me.XrTableCell132.Dpi = 254.0!
        Me.XrTableCell132.Name = "XrTableCell132"
        Me.XrTableCell132.Weight = 0.38960339980249858
        '
        'XrTableRow19
        '
        Me.XrTableRow19.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell133, Me.XrTableCell134, Me.XrTableCell135, Me.XrTableCell136, Me.XrTableCell137, Me.XrTableCell138, Me.XrTableCell139, Me.XrTableCell140, Me.XrTableCell141})
        Me.XrTableRow19.Dpi = 254.0!
        Me.XrTableRow19.Name = "XrTableRow19"
        Me.XrTableRow19.Weight = 0.94339622641509435
        '
        'XrTableCell133
        '
        Me.XrTableCell133.Dpi = 254.0!
        Me.XrTableCell133.Name = "XrTableCell133"
        Me.XrTableCell133.Text = "Bahan + Insheet" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.XrTableCell133.Weight = 0.49571962216054355
        '
        'XrTableCell134
        '
        Me.XrTableCell134.Dpi = 254.0!
        Me.XrTableCell134.Name = "XrTableCell134"
        Me.XrTableCell134.StylePriority.UseTextAlignment = False
        Me.XrTableCell134.Text = ":"
        Me.XrTableCell134.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell134.Weight = 0.079791868633959345
        '
        'XrTableCell135
        '
        Me.XrTableCell135.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_sb_insheet", "")})
        Me.XrTableCell135.Dpi = 254.0!
        Me.XrTableCell135.Name = "XrTableCell135"
        Me.XrTableCell135.Text = "XrTableCell135"
        Me.XrTableCell135.Weight = 0.32147920794879936
        '
        'XrTableCell136
        '
        Me.XrTableCell136.Dpi = 254.0!
        Me.XrTableCell136.Name = "XrTableCell136"
        Me.XrTableCell136.Text = "%"
        Me.XrTableCell136.Weight = 0.29286409763295734
        '
        'XrTableCell137
        '
        Me.XrTableCell137.Dpi = 254.0!
        Me.XrTableCell137.Name = "XrTableCell137"
        Me.XrTableCell137.Text = "Kebutuhan Bhn"
        Me.XrTableCell137.Weight = 0.4384827400607183
        '
        'XrTableCell138
        '
        Me.XrTableCell138.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_sb_bahan_qty", "{0:n2}")})
        Me.XrTableCell138.Dpi = 254.0!
        Me.XrTableCell138.Name = "XrTableCell138"
        Me.XrTableCell138.Text = "XrTableCell138"
        Me.XrTableCell138.Weight = 0.40292485668647893
        '
        'XrTableCell139
        '
        Me.XrTableCell139.Dpi = 254.0!
        Me.XrTableCell139.Name = "XrTableCell139"
        Me.XrTableCell139.Text = "Rim"
        Me.XrTableCell139.Weight = 0.29043844208201086
        '
        'XrTableCell140
        '
        Me.XrTableCell140.Dpi = 254.0!
        Me.XrTableCell140.Name = "XrTableCell140"
        Me.XrTableCell140.Weight = 0.288695764992034
        '
        'XrTableCell141
        '
        Me.XrTableCell141.Dpi = 254.0!
        Me.XrTableCell141.Name = "XrTableCell141"
        Me.XrTableCell141.Weight = 0.38960339980249858
        '
        'XrTableRow21
        '
        Me.XrTableRow21.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell151, Me.XrTableCell152, Me.XrTableCell153, Me.XrTableCell154, Me.XrTableCell155, Me.XrTableCell156, Me.XrTableCell157, Me.XrTableCell158, Me.XrTableCell159})
        Me.XrTableRow21.Dpi = 254.0!
        Me.XrTableRow21.Name = "XrTableRow21"
        Me.XrTableRow21.Weight = 0.94339622641509435
        '
        'XrTableCell151
        '
        Me.XrTableCell151.Dpi = 254.0!
        Me.XrTableCell151.Name = "XrTableCell151"
        Me.XrTableCell151.Text = "Naik Cetak"
        Me.XrTableCell151.Weight = 0.49571962216054355
        '
        'XrTableCell152
        '
        Me.XrTableCell152.Dpi = 254.0!
        Me.XrTableCell152.Name = "XrTableCell152"
        Me.XrTableCell152.StylePriority.UseTextAlignment = False
        Me.XrTableCell152.Text = ":"
        Me.XrTableCell152.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell152.Weight = 0.079791868633959345
        '
        'XrTableCell153
        '
        Me.XrTableCell153.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_sb_naik_cetak", "")})
        Me.XrTableCell153.Dpi = 254.0!
        Me.XrTableCell153.Name = "XrTableCell153"
        Me.XrTableCell153.Text = "XrTableCell153"
        Me.XrTableCell153.Weight = 0.32147920794879936
        '
        'XrTableCell154
        '
        Me.XrTableCell154.Dpi = 254.0!
        Me.XrTableCell154.Name = "XrTableCell154"
        Me.XrTableCell154.Weight = 0.29286409763295734
        '
        'XrTableCell155
        '
        Me.XrTableCell155.Dpi = 254.0!
        Me.XrTableCell155.Name = "XrTableCell155"
        Me.XrTableCell155.Text = "Jenis Mesin"
        Me.XrTableCell155.Weight = 0.4384827400607183
        '
        'XrTableCell156
        '
        Me.XrTableCell156.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_sb_jns_mesin", "")})
        Me.XrTableCell156.Dpi = 254.0!
        Me.XrTableCell156.Name = "XrTableCell156"
        Me.XrTableCell156.Text = "XrTableCell156"
        Me.XrTableCell156.Weight = 0.40292485668647893
        '
        'XrTableCell157
        '
        Me.XrTableCell157.Dpi = 254.0!
        Me.XrTableCell157.Name = "XrTableCell157"
        Me.XrTableCell157.Weight = 0.29043844208201086
        '
        'XrTableCell158
        '
        Me.XrTableCell158.Dpi = 254.0!
        Me.XrTableCell158.Name = "XrTableCell158"
        Me.XrTableCell158.Weight = 0.288695764992034
        '
        'XrTableCell159
        '
        Me.XrTableCell159.Dpi = 254.0!
        Me.XrTableCell159.Name = "XrTableCell159"
        Me.XrTableCell159.Weight = 0.38960339980249858
        '
        'XrTableRow20
        '
        Me.XrTableRow20.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
        Me.XrTableRow20.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell142, Me.XrTableCell143, Me.XrTableCell144, Me.XrTableCell145, Me.XrTableCell146, Me.XrTableCell147, Me.XrTableCell148, Me.XrTableCell149, Me.XrTableCell150})
        Me.XrTableRow20.Dpi = 254.0!
        Me.XrTableRow20.Name = "XrTableRow20"
        Me.XrTableRow20.StylePriority.UseBorders = False
        Me.XrTableRow20.Weight = 0.94339622641509435
        '
        'XrTableCell142
        '
        Me.XrTableCell142.Dpi = 254.0!
        Me.XrTableCell142.Name = "XrTableCell142"
        Me.XrTableCell142.Weight = 0.49571962216054355
        '
        'XrTableCell143
        '
        Me.XrTableCell143.Dpi = 254.0!
        Me.XrTableCell143.Name = "XrTableCell143"
        Me.XrTableCell143.Weight = 0.079791868633959345
        '
        'XrTableCell144
        '
        Me.XrTableCell144.Dpi = 254.0!
        Me.XrTableCell144.Name = "XrTableCell144"
        Me.XrTableCell144.Weight = 0.32147920794879936
        '
        'XrTableCell145
        '
        Me.XrTableCell145.Dpi = 254.0!
        Me.XrTableCell145.Name = "XrTableCell145"
        Me.XrTableCell145.Weight = 0.29286409763295734
        '
        'XrTableCell146
        '
        Me.XrTableCell146.Dpi = 254.0!
        Me.XrTableCell146.Name = "XrTableCell146"
        Me.XrTableCell146.Weight = 0.4384827400607183
        '
        'XrTableCell147
        '
        Me.XrTableCell147.Dpi = 254.0!
        Me.XrTableCell147.Name = "XrTableCell147"
        Me.XrTableCell147.Weight = 0.40292485668647893
        '
        'XrTableCell148
        '
        Me.XrTableCell148.Dpi = 254.0!
        Me.XrTableCell148.Name = "XrTableCell148"
        Me.XrTableCell148.Weight = 0.29043844208201086
        '
        'XrTableCell149
        '
        Me.XrTableCell149.Dpi = 254.0!
        Me.XrTableCell149.Name = "XrTableCell149"
        Me.XrTableCell149.Text = "Outsource"
        Me.XrTableCell149.Weight = 0.288695764992034
        '
        'XrTableCell150
        '
        Me.XrTableCell150.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_sb_outsource_nilai", "{0:n2}")})
        Me.XrTableCell150.Dpi = 254.0!
        Me.XrTableCell150.Name = "XrTableCell150"
        Me.XrTableCell150.StylePriority.UseTextAlignment = False
        Me.XrTableCell150.Text = "XrTableCell150"
        Me.XrTableCell150.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell150.Weight = 0.38960339980249858
        '
        'XrTableRow22
        '
        Me.XrTableRow22.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell160, Me.XrTableCell161, Me.XrTableCell162, Me.XrTableCell163, Me.XrTableCell164, Me.XrTableCell165, Me.XrTableCell166, Me.XrTableCell167, Me.XrTableCell168})
        Me.XrTableRow22.Dpi = 254.0!
        Me.XrTableRow22.Name = "XrTableRow22"
        Me.XrTableRow22.Weight = 0.94339622641509413
        '
        'XrTableCell160
        '
        Me.XrTableCell160.Dpi = 254.0!
        Me.XrTableCell160.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCell160.Name = "XrTableCell160"
        Me.XrTableCell160.StylePriority.UseFont = False
        Me.XrTableCell160.Text = "Cover"
        Me.XrTableCell160.Weight = 0.49571962216054355
        '
        'XrTableCell161
        '
        Me.XrTableCell161.Dpi = 254.0!
        Me.XrTableCell161.Name = "XrTableCell161"
        Me.XrTableCell161.StylePriority.UseTextAlignment = False
        Me.XrTableCell161.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell161.Weight = 0.079791868633959345
        '
        'XrTableCell162
        '
        Me.XrTableCell162.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.cf_opsi_cv", "")})
        Me.XrTableCell162.Dpi = 254.0!
        Me.XrTableCell162.Name = "XrTableCell162"
        Me.XrTableCell162.Weight = 0.32147920794879936
        '
        'XrTableCell163
        '
        Me.XrTableCell163.Dpi = 254.0!
        Me.XrTableCell163.Name = "XrTableCell163"
        Me.XrTableCell163.Weight = 0.29286409763295734
        '
        'XrTableCell164
        '
        Me.XrTableCell164.Dpi = 254.0!
        Me.XrTableCell164.Name = "XrTableCell164"
        Me.XrTableCell164.Weight = 0.4384827400607183
        '
        'XrTableCell165
        '
        Me.XrTableCell165.Dpi = 254.0!
        Me.XrTableCell165.Name = "XrTableCell165"
        Me.XrTableCell165.Weight = 0.40292485668647893
        '
        'XrTableCell166
        '
        Me.XrTableCell166.Dpi = 254.0!
        Me.XrTableCell166.Name = "XrTableCell166"
        Me.XrTableCell166.Weight = 0.29043844208201086
        '
        'XrTableCell167
        '
        Me.XrTableCell167.Dpi = 254.0!
        Me.XrTableCell167.Name = "XrTableCell167"
        Me.XrTableCell167.Weight = 0.288695764992034
        '
        'XrTableCell168
        '
        Me.XrTableCell168.Dpi = 254.0!
        Me.XrTableCell168.Name = "XrTableCell168"
        Me.XrTableCell168.Weight = 0.38960339980249858
        '
        'XrTableRow23
        '
        Me.XrTableRow23.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell169, Me.XrTableCell170, Me.XrTableCell171, Me.XrTableCell172, Me.XrTableCell173, Me.XrTableCell174, Me.XrTableCell175, Me.XrTableCell176, Me.XrTableCell177})
        Me.XrTableRow23.Dpi = 254.0!
        Me.XrTableRow23.Name = "XrTableRow23"
        Me.XrTableRow23.Weight = 0.94339622641509413
        '
        'XrTableCell169
        '
        Me.XrTableCell169.Dpi = 254.0!
        Me.XrTableCell169.Name = "XrTableCell169"
        Me.XrTableCell169.Text = "Jumlah Design"
        Me.XrTableCell169.Weight = 0.49571962216054355
        '
        'XrTableCell170
        '
        Me.XrTableCell170.Dpi = 254.0!
        Me.XrTableCell170.Name = "XrTableCell170"
        Me.XrTableCell170.StylePriority.UseTextAlignment = False
        Me.XrTableCell170.Text = ":"
        Me.XrTableCell170.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell170.Weight = 0.079791868633959345
        '
        'XrTableCell171
        '
        Me.XrTableCell171.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_cv_jml_design", "")})
        Me.XrTableCell171.Dpi = 254.0!
        Me.XrTableCell171.Name = "XrTableCell171"
        Me.XrTableCell171.Text = "XrTableCell171"
        Me.XrTableCell171.Weight = 0.32147920794879936
        '
        'XrTableCell172
        '
        Me.XrTableCell172.Dpi = 254.0!
        Me.XrTableCell172.Name = "XrTableCell172"
        Me.XrTableCell172.Text = "Uk Terbuka"
        Me.XrTableCell172.Weight = 0.29286409763295734
        '
        'XrTableCell173
        '
        Me.XrTableCell173.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_cv_panjang", "")})
        Me.XrTableCell173.Dpi = 254.0!
        Me.XrTableCell173.Name = "XrTableCell173"
        Me.XrTableCell173.Text = "XrTableCell173"
        Me.XrTableCell173.Weight = 0.4384827400607183
        '
        'XrTableCell174
        '
        Me.XrTableCell174.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_cv_lebar", "")})
        Me.XrTableCell174.Dpi = 254.0!
        Me.XrTableCell174.Name = "XrTableCell174"
        Me.XrTableCell174.Text = "XrTableCell174"
        Me.XrTableCell174.Weight = 0.40292485668647893
        '
        'XrTableCell175
        '
        Me.XrTableCell175.Dpi = 254.0!
        Me.XrTableCell175.Name = "XrTableCell175"
        Me.XrTableCell175.Text = "Berat"
        Me.XrTableCell175.Weight = 0.29043844208201086
        '
        'XrTableCell176
        '
        Me.XrTableCell176.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_cv_berat", "")})
        Me.XrTableCell176.Dpi = 254.0!
        Me.XrTableCell176.Name = "XrTableCell176"
        Me.XrTableCell176.Text = "XrTableCell176"
        Me.XrTableCell176.Weight = 0.288695764992034
        '
        'XrTableCell177
        '
        Me.XrTableCell177.Dpi = 254.0!
        Me.XrTableCell177.Name = "XrTableCell177"
        Me.XrTableCell177.Weight = 0.38960339980249858
        '
        'XrTableRow24
        '
        Me.XrTableRow24.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell178, Me.XrTableCell179, Me.XrTableCell180, Me.XrTableCell181, Me.XrTableCell182, Me.XrTableCell183, Me.XrTableCell184, Me.XrTableCell185, Me.XrTableCell186})
        Me.XrTableRow24.Dpi = 254.0!
        Me.XrTableRow24.Name = "XrTableRow24"
        Me.XrTableRow24.Weight = 0.94339622641509413
        '
        'XrTableCell178
        '
        Me.XrTableCell178.Dpi = 254.0!
        Me.XrTableCell178.Name = "XrTableCell178"
        Me.XrTableCell178.StylePriority.UseTextAlignment = False
        Me.XrTableCell178.Text = "Bahan"
        Me.XrTableCell178.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell178.Weight = 0.49571962216054355
        '
        'XrTableCell179
        '
        Me.XrTableCell179.Dpi = 254.0!
        Me.XrTableCell179.Name = "XrTableCell179"
        Me.XrTableCell179.StylePriority.UseTextAlignment = False
        Me.XrTableCell179.Text = ":"
        Me.XrTableCell179.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell179.Weight = 0.079791868633959345
        '
        'XrTableCell180
        '
        Me.XrTableCell180.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_cv_jns_bahan", "")})
        Me.XrTableCell180.Dpi = 254.0!
        Me.XrTableCell180.Name = "XrTableCell180"
        Me.XrTableCell180.Text = "XrTableCell180"
        Me.XrTableCell180.Weight = 0.32147920794879936
        '
        'XrTableCell181
        '
        Me.XrTableCell181.Dpi = 254.0!
        Me.XrTableCell181.Name = "XrTableCell181"
        Me.XrTableCell181.Weight = 0.29286409763295734
        '
        'XrTableCell182
        '
        Me.XrTableCell182.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_cv_bahan_qty", "")})
        Me.XrTableCell182.Dpi = 254.0!
        Me.XrTableCell182.Name = "XrTableCell182"
        Me.XrTableCell182.Text = "XrTableCell182"
        Me.XrTableCell182.Weight = 0.4384827400607183
        '
        'XrTableCell183
        '
        Me.XrTableCell183.Dpi = 254.0!
        Me.XrTableCell183.Name = "XrTableCell183"
        Me.XrTableCell183.Text = "Gram/m2"
        Me.XrTableCell183.Weight = 0.40292485668647893
        '
        'XrTableCell184
        '
        Me.XrTableCell184.Dpi = 254.0!
        Me.XrTableCell184.Name = "XrTableCell184"
        Me.XrTableCell184.Weight = 0.29043844208201086
        '
        'XrTableCell185
        '
        Me.XrTableCell185.Dpi = 254.0!
        Me.XrTableCell185.Name = "XrTableCell185"
        Me.XrTableCell185.Text = "Harga"
        Me.XrTableCell185.Weight = 0.288695764992034
        '
        'XrTableCell186
        '
        Me.XrTableCell186.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_cv_nilai", "{0:n2}")})
        Me.XrTableCell186.Dpi = 254.0!
        Me.XrTableCell186.Name = "XrTableCell186"
        Me.XrTableCell186.StylePriority.UseTextAlignment = False
        Me.XrTableCell186.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell186.Weight = 0.38960339980249858
        '
        'XrTableRow25
        '
        Me.XrTableRow25.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell187, Me.XrTableCell188, Me.XrTableCell189, Me.XrTableCell190, Me.XrTableCell191, Me.XrTableCell192, Me.XrTableCell193, Me.XrTableCell194, Me.XrTableCell195})
        Me.XrTableRow25.Dpi = 254.0!
        Me.XrTableRow25.Name = "XrTableRow25"
        Me.XrTableRow25.Weight = 0.94339622641509413
        '
        'XrTableCell187
        '
        Me.XrTableCell187.Dpi = 254.0!
        Me.XrTableCell187.Name = "XrTableCell187"
        Me.XrTableCell187.StylePriority.UseTextAlignment = False
        Me.XrTableCell187.Text = "Warna"
        Me.XrTableCell187.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell187.Weight = 0.49571962216054355
        '
        'XrTableCell188
        '
        Me.XrTableCell188.Dpi = 254.0!
        Me.XrTableCell188.Name = "XrTableCell188"
        Me.XrTableCell188.StylePriority.UseTextAlignment = False
        Me.XrTableCell188.Text = ":"
        Me.XrTableCell188.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell188.Weight = 0.079791868633959345
        '
        'XrTableCell189
        '
        Me.XrTableCell189.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_cv_jml_warna", "")})
        Me.XrTableCell189.Dpi = 254.0!
        Me.XrTableCell189.Name = "XrTableCell189"
        Me.XrTableCell189.Text = "XrTableCell189"
        Me.XrTableCell189.Weight = 0.32147920794879936
        '
        'XrTableCell190
        '
        Me.XrTableCell190.Dpi = 254.0!
        Me.XrTableCell190.Name = "XrTableCell190"
        Me.XrTableCell190.Weight = 0.29286409763295734
        '
        'XrTableCell191
        '
        Me.XrTableCell191.Dpi = 254.0!
        Me.XrTableCell191.Name = "XrTableCell191"
        Me.XrTableCell191.Text = "Ukuran"
        Me.XrTableCell191.Weight = 0.4384827400607183
        '
        'XrTableCell192
        '
        Me.XrTableCell192.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_cv_bhn_panjang", "")})
        Me.XrTableCell192.Dpi = 254.0!
        Me.XrTableCell192.Name = "XrTableCell192"
        Me.XrTableCell192.Text = "XrTableCell192"
        Me.XrTableCell192.Weight = 0.40292485668647893
        '
        'XrTableCell193
        '
        Me.XrTableCell193.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_cv_bhn_lebar", "")})
        Me.XrTableCell193.Dpi = 254.0!
        Me.XrTableCell193.Name = "XrTableCell193"
        Me.XrTableCell193.Text = "XrTableCell193"
        Me.XrTableCell193.Weight = 0.29043844208201086
        '
        'XrTableCell194
        '
        Me.XrTableCell194.Dpi = 254.0!
        Me.XrTableCell194.Name = "XrTableCell194"
        Me.XrTableCell194.Weight = 0.288695764992034
        '
        'XrTableCell195
        '
        Me.XrTableCell195.Dpi = 254.0!
        Me.XrTableCell195.Name = "XrTableCell195"
        Me.XrTableCell195.Weight = 0.38960339980249858
        '
        'XrTableRow26
        '
        Me.XrTableRow26.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell196, Me.XrTableCell197, Me.XrTableCell198, Me.XrTableCell199, Me.XrTableCell200, Me.XrTableCell201, Me.XrTableCell202, Me.XrTableCell203, Me.XrTableCell204})
        Me.XrTableRow26.Dpi = 254.0!
        Me.XrTableRow26.Name = "XrTableRow26"
        Me.XrTableRow26.Weight = 0.94339622641509413
        '
        'XrTableCell196
        '
        Me.XrTableCell196.Dpi = 254.0!
        Me.XrTableCell196.Name = "XrTableCell196"
        Me.XrTableCell196.Text = "Bahan + Insheet" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.XrTableCell196.Weight = 0.49571962216054355
        '
        'XrTableCell197
        '
        Me.XrTableCell197.Dpi = 254.0!
        Me.XrTableCell197.Name = "XrTableCell197"
        Me.XrTableCell197.StylePriority.UseTextAlignment = False
        Me.XrTableCell197.Text = ":"
        Me.XrTableCell197.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell197.Weight = 0.079791868633959345
        '
        'XrTableCell198
        '
        Me.XrTableCell198.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_cv_insheet", "")})
        Me.XrTableCell198.Dpi = 254.0!
        Me.XrTableCell198.Name = "XrTableCell198"
        Me.XrTableCell198.Text = "XrTableCell198"
        Me.XrTableCell198.Weight = 0.32147920794879936
        '
        'XrTableCell199
        '
        Me.XrTableCell199.Dpi = 254.0!
        Me.XrTableCell199.Name = "XrTableCell199"
        Me.XrTableCell199.Text = "%"
        Me.XrTableCell199.Weight = 0.29286409763295734
        '
        'XrTableCell200
        '
        Me.XrTableCell200.Dpi = 254.0!
        Me.XrTableCell200.Name = "XrTableCell200"
        Me.XrTableCell200.Text = "Kebutuhan Bhn"
        Me.XrTableCell200.Weight = 0.4384827400607183
        '
        'XrTableCell201
        '
        Me.XrTableCell201.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_cv_bahan_qty", "{0:n2}")})
        Me.XrTableCell201.Dpi = 254.0!
        Me.XrTableCell201.Name = "XrTableCell201"
        Me.XrTableCell201.Text = "XrTableCell201"
        Me.XrTableCell201.Weight = 0.40292485668647893
        '
        'XrTableCell202
        '
        Me.XrTableCell202.Dpi = 254.0!
        Me.XrTableCell202.Name = "XrTableCell202"
        Me.XrTableCell202.Text = "Rim"
        Me.XrTableCell202.Weight = 0.29043844208201086
        '
        'XrTableCell203
        '
        Me.XrTableCell203.Dpi = 254.0!
        Me.XrTableCell203.Name = "XrTableCell203"
        Me.XrTableCell203.Weight = 0.288695764992034
        '
        'XrTableCell204
        '
        Me.XrTableCell204.Dpi = 254.0!
        Me.XrTableCell204.Name = "XrTableCell204"
        Me.XrTableCell204.Weight = 0.38960339980249858
        '
        'XrTableRow28
        '
        Me.XrTableRow28.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrTableRow28.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell214, Me.XrTableCell215, Me.XrTableCell216, Me.XrTableCell217, Me.XrTableCell218, Me.XrTableCell219, Me.XrTableCell220, Me.XrTableCell221, Me.XrTableCell222})
        Me.XrTableRow28.Dpi = 254.0!
        Me.XrTableRow28.Name = "XrTableRow28"
        Me.XrTableRow28.StylePriority.UseBorders = False
        Me.XrTableRow28.Weight = 0.94339622641509413
        '
        'XrTableCell214
        '
        Me.XrTableCell214.Dpi = 254.0!
        Me.XrTableCell214.Name = "XrTableCell214"
        Me.XrTableCell214.Text = "Naik Cetak"
        Me.XrTableCell214.Weight = 0.49571962216054355
        '
        'XrTableCell215
        '
        Me.XrTableCell215.Dpi = 254.0!
        Me.XrTableCell215.Name = "XrTableCell215"
        Me.XrTableCell215.StylePriority.UseTextAlignment = False
        Me.XrTableCell215.Text = ":"
        Me.XrTableCell215.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell215.Weight = 0.079791868633959345
        '
        'XrTableCell216
        '
        Me.XrTableCell216.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_cv_naik_cetak", "")})
        Me.XrTableCell216.Dpi = 254.0!
        Me.XrTableCell216.Name = "XrTableCell216"
        Me.XrTableCell216.Text = "XrTableCell216"
        Me.XrTableCell216.Weight = 0.32147920794879936
        '
        'XrTableCell217
        '
        Me.XrTableCell217.Dpi = 254.0!
        Me.XrTableCell217.Name = "XrTableCell217"
        Me.XrTableCell217.Weight = 0.29286409763295734
        '
        'XrTableCell218
        '
        Me.XrTableCell218.Dpi = 254.0!
        Me.XrTableCell218.Name = "XrTableCell218"
        Me.XrTableCell218.Text = "Jenis Mesin"
        Me.XrTableCell218.Weight = 0.4384827400607183
        '
        'XrTableCell219
        '
        Me.XrTableCell219.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_cv_jns_mesin", "")})
        Me.XrTableCell219.Dpi = 254.0!
        Me.XrTableCell219.Name = "XrTableCell219"
        Me.XrTableCell219.Text = "XrTableCell219"
        Me.XrTableCell219.Weight = 0.40292485668647893
        '
        'XrTableCell220
        '
        Me.XrTableCell220.Dpi = 254.0!
        Me.XrTableCell220.Name = "XrTableCell220"
        Me.XrTableCell220.Weight = 0.29043844208201086
        '
        'XrTableCell221
        '
        Me.XrTableCell221.Dpi = 254.0!
        Me.XrTableCell221.Name = "XrTableCell221"
        Me.XrTableCell221.Weight = 0.288695764992034
        '
        'XrTableCell222
        '
        Me.XrTableCell222.Dpi = 254.0!
        Me.XrTableCell222.Name = "XrTableCell222"
        Me.XrTableCell222.Weight = 0.38960339980249858
        '
        'XrTableRow27
        '
        Me.XrTableRow27.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
        Me.XrTableRow27.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell205, Me.XrTableCell206, Me.XrTableCell207, Me.XrTableCell208, Me.XrTableCell209, Me.XrTableCell210, Me.XrTableCell211, Me.XrTableCell212, Me.XrTableCell213})
        Me.XrTableRow27.Dpi = 254.0!
        Me.XrTableRow27.Name = "XrTableRow27"
        Me.XrTableRow27.StylePriority.UseBorders = False
        Me.XrTableRow27.Weight = 0.94339622641509413
        '
        'XrTableCell205
        '
        Me.XrTableCell205.Dpi = 254.0!
        Me.XrTableCell205.Name = "XrTableCell205"
        Me.XrTableCell205.Weight = 0.49571962216054355
        '
        'XrTableCell206
        '
        Me.XrTableCell206.Dpi = 254.0!
        Me.XrTableCell206.Name = "XrTableCell206"
        Me.XrTableCell206.StylePriority.UseTextAlignment = False
        Me.XrTableCell206.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell206.Weight = 0.079791868633959345
        '
        'XrTableCell207
        '
        Me.XrTableCell207.Dpi = 254.0!
        Me.XrTableCell207.Name = "XrTableCell207"
        Me.XrTableCell207.Weight = 0.32147920794879936
        '
        'XrTableCell208
        '
        Me.XrTableCell208.Dpi = 254.0!
        Me.XrTableCell208.Name = "XrTableCell208"
        Me.XrTableCell208.Weight = 0.29286409763295734
        '
        'XrTableCell209
        '
        Me.XrTableCell209.Dpi = 254.0!
        Me.XrTableCell209.Name = "XrTableCell209"
        Me.XrTableCell209.Weight = 0.4384827400607183
        '
        'XrTableCell210
        '
        Me.XrTableCell210.Dpi = 254.0!
        Me.XrTableCell210.Name = "XrTableCell210"
        Me.XrTableCell210.Weight = 0.40292485668647893
        '
        'XrTableCell211
        '
        Me.XrTableCell211.Dpi = 254.0!
        Me.XrTableCell211.Name = "XrTableCell211"
        Me.XrTableCell211.Weight = 0.29043844208201086
        '
        'XrTableCell212
        '
        Me.XrTableCell212.Dpi = 254.0!
        Me.XrTableCell212.Name = "XrTableCell212"
        Me.XrTableCell212.Text = "Outsource"
        Me.XrTableCell212.Weight = 0.288695764992034
        '
        'XrTableCell213
        '
        Me.XrTableCell213.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_cv_outsource_nilai", "{0:n2}")})
        Me.XrTableCell213.Dpi = 254.0!
        Me.XrTableCell213.Name = "XrTableCell213"
        Me.XrTableCell213.StylePriority.UseTextAlignment = False
        Me.XrTableCell213.Text = "XrTableCell213"
        Me.XrTableCell213.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell213.Weight = 0.38960339980249858
        '
        'XrTableRow29
        '
        Me.XrTableRow29.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell223, Me.XrTableCell224, Me.XrTableCell225, Me.XrTableCell226, Me.XrTableCell227, Me.XrTableCell228, Me.XrTableCell229, Me.XrTableCell230, Me.XrTableCell231})
        Me.XrTableRow29.Dpi = 254.0!
        Me.XrTableRow29.Name = "XrTableRow29"
        Me.XrTableRow29.Weight = 0.94339622641509413
        '
        'XrTableCell223
        '
        Me.XrTableCell223.Dpi = 254.0!
        Me.XrTableCell223.Name = "XrTableCell223"
        Me.XrTableCell223.Text = "Karton"
        Me.XrTableCell223.Weight = 0.49571962216054355
        '
        'XrTableCell224
        '
        Me.XrTableCell224.Dpi = 254.0!
        Me.XrTableCell224.Name = "XrTableCell224"
        Me.XrTableCell224.StylePriority.UseTextAlignment = False
        Me.XrTableCell224.Text = ":"
        Me.XrTableCell224.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell224.Weight = 0.079791868633959345
        '
        'XrTableCell225
        '
        Me.XrTableCell225.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_kr_jns_bahan", "")})
        Me.XrTableCell225.Dpi = 254.0!
        Me.XrTableCell225.Name = "XrTableCell225"
        Me.XrTableCell225.Text = "XrTableCell225"
        Me.XrTableCell225.Weight = 0.32147920794879936
        '
        'XrTableCell226
        '
        Me.XrTableCell226.Dpi = 254.0!
        Me.XrTableCell226.Name = "XrTableCell226"
        Me.XrTableCell226.Weight = 0.29286409763295734
        '
        'XrTableCell227
        '
        Me.XrTableCell227.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_kr_jns_bhn_qty", "")})
        Me.XrTableCell227.Dpi = 254.0!
        Me.XrTableCell227.Name = "XrTableCell227"
        Me.XrTableCell227.Text = "XrTableCell227"
        Me.XrTableCell227.Weight = 0.4384827400607183
        '
        'XrTableCell228
        '
        Me.XrTableCell228.Dpi = 254.0!
        Me.XrTableCell228.Name = "XrTableCell228"
        Me.XrTableCell228.Weight = 0.40292485668647893
        '
        'XrTableCell229
        '
        Me.XrTableCell229.Dpi = 254.0!
        Me.XrTableCell229.Name = "XrTableCell229"
        Me.XrTableCell229.Weight = 0.29043844208201086
        '
        'XrTableCell230
        '
        Me.XrTableCell230.Dpi = 254.0!
        Me.XrTableCell230.Name = "XrTableCell230"
        Me.XrTableCell230.Weight = 0.288695764992034
        '
        'XrTableCell231
        '
        Me.XrTableCell231.Dpi = 254.0!
        Me.XrTableCell231.Name = "XrTableCell231"
        Me.XrTableCell231.Weight = 0.38960339980249858
        '
        'XrTableRow30
        '
        Me.XrTableRow30.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell232, Me.XrTableCell233, Me.XrTableCell234, Me.XrTableCell235, Me.XrTableCell236, Me.XrTableCell237, Me.XrTableCell238, Me.XrTableCell239, Me.XrTableCell240})
        Me.XrTableRow30.Dpi = 254.0!
        Me.XrTableRow30.Name = "XrTableRow30"
        Me.XrTableRow30.Weight = 0.94339622641509413
        '
        'XrTableCell232
        '
        Me.XrTableCell232.Dpi = 254.0!
        Me.XrTableCell232.Name = "XrTableCell232"
        Me.XrTableCell232.Text = "Bahan + Insheet" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.XrTableCell232.Weight = 0.49571962216054355
        '
        'XrTableCell233
        '
        Me.XrTableCell233.Dpi = 254.0!
        Me.XrTableCell233.Name = "XrTableCell233"
        Me.XrTableCell233.StylePriority.UseTextAlignment = False
        Me.XrTableCell233.Text = ":"
        Me.XrTableCell233.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell233.Weight = 0.079791868633959345
        '
        'XrTableCell234
        '
        Me.XrTableCell234.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_kr_insheet", "")})
        Me.XrTableCell234.Dpi = 254.0!
        Me.XrTableCell234.Name = "XrTableCell234"
        Me.XrTableCell234.Weight = 0.32147920794879936
        '
        'XrTableCell235
        '
        Me.XrTableCell235.Dpi = 254.0!
        Me.XrTableCell235.Name = "XrTableCell235"
        Me.XrTableCell235.Text = "%"
        Me.XrTableCell235.Weight = 0.29286409763295734
        '
        'XrTableCell236
        '
        Me.XrTableCell236.Dpi = 254.0!
        Me.XrTableCell236.Name = "XrTableCell236"
        Me.XrTableCell236.Text = "Ukuran"
        Me.XrTableCell236.Weight = 0.4384827400607183
        '
        'XrTableCell237
        '
        Me.XrTableCell237.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_kr_bhn_panjang", "")})
        Me.XrTableCell237.Dpi = 254.0!
        Me.XrTableCell237.Name = "XrTableCell237"
        Me.XrTableCell237.Text = "XrTableCell237"
        Me.XrTableCell237.Weight = 0.40292485668647893
        '
        'XrTableCell238
        '
        Me.XrTableCell238.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_kr_bhn_lebar", "")})
        Me.XrTableCell238.Dpi = 254.0!
        Me.XrTableCell238.Name = "XrTableCell238"
        Me.XrTableCell238.Text = "XrTableCell238"
        Me.XrTableCell238.Weight = 0.29043844208201086
        '
        'XrTableCell239
        '
        Me.XrTableCell239.Dpi = 254.0!
        Me.XrTableCell239.Name = "XrTableCell239"
        Me.XrTableCell239.Text = "Harga"
        Me.XrTableCell239.Weight = 0.288695764992034
        '
        'XrTableCell240
        '
        Me.XrTableCell240.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_kr_nilai", "{0:n2}")})
        Me.XrTableCell240.Dpi = 254.0!
        Me.XrTableCell240.Name = "XrTableCell240"
        Me.XrTableCell240.StylePriority.UseTextAlignment = False
        Me.XrTableCell240.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell240.Weight = 0.38960339980249858
        '
        'XrTableRow31
        '
        Me.XrTableRow31.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrTableRow31.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell241, Me.XrTableCell242, Me.XrTableCell243, Me.XrTableCell244, Me.XrTableCell245, Me.XrTableCell246, Me.XrTableCell247, Me.XrTableCell248, Me.XrTableCell249})
        Me.XrTableRow31.Dpi = 254.0!
        Me.XrTableRow31.Name = "XrTableRow31"
        Me.XrTableRow31.StylePriority.UseBorders = False
        Me.XrTableRow31.Weight = 0.94339622641509413
        '
        'XrTableCell241
        '
        Me.XrTableCell241.Dpi = 254.0!
        Me.XrTableCell241.Name = "XrTableCell241"
        Me.XrTableCell241.Weight = 0.49571962216054355
        '
        'XrTableCell242
        '
        Me.XrTableCell242.Dpi = 254.0!
        Me.XrTableCell242.Name = "XrTableCell242"
        Me.XrTableCell242.Weight = 0.079791868633959345
        '
        'XrTableCell243
        '
        Me.XrTableCell243.Dpi = 254.0!
        Me.XrTableCell243.Name = "XrTableCell243"
        Me.XrTableCell243.Weight = 0.32147920794879936
        '
        'XrTableCell244
        '
        Me.XrTableCell244.Dpi = 254.0!
        Me.XrTableCell244.Name = "XrTableCell244"
        Me.XrTableCell244.Weight = 0.29286409763295734
        '
        'XrTableCell245
        '
        Me.XrTableCell245.Dpi = 254.0!
        Me.XrTableCell245.Name = "XrTableCell245"
        Me.XrTableCell245.Text = "Kebutuhan Bhn"
        Me.XrTableCell245.Weight = 0.4384827400607183
        '
        'XrTableCell246
        '
        Me.XrTableCell246.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_kr_bhn_qty", "{0:n2}")})
        Me.XrTableCell246.Dpi = 254.0!
        Me.XrTableCell246.Name = "XrTableCell246"
        Me.XrTableCell246.Text = "XrTableCell246"
        Me.XrTableCell246.Weight = 0.40292485668647893
        '
        'XrTableCell247
        '
        Me.XrTableCell247.Dpi = 254.0!
        Me.XrTableCell247.Name = "XrTableCell247"
        Me.XrTableCell247.Text = "Rim"
        Me.XrTableCell247.Weight = 0.29043844208201086
        '
        'XrTableCell248
        '
        Me.XrTableCell248.Dpi = 254.0!
        Me.XrTableCell248.Name = "XrTableCell248"
        Me.XrTableCell248.Weight = 0.288695764992034
        '
        'XrTableCell249
        '
        Me.XrTableCell249.Dpi = 254.0!
        Me.XrTableCell249.Name = "XrTableCell249"
        Me.XrTableCell249.Weight = 0.38960339980249858
        '
        'XrTableRow32
        '
        Me.XrTableRow32.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
        Me.XrTableRow32.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell250, Me.XrTableCell251, Me.XrTableCell252, Me.XrTableCell253, Me.XrTableCell254, Me.XrTableCell255, Me.XrTableCell256, Me.XrTableCell257, Me.XrTableCell258})
        Me.XrTableRow32.Dpi = 254.0!
        Me.XrTableRow32.Name = "XrTableRow32"
        Me.XrTableRow32.StylePriority.UseBorders = False
        Me.XrTableRow32.Weight = 0.94339622641509413
        '
        'XrTableCell250
        '
        Me.XrTableCell250.Dpi = 254.0!
        Me.XrTableCell250.Name = "XrTableCell250"
        Me.XrTableCell250.Weight = 0.49571962216054355
        '
        'XrTableCell251
        '
        Me.XrTableCell251.Dpi = 254.0!
        Me.XrTableCell251.Name = "XrTableCell251"
        Me.XrTableCell251.Weight = 0.079791868633959345
        '
        'XrTableCell252
        '
        Me.XrTableCell252.Dpi = 254.0!
        Me.XrTableCell252.Name = "XrTableCell252"
        Me.XrTableCell252.Weight = 0.32147920794879936
        '
        'XrTableCell253
        '
        Me.XrTableCell253.Dpi = 254.0!
        Me.XrTableCell253.Name = "XrTableCell253"
        Me.XrTableCell253.Weight = 0.29286409763295734
        '
        'XrTableCell254
        '
        Me.XrTableCell254.Dpi = 254.0!
        Me.XrTableCell254.Name = "XrTableCell254"
        Me.XrTableCell254.Weight = 0.4384827400607183
        '
        'XrTableCell255
        '
        Me.XrTableCell255.Dpi = 254.0!
        Me.XrTableCell255.Name = "XrTableCell255"
        Me.XrTableCell255.Weight = 0.40292485668647893
        '
        'XrTableCell256
        '
        Me.XrTableCell256.Dpi = 254.0!
        Me.XrTableCell256.Name = "XrTableCell256"
        Me.XrTableCell256.Weight = 0.29043844208201086
        '
        'XrTableCell257
        '
        Me.XrTableCell257.Dpi = 254.0!
        Me.XrTableCell257.Name = "XrTableCell257"
        Me.XrTableCell257.Text = "Outsource"
        Me.XrTableCell257.Weight = 0.288695764992034
        '
        'XrTableCell258
        '
        Me.XrTableCell258.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_kr_outsource_nilai", "{0:n2}")})
        Me.XrTableCell258.Dpi = 254.0!
        Me.XrTableCell258.Name = "XrTableCell258"
        Me.XrTableCell258.StylePriority.UseTextAlignment = False
        Me.XrTableCell258.Text = "XrTableCell258"
        Me.XrTableCell258.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell258.Weight = 0.38960339980249858
        '
        'XrLabel2
        '
        Me.XrLabel2.Dpi = 254.0!
        Me.XrLabel2.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel2.Location = New System.Drawing.Point(455, 381)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel2.Size = New System.Drawing.Size(984, 42)
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.StylePriority.UseTextAlignment = False
        Me.XrLabel2.Text = "SPESIFIKASI"
        Me.XrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel1
        '
        Me.XrLabel1.Dpi = 254.0!
        Me.XrLabel1.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel1.Location = New System.Drawing.Point(455, 0)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel1.Size = New System.Drawing.Size(984, 42)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "PENGHITUNGAN COST PRODUK"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrTable1
        '
        Me.XrTable1.Dpi = 254.0!
        Me.XrTable1.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTable1.Location = New System.Drawing.Point(0, 85)
        Me.XrTable1.Name = "XrTable1"
        Me.XrTable1.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow5, Me.XrTableRow1, Me.XrTableRow2, Me.XrTableRow3, Me.XrTableRow4})
        Me.XrTable1.Size = New System.Drawing.Size(1873, 265)
        Me.XrTable1.StylePriority.UseFont = False
        '
        'XrTableRow5
        '
        Me.XrTableRow5.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell13, Me.XrTableCell14, Me.XrTableCell15})
        Me.XrTableRow5.Dpi = 254.0!
        Me.XrTableRow5.Name = "XrTableRow5"
        Me.XrTableRow5.Weight = 1
        '
        'XrTableCell13
        '
        Me.XrTableCell13.Dpi = 254.0!
        Me.XrTableCell13.Name = "XrTableCell13"
        Me.XrTableCell13.Text = "Kode"
        Me.XrTableCell13.Weight = 0.49052707840243959
        '
        'XrTableCell14
        '
        Me.XrTableCell14.Dpi = 254.0!
        Me.XrTableCell14.Name = "XrTableCell14"
        Me.XrTableCell14.StylePriority.UseTextAlignment = False
        Me.XrTableCell14.Text = ":"
        Me.XrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell14.Weight = 0.086251231409741724
        '
        'XrTableCell15
        '
        Me.XrTableCell15.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_code", "")})
        Me.XrTableCell15.Dpi = 254.0!
        Me.XrTableCell15.Name = "XrTableCell15"
        Me.XrTableCell15.Text = "XrTableCell15"
        Me.XrTableCell15.Weight = 2.4232216901878187
        '
        'XrTableRow1
        '
        Me.XrTableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell1, Me.XrTableCell2, Me.XrTableCell3})
        Me.XrTableRow1.Dpi = 254.0!
        Me.XrTableRow1.Name = "XrTableRow1"
        Me.XrTableRow1.Weight = 1
        '
        'XrTableCell1
        '
        Me.XrTableCell1.Dpi = 254.0!
        Me.XrTableCell1.Name = "XrTableCell1"
        Me.XrTableCell1.Text = "Judul Buku"
        Me.XrTableCell1.Weight = 0.49052707840243959
        '
        'XrTableCell2
        '
        Me.XrTableCell2.Dpi = 254.0!
        Me.XrTableCell2.Name = "XrTableCell2"
        Me.XrTableCell2.StylePriority.UseTextAlignment = False
        Me.XrTableCell2.Text = ":"
        Me.XrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell2.Weight = 0.086251231409741724
        '
        'XrTableCell3
        '
        Me.XrTableCell3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_judul", "")})
        Me.XrTableCell3.Dpi = 254.0!
        Me.XrTableCell3.Name = "XrTableCell3"
        Me.XrTableCell3.Text = "XrTableCell3"
        Me.XrTableCell3.Weight = 2.4232216901878187
        '
        'XrTableRow2
        '
        Me.XrTableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell4, Me.XrTableCell5, Me.XrTableCell6})
        Me.XrTableRow2.Dpi = 254.0!
        Me.XrTableRow2.Name = "XrTableRow2"
        Me.XrTableRow2.Weight = 1
        '
        'XrTableCell4
        '
        Me.XrTableCell4.Dpi = 254.0!
        Me.XrTableCell4.Name = "XrTableCell4"
        Me.XrTableCell4.Text = "Oplah cetak"
        Me.XrTableCell4.Weight = 0.49052707840243959
        '
        'XrTableCell5
        '
        Me.XrTableCell5.Dpi = 254.0!
        Me.XrTableCell5.Name = "XrTableCell5"
        Me.XrTableCell5.StylePriority.UseTextAlignment = False
        Me.XrTableCell5.Text = ":"
        Me.XrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell5.Weight = 0.086251231409741724
        '
        'XrTableCell6
        '
        Me.XrTableCell6.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_oplah", "")})
        Me.XrTableCell6.Dpi = 254.0!
        Me.XrTableCell6.Name = "XrTableCell6"
        Me.XrTableCell6.Text = "XrTableCell6"
        Me.XrTableCell6.Weight = 2.4232216901878187
        '
        'XrTableRow3
        '
        Me.XrTableRow3.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell7, Me.XrTableCell8, Me.XrTableCell9})
        Me.XrTableRow3.Dpi = 254.0!
        Me.XrTableRow3.Name = "XrTableRow3"
        Me.XrTableRow3.Weight = 1
        '
        'XrTableCell7
        '
        Me.XrTableCell7.Dpi = 254.0!
        Me.XrTableCell7.Name = "XrTableCell7"
        Me.XrTableCell7.Text = "Jenis Buku"
        Me.XrTableCell7.Weight = 0.49052707840243959
        '
        'XrTableCell8
        '
        Me.XrTableCell8.Dpi = 254.0!
        Me.XrTableCell8.Name = "XrTableCell8"
        Me.XrTableCell8.StylePriority.UseTextAlignment = False
        Me.XrTableCell8.Text = ":"
        Me.XrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell8.Weight = 0.086251231409741724
        '
        'XrTableCell9
        '
        Me.XrTableCell9.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_jns_buku", "")})
        Me.XrTableCell9.Dpi = 254.0!
        Me.XrTableCell9.Name = "XrTableCell9"
        Me.XrTableCell9.Text = "XrTableCell9"
        Me.XrTableCell9.Weight = 2.4232216901878187
        '
        'XrTableRow4
        '
        Me.XrTableRow4.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell10, Me.XrTableCell11, Me.XrTableCell12})
        Me.XrTableRow4.Dpi = 254.0!
        Me.XrTableRow4.Name = "XrTableRow4"
        Me.XrTableRow4.Weight = 1
        '
        'XrTableCell10
        '
        Me.XrTableCell10.Dpi = 254.0!
        Me.XrTableCell10.Name = "XrTableCell10"
        Me.XrTableCell10.Text = "Ukuran Buku"
        Me.XrTableCell10.Weight = 0.49052707840243959
        '
        'XrTableCell11
        '
        Me.XrTableCell11.Dpi = 254.0!
        Me.XrTableCell11.Name = "XrTableCell11"
        Me.XrTableCell11.StylePriority.UseTextAlignment = False
        Me.XrTableCell11.Text = ":"
        Me.XrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell11.Weight = 0.086251231409741724
        '
        'XrTableCell12
        '
        Me.XrTableCell12.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_ukuran", "")})
        Me.XrTableCell12.Dpi = 254.0!
        Me.XrTableCell12.Name = "XrTableCell12"
        Me.XrTableCell12.Text = "XrTableCell12"
        Me.XrTableCell12.Weight = 2.4232216901878187
        '
        'XrLabel5
        '
        Me.XrLabel5.Dpi = 254.0!
        Me.XrLabel5.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel5.Location = New System.Drawing.Point(455, 2148)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel5.Size = New System.Drawing.Size(984, 42)
        Me.XrLabel5.StylePriority.UseFont = False
        Me.XrLabel5.StylePriority.UseTextAlignment = False
        Me.XrLabel5.Text = "PASCA CETAK"
        Me.XrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrSubreport3
        '
        Me.XrSubreport3.Dpi = 254.0!
        Me.XrSubreport3.Location = New System.Drawing.Point(0, 2201)
        Me.XrSubreport3.Name = "XrSubreport3"
        Me.XrSubreport3.Size = New System.Drawing.Size(1894, 21)
        '
        'cf_total
        '
        Me.cf_total.DataMember = "Table"
        'Me.cf_total.DataSource = kc
        Me.cf_total.Expression = "[calc_cv_nilai] + [calc_isi_nilai] + [calc_kr_nilai] + [calc_sb_nilai]"
        Me.cf_total.Name = "cf_total"
        '
        'XrLabel6
        '
        Me.XrLabel6.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.cf_total", "{0:n2}")})
        Me.XrLabel6.Dpi = 254.0!
        Me.XrLabel6.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel6.Location = New System.Drawing.Point(1619, 1799)
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96.0!)
        Me.XrLabel6.Size = New System.Drawing.Size(254, 53)
        Me.XrLabel6.StylePriority.UseFont = False
        Me.XrLabel6.StylePriority.UseTextAlignment = False
        Me.XrLabel6.Text = "XrLabel6"
        Me.XrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel7
        '
        Me.XrLabel7.Dpi = 254.0!
        Me.XrLabel7.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel7.Location = New System.Drawing.Point(1450, 1799)
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96.0!)
        Me.XrLabel7.Size = New System.Drawing.Size(159, 53)
        Me.XrLabel7.StylePriority.UseFont = False
        Me.XrLabel7.Text = "TOTAL"
        '
        'XrSubreport4
        '
        Me.XrSubreport4.Dpi = 254.0!
        Me.XrSubreport4.Location = New System.Drawing.Point(0, 2297)
        Me.XrSubreport4.Name = "XrSubreport4"
        Me.XrSubreport4.Size = New System.Drawing.Size(1894, 21)
        '
        'XrLabel8
        '
        Me.XrLabel8.Dpi = 254.0!
        Me.XrLabel8.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel8.Location = New System.Drawing.Point(455, 2244)
        Me.XrLabel8.Name = "XrLabel8"
        Me.XrLabel8.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel8.Size = New System.Drawing.Size(984, 42)
        Me.XrLabel8.StylePriority.UseFont = False
        Me.XrLabel8.StylePriority.UseTextAlignment = False
        Me.XrLabel8.Text = "BIAYA TAMBAHAN"
        Me.XrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrTable3
        '
        Me.XrTable3.Dpi = 254.0!
        Me.XrTable3.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTable3.Location = New System.Drawing.Point(0, 2339)
        Me.XrTable3.Name = "XrTable3"
        Me.XrTable3.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow33, Me.XrTableRow34, Me.XrTableRow35, Me.XrTableRow36, Me.XrTableRow37, Me.XrTableRow39, Me.XrTableRow38, Me.XrTableRow40})
        Me.XrTable3.Size = New System.Drawing.Size(1873, 424)
        Me.XrTable3.StylePriority.UseFont = False
        '
        'XrTableRow33
        '
        Me.XrTableRow33.Borders = DevExpress.XtraPrinting.BorderSide.Top
        Me.XrTableRow33.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell259, Me.XrTableCell260, Me.XrTableCell261})
        Me.XrTableRow33.Dpi = 254.0!
        Me.XrTableRow33.Name = "XrTableRow33"
        Me.XrTableRow33.StylePriority.UseBorders = False
        Me.XrTableRow33.Weight = 1
        '
        'XrTableCell259
        '
        Me.XrTableCell259.Dpi = 254.0!
        Me.XrTableCell259.Name = "XrTableCell259"
        Me.XrTableCell259.Text = "Total Biaya Produksi (TBP)"
        Me.XrTableCell259.Weight = 2.5086797746117293
        '
        'XrTableCell260
        '
        Me.XrTableCell260.Dpi = 254.0!
        Me.XrTableCell260.Name = "XrTableCell260"
        Me.XrTableCell260.StylePriority.UseTextAlignment = False
        Me.XrTableCell260.Text = ":"
        Me.XrTableCell260.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell260.Weight = 0.067030729541082135
        '
        'XrTableCell261
        '
        Me.XrTableCell261.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_biaya_produksi2", "{0:n2}")})
        Me.XrTableCell261.Dpi = 254.0!
        Me.XrTableCell261.Name = "XrTableCell261"
        Me.XrTableCell261.StylePriority.UseTextAlignment = False
        Me.XrTableCell261.Text = "XrTableCell15"
        Me.XrTableCell261.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell261.Weight = 0.42428949584718878
        '
        'XrTableRow34
        '
        Me.XrTableRow34.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell274, Me.XrTableCell275, Me.XrTableCell262, Me.XrTableCell263, Me.XrTableCell264})
        Me.XrTableRow34.Dpi = 254.0!
        Me.XrTableRow34.Name = "XrTableRow34"
        Me.XrTableRow34.Weight = 1
        '
        'XrTableCell262
        '
        Me.XrTableCell262.Dpi = 254.0!
        Me.XrTableCell262.Name = "XrTableCell262"
        Me.XrTableCell262.Text = "%"
        Me.XrTableCell262.Weight = 0.9491133499529858
        '
        'XrTableCell263
        '
        Me.XrTableCell263.Dpi = 254.0!
        Me.XrTableCell263.Name = "XrTableCell263"
        Me.XrTableCell263.StylePriority.UseTextAlignment = False
        Me.XrTableCell263.Text = ":"
        Me.XrTableCell263.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell263.Weight = 0.067030729541082135
        '
        'XrTableCell264
        '
        Me.XrTableCell264.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_nilai_margin", "{0:n2}")})
        Me.XrTableCell264.Dpi = 254.0!
        Me.XrTableCell264.Name = "XrTableCell264"
        Me.XrTableCell264.StylePriority.UseTextAlignment = False
        Me.XrTableCell264.Text = "XrTableCell3"
        Me.XrTableCell264.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell264.Weight = 0.42589120433624378
        '
        'XrTableRow35
        '
        Me.XrTableRow35.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell265, Me.XrTableCell266, Me.XrTableCell267})
        Me.XrTableRow35.Dpi = 254.0!
        Me.XrTableRow35.Name = "XrTableRow35"
        Me.XrTableRow35.Weight = 1
        '
        'XrTableCell265
        '
        Me.XrTableCell265.Dpi = 254.0!
        Me.XrTableCell265.Name = "XrTableCell265"
        Me.XrTableCell265.Text = "TBP + Margin"
        Me.XrTableCell265.Weight = 2.5086797746117293
        '
        'XrTableCell266
        '
        Me.XrTableCell266.Dpi = 254.0!
        Me.XrTableCell266.Name = "XrTableCell266"
        Me.XrTableCell266.StylePriority.UseTextAlignment = False
        Me.XrTableCell266.Text = ":"
        Me.XrTableCell266.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell266.Weight = 0.067030729541082135
        '
        'XrTableCell267
        '
        Me.XrTableCell267.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_biaya_produksi_nilai_margin", "{0:n2}")})
        Me.XrTableCell267.Dpi = 254.0!
        Me.XrTableCell267.Name = "XrTableCell267"
        Me.XrTableCell267.StylePriority.UseTextAlignment = False
        Me.XrTableCell267.Text = "XrTableCell6"
        Me.XrTableCell267.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell267.Weight = 0.42428949584718878
        '
        'XrTableRow36
        '
        Me.XrTableRow36.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell268, Me.XrTableCell269, Me.XrTableCell270})
        Me.XrTableRow36.Dpi = 254.0!
        Me.XrTableRow36.Name = "XrTableRow36"
        Me.XrTableRow36.Weight = 1
        '
        'XrTableCell268
        '
        Me.XrTableCell268.Dpi = 254.0!
        Me.XrTableCell268.Name = "XrTableCell268"
        Me.XrTableCell268.Text = "PPn"
        Me.XrTableCell268.Weight = 2.5086797746117293
        '
        'XrTableCell269
        '
        Me.XrTableCell269.Dpi = 254.0!
        Me.XrTableCell269.Name = "XrTableCell269"
        Me.XrTableCell269.StylePriority.UseTextAlignment = False
        Me.XrTableCell269.Text = ":"
        Me.XrTableCell269.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell269.Weight = 0.067030729541082135
        '
        'XrTableCell270
        '
        Me.XrTableCell270.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_nilai_ppn", "{0:n2}")})
        Me.XrTableCell270.Dpi = 254.0!
        Me.XrTableCell270.Name = "XrTableCell270"
        Me.XrTableCell270.StylePriority.UseTextAlignment = False
        Me.XrTableCell270.Text = "XrTableCell9"
        Me.XrTableCell270.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell270.Weight = 0.42428949584718878
        '
        'XrTableRow37
        '
        Me.XrTableRow37.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell271, Me.XrTableCell272, Me.XrTableCell273})
        Me.XrTableRow37.Dpi = 254.0!
        Me.XrTableRow37.Name = "XrTableRow37"
        Me.XrTableRow37.Weight = 1
        '
        'XrTableCell271
        '
        Me.XrTableCell271.Dpi = 254.0!
        Me.XrTableCell271.Name = "XrTableCell271"
        Me.XrTableCell271.Text = "Total Biaya Produksi (TBP) + Margin + PPn"
        Me.XrTableCell271.Weight = 2.5086797746117293
        '
        'XrTableCell272
        '
        Me.XrTableCell272.Dpi = 254.0!
        Me.XrTableCell272.Name = "XrTableCell272"
        Me.XrTableCell272.StylePriority.UseTextAlignment = False
        Me.XrTableCell272.Text = ":"
        Me.XrTableCell272.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell272.Weight = 0.067030729541082135
        '
        'XrTableCell273
        '
        Me.XrTableCell273.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_biaya_produksi_nilai_margin_nilai_ppn", "{0:n2}")})
        Me.XrTableCell273.Dpi = 254.0!
        Me.XrTableCell273.Name = "XrTableCell273"
        Me.XrTableCell273.StylePriority.UseTextAlignment = False
        Me.XrTableCell273.Text = "XrTableCell12"
        Me.XrTableCell273.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell273.Weight = 0.42428949584718878
        '
        'XrTableCell274
        '
        Me.XrTableCell274.Dpi = 254.0!
        Me.XrTableCell274.Name = "XrTableCell274"
        Me.XrTableCell274.Text = "Margin"
        Me.XrTableCell274.Weight = 1.2543398873058647
        '
        'XrTableCell275
        '
        Me.XrTableCell275.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_margin", "")})
        Me.XrTableCell275.Dpi = 254.0!
        Me.XrTableCell275.Name = "XrTableCell275"
        Me.XrTableCell275.Text = "XrTableCell275"
        Me.XrTableCell275.Weight = 0.30362482886382397
        '
        'XrTableRow38
        '
        Me.XrTableRow38.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell284, Me.XrTableCell282, Me.XrTableCell283, Me.XrTableCell285, Me.XrTableCell276, Me.XrTableCell277, Me.XrTableCell278})
        Me.XrTableRow38.Dpi = 254.0!
        Me.XrTableRow38.Name = "XrTableRow38"
        Me.XrTableRow38.Weight = 1
        '
        'XrTableCell276
        '
        Me.XrTableCell276.Dpi = 254.0!
        Me.XrTableCell276.Name = "XrTableCell276"
        Me.XrTableCell276.Text = "Nilai Jaket"
        Me.XrTableCell276.Weight = 0.88539490241909846
        '
        'XrTableCell277
        '
        Me.XrTableCell277.Dpi = 254.0!
        Me.XrTableCell277.Name = "XrTableCell277"
        Me.XrTableCell277.StylePriority.UseTextAlignment = False
        Me.XrTableCell277.Text = ":"
        Me.XrTableCell277.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell277.Weight = 0.067030729541082135
        '
        'XrTableCell278
        '
        Me.XrTableCell278.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_nilai_jaket", "{0:n2}")})
        Me.XrTableCell278.Dpi = 254.0!
        Me.XrTableCell278.Name = "XrTableCell278"
        Me.XrTableCell278.StylePriority.UseTextAlignment = False
        Me.XrTableCell278.Text = "XrTableCell278"
        Me.XrTableCell278.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell278.Weight = 0.42428949584718878
        '
        'XrTableRow39
        '
        Me.XrTableRow39.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell279, Me.XrTableCell280, Me.XrTableCell281})
        Me.XrTableRow39.Dpi = 254.0!
        Me.XrTableRow39.Name = "XrTableRow39"
        Me.XrTableRow39.Weight = 1
        '
        'XrTableCell279
        '
        Me.XrTableCell279.Dpi = 254.0!
        Me.XrTableCell279.Name = "XrTableCell279"
        Me.XrTableCell279.Text = "Biaya Cetak 1 Buku"
        Me.XrTableCell279.Weight = 2.5086797746117293
        '
        'XrTableCell280
        '
        Me.XrTableCell280.Dpi = 254.0!
        Me.XrTableCell280.Name = "XrTableCell280"
        Me.XrTableCell280.StylePriority.UseTextAlignment = False
        Me.XrTableCell280.Text = ":"
        Me.XrTableCell280.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell280.Weight = 0.067030729541082135
        '
        'XrTableCell281
        '
        Me.XrTableCell281.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_biaya_cetak_pcs", "{0:n2}")})
        Me.XrTableCell281.Dpi = 254.0!
        Me.XrTableCell281.Name = "XrTableCell281"
        Me.XrTableCell281.StylePriority.UseTextAlignment = False
        Me.XrTableCell281.Text = "XrTableCell281"
        Me.XrTableCell281.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell281.Weight = 0.42428949584718878
        '
        'XrTableCell282
        '
        Me.XrTableCell282.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_harga_jaket", "{0:n2}")})
        Me.XrTableCell282.Dpi = 254.0!
        Me.XrTableCell282.Name = "XrTableCell282"
        Me.XrTableCell282.Text = "XrTableCell282"
        Me.XrTableCell282.Weight = 0.3724982938931885
        '
        'XrTableCell283
        '
        Me.XrTableCell283.Dpi = 254.0!
        Me.XrTableCell283.Name = "XrTableCell283"
        Me.XrTableCell283.Text = "Margin Jaket"
        Me.XrTableCell283.Weight = 0.40773588065239863
        '
        'XrTableCell284
        '
        Me.XrTableCell284.Dpi = 254.0!
        Me.XrTableCell284.Name = "XrTableCell284"
        Me.XrTableCell284.Text = "Harga Jaket"
        Me.XrTableCell284.Weight = 0.47500763719270811
        '
        'XrTableCell285
        '
        Me.XrTableCell285.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_margin_jaket", "{0:n2}")})
        Me.XrTableCell285.Dpi = 254.0!
        Me.XrTableCell285.Name = "XrTableCell285"
        Me.XrTableCell285.Text = "XrTableCell285"
        Me.XrTableCell285.Weight = 0.36804306045433566
        '
        'XrTableRow40
        '
        Me.XrTableRow40.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
        Me.XrTableRow40.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell286, Me.XrTableCell291, Me.XrTableCell292})
        Me.XrTableRow40.Dpi = 254.0!
        Me.XrTableRow40.Name = "XrTableRow40"
        Me.XrTableRow40.StylePriority.UseBorders = False
        Me.XrTableRow40.Weight = 1
        '
        'XrTableCell286
        '
        Me.XrTableCell286.Dpi = 254.0!
        Me.XrTableCell286.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCell286.Name = "XrTableCell286"
        Me.XrTableCell286.StylePriority.UseFont = False
        Me.XrTableCell286.Text = "Harga per pcs"
        Me.XrTableCell286.Weight = 2.5091774182925479
        '
        'XrTableCell291
        '
        Me.XrTableCell291.Dpi = 254.0!
        Me.XrTableCell291.Name = "XrTableCell291"
        Me.XrTableCell291.StylePriority.UseTextAlignment = False
        Me.XrTableCell291.Text = ":"
        Me.XrTableCell291.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell291.Weight = 0.066533085860263519
        '
        'XrTableCell292
        '
        Me.XrTableCell292.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.calc_biaya_cetak_final", "{0:n2}")})
        Me.XrTableCell292.Dpi = 254.0!
        Me.XrTableCell292.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCell292.Name = "XrTableCell292"
        Me.XrTableCell292.StylePriority.UseFont = False
        Me.XrTableCell292.StylePriority.UseTextAlignment = False
        Me.XrTableCell292.Text = "XrTableCell292"
        Me.XrTableCell292.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell292.Weight = 0.42428949584718878
        '
        'XrLabel9
        '
        Me.XrLabel9.Dpi = 254.0!
        Me.XrLabel9.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel9.Location = New System.Drawing.Point(0, 0)
        Me.XrLabel9.Name = "XrLabel9"
        Me.XrLabel9.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel9.Size = New System.Drawing.Size(148, 48)
        Me.XrLabel9.StylePriority.UseFont = False
        Me.XrLabel9.Text = "Print on : "
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Dpi = 254.0!
        Me.XrPageInfo1.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrPageInfo1.Format = "{0:dddd, dd MMMM yyyy HH:mm:ss}"
        Me.XrPageInfo1.Location = New System.Drawing.Point(159, 0)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo1.Size = New System.Drawing.Size(577, 42)
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrPageInfo2
        '
        Me.XrPageInfo2.Dpi = 254.0!
        Me.XrPageInfo2.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrPageInfo2.Location = New System.Drawing.Point(1640, 0)
        Me.XrPageInfo2.Name = "XrPageInfo2"
        Me.XrPageInfo2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96.0!)
        Me.XrPageInfo2.Size = New System.Drawing.Size(254, 42)
        Me.XrPageInfo2.StylePriority.UseFont = False
        Me.XrPageInfo2.StylePriority.UseTextAlignment = False
        Me.XrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'rptCalculateCost
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.ReportHeader, Me.Detail, Me.PageHeader, Me.PageFooter})
        Me.CalculatedFields.AddRange(New DevExpress.XtraReports.UI.CalculatedField() {Me.cf_opsi_isi, Me.cf_opsi_sb, Me.cf_opsi_cv, Me.cf_total})
        Me.DataAdapter = Me.OdbcDataAdapter1
        Me.DataMember = "Table"
        'Me.DataSource = Me.DsCalculateCost1
        Me.Dpi = 254.0!
        Me.DrawGrid = False
        Me.GridSize = New System.Drawing.Size(4, 4)
        Me.Margins = New System.Drawing.Printing.Margins(100, 100, 125, 125)
        Me.PageHeight = 2969
        Me.PageWidth = 2101
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.Version = "9.2"
        'CType(Me.DsCalculateCost1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents OdbcSelectCommand1 As System.Data.Odbc.OdbcCommand
    Friend WithEvents OdbcDataAdapter1 As System.Data.Odbc.OdbcDataAdapter
    Friend WithEvents OdbcConnection1 As System.Data.Odbc.OdbcConnection
    'Friend WithEvents DsCalculateCost1 As sygma_solution_system.DsCalculateCost
    Friend WithEvents cf_opsi_isi As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents cf_opsi_sb As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents cf_opsi_cv As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents XrSubreport2 As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrSubreport1 As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrTable2 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow6 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell16 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell17 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell18 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell31 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell36 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell41 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell46 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell51 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell56 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow7 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell19 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell20 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell21 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell32 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell37 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell42 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell47 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell52 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell57 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow8 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell22 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell23 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell24 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell33 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell38 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell43 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell48 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell53 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell58 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow9 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell25 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell26 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell27 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell34 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell39 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell44 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell49 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell54 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell59 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow10 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell28 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell29 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell30 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell35 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell40 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell45 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell50 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell55 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell60 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow11 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell61 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell62 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell63 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell64 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell65 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell66 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell67 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell68 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell69 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow13 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell79 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell80 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell81 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell82 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell83 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell84 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell85 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell86 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell87 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow12 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell70 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell71 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell72 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell73 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell74 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell75 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell76 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell77 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell78 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow14 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell88 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell89 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell90 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell91 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell92 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell93 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell94 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell95 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell96 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow15 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell97 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell98 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell99 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell100 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell101 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell102 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell103 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell104 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell105 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow16 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell106 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell107 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell108 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell109 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell110 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell111 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell112 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell113 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell114 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow17 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell115 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell116 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell117 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell118 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell119 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell120 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell121 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell122 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell123 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow18 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell124 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell125 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell126 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell127 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell128 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell129 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell130 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell131 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell132 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow19 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell133 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell134 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell135 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell136 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell137 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell138 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell139 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell140 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell141 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow21 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell151 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell152 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell153 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell154 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell155 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell156 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell157 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell158 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell159 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow20 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell142 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell143 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell144 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell145 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell146 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell147 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell148 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell149 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell150 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow22 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell160 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell161 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell162 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell163 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell164 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell165 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell166 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell167 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell168 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow23 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell169 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell170 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell171 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell172 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell173 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell174 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell175 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell176 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell177 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow24 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell178 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell179 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell180 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell181 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell182 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell183 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell184 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell185 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell186 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow25 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell187 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell188 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell189 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell190 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell191 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell192 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell193 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell194 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell195 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow26 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell196 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell197 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell198 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell199 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell200 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell201 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell202 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell203 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell204 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow28 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell214 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell215 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell216 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell217 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell218 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell219 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell220 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell221 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell222 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow27 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell205 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell206 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell207 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell208 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell209 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell210 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell211 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell212 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell213 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow29 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell223 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell224 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell225 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell226 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell227 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell228 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell229 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell230 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell231 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow30 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell232 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell233 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell234 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell235 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell236 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell237 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell238 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell239 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell240 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow31 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell241 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell242 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell243 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell244 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell245 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell246 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell247 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell248 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell249 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow32 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell250 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell251 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell252 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell253 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell254 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell255 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell256 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell257 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell258 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrTable1 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow5 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell13 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell14 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell15 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell3 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell4 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell5 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell6 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow3 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell7 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell8 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell9 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow4 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell10 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell11 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell12 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrSubreport3 As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents cf_total As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrSubreport4 As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents XrLabel8 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrTable3 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow33 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell259 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell260 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell261 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow34 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell262 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell263 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell264 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow35 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell265 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell266 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell267 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow36 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell268 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell269 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell270 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow37 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell271 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell272 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell273 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell274 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell275 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow39 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell279 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell280 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell281 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow38 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell276 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell277 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell278 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell284 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell282 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell283 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell285 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow40 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell286 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell291 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell292 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrLabel9 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
End Class
