using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DrivingSchoolApp.Models;
using DrivingSchoolApp.Services;


namespace DrivingSchoolApp.ViewModels
{
    public class StudentProfileViewModel:ViewModelBase
    {

        private Student currentStudent;
        private DrivingSchoolAppWebAPIProxy proxy;
        private IServiceProvider serviceProvider;

        public StudentProfileViewModel(DrivingSchoolAppWebAPIProxy proxy)
        {
            currentStudent = ((App)Application.Current).LoggedInStudent;

            this.proxy = proxy;
            EditCommand = new Command(OnEdit);
            SaveCommand = new Command(OnSave);
            Change = false;
            CancelCommand = new Command(OnCancel);
            ShowPasswordCommand = new Command(OnShowPassword);

            FirstName = currentStudent.FirstName;
            LastName = currentStudent.LastName;
            Password = currentStudent.StudentPass;
            Email = currentStudent.StudentEmail;
            DrivingTechnic = currentStudent.DrivingTechnic;
            Id = currentStudent.StudentId;
            PhoneNumber = currentStudent.PhoneNumber;
            Language = currentStudent.StudentLanguage;
            Date = currentStudent.DateOfBirth;
            TheoryDate = currentStudent.DateOfTheory;
            NumOfLessons = currentStudent.CurrentLessonNum;
            LengthOfLesson = currentStudent.LengthOfLesson;
            Gender = currentStudent.Gender;
            Internaltest= currentStudent.InternalTestDone;
            Address = currentStudent.StudentAddress;
            SchoolName = currentStudent.SchoolName;
            Teacher = currentStudent.TeacherId;
            Package = currentStudent.PackageId;
           
            IsPassword = true;
            PhoneNumberError = "בדקו שכתבתם את מספר הטלפון הנכון";
            FirstNameError = "שם פרטי נדרש";
            LastNameError = "שם משפחה נדרש";
            EmailError = "אימייל נדרש";
            PasswordError = "סיסמה אמורה להיות לפחות 3 תווים ולהכיל אותיות ומספרים"; /* "Password must be at least 2 characters long and contain letters and numbers";*/
            SchoolNameError = "בדקו שבחרתם בית ספר";
            IdError = "בדקו שכתבתם את מספר תעודת הזהות הנכון";
            DateError = "הגיל שלך אינו מתאים ללימוד נהיגה";
            TheoryDateError = "התאוריה שלך אינה תקפה";
            NumOfLessonsError = "מספר השיעורים שלך אינו תקין";
            AddressError = "הכתובת שלך אינה תקינה";

        }

        #region SchoolName
        private bool showSchoolNameError;

        public bool ShowSchoolNameError
        {
            get => showSchoolNameError;
            set
            {
                showSchoolNameError = value;
                OnPropertyChanged("ShowSchoolNameError");
            }
        }

        private string schoolName;

        public string SchoolName
        {
            get => schoolName;
            set
            {
                schoolName = value;
                ValidateSchoolName();
                OnPropertyChanged("SchoolName");
            }
        }

        private string schoolNameError;

        public string SchoolNameError
        {
            get => schoolNameError;
            set
            {
                schoolNameError = value;
                OnPropertyChanged("SchoolNameError");
            }
        }
        private void ValidateSchoolName()
        {
            this.ShowSchoolNameError = string.IsNullOrEmpty(SchoolName);
        }
        #endregion

        #region Teacher
        private int teacher;

        public int Teacher
        {
            get => teacher;
            set
            {
                teacher = value;
                OnPropertyChanged("Teacher");
            }
        }
        #endregion

        #region Package
        private int package;

        public int Package
        {
            get => package;
            set
            {
                package = value;
                OnPropertyChanged("Package");
            }
        }
        #endregion

        #region FirstName 
        private bool showFirstNameError;

        public bool ShowFirstNameError
        {
            get => showFirstNameError;
            set
            {
                showFirstNameError = value;
                OnPropertyChanged("ShowFirstNameError");
            }
        }

        private string firstname;

        public string FirstName
        {
            get => firstname;
            set
            {
                firstname = value;
                ValidateFirstName();
                OnPropertyChanged("FirstName");
            }
        }

        private string firstnameError;

        public string FirstNameError
        {
            get => firstnameError;
            set
            {
                firstnameError = value;
                OnPropertyChanged("FirstNameError");
            }
        }

