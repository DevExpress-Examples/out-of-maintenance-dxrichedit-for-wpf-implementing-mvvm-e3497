using RichEditMVVMScenarioWpf.Model;
using System.Windows.Input;
using DevExpress.Xpf.Core.Commands;
using DevExpress.Xpf.RichEdit;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace RichEditMVVMScenarioWpf.ViewModel {
    public class CarInfoViewModel : ObservableObject {
        private CarInfo model;

        public CarInfoViewModel()
            : this(new CarInfo()) {

            LoadCommand.Execute(null);
        }

        public CarInfoViewModel(CarInfo model) {
            this.model = model;

            name = model.Name;
            description = model.Description;

            LoadCommand = new DevExpress.Mvvm.DelegateCommand<object>(LoadCommandExecute);
            SaveCommand = new DevExpress.Mvvm.DelegateCommand<object>(SaveCommandExecute);
        }

        private string name;
        private string description;

        public string Name {
            get { return name; }
            set {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Description {
            get { return description; }
            set {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public ICommand LoadCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        private void LoadCommandExecute(object parameter) {
            //RichEditControl richEditControl = (RichEditControl)parameter;
            //richEditControl.LoadDocument();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["RichEditMVVMScenarioWpf.Properties.Settings.CarsDBConnectionString"].ConnectionString)) {
                SqlCommand selectCommand = new SqlCommand("SELECT * FROM Cars WHERE ID = @ID", connection);

                connection.Open();
                selectCommand.Parameters.Add("@ID", SqlDbType.Int).Value = 1;

                SqlDataReader dataReader = selectCommand.ExecuteReader();

                if (dataReader.HasRows) {
                    dataReader.Read();

                    model.Name = dataReader.GetString(dataReader.GetOrdinal("Model"));
                    model.Description = dataReader.GetString(dataReader.GetOrdinal("RtfContent"));

                    this.Name = model.Name;
                    this.Description = model.Description;
                }
            }
        }

        private void SaveCommandExecute(object parameter) {
            model.Name = name;
            model.Description = description;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["RichEditMVVMScenarioWpf.Properties.Settings.CarsDBConnectionString"].ConnectionString)) {
                SqlCommand updateCommand = new SqlCommand("UPDATE Cars SET Model = @Model, RtfContent = @RtfContent WHERE ID = @ID", connection);

                connection.Open();
                updateCommand.Parameters.Add("@ID", SqlDbType.Int).Value = 1;
                updateCommand.Parameters.Add("@Model", SqlDbType.NVarChar).Value = model.Name;
                updateCommand.Parameters.Add("@RtfContent", SqlDbType.VarChar).Value = model.Description;

                int rowsAffected = updateCommand.ExecuteNonQuery();
            }
        }
    }
}
