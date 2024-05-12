using SQLite;

namespace HealthyFoodApp
{
    public class LocalDbService
    {
        private const string DB_NAME = "demo_local_db.db3";
        private readonly SQLiteAsyncConnection _connection;

        public  LocalDbService()
        {

            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<Item>().Wait(); // Ensure table creation is complete before initializing data

            _ = InitializeData();
        }
        private async Task InitializeData()
        {
            try
            {
                // Check if any items exist in the table
                var existingItems = await _connection.Table<Item>().CountAsync();
                if (existingItems == 2)
                {
                    // Insert initial data
                    await _connection.InsertAllAsync(new List<Item>
            {

                new Item {Id=1, Name = "Apple", Description = "Delicious fruit", Calories = 52 },
                new Item {Id=2, Name = "Banana", Description = "Delicious fruit", Calories = 89 },
                new Item {Id=3, Name = "Оранге", Description = "Delicious fruit", Calories = 78 },
                new Item {Id=4, Name = "Grill Chicken", Description = "suitable for lunch or dinner", Calories = 180 },
                new Item {Id=5, Name = "Fish Safrid", Description = "suitable for lunch or dinner", Calories = 190 },
                new Item {Id=6, Name = "Fish Mullus", Description = "suitable for lunch or dinner", Calories = 250 },
                new Item {Id=7, Name = "Port Steak", Description = "suitable for lunch ", Calories = 340 },
                new Item {Id=8, Name = "meatballs", Description = "suitable for lunch or dinner", Calories = 197 },
                new Item {Id=9, Name = "Vanila Ice Cream", Description = "Delicious", Calories = 407 },
                new Item {Id=10, Name = "Chocolate cake", Description = "Delicious", Calories = 607 }
                   });
                }

            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error occurred while initializing data: {ex.Message}");
                // You can also use a logging framework like Serilog or Xamarin.Essentials' Logger
            }

        }

        public async Task<List<Item>> GetFoods()
        {
            return await _connection.Table<Item>().ToListAsync();
        }
    }
}
