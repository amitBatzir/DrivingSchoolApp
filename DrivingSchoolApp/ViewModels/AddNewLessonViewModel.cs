using Android.Locations;
using DrivingSchoolApp.Models;
using DrivingSchoolApp.Services;
using IntelliJ.Lang.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchoolApp.ViewModels
{
    public class AddNewLessonViewModel:ViewModelBase
    {
        private List<Lesson> teacherLessons;
        private DrivingSchoolAppWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
     
        public AddNewLessonViewModel(DrivingSchoolAppWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            lessons = new List<Lesson>();
            PickerDates = new ObservableCollection<DateTime>();
            ReadTeacherLessons();
            PickUpLocationError = "מקום איסוף נדרש";
            DropoffLocationError = "מקום הורדה נדרש";
            ScheduleLessonCommand = new Command(OnScheduleLesson);
        }


        private DateTime selectedDate;
        public DateTime SelectedDate //The date selected by user in the date picker
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                OnSelectedDateChanged();
                OnPropertyChanged();
            }
        }

        private DateTime selectedPickerDate;
        public DateTime SelectedPickerDate //the selected date and time for the lesson
        {
            get { return selectedPickerDate; }
            set
            {
                selectedPickerDate = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DateTime> pickerDates;
        public ObservableCollection<DateTime> PickerDates //All date and times available for the lesson
        {
            get { return pickerDates; }
            set
            {
                pickerDates = value;
                OnPropertyChanged();
            }
        }

        #region Pick Up Location
        private bool showPickUpLocationError;

        public bool ShowPickUpLocationError
        {
            get => showPickUpLocationError;
            set
            {
                showPickUpLocationError = value;
                OnPropertyChanged("ShowPickUpLocationError");
            }
        }

        private string pickUpLocation;
        public string PickUpLocation
        {
            get { return pickUpLocation; }
            set
            {
                pickUpLocation = value;
                ValidatePickUpLocation();
                OnPropertyChanged();
            }
        }

        private string pickUpLocationError;

        public string PickUpLocationError
        {
            get => pickUpLocationError;
            set
            {
                pickUpLocationError = value;
                OnPropertyChanged("PickUpLocationError");
            }
        }

        private void ValidatePickUpLocation()
        {
            this.ShowPickUpLocationError = string.IsNullOrEmpty(PickUpLocation);
        }

        #endregion

        #region Drop Off Location
        private bool showDropoffLocationError;

        public bool ShowDropoffLocationError
        {
            get => showDropoffLocationError;
            set
            {
                showDropoffLocationError = value;
                OnPropertyChanged("ShowDropoffLocationError");
            }
        }

        private string dropoffLocation;
        public string DropoffLocation
        {
            get { return dropoffLocation; }
            set
            {
                dropoffLocation = value;
                ValidatePDropoffLocation();
                OnPropertyChanged();
            }
        }

        private string dropoffLocationError;

        public string DropoffLocationError
        {
            get => dropoffLocationError;
            set
            {
                dropoffLocationError = value;
                OnPropertyChanged("DropoffLocationError");
            }
        }

        private void ValidatePDropoffLocation()
        {
            this.ShowDropoffLocationError = string.IsNullOrEmpty(DropoffLocation);
        }

        #endregion



        public Command ScheduleLessonCommand { get; set; }

        private async void OnScheduleLesson()
        {       
            ValidatePDropoffLocation();
            ValidatePickUpLocation();

            if (ShowPickUpLocationError || ShowDropoffLocationError /*|| SelectedPickerDate == default*/)
            {
                await Shell.Current.DisplayAlert("שגיאה", "אנא מלא את כל הפרטים", "אישור");
                return;
            }

            Lesson l = new Lesson()
            {
                DateOfLesson = SelectedPickerDate,
                StudentId = ((App)Application.Current).LoggedInStudent.UserStudentId,
                TeacherId = ((App)Application.Current).LoggedInStudent.TeacherId,
                PickUpLoc = PickUpLocation,
                DropOffLoc = DropoffLocation,
                StatusId = 1, //Pending
            };

            l = await proxy.AddLesson(l);
            if (l != null)
            {
                await Shell.Current.Navigation.PopAsync();
            }
            else
            {
                await Shell.Current.DisplayAlert("שגיאה", "בעיה בקביעת השיעור", "אוקיי");
            }
            
        }

        private List<Lesson> lessons;
        private async void ReadTeacherLessons()
        {
            List<Lesson> teacherLessons = await proxy.getTeacherLessons();
            if (teacherLessons != null)
            {
                lessons = teacherLessons;
                SelectedDate = DateTime.Now.Date;
            }
        }

        private void OnSelectedDateChanged()
        {

            PickerDates.Clear();
            for (int hour = 8; hour < 20; hour++)
            {
                DateTime current = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, hour, 0, 0);
                bool exist = lessons.Where(l => l.DateOfLesson == current && l.StatusId < 3).Any();
                if (!exist)
                {
                    PickerDates.Add(current);
                }
            }

        }

    }
}
