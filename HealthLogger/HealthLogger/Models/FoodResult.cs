using System;
using System.Collections.Generic;
using System.Text;

namespace HealthLogger.Models
{
    public class FoodResult
    {
        public string text { get; set; }
        public List<food> parsed { get; set; }
        public List<hint> hints { get; set; }
        public List<link> links { get; set; }


    }
    public class hint
    {
        public food food { get; set; }
        public List<measure> measures { get; set; }

    }
    public class link
    {
        public string title { get; set; }
        public string href { get; set; }
    }
    public class measure
    {
        public string uri { get; set; }
        public string label { get; set; }
        public float weight { get; set; }
    }
    public class food
    {
        public string foodID { get; set; }
        public string label { get; set; }
        public string Measure { get; set; }
        public nutrients nutrients { get; set; }
        public string category { get; set; }
        public string categoryLabel { get; set; }
        public string image { get; set; }
    }
    public class nutrients
    {
        public double ENERC_KCAL { get; set; }
        public double PROCNT { get; set; }
        public double FAT { get; set; }
        public double CHOCDF { get; set; }
        public double FIBTG { get; set; }
    }
}
