using FoodSaverWebApp.Entities;

namespace FoodSaverWebApp.Models;

public class DiscountFormModel
{
    public ICollection<Item> Items { get; set; }

    public DiscountInfo DiscountInfo { get; set; }
}