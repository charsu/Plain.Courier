using System;
using System.Collections.Generic;
using System.Text;
using Plain.Courier.Core.Delivery.Constants;

namespace Plain.Courier.Core.Delivery.Models {
   public class ParcelDeliverySummary {
      public decimal BasicDeliveryPrice { get; set; }
      public decimal DeliveryPriceFactor { get; set; } = 1;

      public ParcelSize ParcelSize { get; set; }
      public decimal Price { get => BasicDeliveryPrice * DeliveryPriceFactor; }
   }
}
