using DrivingSchoolApp.Models;
using DrivingSchoolApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchoolApp.ViewModels
{
    [QueryProperty("ThePackage", "selectedPackage")]
    public class AddNewPackageViewModel:ViewModelBase
    {
        private DrivingSchoolAppWebAPIProxy proxy;
        private IServiceProvider serviceProvider;

        public AddNewPackageViewModel(DrivingSchoolAppWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            PackageId = 0;
            ScheduleLessonCommand = new Command(OnScheduleLesson);
            
        }

        public Command ScheduleLessonCommand { get; set; }
        //add property for a package
        private Package thePackage;
        public Package ThePackage
        {
            get
            {
                return thePackage;
            }
            set
            {
                thePackage = value;
                Title = thePackage.Title;
                Text = thePackage.TheText;
                PackageId = thePackage.PackageId;
                OnPropertyChanged();
            }
        }

        private int packageId;
        public int PackageId
        {
            get
            {
                return packageId;
            }
            set
            {
                packageId = value;
                OnPropertyChanged();
            }
        }


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

        private async void OnScheduleLesson()
        {
            ValidateTitle();
            ValidateText();

            if (ShowTitleError || ShowTextError)
            {
                await Shell.Current.DisplayAlert("שגיאה", "אנא מלא את כל הפרטים", "אישור");
                return;
            }

            Package p = new Package()
            {
                ManagerId = ((App)Application.Current).LoggedInManager.UserManagerId,
                Title = Title,
                TheText = Text,
                PackageId = PackageId,
            };

    p = await proxy.addPackage(p);
            if (p != null)
            {
                ((AppShell)Shell.Current).Refresh(typeof(PackagesViewModel));
                await Shell.Current.DisplayAlert("סטטוס", "החבילה נוספה / עודכנה בהצלחה", "אוקיי");
                await Shell.Current.Navigation.PopAsync();
            }
            else
            {
                await Shell.Current.DisplayAlert("שגיאה", "בעיה בהוספת החבילה", "אוקיי");
            }
            
        }
    }
}
