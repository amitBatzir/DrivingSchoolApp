using DrivingSchoolApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DrivingSchoolApp.Services;

namespace DrivingSchoolApp.ViewModels
{
    public class TeacherProfileViewModel:ViewModelBase
    {
        private Teacher currentTeacher;
        private DrivingSchoolAppWebAPIProxy proxy;
        private IServiceProvider serviceProvider;

        public TeacherProfileViewModel(DrivingSchoolAppWebAPIProxy proxy)
        {

            currentTeacher = ((App)Application.Current).LoggedInTeacher;

            this.proxy = proxy;
            EditCommand = new Command(OnEdit);
            SaveCommand = new Command(OnSave);

            FirstName = currentTeacher.FirstName;
            LastName = currentTeacher.LastName;
            Password = currentTeacher.TeacherPass;
            Email = currentTeacher.TeacherEmail;
            Phone = currentTeacher.PhoneNumber;
            schoolName = currentTeacher.SchoolName;
            Id = currentTeacher.TeacherId;
            WayToPay = currentTeacher.WayToPay;
            Gender = currentTeacher.Gender;
            DrivingTechnic = currentTeacher.DrivingTechnic;

        PhotoURL = proxy.GetImagesBaseAddress() + currentTeacher.ProfilePic;

            Change = false;
            CancelCommand = new Command(OnCancel);
            ShowPasswordCommand = new Command(OnShowPassword);
            //UploadPhotoCommand = new Command(OnUploadPhoto); לעשות עם עופר
            //UploadTakePhotoCommand = new Command(OnUploadTakePhoto);
            PhotoURL = proxy.GetDefaultProfilePhotoUrl();

            //UploadPhotoCommand = new Command(OnUploadPhoto); לעשות עם עופר
            //UploadTakePhotoCommand = new Command(OnUploadTakePhoto); 


            ShowPasswordCommand = new Command(OnShowPassword);
            LocalPhotoPath = "";
            IsPassword = true;
            PhoneError = "בדקו שכתבתם את מספר הטלפון הנכון";        
            FirstNameError = "שם פרטי נדרש";
            LastNameError = "שם משפחה נדרש";
            EmailError = "אימייל נדרש";
            PasswordError = "סיסמה אמורה להיות לפחות 3 תווים ולהכיל אותיות ומספרים"; /* "Password must be at least 2 characters long and contain letters and numbers";*/
            SchoolNameError = "בדקו שבחרתם בית ספר";
            IdError = "בדקו שכתבתם את מספר תעודת הזהות הנכון";


        }
        //RefreshCommand = new Command(Refresh);

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

        #region Phone 
        private bool showPhoneError;

        public bool ShowPhoneError
        {
            get => showPhoneError;
            set
            {
                showPhoneError = value;
                OnPropertyChanged("ShowPhoneError");
            }
        }

        private string phone;

        public string Phone
        {
            get => phone;
            set
            {
               phone = value;
                ValidatePhone();
                OnPropertyChanged("Phone");
            }
        }

        private string phoneError;

        public string PhoneError
        {
            get => phoneError;
            set
            {
               phoneError = value;
                OnPropertyChanged("PhoneError");
            }
        }

        private void ValidatePhone()
        {
            this.ShowPhoneError = string.IsNullOrEmpty(Phone) || (Phone.Length != 10 && Phone.Length != 9);
        }
        #endregion

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

        #region Id 
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

        #region WayToPay 
        private string wayToPay;

        public string WayToPay
        {
            get => wayToPay;
            set
            {
                wayToPay = value;
                OnPropertyChanged("WayToPay");
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
            ValidatePhone();
            ValidateSchoolName();
            ValidateId();

            if (!ShowFirstNameError && !ShowLastNameError && !ShowPasswordError && !ShowEmailError && !ShowPhoneError  && !ShowSchoolNameError && !ShowIdError)
            {
                Teacher teacher = ((App)App.Current).LoggedInTeacher;
                teacher.FirstName = FirstName;
                teacher.LastName = LastName;
                teacher.TeacherEmail = Email;
                teacher.TeacherPass = Password;
                teacher.PhoneNumber = Phone;
                teacher.TeacherId =Id;
                teacher.SchoolName = SchoolName;
                teacher.ProfilePic = PhotoURL;
                teacher.WayToPay = WayToPay;
                teacher.Gender = Gender;
                teacher.DrivingTechnic = DrivingTechnic;



                //Call the Register method on the proxy to register the new user
                InServerCall = true;
                bool success = await proxy.UpdateTeacher(teacher);

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
            await Shell.Current.GoToAsync("TeacherProfileView");
        }

    }
}

