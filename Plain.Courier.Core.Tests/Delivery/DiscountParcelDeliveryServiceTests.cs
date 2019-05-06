using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac.Extras.Moq;
using NUnit.Framework;
using Plain.Courier.Core.Delivery.Constants;
using Plain.Courier.Core.Delivery.Models;
using Plain.Courier.Core.Delivery.Services.Discount;
using static Plain.Courier.Core.Tests.Delivery.DeliveryCostsServiceHelper;

namespace Plain.Courier.Core.Tests.Delivery {
   public class DiscountParcelDeliveryServiceTests {
      private AutoMock GetMock()
         => AutoMock.GetLoose();

      [Test]
      public void ComputeDiscount_OK() {
         // assumes we have computed the summaries for an order and afterwards we want to apply the discounts
         var summaries = new List<ParcelDeliverySummary>();
         summaries.AddRange(GetSmallParcelsSummariesForDiscount());
         summaries.AddRange(GetMediumParcelsSummariesForDiscount());

         var mock = GetMock();
         var service = mock.Create<DiscountParcelDeliveryService>();

         var result = service.ComputeDiscounts(summaries);

         // small : total 10 => 2 discounted by 1st rule and 3 discounted by 3rd rule
         // medium: total 10 => 3 discounted by 2nd rule and 1 discounted by 3rd rule 
         // note this is due to the setup of the prices and is more to check the global behaviour of hte rule sets
         Assert.IsTrue(result.Count(x => x.Discount != 0 && x.ParcelSize == ParcelSize.Small) == 5);
         Assert.IsTrue(result.Count(x => x.Discount != 0 && x.ParcelSize == ParcelSize.Medium) == 4);
      }
   }
}
