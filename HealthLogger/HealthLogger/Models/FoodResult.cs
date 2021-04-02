using System;
using System.Collections.Generic;
using System.Text;

namespace HealthLogger.Models
{
    public class FoodResult
    {
        public string Text { get; set; }
        public List<Food> Parsed { get; set; }
        public List<Hint> Hints { get; set; }
        public List<Link> Links { get; set; }
    }
    public class Hint
    {
        public Food Food { get; set; }
        public List<Measure> Measures { get; set; }

    }
    public class Link
    {
        public string Title { get; set; }
        public string Href { get; set; }
    }
    public class Measure
    {
        public string Uri { get; set; }
        public string Label { get; set; }
        public float Weight { get; set; }
    }
    public class Food
    {
        public string FoodID { get; set; }
        public string Label { get; set; }
        public string Measure { get; set; }
        public Nutrients Nutrients { get; set; }
        public string Category { get; set; }
        public string CategoryLabel { get; set; }
        public string Image { get; set; }
    }
    public class Nutrients
    {
        public double ENERC_KCAL { get; set; }
        public double PROCNT { get; set; }
        public double FAT { get; set; }
        public double CHOCDF { get; set; }
        public double FIBTG { get; set; }
    }
}
