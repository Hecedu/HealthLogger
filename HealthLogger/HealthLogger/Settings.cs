using System;
using System.Collections.Generic;
using System.Text;

namespace HealthLogger
{
    class Settings
    {
        public static string FoodApiId
        {
            get
            {
                return "e1a0f863";
            }
        }
        public static string FoodApiKey
        {
            get
            {
                return "33c31ea9beaaff6766273660d6b55b2f";
            }
        }
        public static string HealthTrackerApiUri
        {
            get
            {
                return "https://healthtrackerapi2.azurewebsites.net";
            }
        }
        public static string JWDToken { get; set; }
        public static string UserId { get; set; }
        public static int CalorieGoal { get; set; }
        public static int ActiveMinutesGoal { get; set; }
        public static bool Logged { get; set; }
    }
}
