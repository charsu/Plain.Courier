using System;
using System.Collections.Generic;
using System.Text;
using Plain.Courier.Core.Delivery.Constants;

namespace Plain.Courier.Core.Delivery.Models {
   public class ParcelDeliverySummary {
      public decimal? Price { get; set; }
      public decimal? PriceFactor { get; set; }
      public bool? IsSpeedy { get; set; }

      public ParcelSize? ParcelSize { get; set; }
      public decimal Total { get => (Price ?? 0) * (PriceFactor ?? 1); }
   }

   public static class ParcelDeliverySummaryHelper {
      public static ParcelDeliverySummary Update(this ParcelDeliverySummary parcelDeliverySummary, ParcelDeliverySummary right) {
         if (right.Price.HasValue) {
            parcelDeliverySummary.Price = (parcelDeliverySummary.Price ?? 0) + right.Price;
         }
         if (right.PriceFactor.HasValue) {
            parcelDeliverySummary.PriceFactor = right.PriceFactor;
         }
         if (right.IsSpeedy.HasValue) {
            parcelDeliverySummary.IsSpeedy = right.IsSpeedy;
         }
         if (right.ParcelSize.HasValue) {
            parcelDeliverySummary.ParcelSize = right.ParcelSize;
         }

         return parcelDeliverySummary;
      }
   }
}
