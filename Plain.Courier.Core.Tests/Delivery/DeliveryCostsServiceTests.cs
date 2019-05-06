using System;
using System.Collections.Generic;
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
      public void DeliveryForOrder_OK() {
         var orders = GetOrdersSetup1();

         var service = GetMock().Create<DeliveryCostsService>();

         var result = service.ComputeCost(orders);

         Assert.IsNotNull(result);
      }
   }

   public static class DeliveryCostsServiceHelper {
      public static List<Order> GetOrdersSetup1()
         => new List<Order>() {
         new Order() {
            Parcels = new List<Parcel>(){
            new Parcel() {
               Width = 3, Height = 3 , Length = 3
            }
         }
      }
     };
   }
}
