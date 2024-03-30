using IMStore.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IMStore.Extensions
{
    public static class ConvertExtensions
    {
        //make a template for list use in the razor pages
        public static List<SelectListItem> ConvertToSelectList<T>(this IEnumerable<T> collection, int selectedValue) where T : IPrimaryProperties
        {
            return (from item in collection
                    select new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                        Selected = (item.Id == selectedValue)

                    }).ToList();
        }

    }
}
