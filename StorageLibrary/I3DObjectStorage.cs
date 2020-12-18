using System;
using System.Collections.Generic;
using System.Text;

namespace StorageLibrary
{
  public interface I3DObjectStorage
    {
        int ID { get; }
        string Description { get; }
        double Weight { get; }
        double Volume { get;}
        double Area { get; }
        bool IsFragile { get; }
        int LongestSide { get; }
        decimal InsuranceValue { get; set; }

    }
}
