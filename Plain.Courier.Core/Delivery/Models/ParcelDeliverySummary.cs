using System;
using System.Collections.Generic;
using System.Text;
using Plain.Courier.Core.Delivery.Constants;

namespace Plain.Courier.Core.Delivery.Models {
   public class ParcelDeliverySummary {
      public ParcelSize ParcelSize { get; set; }
      public decimal Price { get; set; }
   }
}
