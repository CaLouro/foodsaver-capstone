using System.Globalization;
using FoodSaverWebApp.Entities;

namespace FoodSaverWebApp.Models;

public class DiscountFormModel
{
    public ICollection<Item> Items { get; set; } = new List<Item>();

    public int ItemId { get; set; }

    public int? QuantityAvailable { get; set; }

    public float Price { get; set; }

    public DateTime? AvailabilityStarts { get; set; }

    public DateTime? AvailabilityEnds { get; set; }
    
    public int BusinessId { get; set; }
}