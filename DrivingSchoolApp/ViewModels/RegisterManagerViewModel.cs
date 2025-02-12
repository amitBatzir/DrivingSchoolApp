using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrivingSchoolApp.Models;
using DrivingSchoolApp.Services;
using DrivingSchoolApp.View;
using Microsoft.Extensions.DependencyInjection;

namespace DrivingSchoolApp.ViewModels
{
    public class RegisterManagerViewModel : ViewModelBase
    {
        private DrivingSchoolAppWebAPIProxy proxy;
        public Command RegisterCommand { get; }
        public Command CancelCommand { get; }
        public Command UploadPhotoCommand { get; }
        public Command Student { get; }
        public Command Teacher { get; }
        public Command Manager { get; }
        private IServiceProvider serviceProvider;
        public RegisterManagerViewModel(DrivingSchoolAppWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.proxy = proxy;
            RegisterCommand = new Command(OnRegister);
            CancelCommand = new Command(OnCancel);
            ShowPasswordCommand = new Command(OnShowPassword);
            UploadPhotoCommand = new Command(OnUploadPhoto);
            PhotoURL = proxy.GetDefaultProfilePhotoUrl();
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
            this.ShowManagerPhoneError = string.IsNullOrEmpty(ManagerPhone) || (ManagerPhone.Length != 10 && ManagerPhone.Length != 9);

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
            this.ShowSchoolPhoneError = string.IsNullOrEmpty(SchoolPhone) || (SchoolPhone.Length != 10  && SchoolPhone.Length != 9);

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

        #region ManagerId // entry
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



        //Define a command for the register button
        private bool ValidateForm()
        { // פעולה שבודקת האם הפרטים של המשתמש נכונים ותקינים בעזרת פעולות עזר
            ValidateSchoolName();
            ValidateFirstName();
            ValidateLastName();
            ValidateEmail();
            ValidatePassword();
            ValidateMangagerPhone();
            ValidateSchoolPhone();
            ValidateSchoolAddress();
            ValidateManagerId();
            if (ShowManagerIdError || ShowEmailError || ShowSchoolAddressError || ShowFirstNameError || ShowPasswordError || ShowSchoolNameError || ShowLastNameError || ShowManagerPhoneError || ShowSchoolPhoneError)
            {
                return false;
            }
            return true;
        }

        //Define a method that will be called when the register button is clicked
        public async void OnRegister()
        {
            if (ValidateForm())
            {
                //Create a new Manager object with the data from the registration form
                var newManager = new Manager
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    ManagerEmail = Email,
                    ManagerPass = Password,
                    ManagerStatus = 1,
                    SchoolAddress = SchoolAddress,
                    ManagerPhone = ManagerPhone,
                    SchoolPhone = SchoolPhone,
                    Schoolname = SchoolName,
                    ManagerId = ManagerId,
                    ProfilePic = PhotoURL
                };

                //Call the Register method on the proxy to register the new user
                InServerCall = true;
                newManager = await proxy.RegisterManager(newManager);
                InServerCall = false;

                //If the registration was successful, navigate to the login page
                if (newManager != null)
                {
                    //UPload profile imae if needed
                    if (!string.IsNullOrEmpty(LocalPhotoPath))
                    {
                        await proxy.LoginAsync(new LoginInfo { Email = newManager.ManagerEmail, Password = newManager.ManagerPass});
                        Manager? updatedManager = await proxy.UploadProfileImage(LocalPhotoPath);
                        if (updatedManager == null)
                        {
                            InServerCall = false;
                            await Application.Current.MainPage.DisplayAlert("הרשמה", "הנתונים שלך נרשמו אבל העלאת תמונה הפרופיל נכשלה", "OK");
                        }
                    }
                    InServerCall = false;

                    await Application.Current.MainPage.DisplayAlert("הפעולה הצליחה", "הנתונים נרשמו, תוכל/י להיכנס למערכת לאחר אישור מנהל המערכת", "OK");
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

        //Define a method that will be called upon pressing the cancel button
        public void OnCancel()
        {
            //Navigate back to the login page
            ((App)(Application.Current)).MainPage.Navigation.PopAsync();
        }

    }
}
