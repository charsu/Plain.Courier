using System;
using System.Collections.Generic;
using System.Text;

namespace Plain.Courier.Core.Delivery.Models {
   public class DeliveryTotal {
      public decimal Total { get; set; }
      public List<ParcelDeliverySummary> ParcelSummary { get; set; } = new List<ParcelDeliverySummary>();
   }

   public static class DeliveryTotalHelper {
      public static DeliveryTotal AddParcelSummary(this DeliveryTotal dt, ParcelDeliverySummary parcelDeliverySummary) {
         if (parcelDeliverySummary != null) {
            dt.ParcelSummary.Add(parcelDeliverySummary);
            dt.Total += parcelDeliverySummary.Price;
         }

         return dt;
      }
   }
}
