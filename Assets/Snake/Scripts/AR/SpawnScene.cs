using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class SpawnScene : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private GameObject spawnedObject;
    private ARPlaneManager planeManager;
    
    [FormerlySerializedAs("Prefab")] [SerializeField] private GameObject prefab;

    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject startUI;

    private bool stopSpawn = false;
    private static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
    }
    

    bool TryToGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    
    private void Update()
    {
        if (stopSpawn)
        {
            foreach (var plane in planeManager.trackables)
            {
                plane.gameObject.SetActive(false);
            }

            startUI.SetActive(false);
            inGameUI.SetActive(true);
        }
        
        
        if (!TryToGetTouchPosition(out Vector2 touchPosition))
        {
            return;
        }
        
        

        if (raycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPos = s_Hits[0].pose;
            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(prefab, hitPos.position, hitPos.rotation);
                spawnedObject.transform.rotation = hitPos.rotation;
                inGameUI.SetActive(true);
                stopSpawn = true;
            }
        }
    }
}
