using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using UCommerce.EntitiesV2;
using UCommerce.EntitiesV2.Definitions;
using UCommerce.Presentation.Web.Controls;

namespace MyUCommerceApp.BusinessLogic.Datatypes
{
    public class PriceGroupControlFactory : IControlFactory
    {
        public bool Supports(DataType dataType)
        {
            return dataType.DefinitionName == "PriceGroup";
        }

        public Control GetControl(IProperty property)
        {
            var dropdownList = new SafeDropDownList();
            var priceGroups = PriceGroup.All().ToList();
            foreach (var item in priceGroups)
            {
                var listItem = new ListItem(item.Name, item.PriceGroupId.ToString());
                listItem.Selected = property.GetValue().ToString() == item.PriceGroupId.ToString();
                dropdownList.Items.Add(listItem);
            }
            return dropdownList;
        }
    }
}
