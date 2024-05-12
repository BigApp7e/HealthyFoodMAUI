namespace HealthyFoodApp.ViewModel
{
    public class ItemFood
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int Calories { get; set; }
        public ItemFood(string name, string description, int calories)
        {
            this.Name = name;
            this.Description = description;
            this.Calories = calories;    
        }
    }
}
