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

namespace DrivingSchoolApp.ViewModels
{
    [QueryProperty(nameof(Manager), "selectedManager")]

    public class ProfileViewModel : ViewModelBase
    {
        private DrivingSchoolAppWebAPIProxy proxy;
        private IServiceProvider serviceProvider;

        private Manager manager;
        public Manager Manager
        {
            get => manager;
            set
            {
                if (manager != value)
                {
                    manager = value;
                    //InItFieldsDataAsync();

                    OnPropertyChanged(nameof(Manager));
                }
            }
        }
        public ProfileViewModel(DrivingSchoolAppWebAPIProxy proxy, IServiceProvider service)
        {
            this.proxy = proxy;
            this.serviceProvider = service;
            ShowPasswordCommand = new Command(OnShowPassword);

        }

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

        #region ManagerPhone // entry
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

        #region SchoolPhone // entry
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

        #region SchoolName // picker
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

        #region SchoolAdress // entry
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

        #region In it Fields with data
        //Define a method to initialize the fields with data

        private async void InItFieldsDataAsync()
        {
            //FirstName = manager.FirstName;
            //LastName = manager.LastName;
            //Email = manager.Email;
            //Password = user.Pass;
            //TimeOnly time = new TimeOnly();
            //Date = user.DateOfBirth.ToDateTime(time);
            //PhoneNumber = user.PhoneNumber;
            //Address = user.UserAddress;
            //PhotoURL = user.FullProfileImagePath;
            //Gender = (char)user.Gender;
            //ReadPosts();
        }
        #endregion

    }
}
