using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonTopDown
{
    [Flags]
    public enum EnemyType : byte
    {
        Undefined = 0,
        
        Weak = 1 << 1,
        Fast = 1 << 2,
        Strong = 1 << 3,
    }
}