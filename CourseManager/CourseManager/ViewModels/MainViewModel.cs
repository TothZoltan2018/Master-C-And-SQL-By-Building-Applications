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
        private BindableCollection<EnrollmentModel> _enrollments = new BindableCollection<EnrollmentModel>();
        private BindableCollection<StudentModel> _students = new BindableCollection<StudentModel>();
        private BindableCollection<CourseModel> _courses = new BindableCollection<CourseModel>();
        private readonly string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=CourseReport;Integrated Security=True";
        private string _appStatus;
        private EnrollmentModel _selectedEnrollment;
        private EnrollmentCommand _enrollmentCommand;

        public MainViewModel()
        {
            SelectedEnrollment = new EnrollmentModel();

            try
            {   //Ezekbol a Modellekbol csinaljuk itt, a ViewModellben a View szamara hasznalhato property-ket
                _enrollmentCommand = new EnrollmentCommand(_connectionString);
                Enrollments.AddRange(_enrollmentCommand.GetList());
                
                StudentCommand studentCommand = new StudentCommand(_connectionString);
                Students.AddRange(studentCommand.GetList());

                CourseCommand courseCommand = new CourseCommand(_connectionString);
                Courses.AddRange(courseCommand.GetList());//Adds a list from the DB to our list
            }
            catch (Exception ex)
            {
                AppStatus = ex.Message;
                //Mindig, ha megvaltozik az AppStatus, akkor az AppStatus property ertesuljon rola. Alert the UI. (Lasd MainView.xaml, StatusBar)
                UpdateAppStatus(ex.Message);
            }
        }

        #region These are the properties which the View can bind to.
        public CourseModel SelectedEnrollmentCourse
        {
            get
            {
                try
                {
                    var courseDictionary = _courses.ToDictionary(c => c.CourseId);

                    //Az if allandoan teljesul...
                    if (SelectedEnrollment != null && courseDictionary.ContainsKey(SelectedEnrollment.CourseId)) //Ha ki van valasztava egy enrollment a ListView-rol, es az benne vana Courses listaban
                    {
                        return courseDictionary[SelectedEnrollment.CourseId];
                    }
                }
                catch (Exception ex)
                {
                    UpdateAppStatus(ex.Message);
                }

                return null; //A dropdown ebben az esetben nem mutat semmit
            }
            set
            {
                try
                {
                    var selectedEnrollmentCourse = value;
                    SelectedEnrollment.CourseId = selectedEnrollmentCourse.CourseId;

                    NotifyOfPropertyChange(() => SelectedEnrollment);
                }
                catch (Exception ex)
                {
                    UpdateAppStatus(ex.Message);
                }
            }
        }

        public StudentModel SelectedEnrollmentStudent
        {
            get
            {
                try
                {
                    var studentDictionary = _students.ToDictionary(s => s.StudentId);

                    if (SelectedEnrollment != null && studentDictionary.ContainsKey(SelectedEnrollment.StudentId)) //Ha ki van valasztava egy enrollment a ListView-rol, es az benne vana Student listaban
                    {
                        return studentDictionary[SelectedEnrollment.StudentId];
                    }
                }
                catch (Exception ex)
                {
                    UpdateAppStatus(ex.Message);
                }

                return null; //A dropdown ebben az esetben nem mutat semmit
            }
            set
            {
                try
                {
                    var selectedEnrollmentStudent = value;
                    //ListView-nak a StudentId-ja = Student ComboBox StudentId-ja
                    SelectedEnrollment.StudentId = selectedEnrollmentStudent.StudentId;

                    NotifyOfPropertyChange(() => SelectedEnrollment);
                }
                catch (Exception ex)
                {
                    UpdateAppStatus(ex.Message);
                }
            }
        }

        public BindableCollection<EnrollmentModel> Enrollments
        {
            get { return _enrollments; }
            set { _enrollments = value; }
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
            set 
            {
                _selectedEnrollment = value;
                //Amikor erteket kapott, akkor ertesiteseket kell kuldenunk.           
                NotifyOfPropertyChange(() => SelectedEnrollment);
                NotifyOfPropertyChange(() => SelectedEnrollmentCourse);
                NotifyOfPropertyChange(() => SelectedEnrollmentStudent);
            }
        }
        #endregion

        private void UpdateAppStatus(string message)
        {            
            NotifyOfPropertyChange(() => message);
        }

    }
}
