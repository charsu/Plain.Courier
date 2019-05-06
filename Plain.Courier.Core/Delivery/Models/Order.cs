using System;
using System.Collections.Generic;
using System.Text;

namespace Plain.Courier.Core.Delivery.Models {
   public class Order {
      public bool IsSpeedyDelivery { get; set; } = false;
      public List<Parcel> Parcels { get; set; }
   }
}
