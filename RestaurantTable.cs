using System;
using System.Collections.Generic;

namespace Lab_7
{
    public class RestaurantTable
    {
        private readonly HashSet<DateTime> bookedDates = new();
        public bool Book(DateTime date)
        {
            return bookedDates.Add(date);
        }
        public bool IsBooked(DateTime date)
        {
            return bookedDates.Contains(date);
        }
    }
}