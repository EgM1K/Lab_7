using System;
using System.Collections.Generic;

namespace Lab_7
{
    public class Restaurant
    {
        public string Name { get; }
        private readonly RestaurantTable[] tables;
        public int TableCount => tables.Length;
        public Restaurant(string name, int tableCount)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");

            Name = name;
            tables = new RestaurantTable[tableCount];
            for (int i = 0; i < tableCount; i++)
            {
                tables[i] = new RestaurantTable();
            }
        }
        public List<int> GetAvailableTables(DateTime date)
        {
            List<int> availableTables = new();
            for (int i = 0; i < tables.Length; i++)
            {
                if (!tables[i].IsBooked(date))
                {
                    availableTables.Add(i + 1);
                }
            }
            return availableTables;
        }
        public bool BookTable(int tableNumber, DateTime date)
        {
            return tables[tableNumber].Book(date);
        }
        public int GetAvailableTableCount(DateTime date)
        {
            int count = 0;
            foreach (var table in tables)
            {
                if (!table.IsBooked(date))
                {
                    count++;
                }
            }
            return count;
        }
    }
}