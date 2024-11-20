using System;
using System.Collections.Generic;
using System.IO;

namespace Lab_7
{
    public class ReservationManager
    {
        private readonly List<Restaurant> restaurants = new();
        public void AddRestaurant(string name, int tableCount)
        {
            if (tableCount <= 0)
                throw new ArgumentException("Table count must be greater than 0.");

            restaurants.Add(new Restaurant(name, tableCount));
        }
        public void LoadRestaurantsFromFile(string filePath)
        {
            foreach (string line in File.ReadAllLines(filePath))
            {
                var parts = line.Split(',');
                if (parts.Length == 2 && int.TryParse(parts[1], out int tableCount))
                {
                    AddRestaurant(parts[0], tableCount);
                }
                else
                {
                    Console.WriteLine($"Invalid line format: {line}");
                }
            }
        }
        public List<string> GetAllAvailableTables(DateTime date)
        {
            List<string> freeTables = new List<string>();
            foreach (var restaurant in restaurants)
            {
                foreach (var tableInfo in restaurant.GetAvailableTables(date))
                {
                    freeTables.Add($"{restaurant.Name} - Table {tableInfo}");
                }
            }
            Console.WriteLine($"Available tables on {date}: {string.Join(", ", freeTables)}");
            return freeTables;
        }
        public bool BookTable(string restaurantName, DateTime date, int tableNumber)
        {
            foreach (var restaurant in restaurants)
            {
                if (restaurant.Name == restaurantName)
                {
                    if (tableNumber < 0 || tableNumber >= restaurant.TableCount)
                    {
                        throw new ArgumentOutOfRangeException(nameof(tableNumber), "Invalid table number.");
                    }
                    return restaurant.BookTable(tableNumber, date);
                }
            }
            throw new ArgumentException("Restaurant not found.", nameof(restaurantName));
        }
        public void SortRestaurantsByAvailability(DateTime date)
        {
            restaurants.Sort((a, b) => b.GetAvailableTableCount(date).CompareTo(a.GetAvailableTableCount(date)));
        }
    }
}