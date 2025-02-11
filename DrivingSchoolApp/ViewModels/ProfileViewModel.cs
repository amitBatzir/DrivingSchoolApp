using DrivingSchoolApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrivingSchoolApp.Services;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using System.Reflection;
using System.Windows.Input;
namespace DrivingSchoolApp.ViewModels
{
    [QueryProperty(nameof(Manager), "selectedManager")]

    public class ProfileViewModel : ViewModelBase
    {
        private Manager currentManager;
        private DrivingSchoolAppWebAPIProxy proxy;
        private IServiceProvider serviceProvider;

        public ProfileViewModel(DrivingSchoolAppWebAPIProxy proxy)
        {

            currentManager = ((App)Application.Current).LoggedInManager;

            this.proxy = proxy;
            EditCommand = new Command(OnEdit);
            SaveCommand = new Command(OnSave);

            FirstName = currentManager.FirstName;
            LastName = currentManager.LastName;
            Password = currentManager.ManagerPass;
            Email = currentManager.ManagerEmail;
            ManagerPhone = currentManager.ManagerPhone;
            SchoolPhone = currentManager.SchoolPhone;
            schoolName = currentManager.Schoolname;
            schoolAddress = currentManager.SchoolAddress;
            managerId = currentManager.ManagerId;

            PhotoURL = proxy.GetImagesBaseAddress() + currentManager.ProfilePic;

            EditCommand = new Command(OnEdit);
            //CancelCommand = new Command(OnCancel);
            ShowPasswordCommand = new Command(OnShowPassword);
            //UploadPhotoCommand = new Command(OnUploadPhoto); לעשות עם עופר
            //UploadTakePhotoCommand = new Command(OnUploadTakePhoto);
            PhotoURL = proxy.GetDefaultProfilePhotoUrl();

            //UploadPhotoCommand = new Command(OnUploadPhoto); לעשות עם עופר
            //UploadTakePhotoCommand = new Command(OnUploadTakePhoto); 


            ShowPasswordCommand = new Command(OnShowPassword);
            LocalPhotoPath = "";
            IsPassword = true;
            ManagerPhoneError = "בדקו שכתבתם את מספר הטלפון הנכון";
            SchoolAddressError = "השדה של כתובת בית הספר ריקה";
            FirstNameError = "שם פרטי נדרש";
            LastNameError = "שם משפחה נדרש";
            EmailError = "אימייל נדרש";
            PasswordError = "סיסמה אמורה להיות לפחות 3 תווים ולהכיל אותיות ומספרים"; /* "Password must be at least 2 characters long and contain letters and numbers";*/
            SchoolNameError = "בדקו שבחרתם בית ספר";
            ManagerIdError = "בדקו שכתבתם את מספר תעודת הזהות הנכון";
            SchoolPhoneError = " בדקו שכתבתם את מספר הטלפון הנכון";

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

        #region ManagerPhone 
        private bool showManagerPhoneError;

        public bool ShowManagerPhoneError
        {
            get => showManagerPhoneError;
            set
            {
                showManagerPhoneError = value;
                OnPropertyChanged("ShowManagerPhoneError");
            }
        }

        private string managerPhone;

        public string ManagerPhone
        {
            get => managerPhone;
            set
            {
                managerPhone = value;
                ValidateMangagerPhone();
                OnPropertyChanged("ManagerPhone");
            }
        }

        private string managerPhoneError;

        public string ManagerPhoneError
        {
            get => managerPhoneError;
            set
            {
                managerPhoneError = value;
                OnPropertyChanged("ManagerPhoneError");
            }
        }

        private void ValidateMangagerPhone()
        {
            this.ShowManagerPhoneError = string.IsNullOrEmpty(ManagerPhone) || ManagerPhone.Length != 10 || ManagerPhone.Length != 9;
        }
        #endregion

        #region SchoolPhone
        private bool showSchoolPhoneError;

        public bool ShowSchoolPhoneError
        {
            get => showSchoolPhoneError;
            set
            {
                showSchoolPhoneError = value;
                OnPropertyChanged("ShowSchoolPhoneError");
            }
        }

        private string schoolPhone;

        public string SchoolPhone
        {
            get => schoolPhone;
            set
            {
                schoolPhone = value;
                ValidateSchoolPhone();
                OnPropertyChanged("SchoolPhone");
            }
        }

        private string schoolPhoneError;

        public string SchoolPhoneError
        {
            get => schoolPhoneError;
            set
            {
                schoolPhoneError = value;
                OnPropertyChanged("SchoolPhoneError");
            }
        }

        private void ValidateSchoolPhone()
        {
            this.ShowSchoolPhoneError = string.IsNullOrEmpty(SchoolPhone) || SchoolPhone.Length != 10 || SchoolPhone.Length != 9;

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

        #region SchoolAdress 
        private bool showSchoolAddressError;

        public bool ShowSchoolAddressError
        {
            get => showSchoolAddressError;
            set
            {
                showSchoolAddressError = value;
                OnPropertyChanged("ShowSchoolAddressError");
            }
        }

        private string schoolAddress;

        public string SchoolAddress
        {
            get => schoolAddress;
            set
            {
                schoolAddress = value;
                ValidateSchoolAddress();
                OnPropertyChanged("SchoolAddress");
            }
        }

        private string schoolAddressError;

        public string SchoolAddressError
        {
            get => schoolAddressError;
            set
            {
                schoolAddressError = value;
                OnPropertyChanged("SchoolAddressError");
            }
        }

        private void ValidateSchoolAddress()
        {
            this.ShowSchoolAddressError = string.IsNullOrEmpty(SchoolAddress);
        }
        #endregion

        #region ManagerId 
        private bool showManagerIdError;

        public bool ShowManagerIdError
        {
            get => showManagerIdError;
            set
            {
                showManagerIdError = value;
                OnPropertyChanged("ShowManagerIdError");
            }
        }

        private string managerId;

        public string ManagerId
        {
            get => managerId;
            set
            {
                managerId = value;
                ValidateManagerId();
                OnPropertyChanged("ManagerId");
            }
        }

        private string managerIdError;

        public string ManagerIdError
        {
            get => managerIdError;
            set
            {
                managerIdError = value;
                OnPropertyChanged("ManagerIdError");
            }
        }

        private void ValidateManagerId()
        {
            this.ShowManagerIdError = string.IsNullOrEmpty(ManagerId) || ManagerId.Length != 9;
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

        #region Change
        private bool change;

        public bool Change
        {
            get => change;
            set
            {
                change = value;
                OnPropertyChanged("Change");
            }
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
            ValidateMangagerPhone();
            ValidateSchoolPhone();
            ValidateSchoolAddress();
            ValidateSchoolName();
            ValidateManagerId();

            if (!showFirstNameError && !showLastNameError && !showPasswordError && !ShowEmailError && !ShowManagerPhoneError && !ShowSchoolPhoneError && !ShowSchoolAddressError && !ShowSchoolNameError && !ShowManagerIdError)
            {
                Manager manager = ((App)App.Current).LoggedInManager;
                manager.FirstName = FirstName;
                manager.LastName = LastName;
                manager.ManagerEmail = Email;
                manager.ManagerPass = ManagerPhone;
                manager.ManagerPhone = ManagerPhone;
                manager.SchoolPhone = SchoolPhone;
                manager.SchoolAddress = SchoolAddress;
                manager.ManagerId = ManagerId;
                manager.Schoolname = SchoolName;


                //Call the Register method on the proxy to register the new user
                InServerCall = true;
                bool success = await proxy.UpdateManager(manager);

                Change = false;
                //If the save was successful, navigate to the login page
                if (success)
                {
                    //Upload profile imae if needed
                    if (!string.IsNullOrEmpty(LocalPhotoPath))
                    {
                        Manager? updatedManager = (Manager)await proxy.UploadProfileImage(LocalPhotoPath);
                        if (updatedManager == null)
                        {
                            await Shell.Current.DisplayAlert("שמור פרופיל", "נתוניך נשמרו אבל תמונת הפרופיל לא הוחלפה", "ok");
                        }
                        else
                        {
                            manager.ProfilePic = updatedManager.ProfilePic;
                            UpdatePhotoURL(manager.ProfilePic);
                        }

                    }
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
            await Shell.Current.GoToAsync("ProfileView");
        }

    }
}


      


