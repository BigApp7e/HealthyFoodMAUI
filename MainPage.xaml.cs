using HealthyFoodApp.ViewModel;
using System.Collections.ObjectModel;


namespace HealthyFoodApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService _localDbService;

        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            if (_localDbService == null)
            {
                _localDbService = new LocalDbService();
            }

            BindingContext = vm;
        }
        
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is MainViewModel viewModel)
            {
                // Call GetFoods method to retrieve data
                List<Item> foods = await _localDbService.GetFoods();

                viewModel.Foods.Clear();
                foreach (var item in foods)
                {
                    viewModel.Foods.Add(new ItemFood(item.Name,item.Description, item.Calories));
                }
                
            }
 
        }
    }

}
