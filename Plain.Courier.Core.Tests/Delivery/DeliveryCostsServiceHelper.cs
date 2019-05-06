using System.Collections.Generic;
using Autofac.Extras.Moq;
using Plain.Courier.Core.Delivery;
using Plain.Courier.Core.Delivery.Models;
using Plain.Courier.Core.Delivery.Services.Rules;

namespace Plain.Courier.Core.Tests.Delivery {
   public static class DeliveryCostsServiceHelper {
      public static List<Order> GetOrders_OneOfEach(bool isSpeedy = false)
         => new List<Order>() {
         new Order() {
            IsSpeedyDelivery = isSpeedy,
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

      public static List<IParcelDeliveryCostRule> CreateSpeedyParcelRuleSet(AutoMock mock)
         => new List<IParcelDeliveryCostRule>() {
            mock.Create<SimpleParcelDeliveryCostRule>(),
            mock.Create<SpeedyParcelDeliveryCostRule>()
         };

      public static AutoMock SetupRules(this AutoMock mock, List<IParcelDeliveryCostRule> ruleSets = null) {
         if (ruleSets != null) {
            mock.Provide((IEnumerable<IParcelDeliveryCostRule>)ruleSets);
         }
         return mock;
      }
   }
}
