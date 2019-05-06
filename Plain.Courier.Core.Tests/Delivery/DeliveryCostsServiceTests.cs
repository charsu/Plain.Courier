using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Extras.Moq;
using NUnit.Framework;
using Plain.Courier.Core.Delivery;
using Plain.Courier.Core.Delivery.Models;
using Plain.Courier.Core.Delivery.Services;
using Plain.Courier.Core.Delivery.Services.Rules;
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
      }
   }

   public static class DeliveryCostsServiceHelper {
      public static List<Order> GetOrders_OneOfEach()
         => new List<Order>() {
         new Order() {
            Parcels = new List<Parcel>(){
               // small
               new Parcel() {
                  Width = 3, Height = 3 , Length = 3
               },
               // medium 
               new Parcel() {
                  Width = 30, Height = 3 , Length = 3
               },
               // large 
               new Parcel() {
                  Width = 30, Height = 60 , Length = 3
               },
               // xl 
               new Parcel() {
                  Width = 100, Height = 60 , Length = 3
               }
            }
         }
     };

      public static List<IParcelDeliveryCostRule> CreateSimpleParcelRuleSet(AutoMock mock)
         => new List<IParcelDeliveryCostRule>() { mock.Create<SimpleParcelDeliveryCostRule>() };

      public static AutoMock SetupRules(this AutoMock mock, List<IParcelDeliveryCostRule> ruleSets = null) {
         if (ruleSets != null) {
            mock.Provide((IEnumerable<IParcelDeliveryCostRule>)ruleSets);
         }
         return mock;
      }
   }
}
