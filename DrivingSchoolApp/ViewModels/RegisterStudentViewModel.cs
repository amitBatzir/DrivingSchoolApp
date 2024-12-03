using DrivingSchoolApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace DrivingSchoolApp.ViewModels
    {

    public class RegisterStudentViewModel : ViewModelBase
    {
        private DrivingSchoolAppWebAPIProxy proxy;
        public Command RegisterCommand { get; }
        public Command CancelCommand { get; }
        public Command UploadPhotoCommand { get; }
        public Command Student { get; }
        public Command Teacher { get; }
        public Command Manager { get; }

        public RegisterStudentViewModel(DrivingSchoolAppWebAPIProxy proxy)
        {
            this.proxy = proxy;
            RegisterCommand = new Command(OnRegister);
            CancelCommand = new Command(OnCancel);
            ShowPasswordCommand = new Command(OnShowPassword);
            UploadPhotoCommand = new Command(OnUploadPhoto);
            PhotoURL = proxy.GetDefaultProfilePhotoUrl();
            LocalPhotoPath = "";
            IsPassword = true;
            NameError = "Name is required";
            LastNameError = "Last name is required";
            EmailError = "Email is required";
            PasswordError = "Password must be at least 2 characters long and contain letters and numbers";
            LessonLengthError = "The lesngth is supposed to be 45 minutes or 60 minutes";
            SchoolNameError = "Check that you chose a school";
            LanguageError = "Check that you chose a language";

            // ask ofer what it means
            DateTime date = DateTime.Now;
            this.Date = date.AddDays(-1);
            MaxDate = date;

            // ask ofer what it means
            DateTime dateOfBirthMinus16Years = DateTime.Now.AddYears(-16).AddMonths(-9);
            this.DateOfBirth = dateOfBirthMinus16Years.AddDays(-1);
            MaxDateOfBirth = dateOfBirthMinus16Years;
        }



        //Defiine properties for each field in the registration form including error messages and validation logic

        // student

        #region FirstName // entry
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

        #region LastName // entry
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

        #region Email // entry
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
                    EmailError = "Email is not valid";
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
                EmailError = "Email is required";
            }
        }
        #endregion

        #region Password // entry
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

        #region SchoolName // picker
        private bool showSchoolNameError;

        public bool ShowSchoolNameError
        {
            get => showSchoolNameError;
            set
            {
                showFirstNameError = value;
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

        #region Language // picker
        private bool showLanguageError;

        public bool ShowLanguageError
        {
            get => showLanguageError;
            set
            {
                showLanguageError = value;
                OnPropertyChanged("ShowLanguageError");
            }
        }

        private string language;

        public string Language
        {
            get => language;
            set
            {
                language = value;
                ValidateLanguage();
                OnPropertyChanged("Language");
            }
        }

        private string languageError;

        public string LanguageError
        {
            get => languageError;
            set
            {
                languageError = value;
                OnPropertyChanged("LanguageError");
            }
        }
        private void ValidateLanguage()
        {
            this.ShowLanguageError = string.IsNullOrEmpty(Language);
        }
        #endregion

        #region DoneTheoryTest // radio button

        private string doneTheoryTest;

        public string DoneTheoryTest
        {
            get => doneTheoryTest;
            set
            {
                doneTheoryTest = value;
                OnPropertyChanged("DoneTheoryTest");
            }
        }
        #endregion

        #region DateOfTheory // picker of date
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

        private DateTime maxDate;
        public DateTime MaxDate
        {
            get => maxDate;
            set
            {
                maxDate = value;
                OnPropertyChanged("MaxDate");
            }
        }
        #endregion

        #region LessonLength // entry
        private bool showlessonLengthError;

        public bool ShowLessonLengthError
        {
            get => showlessonLengthError;
            set
            {
                showlessonLengthError = value;
                OnPropertyChanged("ShowLessonLengthError");
            }
        }

        private string lessonLength;

        public string LessonLength
        {
            get => lessonLength;
            set
            {
                lessonLength = value;
                ValidateLastName();
                OnPropertyChanged("LessonLength");
            }
        }

        private string lessonLengthError;

        public string LessonLengthError
        {
            get => lessonLengthError;
            set
            {
                lessonLengthError = value;
                OnPropertyChanged("LessonLengthError");
            }
        }

        private void ValidateLessonLength()
        {
            if (string.IsNullOrEmpty(lessonLength) || lessonLength.Length != 2 || !password.Any(char.IsLetter))
            {
                this.ShowLessonLengthError = true;
            }
            else
                this.ShowLessonLengthError = false;
        }
        #endregion

        #region HaveDocuments // radio buttons
        private string haveDocuments;

        public string HaveDocuments
        {
            get => haveDocuments;
            set
            {
                haveDocuments = value;
                OnPropertyChanged("HaveDocuments");
            }
        }
        #endregion

        #region DrivingTechnic // radio buttons
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

        #region Gender // radio buttons
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

        #region ID // entry
        private bool showidError;
        public bool ShowidError
        {
            get => showidError;
            set
            {
                showidError = value;
                OnPropertyChanged("ShowidError");
            }
        }

        private string id;

        public string ID
        {
            get => id;
            set
            {
                id = value;
                ValidateFirstName();
                OnPropertyChanged("ID");
            }
        }

        private string idError;

        public string IDError
        {
            get => idError;
            set
            {
                idError = value;
                OnPropertyChanged("IDError");
            }
        }

        private void ValidateID()
        {
            if (this.ShowidError = string.IsNullOrEmpty(ID) || !id.Any(char.IsLetter) || id.Length > 9)
            {
                this.ShowidError = true;
            }
            else
                this.ShowidError = false;
        }
        #endregion

        #region DateOfBirth // picker of date
        private DateTime dateOfBirth;
        public DateTime DateOfBirth
        {
            get => dateOfBirth;
            set
            {
                dateOfBirth = value;
                OnPropertyChanged("DateOfBirth");
            }
        }

        private DateTime maxDateOfBirth;
        public DateTime MaxDateOfBirth
        {
            get => maxDateOfBirth;
            set
            {
                maxDateOfBirth = value;
                OnPropertyChanged("MaxDateOfBirth");
            }
        }
        #endregion


    }
}
