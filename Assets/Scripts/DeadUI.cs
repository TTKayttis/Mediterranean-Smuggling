using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeadUI : MonoBehaviour
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
        text.text = baseString + gameManager.lost;
    }
}
