using DrivingSchoolApp.Models;
using DrivingSchoolApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DrivingSchoolApp.ViewModels
{
    public class PackagesViewModel:ViewModelBase
    {
        private Package Package;
        private DrivingSchoolAppWebAPIProxy proxy;
        private  IServiceProvider serviceProvider;
        public PackagesViewModel(DrivingSchoolAppWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            Packages = new ObservableCollection<Package>();

            LoadPackages(((App)Application.Current).LoggedInManager.UserManagerId);
            ShowPackagetailsCommand = new Command<Package>(OnShowPackagetails);

            Title="";
            Text = "";

            TitleError = "נדרש שם לחבילה";
            TextError = "נדרש תיאור לחבילה";

            Change = false;
            EditCommand = new Command(OnEdit);
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);


        }

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

        #region the Title
        private bool showTitleError;

        public bool ShowTitleError
        {
            get => showTitleError;
            set
            {
                showTitleError = value;
                OnPropertyChanged("ShowTitleError");
            }
        }

        private string title;

        public string Title
        {
            get => title;
            set
            {
                title = value;
                ValidateTitle();
                OnPropertyChanged("Title");
            }
        }

        private string titleError;

        public string TitleError
        {
            get => titleError;
            set
            {
                titleError = value;
                OnPropertyChanged("TitleError");
            }
        }

        private void ValidateTitle()
        {
            this.ShowTitleError = string.IsNullOrEmpty(Title);
        }
        #endregion

        #region the Text
        private bool showTextError;

        public bool ShowTextError
        {
            get => showTextError;
            set
            {
                showTextError = value;
                OnPropertyChanged("ShowTextError");
            }
        }

        private string text;

        public string Text
        {
            get => text;
            set
            {
                text = value;
                ValidateText();
                OnPropertyChanged("Text");
            }
        }

        private string textError;
        
        public string TextError
        {
            get => textError;
            set
            {
                textError = value;
                OnPropertyChanged("TextError");
            }
        }

        private void ValidateText()
        {
            this.ShowTextError = string.IsNullOrEmpty(Text);
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


        public Command SaveCommand { get; }

        //Define a method that will be called when the register button is clicked
        public async void OnSave()
        {

            ValidateText();
            ValidateTitle();
            

            if (!ShowTitleError && !ShowTextError)
            {
                Package p = SelectedPackage;
                p.Title = Title;
                p.TheText = Text;
               
                //Call the Register method on the proxy to register the new user
                InServerCall = true;
                bool success = await proxy.UpdatePackage(p);

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
                    await Shell.Current.DisplayAlert("שמור", "החבילה עודכנה בהצלחה", "ok");
                }
                else
                {
                    InServerCall = false;
                    //If the registration failed, display an error message
                    string errorMsg = "עדכון החבילה נכשל. בבקשה נסה שוב";
                    await Shell.Current.DisplayAlert("עדכון חבילה", errorMsg, "אוקיי");
                }
            }
        }
        public ICommand CancelCommand { get; }

        public async void OnCancel()
        {
            await Shell.Current.GoToAsync("PackagesView");
        }

        public Command EditCommand { get; }

        public void OnEdit()
        {
            Change = true;
        }



        public Command ShowPackagetailsCommand { get; }
        private async void OnShowPackagetails(Package Package)
        {
            if (Package == null) return;

            string message = $"מה שהחבילה מציעה : {Package.TheText}";


            await Application.Current.MainPage.DisplayAlert("פרטי החבילה", message, "סגור");
        }

    }
}
