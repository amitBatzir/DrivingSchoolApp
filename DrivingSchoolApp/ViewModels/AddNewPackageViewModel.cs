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
    public class AddNewPackageViewModel:ViewModelBase
    {
        private DrivingSchoolAppWebAPIProxy proxy;
        private IServiceProvider serviceProvider;

        public AddNewPackageViewModel(DrivingSchoolAppWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            
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
            };

    p = await proxy.addPackage(p);
            if (p != null)
            {
                await Shell.Current.Navigation.PopAsync();
}
            else
{
    await Shell.Current.DisplayAlert("שגיאה", "בעיה בהוספת החבילה", "אוקיי");
}
            
        }
    }
}
