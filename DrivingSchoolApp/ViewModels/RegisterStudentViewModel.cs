using DrivingSchoolApp.Models;
using DrivingSchoolApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DrivingSchoolApp.View;


namespace DrivingSchoolApp.ViewModels
{

    public class RegisterStudentViewModel : ViewModelBase
    {
        private DrivingSchoolAppWebAPIProxy proxy;
        private IServiceProvider serviceProvider;

        public RegisterStudentViewModel(DrivingSchoolAppWebAPIProxy proxy, IServiceProvider serviceProvider)
        {

            this.serviceProvider = serviceProvider;
            this.proxy = proxy;
            //IsPassword = true;

            PhotoURL = proxy.GetDefaultProfilePhotoUrl();
            LocalPhotoPath = "";

            ShowPasswordCommand = new Command(OnShowPassword);
            CancelCommand = new Command(OnCancel);
            RegisterCommand = new Command(OnRegister);
            UploadPhotoCommand = new Command(OnUploadPhoto);
            TakePhotoCommand = new Command(OnTakePhoto);

            Managers = new ObservableCollection<Manager>();
            Teachers = new ObservableCollection<Teacher>();
            Packages = new ObservableCollection<Package>();
            LoadManagers();
            
            SelectedManagerError = "בדקו שבחרתם בית ספר";

            Gender = "0";
            DrivingTechnic = "0";
            FirstNameError = "שם פרטי נדרש";
            LastNameError = "שם משפחה נדרש";
            EmailError = "אימייל נדרש";
            PasswordError = "סיסמה אמורה להיות לפחות 3 תווים ולהכיל אותיות ומספרים";
            IdError = "בדקו שהכנסת את מספר הזהות הנכון";
            PhoneNumberError = "בדקו שהכנסתם את מספר הטלפון הנכון";
            DateError = "הגיל שלך קטן מהגיל המינימלי הנדרש בכדי ללמוד נהיגה";
            TheoryDateError = "תאריך הוצאת התאוריה שלך היה לפני 5 או יותר שנים ולכן התאוריה לא תקפה";
            NumOfLessonsError = "בדוק שהכנסתם מספר. במקרה שלא עשית שיעורים, הכנס/י 0";
            AddressError = "בדקו שהכנסתם כתובת";
            Language = "0";
            //CurrentDate=new DateTime()
            Date = CurrentDate;
            TheoryDate = CurrentDate2;
        }
        public Command UploadPhotoCommand { get; }
        public Command TakePhotoCommand { get; }
        public Command CancelCommand { get; }
        public Command RegisterCommand { get;  }


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

            // If the person is less than 16 years and 6 months old
            if (ageInYears < 16 || (ageInYears == 16 && ageInMonths < 6))
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
                if( yearsDifference > 5)
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
            this.ShowNumOfLessonsError = NumOfLessons < 0;
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

        #region Photo

        private string photoURL;

        public string PhotoURL
        {
            get => photoURL;
            set
            {
                photoURL = value;
                OnPropertyChanged("PhotoURL");
            }
        }

        private string localPhotoPath;

        public string LocalPhotoPath
        {
            get => localPhotoPath;
            set
            {
                localPhotoPath = value;
                OnPropertyChanged("LocalPhotoPath");
            }
        }

        //This method open the file picker to select a photo
        private async void OnTakePhoto()
        {
            try
            {
                var result = await MediaPicker.Default.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = "בבקשה תצלם/י תמונה",
                });

