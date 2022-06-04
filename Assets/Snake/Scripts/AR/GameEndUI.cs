using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;



public class GameEndUI : MonoBehaviour
{
    [SerializeField] private GameObject endGameUI;
    
    void Start()
    {
        SnakeAR.EndGame += EndGame;
    }

    private void Update()
    {
        if (endGameUI.active && Input.touchCount > 0)
        {
            endGameUI.SetActive(false);
            SceneManager.LoadScene("snakeAR");    
        }
        
    }

    private void EndGame()
    {
        if (endGameUI != null)
        {
            endGameUI.SetActive(true);    
        }
        
    }
}
