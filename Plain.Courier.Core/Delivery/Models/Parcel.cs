﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Plain.Courier.Core.Delivery.Models {
   public class Parcel {
      public int Length { get; set; }
      public int Width { get; set; }
      public int Height { get; set; }
      public int Weight { get; set; } = 0;

      public List<int> Dimensions => new List<int>() { Length, Width, Height };
   }
}
