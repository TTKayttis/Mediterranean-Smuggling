using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventUI : MonoSingleton<EventUI>
{
   [SerializeField] TMPro.TextMeshProUGUI text;

    private Queue<string> eventQue = new();
    protected override void Awake()
    {
        base.Awake();
        gameObject.SetActive(false);
    }

    public void SetText(string s)
    {

        //if there is alreayd event queue new one
        if(gameObject.activeInHierarchy)
        {
            eventQue.Enqueue(s);
        }
        else
        {
            gameObject.SetActive(true);
            text.text = s;
        }
    }

    public void NextEvent()
    {
        if (eventQue.Count > 0)
        {
            text.text = eventQue.Dequeue();
        }
        else
        {
          
            gameObject.SetActive(false);
        }
    }
}
