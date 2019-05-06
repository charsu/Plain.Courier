using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plain.Courier.Core.Delivery.Constants;
using Plain.Courier.Core.Delivery.Models;

namespace Plain.Courier.Core.Delivery.Services.Discount {
   public class DiscountParcelDeliveryService : IDiscountParcelDeliveryService {

      private static Dictionary<string, (int limit, ParcelSize? size)> _discountRules = new Dictionary<string, (int, ParcelSize?)>() {
         ["Small parcel mania"] = (4, ParcelSize.Small),
         ["Medium parcel mania"] = (3, ParcelSize.Medium),
         ["Mixed parcel mania"] = (5, null)
      };

      public List<ParcelDeliverySummary> ComputeDiscounts(List<ParcelDeliverySummary> parcelDeliveries) {
         // we only try to determine if the input qualifies for any discounts 
         // and after that substract the chepeast parcels for discount

         // note: this should have been broken down into more sub services each implmenting one rule ,
         // but due to time constaints we got this :( 
         if (parcelDeliveries == null)
            return parcelDeliveries;

         var parcels = parcelDeliveries?.OrderBy(x => x.Price).ToList();

         foreach (var kvp in _discountRules) {
            var count = parcels.Count(x =>
            (kvp.Value.size.HasValue && x.ParcelSize == kvp.Value.size)
            || (!kvp.Value.size.HasValue));

            var discountedItems = count / kvp.Value.limit;

            parcels
               .Where(x => x.Discount == 0) // has not been discounted yet
               .Where(x =>
                  (kvp.Value.size.HasValue && x.ParcelSize == kvp.Value.size)
                  || (!kvp.Value.size.HasValue)) //of same type or not type constaint
               .Take(discountedItems)
               .ToList()
               .ForEach(x => {
                  x.Discount = (x.Price ?? 0) * -1;
               });
         }

         return parcelDeliveries;
      }
   }
}
