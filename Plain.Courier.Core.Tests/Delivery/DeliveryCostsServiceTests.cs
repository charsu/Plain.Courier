using System;
using System.Text;
using Autofac.Extras.Moq;
using NUnit.Framework;
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
   }
}
