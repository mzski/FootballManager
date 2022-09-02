using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FootballManager
{
    /// <summary>
    /// Interaction logic for the class AppointmentWindow.xaml
    /// </summary>
    public partial class AppointmentWindow : Window
    {
        public AppointmentWindow()
        {
            InitializeComponent();
            FootballManagementDBEntities db = new FootballManagementDBEntities();


            /// <summary>
            ///  this piece of code combines data from relational databases
            /// </summary>

            var result = from a in db.Appointments
                         select new
                         {
                             a.ManagerID,
                             ManagerName = a.Manager.Name,
                             a.Manager.Specialization,
                             a.PlayerID,
                            PatientName =  a.Player.Name,
                             a.Player.Contact,
                             a.AppointmentDate
                         };
            var resultOuterManager = from d in db.Managers
                                     from a in d.Appointments.DefaultIfEmpty()
                                     select new
                                     {
                                         d.Name,
                                         ApptID = a.Id.ToString(),
                                         a.AppointmentDate,
                                         Player = a.Player.Name
                                     };

            this.gridAppointments.ItemsSource = resultOuterManager.ToList();
        }
        
    }
}
