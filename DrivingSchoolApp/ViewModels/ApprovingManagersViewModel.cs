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
    public class ApprovingManagersViewModel : ViewModelBase
    {
       
            private DrivingSchoolAppWebAPIProxy proxy;
            private IServiceProvider serviceProvider;
            public ApprovingManagersViewModel(DrivingSchoolAppWebAPIProxy proxy, IServiceProvider serviceProvider)
            {
                this.proxy = proxy;
                this.serviceProvider = serviceProvider;
                PendingManagers = new ObservableCollection<Manager>();
                LoadPendingManagers();
                ApproveCommand = new Command<Manager>(OnApproving);
                DeclineCommand = new Command<Manager>(OnDeclining);
                //Check = false;
            }
            private ObservableCollection<Manager> pendingManagers;
            public ObservableCollection<Manager> PendingManagers
        {
                get => pendingManagers;
                set
                {
                pendingManagers = value;
                    OnPropertyChanged("PendingManagers");
                }
            }

            // פעולה שמחזירה לי רשימת נהלים ושומרת אותם
            private async void LoadPendingManagers()
            {
                List<Manager> ManagerList = await proxy.ShowPendingManagers();
                if (ManagerList != null)
                {
                PendingManagers = new ObservableCollection<Manager>(ManagerList);
                }
            }
            
            public Command ApproveCommand { get; }
            public async void OnApproving(Manager m)
            {
                bool isWorking = await proxy.ApprovingManager(m.UserManagerId);
                if (isWorking == true)
                {
                    await Application.Current.MainPage.DisplayAlert("בוצע בהצלחה", $"בית הספר אושר בהצלחה", "ok");
                PendingManagers.Remove(m);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("שגיאה", $"קרתה שגיאה במהלך האישור", "ok");

                }
            }

            public Command DeclineCommand { get; }
            public async void OnDeclining(Manager m)
            {
                bool isWorking = await proxy.decliningManager(m.UserManagerId);
                if (isWorking == true)
                {
                    await Application.Current.MainPage.DisplayAlert("בוצע בהצלחה", $"בית הספר נדחה בהצלחה", "ok");
                PendingManagers.Remove(m);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("שגיאה", $"קרתה שגיאה במהלך הדחיה", "ok");

                }
            }
        }
    }

