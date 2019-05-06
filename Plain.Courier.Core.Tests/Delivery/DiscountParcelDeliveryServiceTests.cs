using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Extras.Moq;
using NUnit.Framework;
using Plain.Courier.Core.Delivery.Models;
using Plain.Courier.Core.Delivery.Services.Discount;

namespace Plain.Courier.Core.Tests.Delivery {
   public class DiscountParcelDeliveryServiceTests {
      private AutoMock GetMock()
         => AutoMock.GetLoose();

      [Test]
      public void ComputeDiscount_OK() {
         // assumes we have computed the summaries for an order and afterwards we want to apply the discounts
         var summaries = new List<ParcelDeliverySummary>();
         var mock = GetMock();

         var service = mock.Create<DiscountParcelDeliveryService>();

         var result = service.ComputeDiscounts(summaries);
      }
   }
}
