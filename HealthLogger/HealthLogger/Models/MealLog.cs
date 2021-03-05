using System;
using System.Collections.Generic;
using System.Text;

namespace HealthLogger.Models
{
    public class MealLog
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get;set; }
        public int Calories { get; set; }
    }
}
