using DrivingSchoolApp.Models;
using DrivingSchoolApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchoolApp.ViewModels
{
    public class PackagesViewModel:ViewModelBase
    {
        private DrivingSchoolAppWebAPIProxy proxy;
        private  IServiceProvider serviceProvider;
        public PackagesViewModel(DrivingSchoolAppWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            Packages = new ObservableCollection<Package>();
            LoadPackages(((App)Application.Current).LoggedInManager.UserManagerId);
            ShowPackagetailsCommand = new Command<Package>(OnShowPackagetails);
            EditCommand = new Command(OnEdit);
            SaveCommand = new Command(OnSave);

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
        z
        public string TextError
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

        public Command ShowPackagetailsCommand { get; }
        private async void OnShowPackagetails(Package Package)
        {
            if (Package == null) return;

            string message = $"מה שהחבילה מציעה : {Package.TheText}";


            await Application.Current.MainPage.DisplayAlert("פרטי החבילה", message, "סגור");
        }

    }
}
