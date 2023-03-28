using System.ComponentModel.DataAnnotations;
using FoodSaverWebApp.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodSaverWebApp.Models
{
    public class StoreModel
    {
        public Business Business { get; set; }

        public List<string> ProvinceCodes { get; } = new()
        {
            "AB",
            "BC",
            "MB",
            "NB",
            "NL",
            "NT",
            "NS",
            "NU",
            "ON",
            "PE",
            "QC",
            "SK",
            "YT",
        };
        
        public List<SelectListItem> TagItems { get; set; }
        
        public void ConfigureTagsToSelectList(ICollection<Tag> tags)
        {
            TagItems = new List<SelectListItem>();

            foreach (Tag tag in tags)
            {
                TagItems.Add(new SelectListItem
                {
                    Text = tag.Name,
                    Value = tag.TagId.ToString(),
                    Selected = Business.Tags.Any(t => t.TagId == tag.TagId)
                });
            }
        }
    }
}