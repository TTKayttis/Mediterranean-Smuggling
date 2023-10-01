using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoSingleton<MoneyManager>
{
    public float money;


    public bool TryUseMoney(float f)
    {
        if(f > money)
        {
            return false;
        }
        money -= f;
        return true;


    }
}
