using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plain.Courier.Core.Delivery.Models {
   public class DeliveryTotal {
      public decimal Total { get; set; }
      public List<ParcelDeliverySummary> ParcelSummaries { get; set; } = new List<ParcelDeliverySummary>();
      public IReadOnlyCollection<ParcelDeliverySummary> SpeedyDeliveries
         => ParcelSummaries?.Where(x => x.IsSpeedy == true).ToList().AsReadOnly();

      public IReadOnlyCollection<ParcelDeliverySummary> DiscountedDeliveries
         => ParcelSummaries?.Where(x => x.Discount != 0).ToList().AsReadOnly();
   }

   public static class DeliveryTotalHelper {
      public static DeliveryTotal AddParcelSummary(this DeliveryTotal dt, ParcelDeliverySummary parcelDeliverySummary) {
         if (parcelDeliverySummary != null) {
            dt.ParcelSummaries.Add(parcelDeliverySummary);
            dt.Total += parcelDeliverySummary.Total;
         }

         return dt;
      }
   }
}