        private void ValidateFirstName()
        {
            this.ShowFirstNameError = string.IsNullOrEmpty(FirstName);
        }
        #endregion

        #region LastName
        private bool showLastNameError;

        public bool ShowLastNameError
        {
            get => showLastNameError;
            set
            {
                showLastNameError = value;
                OnPropertyChanged("ShowLastNameError");
            }
        }

        private string lastName;

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                ValidateLastName();
                OnPropertyChanged("LastName");
            }
        }

        private string lastNameError;

        public string LastNameError
        {
            get => lastNameError;
            set
            {
                lastNameError = value;
                OnPropertyChanged("LastNameError");
            }
        }

        private void ValidateLastName()
        {
            this.ShowLastNameError = string.IsNullOrEmpty(LastName);

        }
        #endregion

        #region Password
        private bool showPasswordError;

        public bool ShowPasswordError
        {
            get => showPasswordError;
            set
            {
                showPasswordError = value;
                OnPropertyChanged("ShowPasswordError");
            }
        }

        private string password;

        public string Password
        {
            get => password;
            set
            {
                password = value;
                ValidatePassword();
                OnPropertyChanged("Password");
            }
        }

        private string passwordError;

        public string PasswordError
        {
            get => passwordError;
            set
            {
                passwordError = value;
                OnPropertyChanged("PasswordError");
            }
        }

        private void ValidatePassword()
        {
            //Password must include characters and numbers and be longer than 2 characters
            if (string.IsNullOrEmpty(password) ||
                password.Length < 2 ||
                !password.Any(char.IsDigit) ||
                !password.Any(char.IsLetter))
            {
                this.ShowPasswordError = true;
            }
            else
                this.ShowPasswordError = false;
        }

        //This property will indicate if the password entry is a password
        private bool isPassword = true;
        public bool IsPassword
        {
            get => isPassword;
            set
            {
                isPassword = value;
                OnPropertyChanged("IsPassword");
            }
        }
        //This command will trigger on pressing the password eye icon
        public Command ShowPasswordCommand { get; }
        //This method will be called when the password eye icon is pressed
        public void OnShowPassword()
        {
            //Toggle the password visibility
            IsPassword = !IsPassword;
        }
        #endregion

        #region Email
        private bool showEmailError;

        public bool ShowEmailError
        {
            get => showEmailError;
            set
            {
                showEmailError = value;
                OnPropertyChanged("ShowEmailError");
            }
        }

        private string email;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                ValidateEmail();
                OnPropertyChanged("Email");
            }
        }

        private string emailError;

        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged("EmailError");
            }
        }

        private void ValidateEmail()
        {
            this.ShowEmailError = string.IsNullOrEmpty(Email);
            if (!ShowEmailError)
            {
                //check if email is in the correct format using regular expression
                if (!System.Text.RegularExpressions.Regex.IsMatch(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                {
                    EmailError = "אימייל לא תקין";
                    ShowEmailError = true;
                }
                else
                {
                    EmailError = "";
                    ShowEmailError = false;
                }
            }
            else
            {
                EmailError = "אימייל נדרש";
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

        #region ID
        private bool showIdError;

        public bool ShowIdError
        {
            get => showIdError;
            set
            {
                showIdError = value;
                OnPropertyChanged("ShowIdError");
            }
        }

        private string id;

        public string Id
        {
            get => id;
            set
            {
                id = value;
                ValidateId();
                OnPropertyChanged("Id");
            }
        }

        private string idError;

        public string IdError
        {
            get => idError;
            set
            {
                idError = value;
                OnPropertyChanged("IdError");
            }
        }

        private void ValidateId()
        {
            this.ShowIdError = string.IsNullOrEmpty(Id) || Id.Length != 9;
        }
        #endregion

        #region PhoneNumber
        private bool showPhoneNumberError;

        public bool ShowPhoneNumberError
        {
            get => showPhoneNumberError;
            set
            {
                showPhoneNumberError = value;
                OnPropertyChanged("ShowPhoneNumberError");
            }
        }

        private string phoneNumber;

        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
                ValidatePhoneNumber();
                OnPropertyChanged("PhoneNumber");
            }
        }

        private string phoneNumberError;

        public string PhoneNumberError
        {
            get => phoneNumberError;
            set
            {
                phoneNumberError = value;
                OnPropertyChanged("PhoneNumberError");
            }
        }

        private void ValidatePhoneNumber()
        {
            this.ShowPhoneNumberError = string.IsNullOrEmpty(PhoneNumber) || (PhoneNumber.Length != 10 && PhoneNumber.Length != 9);

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
        private bool showDateError;

        public bool ShowDateError
        {
            get => showDateError;
            set
            {
                showDateError = value;
                OnPropertyChanged("ShowDateError");
            }
        }

        private DateTime date;

        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                ValidateDate();
                OnPropertyChanged("Date");
            }
        }


        private string dateError;

        public string DateError
        {
            get => dateError;
            set
            {
                dateError = value;
                OnPropertyChanged("DateError");
            }
        }
        private void ValidateDate()
        {
            DateTime currentDate = DateTime.Now;

            // Calculate the age at the current date
            int ageInYears = currentDate.Year - this.Date.Year;
            if (currentDate.Month < this.Date.Month || (currentDate.Month == this.Date.Month && currentDate.Day < this.Date.Day))
            {
                ageInYears--; // Subtract one year if the birthday hasn't occurred yet this year
            }

            // Calculate the months difference
            int ageInMonths = currentDate.Month - this.Date.Month;
            if (currentDate.Month < this.Date.Month)
            {
                ageInMonths += 12; // Handle month wrapping when current month is earlier than the birth month
            }

            // If the person is less than 16 years and 9 months old
            if (ageInYears < 16 || (ageInYears == 16 && ageInMonths < 9))
            {
                this.ShowDateError = true;
            }
            else
            {
                this.ShowDateError = false;
            }



        }
        private DateTime currentDate;
        public DateTime CurrentDate
        {
            get => DateTime.Now; // Return the current date whenever the property is accessed
            set
            {
                // You don't need to set the value since we want the property to always return DateTime.Now
                currentDate = value;
            }
        }


        #endregion

        #region Date Of Theory
        private bool showTheoryDateError;

        public bool ShowTheoryDateError
        {
            get => showTheoryDateError;
            set
            {
                showTheoryDateError = value;
                OnPropertyChanged("ShowTheoryDateError");
            }
        }

        private DateTime theoryDate;

        public DateTime TheoryDate
        {
            get => theoryDate;
            set
            {
                theoryDate = value;
                ValidateTheoryDate();
                OnPropertyChanged("TheoryDate");
            }
        }


        private string theoryDateError;

        public string TheoryDateError
        {
            get => theoryDateError;
            set
            {
                theoryDateError = value;
                OnPropertyChanged("TheoryDateError");
            }
        }
        private void ValidateTheoryDate()
        {
            // Get the current date
            DateTime currentDate = DateTime.Now;

            // Calculate the difference in years between the current date and the exam date
            int yearsDifference = currentDate.Year - this.TheoryDate.Year;

            // If the current date is before the exam's anniversary this year, subtract one year
            if (currentDate.Month < this.TheoryDate.Month || (currentDate.Month == this.TheoryDate.Month && currentDate.Day < this.TheoryDate.Day))
            {
                yearsDifference--;
            }

            // If the exam was taken more than 5 years ago, return true
            if (yearsDifference > 5)
            {
                this.ShowTheoryDateError = true;
            }
            else
            {
                this.ShowTheoryDateError = false;

            }

        }
        private DateTime currentDate2;
        public DateTime CurrentDate2
        {
            get => DateTime.Now; // Return the current date whenever the property is accessed
            set
            {
                // You don't need to set the value since we want the property to always return DateTime.Now
                currentDate2 = value;
            }
        }
        #endregion

        #region Number Of Lessons
        private bool showNumOfLessonsError;

        public bool ShowNumOfLessonsError
        {
            get => showNumOfLessonsError;
            set
            {
                showNumOfLessonsError = value;
                OnPropertyChanged("ShowNumOfLessonsError");
            }
        }

        private int numOfLessons;

        public int NumOfLessons
        {
            get => numOfLessons;
            set
            {
                numOfLessons = value;
                ValidateNumOfLessons();
                OnPropertyChanged("NumOfLessons");
            }
        }

        private string numOfLessonsError;

        public string NumOfLessonsError
        {
            get => numOfLessonsError;
            set
            {
                numOfLessonsError = value;
                OnPropertyChanged("NumOfLessonsError");
            }
        }

        private void ValidateNumOfLessons()
        {
            this.ShowNumOfLessonsError = NumOfLessons<0 ;
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
        private bool showAddressError;

        public bool ShowAddressError
        {
            get => showAddressError;
            set
            {
                showAddressError = value;
                OnPropertyChanged("ShowAddressError");
            }
        }

        private string address;

        public string Address
        {
            get => address;
            set
            {
                address = value;
                ValidateAddress();
                OnPropertyChanged("Address");
            }
        }

        private string addressError;

        public string AddressError
        {
            get => addressError;
            set
            {
                addressError = value;
                OnPropertyChanged("AddressError");
            }
        }

        private void ValidateAddress()
        {
            this.ShowAddressError = string.IsNullOrEmpty(Address);
        }
        #endregion

        #region Change
        private bool change;

        public bool Change
        {
            get => change;
            set
            {
                change = value;
                OnPropertyChanged("Change");
                OnPropertyChanged("ShowEditButton");
            }
        }
        public bool ShowEditButton
        {
            get => !change;
        }
        #endregion

        public Command EditCommand { get; }

        public void OnEdit()
        {
            Change = true;
        }

        public Command SaveCommand { get; }

        //Define a method that will be called when the register button is clicked
        public async void OnSave()
        {

            ValidateFirstName();
            ValidateLastName();
            ValidatePassword();
            ValidateEmail();
            ValidatePhoneNumber();
            ValidateSchoolName();
            ValidateId();
            ValidateDate();
            ValidateTheoryDate();
            ValidateNumOfLessons();
            ValidateAddress();

            if (!showFirstNameError && !showLastNameError && !showPasswordError && !ShowEmailError && !ShowPhoneNumberError && !ShowSchoolNameError && !ShowIdError && !ShowDateError && !ShowTheoryDateError && !showNumOfLessonsError && showAddressError)
            {
                Student student = ((App)App.Current).LoggedInStudent;
                student.FirstName = FirstName;
                student.LastName = LastName;
                student.SchoolName = SchoolName;
                student.TeacherId = Teacher;
                student.PackageId = package;
                student.StudentPass = Password;
                student.StudentEmail = Email;
                student.DrivingTechnic = DrivingTechnic;
                student.StudentId = Id;
                student.PhoneNumber = PhoneNumber;
                student.StudentLanguage = Language;
                student.DateOfBirth = Date;
                student.DateOfTheory = TheoryDate;
                student.CurrentLessonNum = NumOfLessons;
                student.LengthOfLesson = LengthOfLesson;
                student.Gender = Gender;
                student.InternalTestDone = Internaltest;
                student.StudentAddress = Address;


                //Call the Register method on the proxy to register the new user
                InServerCall = true;
                bool success = await proxy.UpdateStudent(student);
                Change = false;

                //If the save was successful, navigate to the login page
                if (success)
                {
                    // Upload profile imae if needed
                    //if (!string.IsNullOrEmpty(LocalPhotoPath))
                    //    {
                    //        Teacher? updatedTeacher = (Teacher)await proxy.UploadProfileImage(LocalPhotoPath);
                    //        if (updatedTeacher == null)
                    //        {
                    //            await Shell.Current.DisplayAlert("שמור פרופיל", "נתוניך נשמרו אבל תמונת הפרופיל לא הוחלפה", "ok");
                    //        }
                    //        else
                    //        {
                    //            Teacher.ProfilePic = updatedTeacher.ProfilePic;
                    //            UpdatePhotoURL(Teacher.ProfilePic);
                    //        }
                    //    }
                    InServerCall = false;
                    await Shell.Current.DisplayAlert("שמור פרופיל", "הפרופיל נשמר בהצלחה", "ok");
                }
                else
                {
                    InServerCall = false;
                    //If the registration failed, display an error message
                    string errorMsg = "שמירת הפרופיל נכשלה. בבקשה נסה שוב";
                    await Shell.Current.DisplayAlert("שמור פרופיל", errorMsg, "ok");
                }
            }
        }
        public ICommand CancelCommand { get; }

        public async void OnCancel()
        {
            await Shell.Current.GoToAsync("StudentProfileView");
        }

    

}
}
