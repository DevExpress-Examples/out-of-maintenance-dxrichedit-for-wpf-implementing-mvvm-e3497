Imports Microsoft.VisualBasic
Imports RichEditMVVMScenarioWpf.Model
Imports System.Windows.Input
Imports DevExpress.Xpf.Core.Commands
Imports DevExpress.Xpf.RichEdit
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Data

Namespace RichEditMVVMScenarioWpf.ViewModel
	Public Class CarInfoViewModel
		Inherits ObservableObject
		Private model As CarInfo

		Public Sub New()
			Me.New(New CarInfo())

			LoadCommand.Execute(Nothing)
		End Sub

		Public Sub New(ByVal model As CarInfo)
			Me.model = model

			name_Renamed = model.Name
			description_Renamed = model.Description

			LoadCommand = New DelegateCommand(Of Object)(AddressOf LoadCommandExecute)
			SaveCommand = New DelegateCommand(Of Object)(AddressOf SaveCommandExecute)
		End Sub

		Private name_Renamed As String
		Private description_Renamed As String

		Public Property Name() As String
			Get
				Return name_Renamed
			End Get
			Set(ByVal value As String)
				name_Renamed = value
				OnPropertyChanged("Name")
			End Set
		End Property

		Public Property Description() As String
			Get
				Return description_Renamed
			End Get
			Set(ByVal value As String)
				description_Renamed = value
				OnPropertyChanged("Description")
			End Set
		End Property

		Private privateLoadCommand As ICommand
		Public Property LoadCommand() As ICommand
			Get
				Return privateLoadCommand
			End Get
			Private Set(ByVal value As ICommand)
				privateLoadCommand = value
			End Set
		End Property
		Private privateSaveCommand As ICommand
		Public Property SaveCommand() As ICommand
			Get
				Return privateSaveCommand
			End Get
			Private Set(ByVal value As ICommand)
				privateSaveCommand = value
			End Set
		End Property

		Private Sub LoadCommandExecute(ByVal parameter As Object)
			'RichEditControl richEditControl = (RichEditControl)parameter;
			'richEditControl.LoadDocument();

			Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("RichEditMVVMScenarioWpf.Properties.Settings.CarsDBConnectionString").ConnectionString)
				Dim selectCommand As New SqlCommand("SELECT * FROM Cars WHERE ID = @ID", connection)

				connection.Open()
				selectCommand.Parameters.Add("@ID", SqlDbType.Int).Value = 1

				Dim dataReader As SqlDataReader = selectCommand.ExecuteReader()

				If dataReader.HasRows Then
					dataReader.Read()

					model.Name = dataReader.GetString(dataReader.GetOrdinal("Model"))
					model.Description = dataReader.GetString(dataReader.GetOrdinal("RtfContent"))

					Me.Name = model.Name
					Me.Description = model.Description
				End If
			End Using
		End Sub

		Private Sub SaveCommandExecute(ByVal parameter As Object)
			model.Name = name_Renamed
			model.Description = description_Renamed

			Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("RichEditMVVMScenarioWpf.Properties.Settings.CarsDBConnectionString").ConnectionString)
				Dim updateCommand As New SqlCommand("UPDATE Cars SET Model = @Model, RtfContent = @RtfContent WHERE ID = @ID", connection)

				connection.Open()
				updateCommand.Parameters.Add("@ID", SqlDbType.Int).Value = 1
				updateCommand.Parameters.Add("@Model", SqlDbType.NVarChar).Value = model.Name
				updateCommand.Parameters.Add("@RtfContent", SqlDbType.VarChar).Value = model.Description

				Dim rowsAffected As Integer = updateCommand.ExecuteNonQuery()
			End Using
		End Sub
	End Class
End Namespace
