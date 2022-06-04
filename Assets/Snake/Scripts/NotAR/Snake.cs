using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class Snake : MonoBehaviour
{
    [SerializeField] private List<Transform> _tails;
    [SerializeField] private GameObject _notATrigger;
    [SerializeField] private float _bonesDistance;
    [SerializeField] private GameObject _bonePrefab;
    [SerializeField] private float _speed;
    
   void FixedUpdate()
    {
        MoveHead(_speed);
        MoveTail();
        RotateHead();
    }

    // неиспользуемый аргумент в методе
    private void MoveHead(float rotationSpeed)
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void MoveTail()
    {
        float sqrtDistance = (float) Math.Sqrt(_bonesDistance);
        Vector3 previousPosition = transform.position;

        foreach (var bone in _tails) 
        {
            if ((bone.position - previousPosition).sqrMagnitude > sqrtDistance)
            {
                (bone.position, previousPosition) = (previousPosition, bone.position);
            }
            else
            {
                break;
            }
        }
    }

    private void RotateHead()
    {
        float angle = Input.GetAxis("Horizontal") * 190 * Time.deltaTime;
        transform.Rotate(0, angle, 0);
    }
    
     private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out iEat eat))
            {
                Destroy(other.gameObject);
                
                GameObject bone ; 
                if (GameObject.FindGameObjectsWithTag("notATrigger").Length > 3)
                {
                    bone = Instantiate(_bonePrefab);
                }
                else
                {
                    bone = Instantiate(_notATrigger);
                }
                
                _tails.Add(bone.transform);
            }
            
            if (other.TryGetComponent(out Wall wall))
            {
                Destroy(gameObject);
            }

            if (other.TryGetComponent(out iBone tail)  )
            {
                Destroy(gameObject);
            }
        }
}
