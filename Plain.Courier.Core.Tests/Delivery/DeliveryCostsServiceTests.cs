using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac.Extras.Moq;
using NUnit.Framework;
using Plain.Courier.Core.Delivery.Models;
using Plain.Courier.Core.Delivery.Services;
using static Plain.Courier.Core.Tests.Delivery.DeliveryCostsServiceHelper;

namespace Plain.Courier.Core.Tests.Delivery {
   public class DeliveryCostsServiceTests {

      private AutoMock GetMock()
         => AutoMock.GetLoose();


      [SetUp]
      public void Setup() {
      }

      [Test]
      public void DeliveryForOrder_WithSimpleSetOfRules_OK() {
         var orders = GetOrders_OneOfEach();

         var mock = GetMock();
         var service = mock
               .SetupRules(CreateSimpleParcelRuleSet(mock))
            .Create<DeliveryCostsService>();

         var result = service.ComputeCost(orders);

         // small + med + large + xl ( 3+8+15+25 = 51) 
         Assert.IsTrue(result.Total == 51);
         Assert.IsTrue(result.ParcelSummary.Count == 4);
      }

      [Test]
      public void DeliveryForOrder_WithSpeedySetOfRules_OK() {
         var orders = new List<Order>();
         orders.AddRange(GetOrders_OneOfEach());
         orders.AddRange(GetOrders_OneOfEach(isSpeedy: true));

         var mock = GetMock();
         var service = mock
               .SetupRules(CreateSpeedyParcelRuleSet(mock))
            .Create<DeliveryCostsService>();

         var result = service.ComputeCost(orders);

         // normal order: small + med + large + xl ( 3+8+15+25 = 51)
         // spedy order: small + med + large + xl ( 2* (3+8+15+25) = 2*51 = 102)
         Assert.IsTrue(result.Total == 153);
         Assert.IsTrue(result.ParcelSummary.Count == 8);
      }
   }
}
