using System.ComponentModel.DataAnnotations;
using FoodSaverWebApp.Entities;

namespace FoodSaverWebApp.Models
{
    public class StoreRegistrationModel
    {
        public Business Business { get; set; }
        
        public Address Address { get; set; }
        
        public List<string> ProvinceCodes { get; set; }
    }
}