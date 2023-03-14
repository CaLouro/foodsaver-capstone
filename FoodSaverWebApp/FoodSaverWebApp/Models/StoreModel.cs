using System.ComponentModel.DataAnnotations;
using FoodSaverWebApp.Entities;

namespace FoodSaverWebApp.Models
{
    public class StoreModel
    {
        public Business Business { get; set; }

        public List<string> ProvinceCodes { get; set; }  = new List<string>()
        {
            "NL",
            "PE",
            "NS",
            "NB",
            "QC",
            "ON",
            "MB",
            "NL",
            "AB",
            "BC",
            "YT",
            "NT",
            "NU"
        };
    }
}