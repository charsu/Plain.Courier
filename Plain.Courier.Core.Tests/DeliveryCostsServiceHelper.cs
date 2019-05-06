using System.Collections.Generic;
using System.Linq;
using Autofac.Extras.Moq;
using Moq;
using Plain.Courier.Core.Delivery;
using Plain.Courier.Core.Delivery.Models;
using Plain.Courier.Core.Delivery.Services;
using Plain.Courier.Core.Delivery.Services.Rules;

namespace Plain.Courier.Core.Tests.Delivery {
   public static class DeliveryCostsServiceHelper {
      public static List<Order> GetOrderWithOneOfEach(bool isSpeedy = false)
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

      public static List<Order> GetOrderWithOneOfEachAndWeightExcess(bool isSpeedy = false)
         => new List<Order>() {
         new Order() {
            IsSpeedyDelivery = isSpeedy,
            Parcels = new List<Parcel>(){
               // small + 2kg weight excess
               new Parcel() {
                  Width = 3, Height = 3 , Length = 3 ,Weight= 3
               },
               // medium + 2kg weight excess
               new Parcel() {
                  Width = 30, Height = 3 , Length = 3,Weight= 5
               },
               // large + 2kg weight excess
               new Parcel() {
                  Width = 30, Height = 60 , Length = 3, Weight = 8
               },
               // xl + 2kg weight excess
               new Parcel() {
                  Width = 100, Height = 60 , Length = 3, Weight = 12
               },
               // heavy weight + 2kg weight excess
               new Parcel() {
                  Width = 100, Height = 60 , Length = 3, Weight = 52
               }
            }
         }
     };

      public static List<ParcelDeliverySummary> GetSmallParcelsSummariesForDiscount()
         => Enumerable.Range(0, 10).Select(x => new ParcelDeliverySummary() {
            ParcelSize = Core.Delivery.Constants.ParcelSize.Small,
            Price = 10 + x
         }).ToList();

      public static List<ParcelDeliverySummary> GetMediumParcelsSummariesForDiscount()
         => Enumerable.Range(0, 10).Select(x => new ParcelDeliverySummary() {
            ParcelSize = Core.Delivery.Constants.ParcelSize.Medium,
            Price = 10 + x
         }).ToList();

      public static List<IParcelDeliveryCostRule> CreateSimpleParcelRuleSet(AutoMock mock)
         => new List<IParcelDeliveryCostRule>() { mock.Create<SimpleParcelDeliveryCostRule>() };

      public static List<IParcelDeliveryCostRule> CreateSpeedyParcelRuleSet(AutoMock mock)
         => new List<IParcelDeliveryCostRule>() {
            mock.Create<SimpleParcelDeliveryCostRule>(),
            mock.Create<SpeedyParcelDeliveryCostRule>()
         };

      public static List<IParcelDeliveryCostRule> CreateWeigthParcelRuleSet(AutoMock mock)
         => new List<IParcelDeliveryCostRule>() {
            mock.Create<SimpleParcelDeliveryCostRule>(),
            mock.Create<SpeedyParcelDeliveryCostRule>(),
            mock.Create<WeigthParcelDeliveryCostRule>()
         };

      public static AutoMock SetupRules(this AutoMock mock, List<IParcelDeliveryCostRule> ruleSets = null) {
         if (ruleSets != null) {
            mock.Provide((IEnumerable<IParcelDeliveryCostRule>)ruleSets);
         }
         return mock;
      }

      public static AutoMock SetupDiscountService(this AutoMock autoMock) {
         //setup pass through
         var m = autoMock.Mock<IDiscountParcelDeliveryService>();
         m.Setup(s => s.ComputeDiscounts(It.IsAny<List<ParcelDeliverySummary>>()))
            .Returns<List<ParcelDeliverySummary>>((list) => list);

         return autoMock;
      }
   }
}
