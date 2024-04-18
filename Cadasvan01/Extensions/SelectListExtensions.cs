using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cadasvan01.Extensions
{
    public class SelectListExtensions
    {
        public static List<SelectListItem> MontarSelectListParaEnum<T>(T selected = default(T), bool excludeDefault = false) where T : struct, IConvertible
        {
            var items = new List<SelectListItem>();
            var enums = Enum.GetValues(typeof(T)).Cast<T>();
            foreach (var enumerador in enums)
            {
                if (excludeDefault && enumerador.Equals(default(T)))
                    continue;
                var name = enumerador.GetDisplayName();
                var item = new SelectListItem()
                {
                    Value = enumerador.ToString(),
                    Text = name,
                    Selected = selected.Equals(enumerador)
                };
                items.Add(item);
            }
            return items;
        }
    }
}
