using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{

    [SerializeField] private Transform _target = null;
    [SerializeField] private Vector3 _offset = Vector3.zero;
    [SerializeField] private float _movementSpeed = 5.0f;
    // Start is called before the first frame update
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position + _offset, _movementSpeed *Time.deltaTime);
    }
}
