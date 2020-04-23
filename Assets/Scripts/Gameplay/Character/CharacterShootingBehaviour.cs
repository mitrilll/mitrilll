using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

namespace PolygonTopDown
{
    public class CharacterShootingBehaviour : MonoBehaviour
    {
        [SerializeField] private Vector3 _shootingPointOffset = Vector3.up;

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                var ray = new Ray(transform.TransformPoint(_shootingPointOffset), transform.forward);
                var raycastHits = Physics.RaycastAll
                    (ray, float.MaxValue, LayerMask.GetMask("Enemies", "Obstacles"));

                var hitResults = raycastHits.ToList();
                hitResults.Sort((x,y) => x.distance.CompareTo(y.distance));
                
                for(int i = 0; i < hitResults.Count; ++i)
                {
                    var rayOffset = Vector3.up * 0.05f * i;
                    Debug.DrawLine
                        (ray.origin + rayOffset, raycastHits[i].point + rayOffset, Color.red, 3f);

                    if (raycastHits[i].collider.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
                    {
                        Debug.Log("Obstacle was hitted");
                        return;
                    }

                    Debug.Log($"Enemy? {raycastHits[i].collider.gameObject.name}", raycastHits[i].collider);
                    
                    var enemyComponent = raycastHits[i].rigidbody.GetComponentInParent<Enemy>();
                    if (enemyComponent != null)
                    {
                        Debug.Log("Enemies was hitted");
                    }
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.TransformPoint(_shootingPointOffset), 0.2f);
        }
    }
}
