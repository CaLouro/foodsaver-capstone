using System.ComponentModel.DataAnnotations;
using FoodSaverWebApp.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodSaverWebApp.Models
{
    public class StoreModel
    {
        public Business Business { get; set; }
        public List<string> ProvinceCodes { get; set; }
        public List<SelectListItem> TagItems { get; set; }

        public StoreModel()
        {
            InitializeProvinces();
        }
        
        public void InitializeProvinces()
        {
            ProvinceCodes = new List<string>()
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