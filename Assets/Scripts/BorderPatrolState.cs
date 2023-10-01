using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class BorderPatrolState : ScriptableObject
{
    public float savePrecentage = 0.5f;

    public float minShipSizeToHappen = 5;

    public int minSaved = 3;

    public string eventText; 


    public int SaveCustomers(int count)
    {
        int saved = (int)(count * savePrecentage * Random.Range(0.2f,1f));
        if(count >= minSaved && saved < minSaved)
        {
            saved = minSaved;

        }
        return saved;

    }

    public string GetEventString(int dead, int saved)
    {
       
        return string.Format(eventText,dead, saved);

    }
}
