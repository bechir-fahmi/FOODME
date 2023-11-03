using Platform.FoodMe.Admin.ViewModels.ViewModels.KitchenTypeData;
using Platform.FoodMe.Admin.ViewModels.ViewModels.SupportServiceData;
using Platform.Shared.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.FoodMe.Admin.Business.SuportCategory
{
    public  static class SuportCategoryService
    {
        public static void UploadSuportCategoryImages(SuportCategoryVM SuportCategory, string webRootPath)
        {
            if (SuportCategory != null && SuportCategory.ImageFileResources != null)
            {
                foreach (var imageFile in SuportCategory.ImageFileResources)
                {
                    string fileName = ImageHelper.SaveImage(webRootPath, imageFile.Value);
                    imageFile.Value = fileName;
                }
            }

        }

    }
}
