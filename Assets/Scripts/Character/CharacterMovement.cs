using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace PolygonTopDown
{
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 5f;
        [SerializeField] private Animator _animatro = null;
        
        [SerializeField] private float _сhanged = 5f;
        [SerializeField ]private Rigidbody _rigidbody;
        
        private Vector3 _targetMovementVelocity = Vector3.zero;
        private Vector3 _currentVelocity = Vector3.zero;
        
        private Quaternion _currentRotation = Quaternion.identity;

        private void OnEnable()
        {
            InputManager.Instance.EventPlayerMovementDirectionChanged += OnPlayerMovementDirectionChanged;
        }

        private void OnDisable()
        {
            InputManager.Instance.EventPlayerMovementDirectionChanged -= OnPlayerMovementDirectionChanged;
        }

        private void OnPlayerMovementDirectionChanged(Vector3 targetDirection)
        {
            _targetMovementVelocity = targetDirection * _movementSpeed;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }


        // Update is called once per frame
        private void Update()
        {
            _currentVelocity = Vector3.Lerp(_currentVelocity, _targetMovementVelocity, _сhanged * Time.deltaTime);

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var plane = new Plane(Vector3.up, transform.position);

            var lookVector = transform.forward;

            float distance;
            if (plane.Raycast(ray, out distance))
            {
                Vector3 lookPoint = ray.GetPoint(distance);
                lookPoint.y = transform.position.y;
                lookVector = (lookPoint - transform.position).normalized;
                //_currentRotation = Quaternion.LookRotation(lookPoint - transform.position, Vector3.up);
                _currentRotation = Quaternion.LookRotation(lookVector);
            }

            float moveSpeedX = Vector3.Dot(_currentVelocity / _movementSpeed, -Vector3.Cross
                (lookVector, Vector3.up));
            float moveSpeedZ = Vector3.Dot(_currentVelocity / _movementSpeed, lookVector);

            _animatro.SetFloat("MoveSpeedZ", moveSpeedZ);
            _animatro.SetFloat("MoveSpeedX", moveSpeedX);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _animatro.SetTrigger("AttackTrigger");
            }
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = new Vector3(_currentVelocity.x, _rigidbody.velocity.y, _currentVelocity.z);
            _rigidbody.rotation = _currentRotation;
        }
    }
}