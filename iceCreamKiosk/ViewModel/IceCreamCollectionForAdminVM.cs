using BE;
using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using iceCreamKiosk.model;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iceCreamKiosk.ViewModel
{
    class IceCreamCollectionForAdminVM : ViewModelBase
    {
        private IceCreamLogic iceCreamLogic = new IceCreamLogic();
        private ObservableCollection<IceCream> iceCreams;
        private string search;
        public FilterModel FilterModel { get; set; }

        public string Search { get => search; set { Set(ref search, value); UpdateIceCreamCollection(value); } }
        public ObservableCollection<IceCream> IceCreams { get => iceCreams; set => Set(ref iceCreams, value); }

        public ICommand ShowSelectedCommand { get; set; }
        public ICommand OpenRemoveDialog { get; set; }


        public SnackbarMessageQueue SnackbarMessageQueue { get; set; } = new SnackbarMessageQueue();


        public IceCreamCollectionForAdminVM()
        {
            IceCreams = new ObservableCollection<IceCream>(iceCreamLogic.GetIceCreams());
            ShowSelectedCommand = new RelayCommand<IceCream>(ExecuteShowSelectedCommand);


            OpenRemoveDialog = new RelayCommand<IceCream>((ice) =>
            {
                if (ice is IceCream)
                {
                    IceCream iceCream = (ice as IceCream);
                    SnackbarMessageQueue.Enqueue(string.Format("Are you sure you want to remove: {0} ?", iceCream.Name), "Yes, Delete", () =>
                    {
                        ExecuteRemoveCommand(iceCream);
                    });

                }
            });

        }


        public void UpdateIceCreamCollection(string search = "")
        {
            IceCreams = new ObservableCollection<IceCream>(iceCreamLogic.GetIceCreams(search));
        }

        private void ExecuteShowSelectedCommand(Object iceCream)
        {

            if (iceCream is IceCream)
            {
                MessengerInstance.Send<ViewModelBase>(new IceCreamForAdminVM(iceCream as IceCream));
            }
        }
        private void ExecuteRemoveCommand(IceCream iceCream)
        {
            if (iceCream != null)
            {

                iceCreamLogic.RemoveIceCream(iceCream);
                UpdateIceCreamCollection();


                SnackbarMessageQueue.Enqueue(string.Format("Successful remove {0} .", iceCream.Name), "UNDO", () =>
                {
                    //Notjice!! dont add back the icecreams and feedbacks!!!!
                    var status = iceCreamLogic.AddIceCream(iceCream);
                    if (status == IceCreamLogic.Status.Success)
                    {

                        SnackbarMessageQueue.Enqueue(string.Format("Successful Undo removing {0} .", iceCream.Name));
                        UpdateIceCreamCollection();
                    }
                }
                );


            }
        }

    }
}
