using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrivingSchoolApp.Services;
using DrivingSchoolApp.Models;

namespace DrivingSchoolApp.ViewModels
{
    public class ApprovingTeachersViewModel:ViewModelBase
    {
        private DrivingSchoolAppWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        public ApprovingTeachersViewModel(DrivingSchoolAppWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            PendingTeachers = new ObservableCollection<Teacher>();
            LoadPendingTeachers();
            DeclineCommand = new Command(OnRegister);

        }
        private ObservableCollection<Teacher> pendingTeachers;
        public ObservableCollection<Teacher> PendingTeachers
        {
            get => pendingTeachers;
            set
            {
                pendingTeachers = value;
                OnPropertyChanged("PendingTeachers");
            }
        }

        // פעולה שמחזירה לי רשימת מורים ושומרת אותם
        private async void LoadPendingTeachers()
        {
            List<Teacher> TeacherList = await proxy.ShowPendingTeachers(((App)Application.Current).LoggedInManager.UserManagerId);
            if (TeacherList != null)
            {
                PendingTeachers = new ObservableCollection<Teacher>(TeacherList);
            }
        }
        public Command DeclineCommand { get; }
    }
}
