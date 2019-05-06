using System;
using System.Collections.Generic;
using System.Text;
using Plain.Courier.Core.Delivery.Constants;
using Plain.Courier.Core.Delivery.Models;
using static Plain.Courier.Core.Delivery.Constants.ParcelSize;

namespace Plain.Courier.Core.Delivery.Services.Rules {
   public class WeigthParcelDeliveryCostRule : BaseParcelRuleSet, IParcelDeliveryCostRule {

      private readonly Dictionary<ParcelSize, decimal> _weigthLimits = new Dictionary<ParcelSize, decimal>() {
         [Unknown] = int.MaxValue,
         [Small] = 1,
         [Medium] = 3,
         [Large] = 6,
         [XL] = 10
      };

      public const decimal ExcessPriceCost = 2;

      public ParcelDeliverySummary GetCost(Order order, Parcel parcel) {
         var type = GetParcelType(parcel);
         var limit = _weigthLimits[type];
         var excessWeight = (parcel?.Weight ?? 0) - limit;
         var extraCost = excessWeight > 0 ? (decimal?)(excessWeight * ExcessPriceCost) : null;
         return new ParcelDeliverySummary() {
            Price = extraCost
         };
      }
   }
}
