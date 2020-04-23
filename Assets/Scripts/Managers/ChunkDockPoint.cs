using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonTopDown
{
    [RequireComponent(typeof(SphereCollider))]
    public class ChunkDockPoint : MonoBehaviour
    {
        [SerializeField] public ChunkDockPointType Type = ChunkDockPointType.Undefined;

        public bool IsOverLappedWithAnotherCollider()
        {
            return Physics.OverlapSphere(transform.position, 0.1f, 
                LayerMask.GetMask("ChunkDockPoint")).Length >= 2;
        }
    }
}
