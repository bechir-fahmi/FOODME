using Platform.FoodMe.Admin.ViewModels.ViewModels.LanguageData;
using Platform.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.FoodMe.Admin.ViewModels.ViewModels.DeliveryModeData
{
    public class DeliveryModeVM
    {
        public int Id { get; set; }

        public Guid NameLabelCode { get; set; }

        public Guid ImageLabelCode { get; set; }
        public Status Status { get; set; }

        public List<LanguageResourceVM> NameLanguageResources { get; set; }
        public List<LanguageResourceVM> ImageFileResources { get; set; }
    }
}
