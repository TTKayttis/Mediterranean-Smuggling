using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    private MoneyManager moneyManager;

    public string baseString;

    private TextMeshProUGUI text;
    private void Start()
    {
        moneyManager = MoneyManager.Instance;
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void LateUpdate()
    {
        text.text = baseString + moneyManager.money.ToString("C");
    }
}
