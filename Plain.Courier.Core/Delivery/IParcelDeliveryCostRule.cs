using System;
using System.Collections.Generic;
using System.Text;
using Plain.Courier.Core.Delivery.Models;

namespace Plain.Courier.Core.Delivery {
   public interface IParcelDeliveryCostRule {
      ParcelDeliverySummary GetCost(Order order, Parcel parcel);
   }
}
