using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DrivingSchoolApp.Models;
using DrivingSchoolApp.Services;
using static Microsoft.Maui.ApplicationModel.Permissions;


namespace DrivingSchoolApp.ViewModels
{
    [QueryProperty(nameof(SelectedTeacher), "selectedTeacher")]
    public class TeacherProfileByManagerViewModel:ViewModelBase
    {
        private DrivingSchoolAppWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        public TeacherProfileByManagerViewModel(DrivingSchoolAppWebAPIProxy proxy)
        {
            this.proxy = proxy;
        }

        private Teacher selectedTeacher;
        public Teacher SelectedTeacher
        {
            get
            {
                return selectedTeacher;
            }
            set
            {
                selectedTeacher = value;
                LoadDetails();
                OnPropertyChanged(nameof(SelectedTeacher));
            }
        }
        private void LoadDetails()
        {
            if (SelectedTeacher != null)
            {
                FirstName = SelectedTeacher.FirstName;
                LastName = SelectedTeacher.LastName;
                Email = SelectedTeacher.TeacherEmail;
                Phone = SelectedTeacher.PhoneNumber;
                WayToPay = SelectedTeacher.WayToPay;
                Gender = SelectedTeacher.Gender;
                DrivingTechnic = SelectedTeacher.DrivingTechnic;
            }
        }

        #region FirstName 
      

        private string firstname;

        public string FirstName
        {
            get => firstname;
            set
            {
                firstname = value;
                OnPropertyChanged("FirstName");
            }
        }

        

        
        #endregion

        #region LastName 
       
        private string lastName;

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }
       
        #endregion

        #region Email     

        private string email;

        public string Email
        {
            get => email;
            set
            {
                email = value;               
                OnPropertyChanged("Email");
            }
        }
        #endregion

        #region Phone 
       
        private string phone;

        public string Phone
        {
            get => phone;
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
            }
        }

      
        #endregion

        #region WayToPay 
        private string wayToPay;

        public string WayToPay
        {
            get => wayToPay;
            set
            {
                wayToPay = value;
                OnPropertyChanged("WayToPay");
            }
        }
        #endregion

        #region Gender 
        private string gender;

        public string Gender
        {
            get => gender;
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }
        #endregion

        #region DrivingTechnic 
        private string drivingTechnic;

        public string DrivingTechnic
        {
            get => drivingTechnic;
            set
            {
                drivingTechnic = value;
                OnPropertyChanged("DrivingTechnic");
            }
        }
        #endregion


    }
}
