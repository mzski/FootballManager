using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FootballManager
{
    /// <summary>
    /// Interaction logic for the class MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            FootballManagementDBEntities db = new FootballManagementDBEntities();
            var mgs = from d in db.Managers
                      select new
                      {
                          ManagerName = d.Name,
                          ExperienceTime = d.Experience
                      };
            foreach (var item in mgs)
            {
                Console.WriteLine(item.ManagerName);
                Console.WriteLine(item.ExperienceTime);
            }
            this.gridManagers.ItemsSource = mgs.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            FootballManagementDBEntities db = new FootballManagementDBEntities();
            Manager managerObject = new Manager()
            {
                Name = txtName.Text,
                Specialization = txtSpecialization.Text,
                Experience = txtExperience.Text
            };
            db.Managers.Add(managerObject);
            db.SaveChanges();
        }

        /// <summary>
        ///  This button show us all Managers in DB.
        /// </summary>
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            FootballManagementDBEntities db = new FootballManagementDBEntities();
            this.gridManagers.ItemsSource = db.Managers.ToList();
        }
        private int updatingManagerID = 0;


        /// <summary>
        ///  This button show us information about the selected Manager
        /// </summary>
        private void GridManagers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (this.gridManagers.SelectedIndex >= 0)
            {
                if (this.gridManagers.SelectedItems.Count >= 0)
                {
                    
                        Manager d = (Manager)this.gridManagers.SelectedItems[0];
                        this.txtName1.Text = d.Name;
                        this.txtSpecialization1.Text = d.Specialization;
                        this.txtExperience1.Text = d.Experience;
                        this.updatingManagerID = d.Id;
                    
                }
            }
        }
        /// <summary>
        ///  This button lets us change Managers data in DB.
        /// </summary>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            FootballManagementDBEntities db = new FootballManagementDBEntities();
            var r = from d in db.Managers
                    where d.Id == this.updatingManagerID
                    select d;
            Manager obj = r.SingleOrDefault();

            if (obj != null)
            {
                obj.Name = this.txtName1.Text;
                obj.Specialization = this.txtSpecialization1.Text;
                obj.Experience = this.txtExperience1.Text;
                db.SaveChanges();
            }
        }

        /// <summary>
        ///  This button lets us remove Manager from DB.
        /// </summary>
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            
            MessageBoxResult msgBoxResult = MessageBox.Show("Are you sure you want to Delete?", "Delete Manager",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning,
                MessageBoxResult.No);

            if(msgBoxResult == MessageBoxResult.Yes)
            {
                FootballManagementDBEntities db = new FootballManagementDBEntities();
                var r = from d in db.Managers
                        where d.Id == this.updatingManagerID
                        select d;
                Manager obj = r.SingleOrDefault();

                if (obj != null)
                {
                    db.Managers.Remove(obj);
                    db.SaveChanges();
                }
            }
           
        }
        /// <summary>
        /// This button directs us to a window with information about Appointments.
        /// </summary>

        
        private void btnAppointment_Click(object sender, RoutedEventArgs e)
        {
            AppointmentWindow gridAppointments = new AppointmentWindow();
            gridAppointments.Show();
            this.Close();


            

        }

       
    }
}
