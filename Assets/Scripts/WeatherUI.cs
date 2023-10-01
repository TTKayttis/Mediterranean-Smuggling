using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WeatherUI : MonoBehaviour
{
    private GameManager gameManager;

    public string baseString;

    private TextMeshProUGUI text;
    private void Start()
    {
        gameManager = GameManager.Instance;
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void LateUpdate()
    {

        text.text = baseString + WeahterToString(gameManager.weather); 
    }

    private string WeahterToString(float weather)
    {
        if(weather <= 1)
        {
            return "Clear";
        }
        if(weather <= 2)
        {
            return "Good";
        }
        if (weather <= 3)
        {
            return "Windy";
        }
        if (weather <= 4)
        {
            return "Bad";
        }

        return "Storm";
    }
}
