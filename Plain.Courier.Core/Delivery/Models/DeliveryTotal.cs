using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plain.Courier.Core.Delivery.Models {
   public class DeliveryTotal {
      public decimal Total { get; set; }
      public List<ParcelDeliverySummary> ParcelSummary { get; set; } = new List<ParcelDeliverySummary>();
      public IReadOnlyCollection<ParcelDeliverySummary> SpeedyDeliveries
         => ParcelSummary?.Where(x => x.IsSpeedy == true).ToList().AsReadOnly();
   }

   public static class DeliveryTotalHelper {
      public static DeliveryTotal AddParcelSummary(this DeliveryTotal dt, ParcelDeliverySummary parcelDeliverySummary) {
         if (parcelDeliverySummary != null) {
            dt.ParcelSummary.Add(parcelDeliverySummary);
            dt.Total += parcelDeliverySummary.Total;
         }

         return dt;
      }
   }
}
