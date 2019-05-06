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
               _rules?.ForEach(rule => {
                  output = output.AddParcelSummary(rule.GetCost(order, parcel));
               });
            });
         });

         return output;
      }
   }
}
