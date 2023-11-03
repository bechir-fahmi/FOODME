using Newtonsoft.Json;
using OptimaJet.Workflow.Core.Model;
using Platform.ReferencialData.Business.business_services.RestaurantData;
using Platform.ReferencialData.Business.business_services_implementations.RestaurantData;
using Platform.ReferentialData.DtoModel.RestaurantData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow
{
    public class Conditions
    {
        private readonly IRestaurantService _restaurantService;
        public Conditions(IRestaurantService restaurantService) 
        {
            _restaurantService = restaurantService;
        }

       /* public bool VerifierRestaurantStatusCondition(Order order)
        {
            if (order != null)
            {
                RestaurantDto restaurant = _restaurantService.Get(order.RestaurantId);
                return restaurant.IsOpen;
            }
            return false;
        }*/
    }
}
