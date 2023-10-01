using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBar : MonoBehaviour
{
    GameManager gameManager;
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(  gameManager.TurnTimeLeft /gameManager.TurnLength ,1,1);
    }
}
