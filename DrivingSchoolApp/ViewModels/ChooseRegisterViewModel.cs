using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DrivingSchoolApp.Services;
using DrivingSchoolApp.Models;
using Microsoft.Win32;
using DrivingSchoolApp.ViewModels;
using DrivingSchoolApp.View;
using Microsoft.Extensions.DependencyInjection;

namespace DrivingSchoolApp.ViewModels
{
    public class ChooseRegisterViewModel
    {
        public Command CancelCommand { get; }

        private IServiceProvider serviceProvider;
        public ChooseRegisterViewModel(IServiceProvider serviceProvider)
        {
          this.serviceProvider = serviceProvider;
        ChoseManager = new Command(OnChoseManager);
         ChoseTeacher = new Command(OnChoseTeacher);
          CancelCommand = new Command(OnCancel);
        }
        public ICommand ChoseManager { get; }
        private void OnChoseManager()
        { 
            // Navigate to the manager Register View page
            ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<RegisterManagerView>());
        }

        public ICommand ChoseTeacher { get; }
        private void OnChoseTeacher()
        {
            // Navigate to the teacher Register View page
            ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<RegisterTeacherView>());
        }


        public void OnCancel()
        {
            //Navigate back to the login page
            ((App)(Application.Current)).MainPage.Navigation.PopAsync();
        }





    }
}
