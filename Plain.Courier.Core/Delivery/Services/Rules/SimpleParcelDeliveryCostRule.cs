using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plain.Courier.Core.Delivery.Constants;
using Plain.Courier.Core.Delivery.Models;
using static Plain.Courier.Core.Delivery.Constants.ParcelSize;

namespace Plain.Courier.Core.Delivery.Services.Rules {
   public class SimpleParcelDeliveryCostRule : IParcelDeliveryCostRule {

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

      private ParcelSize GetParcelType(Parcel parcel) {
         // not a nice way to handle as we evaluate the collection way to many times ,but for the sake of the exercise 
         // is considered to be accepted 
         if (parcel?.Dimensions?.All(x => x < 10) ?? false) {
            return Small;
         }

         if (parcel?.Dimensions?.All(x => x < 50) ?? false) {
            return Medium;
         }

         if (parcel?.Dimensions?.All(x => x < 100) ?? false) {
            return Large;
         }

         if (parcel.Dimensions?.Any(x => x >= 100) ?? false) {
            return XL;
         }

         // we dont know it then it is free ... :P
         return Unknown;
      }
   }
}
