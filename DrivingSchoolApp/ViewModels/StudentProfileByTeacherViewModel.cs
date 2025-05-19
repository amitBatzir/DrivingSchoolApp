using DrivingSchoolApp.Models;
using DrivingSchoolApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchoolApp.ViewModels
{
    [QueryProperty(nameof(SelectedStudent), "selectedStudent")]

    public class StudentProfileByTeacherViewModel:ViewModelBase
    {
            private DrivingSchoolAppWebAPIProxy proxy;
            private IServiceProvider serviceProvider;
            public StudentProfileByTeacherViewModel(DrivingSchoolAppWebAPIProxy proxy)
            {
                this.proxy = proxy;
            }

            private Student selectedStudent;
            public Student SelectedStudent
        {
                get
                {
                    return selectedStudent;
                }
                set
                {
                selectedStudent = value;
                    LoadDetails();
                    OnPropertyChanged(nameof(SelectedStudent));
                }
            }
            private void LoadDetails()
            {
                if (SelectedStudent != null)
                {
                    FirstName = SelectedStudent.FirstName;
                    LastName = SelectedStudent.LastName;
                    Email = SelectedStudent.StudentEmail;
                    PhoneNumber = SelectedStudent.PhoneNumber;
                    Language = SelectedStudent.StudentLanguage;
                    Date = SelectedStudent.DateOfBirth;
                    TheoryDate = SelectedStudent.DateOfTheory;
                    NumOfLessons = SelectedStudent.CurrentLessonNum;
                    LengthOfLesson = SelectedStudent.LengthOfLesson;
                    Gender = SelectedStudent.Gender;
                    Internaltest = SelectedStudent.InternalTestDone;
                    Address = SelectedStudent.StudentAddress;
                }
            }

         #region FirstName 
       
            private string firstname;

            public string FirstName
            {
                get => firstname;
                set
                {
                    firstname = value;
                    OnPropertyChanged("FirstName");
                }
            }

         
            #endregion

         #region LastName 
           

            private string lastName;

            public string LastName
            {
                get => lastName;
                set
                {
                    lastName = value;
                    OnPropertyChanged("LastName");
                }
            }

          
            #endregion

         #region Email 
            
            private string email;

            public string Email
            {
                get => email;
                set
                {
                    email = value;
                    OnPropertyChanged("Email");
                }
            }

           
            #endregion
             
        #region DrivingTechnic 
            private string drivingTechnic;

            public string DrivingTechnic
            {
                get => drivingTechnic;
                set
                {
                    drivingTechnic = value;
                    OnPropertyChanged("DrivingTechnic");
                }
            }
        #endregion

        #region PhoneNumber      
        private string phoneNumber;

        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }

        #endregion

        #region Language
        private string language;
        public string Language
        {
            get => language;
            set
            {
                language = value;
                OnPropertyChanged("Language");
            }
        }
        #endregion

        #region Date Of Birth    
        private DateTime date;

        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        #endregion

        #region Date Of Theory
        private DateTime theoryDate;

        public DateTime TheoryDate
        {
            get => theoryDate;
            set
            {
                theoryDate = value;
                OnPropertyChanged("TheoryDate");
            }
        }

       
       
        #endregion

        #region Number Of Lessons
        

        private int numOfLessons;

        public int NumOfLessons
        {
            get => numOfLessons;
            set
            {
                numOfLessons = value;
                OnPropertyChanged("NumOfLessons");
            }
        }

        #endregion

        #region length of lessons

        private int lengthOfLesson;
        public int LengthOfLesson
        {
            get => lengthOfLesson;
            set
            {
                lengthOfLesson = value;
                OnPropertyChanged("LengthOfLesson");
            }
        }
        #endregion

        #region Gender

        private string gender;

        public string Gender
        {
            get => gender;
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }


        #endregion

        #region internal test
        private bool internaltest;

        public bool Internaltest
        {
            get => internaltest;
            set
            {
                internaltest = value;
                OnPropertyChanged("Internaltest");
            }
        }
        #endregion

        #region Address 
        
        private string address;

        public string Address
        {
            get => address;
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }

        #endregion

    }
}
