using System;
using System.Collections.Generic;
using System.Text;
using Plain.Courier.Core.Delivery.Models;

namespace Plain.Courier.Core.Delivery.Services.Rules {
   public class SpeedyParcelDeliveryCostRule : IParcelDeliveryCostRule {
      public ParcelDeliverySummary GetCost(Order order, Parcel parcel) {
         throw new NotImplementedException();
      }
   }
}
