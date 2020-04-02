using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonTopDown
{
    public enum Direction : byte
    {
       // Undefined,
        
        North = 0,
        NorthEast = 1,
        East = 2,
        SouthEast = 3,
        South = 4,
        SouthWest = 5,
        West = 6,
        NorthWest = 7
    }

    public static class DirectionExtension
    {
        public static Direction GetOpposite(this Direction _this)
        {
            int underlyingValue = (int) _this;
            return (Direction)((underlyingValue + 4) % 8);
        }
    }
}
