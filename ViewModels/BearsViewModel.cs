using CoreML;
using ShellLessonStep2.Models;
using ShellLessonStep2.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShellLessonStep2.ViewModels
{
    public class BearsViewModel : ViewModelBase
    {

        private ObservableCollection<Animal> bears;
        public ObservableCollection<Animal> Bears
        {
            get
            {
                return this.bears;
            }
            set
            {
                this.bears = value;
                OnPropertyChanged();
            }
        }

        private AnimalService bearService;
        public BearsViewModel(AnimalService service)
        {
            this.bearService = service;
            bears = new ObservableCollection<Animal>();
            ReadBears();
        }

        private async void ReadBears()
        {
            AnimalService service = new AnimalService();
            List<Animal> list = await service.GetBears();
            this.Bears = new ObservableCollection<Animal>(list);
        }

        private Object selectedBear;
        public Object SelectedBear
        {
            get
            {
                return this.selectedBear;
            }
            set
            {
                this.selectedBear = value;
                OnPropertyChanged();
            }
        }

        public ICommand SingleSelectCommand => new Command(OnSingleSelectBear);

        async void OnSingleSelectBear()
        {
            if (SelectedBear != null)
            {
                var navParam = new Dictionary<string, object>()
                {
                    { "selectedBear",SelectedBear}
                };
                //Add goto here to show details
                await Shell.Current.GoToAsync("animalDetails", navParam);

                SelectedBear = null;
            }
        }

    }
}
