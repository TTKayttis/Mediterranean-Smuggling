using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideText : MonoSingleton<GuideText>
{
    [SerializeField] GameObject template;



    public  void AddGuideText(string s)
    {
        GameObject o = Instantiate(template, transform);
        o.SetActive(true);
        o.GetComponent<TMPro.TextMeshProUGUI>().text = s;
    }
}
