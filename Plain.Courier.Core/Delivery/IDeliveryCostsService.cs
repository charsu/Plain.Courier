using System;
using System.Collections.Generic;
using System.Text;
using Plain.Courier.Core.Delivery.Models;

namespace Plain.Courier.Core.Delivery {
   public interface IDeliveryCostsService {
      DeliveryTotal ComputeCost(List<Order> orders);
   }
}