                if (result != null)
                {
                    // The user picked a file
                    this.LocalPhotoPath = result.FullPath;
                    this.PhotoURL = result.FullPath;
                }
            }
            catch (Exception ex)
            {
            }

        }
        //This method open the file picker to select a photo
        private async void OnUploadPhoto()
        {
            try
            {
                var result = await MediaPicker.Default.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "בבקשה תבחר/י תמונה",
                });

                if (result != null)
                {
                    // The user picked a file
                    this.LocalPhotoPath = result.FullPath;
                    this.PhotoURL = result.FullPath;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void UpdatePhotoURL(string virtualPath)
        {
            Random r = new Random();
            PhotoURL = proxy.GetImagesBaseAddress() + virtualPath + "?v=" + r.Next();
            LocalPhotoPath = "";
        }



        #endregion

        private bool ValidateForm()
        { // פעולה שבודקת האם הפרטים של המשתמש נכונים ותקינים בעזרת פעולות עזר
            //ValidateSchoolName();
            ValidateFirstName();
            ValidateLastName();
            ValidateEmail();
            ValidatePassword();
            ValidatePhoneNumber();
            ValidateId();
            ValidateDate();
            ValidateTheoryDate();
            ValidateNumOfLessons();
            ValidateAddress();

            if (ShowIdError || ShowEmailError || ShowFirstNameError || ShowPasswordError || ShowLastNameError || ShowPhoneNumberError || ShowDateError || ShowTheoryDateError ||  ShowNumOfLessonsError || ShowAddressError)
            {
                return false;
            }
            return true;
        }

        public async void OnRegister()
        {

            if (ValidateForm())
            {
                //Create a new Manager object with the data from the registration form
                var newStudent = new Student
                {
                    FirstName = FirstName,
                    StudentAddress = Address,
                    StudentLanguage = Language,
                    LastName = LastName,
                    LengthOfLesson = LengthOfLesson,
                    CurrentLessonNum = NumOfLessons,
                    InternalTestDone = Internaltest,
                    StudentEmail = Email,
                    StudentPass = Password,
                    StudentStatus = 1,
                    PhoneNumber = PhoneNumber,
                    SchoolName = SelectedManager.Schoolname,
                    TeacherId = SelectedTeacher.UserTeacherId,
                    PackageId = SelectedPackage.PackageId,
                    StudentId = Id,
                    Gender = Gender,
                    DateOfBirth = Date,
                    DateOfTheory = TheoryDate,
                    DrivingTechnic = DrivingTechnic,
                    ProfilePic = null
                };

                //Call the Register method on the proxy to register the new user
                InServerCall = true;
                newStudent = await proxy.RegisterStudent(newStudent);
                InServerCall = false;

                //If the registration was successful, navigate to the login page
                if (newStudent != null)
                {
                    //UPload profile imae if needed
                    if (!string.IsNullOrEmpty(LocalPhotoPath))
                    {
                        await proxy.LoginAsync(new LoginInfo { Email = newStudent.StudentEmail, Password = newStudent.StudentPass });
                        Student? updatedStudent = await proxy.UploadProfileImageStudent(LocalPhotoPath);
                        if (updatedStudent == null)
                        {
                            InServerCall = false;
                            await Application.Current.MainPage.DisplayAlert("הרשמה", "הנתונים שלך נרשמו אבל העלאת תמונה הפרופיל נכשלה", "OK");
                        }
                    }
                    InServerCall = false;

                    await Application.Current.MainPage.DisplayAlert("הפעולה הצליחה", "הנתונים נרשמו, תוכל/י להיכנס למערכת לאחר אישור המורה", "OK");
                    LoginView login = serviceProvider.GetService<LoginView>();
                    //homePageViewModel.Refresh(); //Refresh data and user in the homepageViewModel as it is a singleton
                    ((App)Application.Current).MainPage = login;
                }
                else
                {

                    //If the registration failed, display an error message
                    string errorMsg = "ההרשמה נכשלה, בבקשה נסה שוב";
                    await Application.Current.MainPage.DisplayAlert("הרשמה", errorMsg, "OK");
                }
            }
        }


        #region load managers
        private ObservableCollection<Manager> managers;

        public ObservableCollection<Manager> Managers
        {
            get => managers;
            set
            {
                managers = value;
                OnPropertyChanged("Managers");
            }
        }

        // פעולה שמחזירה לי רשימת מנהלים ושומרת אותם
        private async void LoadManagers()
        {
            List<Manager> managerList = await proxy.GetSchools();
            if (managerList != null)
            {
                foreach (Manager manager in managerList)
                {
                    Managers.Add(manager);
                }
            }          
        }
        #endregion

        #region load teachers
        private ObservableCollection<Teacher> teachers;

        public ObservableCollection<Teacher> Teachers
        {
            get => teachers;
            set
            {
                teachers = value;
                OnPropertyChanged("Teachers");
            }
        }
        private async void LoadTeachers(int managerId)
        {
            List<Teacher> teacherList = await proxy.GetTeacherOfSchool(managerId);
            if (teacherList != null)
                foreach (Teacher t in teacherList)
                {
                    Teachers.Add(t);
                }

        }
        #endregion

        #region load packages
        private ObservableCollection<Package> packages;

        public ObservableCollection<Package> Packages
        {
            get => packages;
            set
            {
                packages = value;
                OnPropertyChanged("Packages");
            }
        }
        private async void LoadPackages(int managerId)
        {
            List<Package> packageslist = await proxy.GetPackageOfSchool(managerId);
            if (packageslist != null)
                foreach (Package p in packageslist)
                {
                    Packages.Add(p);
                }

        }
        #endregion

        #region Selected Manager
        private bool showSelectedManagerError;
        public bool ShowSelectedManagerError
        {
            get => showSelectedManagerError;
            set
            {
                showSelectedManagerError = value;
                OnPropertyChanged("ShowSelectedManagerError");
            }
        }
        private Manager selectedManager;
        public Manager SelectedManager
        {
            get
            {
                return this.selectedManager;
            }
            set
            {
                selectedManager = value;
                ValidateSelectedManager();
                LoadTeachers(SelectedManager.UserManagerId);
                LoadPackages(SelectedManager.UserManagerId);
                OnPropertyChanged("SelectedManager");
            }
        }
        private string selectedManagerError;

        public string SelectedManagerError
        {
            get => selectedManagerError;
            set
            {
                selectedManagerError = value;
                OnPropertyChanged("SelectedManagerError");
            }
        }

        private void ValidateSelectedManager()
        {
            this.ShowSelectedManagerError = SelectedManager == null;
        }
        #endregion

        #region selected teacher
        private bool showSelectedTeacherError;
        public bool ShowSelectedTeacherError
        {
            get => showSelectedTeacherError;
            set
            {
                showSelectedTeacherError = value;
                OnPropertyChanged("ShowSelectedTeacherError");
            }
        }
        private Teacher selectedTeacher;
        public Teacher SelectedTeacher
        {
            get
            {
                return this.selectedTeacher;
            }
            set
            {
                selectedTeacher = value;
                ValidateSelectedTeacher();
                OnPropertyChanged("SelectedTeacher");
            }
        }
        private string selectedTeacherError;

        public string SelectedTeacherError
        {
            get => selectedTeacherError;
            set
            {
                selectedTeacherError = value;
                OnPropertyChanged("SelectedTeacherError");
            }
        }

        private void ValidateSelectedTeacher()
        {
            this.ShowSelectedTeacherError = SelectedTeacher == null;
        }
        #endregion

        #region selected package
        private bool showSelectedPackageError;
        public bool ShowSelectedPackageError
        {
            get => showSelectedPackageError;
            set
            {
                showSelectedPackageError = value;
                OnPropertyChanged("ShowSelectedPackageError");
            }
        }
        private Package selectedPackage;
        public Package SelectedPackage
        {
            get
            {
                return this.selectedPackage;
            }
            set
            {
                selectedPackage = value;
                ValidateSelectedPackage();
                OnPropertyChanged("SelectedPackage");
            }
        }
        private string selectedPackageError;

        public string SelectedPackageError
        {
            get => selectedPackageError;
            set
            {
                selectedPackageError = value;
                OnPropertyChanged("SelectedPackageError");
            }
        }

        private void ValidateSelectedPackage()
        {
            this.ShowSelectedPackageError = SelectedPackage == null;
        }
        #endregion
        public void OnCancel()
        {
            //Navigate back to the login page
            ((App)(Application.Current)).MainPage.Navigation.PopAsync();
        }
    }
}

