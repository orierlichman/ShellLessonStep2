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
    public class MonkeyViewModel : ViewModelBase
    {

        private ObservableCollection<Animal> monkey;
        public ObservableCollection<Animal> Monkey
        {
            get
            {
                return this.monkey;
            }
            set
            {
                this.monkey = value;
                OnPropertyChanged();
            }
        }

        private AnimalService monkeyService;
        public MonkeyViewModel(AnimalService service)
        {
            this.monkeyService = service;
            monkey = new ObservableCollection<Animal>();
            ReadMonkeys();
        }

        private async void ReadMonkeys()
        {
            AnimalService service = new AnimalService();
            List<Animal> list = await service.GetMonkeys();
            this.Monkey = new ObservableCollection<Animal>(list);
        }

        private Object selectedMonkey;
        public Object SelectedMonkey
        {
            get
            {
                return this.selectedMonkey;
            }
            set
            {
                this.selectedMonkey = value;
                OnPropertyChanged();
            }
        }

        public ICommand SingleSelectCommand => new Command(OnSingleSelectMonkey);

        async void OnSingleSelectMonkey()
        {
            if (SelectedMonkey != null)
            {
                var navParam = new Dictionary<string, object>()
                {
                    { "selectedMonkey",SelectedMonkey}
                };
                //Add goto here to show details
                await Shell.Current.GoToAsync("animalDetails", navParam);

                SelectedMonkey = null;
            }
        }

    }
}
