using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace HealthyFoodApp.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        string _dailyCalories = "1000";

        int dailyLeftCalories = 0;

        [ObservableProperty]
        bool _isCaloriButtonVisible = true;

        [ObservableProperty]
        bool _isRefreshButtonVisible = false;

        [ObservableProperty]
        bool _isSourceListVisible = false;


        [ObservableProperty]
        bool _isMenuListVisible = false;

        bool _isAddBtnEnable = true;

        [ObservableProperty]
        string _dailyLeftCaloriesLabelText = "";


        public ObservableCollection<ItemFood> _foods;

        [ObservableProperty]
        public ObservableCollection<ItemFood> _dailyMenu;

        public ICommand ValidateCommand { get; }
        public ICommand RefreshCommand { get; }

        public MainViewModel()
        {
            _foods = new ObservableCollection<ItemFood>();
            _dailyMenu = new ObservableCollection<ItemFood>();
            ValidateCommand = new Command(ValidateCalories);
            RefreshCommand = new Command(Refresh);
        }


        private void Refresh()
        {
            IsCaloriButtonVisible = true;
            IsRefreshButtonVisible = false;
            IsSourceListVisible = false;
            IsMenuListVisible = false;
            dailyLeftCalories = 0;
            DailyCalories = "1000";
            DailyLeftCaloriesLabelText = "";
            IsAddBtnEnable = true;
            DailyMenu.Clear();
        }

        [ICommand]
        async void Add(ItemFood selectedItem)
        {           

            if(dailyLeftCalories < 0)
            {
                DailyLeftCaloriesLabelText = "This is your daily menu!";
                IsAddBtnEnable = false;
                return;
            }
            else
            {
                IsMenuListVisible = true;
                DailyMenu.Add(selectedItem);
                dailyLeftCalories -= selectedItem.Calories;
                if(dailyLeftCalories < 0)
                {
                    DailyLeftCaloriesLabelText = "This is your daily menu!";
                }
                else
                {
                    DailyLeftCaloriesLabelText = "You have " + dailyLeftCalories + " unused calories for today! Add food from source list";
                }
               
            }
        }

        private void ValidateCalories(object obj)
        {

            if (string.IsNullOrWhiteSpace(DailyCalories) || isNumber(DailyCalories) == false) 
            {
                DailyCalories = "Please write valid number";
                return;
            }
            else if (int.Parse(DailyCalories) < 1000)
            {
                DailyCalories = "Minimum calories per day is 1000. Please write more 1000";
                return;
            }
            else if (int.Parse(DailyCalories) > 10000)
            {
                DailyCalories = "Max calories per day is 10000, Please write les than 10000";
                return;
            }

            dailyLeftCalories = int.Parse(DailyCalories);

            IsCaloriButtonVisible = false;
            IsRefreshButtonVisible = true;
            IsSourceListVisible = true;
            DailyLeftCaloriesLabelText = "You have " + dailyLeftCalories + " unused calories for today! Add food from source list";
        }

        public ObservableCollection<ItemFood> Foods
        {
            get => _foods;
            set
            {
                _foods.Clear();
                _foods = value;
            }
        }

        public bool IsAddBtnEnable
        {
            get => _isAddBtnEnable;
            set => SetProperty(ref _isAddBtnEnable, value);
        }

        private bool isNumber(string text)
        {
            int numericvalue;
            return int.TryParse(text, out numericvalue);
        }
    }

}
