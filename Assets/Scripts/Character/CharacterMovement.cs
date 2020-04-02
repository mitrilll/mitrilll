using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
[SerializeField] private float _movementSpeed = 1f;
[SerializeField] private Animator _animatro = null;
[SerializeField] private float _сhanged = 0;
private Rigidbody _rigidbody;
private Vector3 _currentVelocity;
private Quaternion _currentRotation = Quaternion.identity;
private void Awake() 
{
    _rigidbody = GetComponent<Rigidbody>();
}


    // Update is called once per frame
    private void Update()
    {
        var  newVelocity = Vector3.zero;
        if(Input.GetKey(KeyCode.W))
        {
            newVelocity += Vector3.forward;
        }
        if(Input.GetKey(KeyCode.S))
        {
            newVelocity += -Vector3.forward;
        }
        if(Input.GetKey(KeyCode.A))
        {
            newVelocity += -Vector3.right;
        }
        if(Input.GetKey(KeyCode.D))
        {
            newVelocity += Vector3.right;
        }

        newVelocity = newVelocity.normalized * _movementSpeed;
        _currentVelocity = Vector3.Lerp(_currentVelocity, newVelocity, _сhanged * Time.deltaTime);

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var plane = new Plane(Vector3.up, transform.position);

        var lookVector = transform.forward;

        float distance;
        if (plane.Raycast(ray,out distance))
        {
            Vector3 lookPoint = ray.GetPoint(distance);
            lookPoint.y = transform.position.y;
            lookVector = (lookPoint - transform.position).normalized;
            //_currentRotation = Quaternion.LookRotation(lookPoint - transform.position, Vector3.up);
            _currentRotation = Quaternion.LookRotation(lookVector);
        }

        float moveSpeedX = Vector3.Dot(_currentVelocity / _movementSpeed, -Vector3.Cross(lookVector, Vector3.up));
        float moveSpeedZ = Vector3.Dot(_currentVelocity / _movementSpeed, lookVector);
        
        _animatro.SetFloat("MoveSpeedZ",moveSpeedZ);
        _animatro.SetFloat("MoveSpeedX",moveSpeedX);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animatro.SetTrigger("AttackTrigger");
        }
    }
    
    private void FixedUpdate() 
    {
        _rigidbody.velocity = _currentVelocity;
        _rigidbody.rotation = _currentRotation;
    }
}
