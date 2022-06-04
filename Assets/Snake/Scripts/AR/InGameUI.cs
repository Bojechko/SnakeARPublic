using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using Debug = System.Diagnostics.Debug;

public class InGameUI : MonoBehaviour
{
    // неконсистентный код стайл строки 11-17 done
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private Text scoreText;
    private int score = 0;
    [Header("Sprite")]
    //[SerializeField] private SpriteRenderer Sprite;
        
    [Header("Image")]
    //[SerializeField] private Image img;
    
    [Header("Sprites")]
    [SerializeField] private Sprite pause;
    [SerializeField] private Sprite go;
    public static event Action OnLeft;
    public static event Action OnRight;
    public static event Action OnMenu;
  

    void Start()
    {
        // такой вариант потенциально может привести к отсутствию подписки при нажатии на кнопку, почему  так решил написать? 
        // Потому что при invoke у меня не осуществлялась подписка на них, я долго бился с этой проблемой, пока один из гугл запросов
        // не предложил сделать так
        if (OnLeft != null) _leftButton.onClick.AddListener(OnLeft.Invoke);
        if (OnRight != null) _rightButton.onClick.AddListener(OnRight.Invoke);
        if (OnMenu != null) _menuButton.onClick.AddListener(OnMenu.Invoke);
        SnakeAR.ChangeScore += ChangeScore;
        SnakeAR.ChangeSprite += ChangeSprite;
        
        _menuButton.image.sprite = pause;
    }

    

    private void Update()
    {
        scoreText.text = "Score: " + score;
    }

    private void ChangeScore()
    {
        score++;
    }
    private void ChangeSprite()
    {
        var imageSprite = _menuButton.image.sprite == pause ? _menuButton.image.sprite = go : _menuButton.image.sprite = pause;
    }
}
