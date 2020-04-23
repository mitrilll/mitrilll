using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonTopDown
{
    public enum PlayerMovementDirection
    {
        Undefined = 0,
        
        None = 1,
        North = 2,
        NorthEast = 3,
        East = 4,
        SouthEast = 5,
        South = 6,
        SouthWest = 7,
        West = 8,
        NorthWest = 9,
    }
    public class InputManager : SingletonGameObject<InputManager>
    {
        public  Action<Vector3> EventPlayerMovementDirectionChanged;

        private Vector3 _targetMovementVector = Vector3.zero;

        private void Update()
        {
            var newVelocity = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                newVelocity += Vector3.forward;
            }

            if (Input.GetKey(KeyCode.S))
            {
                newVelocity += -Vector3.forward;
            }

            if (Input.GetKey(KeyCode.A))
            {
                newVelocity += -Vector3.right;
            }

            if (Input.GetKey(KeyCode.D))
            {
                newVelocity += Vector3.right;
            }

            newVelocity = newVelocity.normalized;

            if (_targetMovementVector != newVelocity)
            {
                _targetMovementVector = newVelocity;
                EventPlayerMovementDirectionChanged?.Invoke(_targetMovementVector);
            }
        }
    }
}
