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
         [XL] = 10,
         [Heavy] = 50
      };

      public const decimal NormalExcessPriceCost = 2;
      public const decimal HeavyExcessPriceCost = 1;

      private readonly Dictionary<ParcelSize, decimal> _excessPriceCost = new Dictionary<ParcelSize, decimal>() {
         [Unknown] = 0,
         [Small] = NormalExcessPriceCost,
         [Medium] = NormalExcessPriceCost,
         [Large] = NormalExcessPriceCost,
         [XL] = NormalExcessPriceCost,
         [Heavy] = HeavyExcessPriceCost
      };

      public ParcelDeliverySummary GetCost(Order order, Parcel parcel) {
         var type = GetParcelType(parcel);
         var limit = _weigthLimits[type];
         var excessWeight = (parcel?.Weight ?? 0) - limit;
         var excessPricePerKg = _excessPriceCost[type];
         var extraCost = excessWeight > 0 ? (decimal?)(excessWeight * excessPricePerKg) : null;
         return new ParcelDeliverySummary() {
            Price = extraCost
         };
      }
   }
}
