using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class CustomerSpawner : MonoSingleton<CustomerSpawner>
{ 
    public List<GameObject> customers = new();

    public RectTransform spawnZone;

    public float spawnMargin = 5;

    private void Start()
    {
      
        SpawnCustomer();
    }
  
    public List<Customer> SpawnCustomer()
    {
        List<Customer> leftBehind = new List<Customer>();
       
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject o = transform.GetChild(i).gameObject;
            if(o != null)
            {
                leftBehind.Add(o.GetComponent<DragableCustomer>().customer);
                Destroy(o);
            }
          
        }

        int customerCount = 5 + GameManager.round * 3;
        for (int i = 0; i < customerCount; i++)
        {
            int index = Random.Range(0, customers.Count);
            Instantiate(customers[index], GetRandomSpotInRectTranform(spawnZone, spawnMargin), Quaternion.identity, transform);
        }

        return leftBehind;
    }

    public void Clear()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject o = transform.GetChild(i).gameObject;
            Destroy(o);
            

        }

    }
    private Vector2 RandomSpotInZone()
    {
       
        return new Vector2(Random.Range(spawnZone.rect.min.x, spawnZone.rect.max.x) +spawnZone.position.x,
                            Random.Range(spawnZone.rect.min.y, spawnZone.rect.max.y) + spawnZone.position.y);


    }

    public static Vector2 GetRandomSpotInRectTranform(RectTransform rectTransform, float margin)
    {
        Vector3[] vecs = new Vector3[4];
        rectTransform.GetWorldCorners(vecs);
        return new Vector2(Random.Range(vecs[0].x+margin, vecs[2].x - margin),
                           Random.Range(vecs[0].y + margin, vecs[2].y - margin));

    }
}
