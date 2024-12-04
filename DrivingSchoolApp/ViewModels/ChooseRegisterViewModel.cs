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
        private IServiceProvider serviceProvider;
        public ChooseRegisterViewModel(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            ChoseManager = new Command(OnChoseManager);
        }
        public ICommand ChoseManager { get; }
        private void OnChoseManager()
        { 
            // Navigate to the Register View page
            ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<RegisterManagerView>());
        }





    }
}
