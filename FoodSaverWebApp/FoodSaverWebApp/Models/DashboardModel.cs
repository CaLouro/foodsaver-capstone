using FoodSaverWebApp.Entities;

namespace FoodSaverWebApp.Models;

public class DashboardModel
{
    public ICollection<Business> Businesses { get; set; }
    public ICollection<Item> Items { get; set; }
}