using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plain.Courier.Core.Delivery.Constants;
using Plain.Courier.Core.Delivery.Models;
using static Plain.Courier.Core.Delivery.Constants.ParcelSize;

namespace Plain.Courier.Core.Delivery.Services.Rules {
   public class SimpleParcelDeliveryCostRule : BaseParcelRuleSet, IParcelDeliveryCostRule {

      private readonly Dictionary<ParcelSize, decimal> _prices = new Dictionary<ParcelSize, decimal>() {
         [Unknown] = 0,
         [Small] = 3,
         [Medium] = 8,
         [Large] = 15,
         [XL] = 25
      };

      public ParcelDeliverySummary GetCost(Order order, Parcel parcel) {
         var parcelType = GetParcelType(parcel);
         return new ParcelDeliverySummary() {
            Price = _prices[parcelType],
            ParcelSize = parcelType
         };
      }
   }
}
