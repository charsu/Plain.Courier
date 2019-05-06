using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plain.Courier.Core.Delivery.Models;

namespace Plain.Courier.Core.Delivery.Services {
   public class DeliveryCostsService {
      private readonly List<IParcelDeliveryCostRule> _rules;

      public DeliveryCostsService(IEnumerable<IParcelDeliveryCostRule> rules) {
         _rules = rules?.ToList();
      }

      public DeliveryTotal ComputeCost(List<Order> orders) {
         var output = new DeliveryTotal();
         orders?.ForEach(order => {
            order?.Parcels.ForEach(parcel => {
               var parcelSummary = new ParcelDeliverySummary();

               _rules?.ForEach(rule => {
                  parcelSummary = parcelSummary.Update(rule.GetCost(order, parcel));
               });

               output = output.AddParcelSummary(parcelSummary);
            });
         });

         return output;
      }
   }
}
