using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthLogger.Models
{
    public class ActivityLog
    {
        public string UserId { get; set; }
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int ActiveMinutes { get; set; }
        public int CaloriesBurnt { get; set; }
    }
    public class ActivityLogPacket
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int ActiveMinutes { get; set; }
        public int CaloriesBurnt { get; set; }
    }
    public class MealLog
    {
        public string UserId { get; set; }
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Calories { get; set; }
    }
    public class MealLogPacket
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Calories { get; set; }
    }
}
