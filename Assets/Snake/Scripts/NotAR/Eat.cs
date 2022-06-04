using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Eat : MonoBehaviour
{
    [SerializeField] private GameObject _dotPrefab;
    [SerializeField] private Transform plane;
    private void FixedUpdate()
    {
        Spawn();
    }

    private void Spawn()
    {
        if ( GameObject.FindGameObjectsWithTag("Dot").Length == 0)
        {
            Vector3 position = new Vector3(UnityEngine.Random.Range(-4.5f, 4.5f), 0.1f, Random.Range(-4.5f, 4.5f));
            GameObject dot = Instantiate(_dotPrefab, position, Quaternion.identity);
            dot.transform.SetParent(plane);
            dot.transform.localPosition = new Vector3(UnityEngine.Random.Range(-4.5f, 4.5f), 0.1f, Random.Range(-4.5f, 4.5f));
        }

        Debug.Log(_dotPrefab == null);
    }
}
