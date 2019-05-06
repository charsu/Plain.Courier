using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plain.Courier.Core.Delivery.Models;

namespace Plain.Courier.Core.Delivery.Services {
   public class DeliveryCostsService {
      private readonly List<IParcelDeliveryCostRule> _rules;
      private readonly IDiscountParcelDeliveryService _discountService;

      public DeliveryCostsService(IDiscountParcelDeliveryService discountService, IEnumerable<IParcelDeliveryCostRule> rules) {
         _rules = rules?.ToList();
         _discountService = discountService;
      }

      public DeliveryTotal ComputeCost(List<Order> orders) {
         var output = new DeliveryTotal();
         orders?.ForEach(order => {
            var orderSummaries = new List<ParcelDeliverySummary>();

            order?.Parcels.ForEach(parcel => {
               var parcelSummary = new ParcelDeliverySummary();

               _rules?.ForEach(rule => {
                  parcelSummary = parcelSummary.Update(rule.GetCost(order, parcel));
               });

               orderSummaries.Add(parcelSummary);
            });

            // compute discounts 
            var discounted = _discountService.ComputeDiscounts(orderSummaries);

            // add the output
            discounted.ForEach(d => {
               output = output.AddParcelSummary(d);
            });
         });

         return output;
      }
   }
}
