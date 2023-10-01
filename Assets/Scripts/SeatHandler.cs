using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SeatHandler : MonoBehaviour
{
   
    private RectTransform rectTransform;

    private int seatsTaken = 0;

    public List<Transform> premadeSeats;

    [SerializeField] GameObject scaledCustomer;
    private void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
    }
    public void AddCustomer()
    {

       
        if(seatsTaken < premadeSeats.Count)
        {
            premadeSeats[seatsTaken].gameObject.SetActive(true);
            seatsTaken++;

        }
        else
        {
            GameObject newObject = Instantiate(scaledCustomer, transform);
            newObject.transform.position = RandomSpotInZone();
            newObject.gameObject.SetActive(true);
        }


    }

    public void Clear()
    {
        seatsTaken = 0;
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(false);
        }

    }
    private Vector2 RandomSpotInZone()
    {
        return new Vector2(Random.Range(rectTransform.rect.min.x, rectTransform.rect.max.x) + rectTransform.position.x,
                            Random.Range(rectTransform.rect.min.y, rectTransform.rect.max.y) + rectTransform.position.y);


    }
}
