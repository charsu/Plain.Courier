using System;
using System.Collections.Generic;
using System.Text;

namespace Plain.Courier.Core.Delivery.Models {
   public class DeliveryTotal {
      public decimal Total { get; set; }

      public static DeliveryTotal operator +(DeliveryTotal left, DeliveryTotal right) {
         var ltotal = left?.Total ?? 0;
         var rtotal = right?.Total ?? 0;

         return new DeliveryTotal() {
            Total = ltotal + rtotal
         };
      }
   }
}
