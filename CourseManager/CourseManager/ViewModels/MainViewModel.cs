using Caliburn.Micro;
using CourseManager.Models;
using CourseManager.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManager.ViewModels
{
    class MainViewModel : Screen //Ez is egy Caliburn osztaly
    {
        private BindableCollection<EnrollmentModel> enrollments = new BindableCollection<EnrollmentModel>();
        private BindableCollection<StudentModel> _students = new BindableCollection<StudentModel>();
        private BindableCollection<CourseModel> _courses = new BindableCollection<CourseModel>();
        private readonly string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=CourseReport;Integrated Security=True";
        private string _appStatus;
        private EnrollmentModel _selectedEnrollment;

        public MainViewModel()
        {
            SelectedEnrollment = new EnrollmentModel();

            try
            {
                //Ezekbol a Modellekbol csinaljuk itt, a ViewModellben a View szamara hasznalhato property-ket
                StudentCommand studentCommand = new StudentCommand(_connectionString);
                Students.AddRange(studentCommand.GetList());//Adds a list from the DB to our list

                CourseCommand courseCommand = new CourseCommand(_connectionString);
                Courses.AddRange(courseCommand.GetList());//Adds a list from the DB to our list

            }
            catch (Exception ex)
            {

                AppStatus = ex.Message;
                //Mindig, ha megvaltozik az AppStatus, akkor az AppStatus property ertesuljon rola. Alert the UI. (Lasd MainView.xaml, StatusBar)
                NotifyOfPropertyChange(() => AppStatus);
            }
        }

        public BindableCollection<CourseModel> Courses
        {
            get { return _courses; }
            set { _courses = value; }
        }

        public BindableCollection<StudentModel> Students
        {
            get { return _students; }
            set { _students= value; }
        }

        public string AppStatus
        {
            get { return _appStatus; }
            set { _appStatus = value; }
        }

        public EnrollmentModel SelectedEnrollment //A ListView kivalasztott eleme
        {
            get { return _selectedEnrollment;}
            set { _selectedEnrollment = value; }//Amikor erteket kapott, akkor ertesiteseket kell kuldenunk.           
        }
    }
}
