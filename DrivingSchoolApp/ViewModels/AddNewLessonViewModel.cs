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
    public class AddNewLessonViewModel:ViewModelBase
    {
        private List<Lesson> teacherLessons;
        private DrivingSchoolAppWebAPIProxy proxy;
        private IServiceProvider serviceProvider;

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
        public DateTime SelectedPickerDate //the selected date and time for the lasson
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

        private string pickupLocation;
        public string PickupLocation 
        {
            get { return pickupLocation; }
            set
            {
                pickupLocation = value;
                OnPropertyChanged();
            }
        }

        private string dropoffLocation;
        public string DropoffLocation
        {
            get { return dropoffLocation; }
            set
            {
                dropoffLocation = value;
                OnPropertyChanged();
            }
        }

        public Command ScheduleLessonCommand { get; set; }

        private async void ScheduleLesson()
        {
            Lesson l = new Lesson()
            {
                DateOfLesson = SelectedPickerDate,
                StudentId = ((App)Application.Current).LoggedInStudent.UserStudentId,
                TeacherId = ((App)Application.Current).LoggedInStudent.TeacherId,
                PickUpLoc = pickupLocation,
                DropOffLoc = dropoffLocation,
                StatusId = 1, //Pending
            };

            l = await proxy.AddLesson(l);
            if (l != null)
            {
                await Shell.Current.Navigation.PopAsync();
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "The Lesson was not scheduled", "ok");
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
        public AddNewLessonViewModel(DrivingSchoolAppWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            lessons = new List<Lesson>();
            PickerDates = new ObservableCollection<DateTime>(); 
            ReadTeacherLessons();
        }

    }
}
