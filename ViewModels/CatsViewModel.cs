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
    public class CatsViewModel : ViewModelBase
    {
        private ObservableCollection<Animal> cats;
        public ObservableCollection<Animal> Cats
        {
            get
            {
                return this.cats;
            }
            set
            {
                this.cats = value;
                OnPropertyChanged();
            }
        }

        private AnimalService catService;
        public CatsViewModel(AnimalService service)
        {
            this.catService = service;
            cats = new ObservableCollection<Animal>();
            ReadCats();
        }

        private async void ReadCats()
        {
            AnimalService service = new AnimalService();
            List<Animal> list = await service.GetCats();
            this.Cats = new ObservableCollection<Animal>(list);
        }

        private Object selectedCat;
        public Object SelectedCat
        {
            get
            {
                return this.selectedCat;
            }
            set
            {
                this.selectedCat = value;
                OnPropertyChanged();
            }
        }

        public ICommand SingleSelectCommand => new Command(OnSingleSelectCat);

        async void OnSingleSelectCat()
        {
            if (SelectedCat != null)
            {
                var navParam = new Dictionary<string, object>()
                {
                    { "selectedCat",SelectedCat}
                };
                //Add goto here to show details
                await Shell.Current.GoToAsync("animalDetails", navParam);

                SelectedCat = null;
            }
        }
    }
}
