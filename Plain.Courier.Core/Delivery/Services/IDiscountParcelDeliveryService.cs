using System;
using System.Collections.Generic;
using System.Text;
using Plain.Courier.Core.Delivery.Models;

namespace Plain.Courier.Core.Delivery.Services {
   public interface IDiscountParcelDeliveryService {
      List<ParcelDeliverySummary> ComputeDiscounts(List<ParcelDeliverySummary> parcelDeliveries);
   }
}
