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
    public class ElephantsViewModel : ViewModelBase
    {

        private ObservableCollection<Animal> elephants;
        public ObservableCollection<Animal> Elephants
        {
            get
            {
                return this.elephants;
            }
            set
            {
                this.elephants = value;
                OnPropertyChanged();
            }
        }

        private AnimalService elephantService;
        public ElephantsViewModel(AnimalService service)
        {
            this.elephantService = service;
            elephants = new ObservableCollection<Animal>();
            ReadElephants();
        }

        private async void ReadElephants()
        {
            AnimalService service = new AnimalService();
            List<Animal> list = await service.GetElephants();
            this.Elephants = new ObservableCollection<Animal>(list);
        }

        private Object selectedElephant;
        public Object SelectedElephant
        {
            get
            {
                return this.selectedElephant;
            }
            set
            {
                this.selectedElephant = value;
                OnPropertyChanged();
            }
        }

        public ICommand SingleSelectCommand => new Command(OnSingleSelectElephant);

        async void OnSingleSelectElephant()
        {
            if (SelectedElephant != null)
            {
                var navParam = new Dictionary<string, object>()
                {
                    { "selectedElephant",SelectedElephant}
                };
                //Add goto here to show details
                await Shell.Current.GoToAsync("animalDetails", navParam);

                SelectedElephant = null;
            }
        }

    }
}
