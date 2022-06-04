using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
public class SnakeAR : MonoBehaviour
{
    [SerializeField] private List<Transform> _tails;
    [SerializeField] private GameObject _notATrigger;
    [SerializeField] private float _bonesDistance;
    [SerializeField] private GameObject _bonePrefab;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    
    private int leftOrRight = 0;
    protected bool pause = false;

    public static event Action ChangeScore;
    public static event Action EndGame;
    public static event Action ChangeSprite;
    
    
    

    private void Start()
    {
        InGameUI.OnLeft += TurnLeft;
        InGameUI.OnRight += TurnRight;
        InGameUI.OnMenu += Pause;
    }

    private void Pause()
    {
        // можно написать немного короче при инверсии: pause = !pause; done
        pause = !pause;
        if (ChangeSprite != null) ChangeSprite();        
    }
    
    private void TurnRight()
    {
        leftOrRight = 1;
    }

    private void TurnLeft()
    {
        leftOrRight = -1;
    }
    
    // Почему для движения выбрал fixed update? особенно с учетом того, что в MoveHead() используется Time.deltaTime. 
    // Насколько это заметно, вопрос, но будет получаться неравномерное движение
    // Это мне позволило высчитать  _rotationSpeed, чтобы поворот всегда был на 90 градусов. Иначе он поворачивается на случайное число градусов
   void FixedUpdate()
    {
        if (pause == false)
        {
            MoveHead();
            MoveTail();
            RotateHead();
        }
        
    }

    // неиспользуемый аргумент в методе done
    private void MoveHead()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void MoveTail()
    {
        float sqrtDistance = (float) Math.Sqrt(_bonesDistance);
        Vector3 previousPosition = transform.position;
        Quaternion previousRotation = transform.rotation ;

        foreach (var bone in _tails) 
        {
            if ((bone.position - previousPosition).sqrMagnitude > sqrtDistance)
            {
                (bone.position, previousPosition) = (previousPosition, bone.position);
                (bone.rotation, previousRotation) = (previousRotation, bone.rotation);
            }
            else
            {
                break;
            }
        }
    }


    private void RotateHead()
    {
        float angle = leftOrRight * _rotationSpeed * Time.deltaTime ;
        transform.Rotate(0, angle, 0);
        leftOrRight = 0;
    }
    
     private void OnTriggerEnter(Collider other)
     {
         EatDot(other);
         EatWall(other);
         EatTail(other);
     }

     private void EatDot(Collider other)
     {
         if (other.TryGetComponent(out iEat eat))
         {
             Destroy(other.gameObject);
                
             GameObject bone ; 
             if (GameObject.FindGameObjectsWithTag("notATrigger").Length > 1)
             {
                 bone = Instantiate(_bonePrefab);
             }
             else
             {
                 bone = Instantiate(_notATrigger);
             }
                
             _tails.Add(bone.transform);
             if (ChangeScore != null) ChangeScore();
         } 
     }


     
     private void EatWall(Collider other)
     {  
         if (other.TryGetComponent(out Wall wall))
         {
             Destroy(gameObject);
             if (EndGame != null) EndGame();
         }
     }

     private void EatTail(Collider other)
     {
         if (other.TryGetComponent(out iBone tail)  )
         {
             Destroy(gameObject);
             if (EndGame != null) EndGame();
         }
         
     }
    
     
   
}



