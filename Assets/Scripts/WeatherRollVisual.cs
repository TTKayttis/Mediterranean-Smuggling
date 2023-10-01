using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class WeatherRollVisual : MonoBehaviour
{
    [SerializeField] GameObject baseObject;

    private Vector3 spawnVector = Vector3.zero;

    public float valueStep = 0.1f;

    public float end = 5;

    List<GameObject> objectLinks = new();
    [SerializeField] int rollCount = 5;
    [SerializeField] int lowestValues = 5;

    private void Start()
    {
        for (float i = 0; i <= end; i+= valueStep)
        {
            GameObject o = Instantiate(baseObject, transform.position + spawnVector, Quaternion.identity, transform);
            spawnVector += Vector3.right;
            objectLinks.Add(o);
        }
    }

    private void Update()
    {

        for (int x = 0; x < 10; x++)
        {
            
       
        List<float> rolls = new();
        for (int j = 0; j < rollCount; j++)
        {
            rolls.Add(Random.Range(0, end / lowestValues));
        }
        rolls =  rolls.OrderBy( x => x).ToList();
        float roll = 0;
        for (int j = 0; j < lowestValues; j++)
        {
            roll += rolls[j];
        }


        int i = (int) (roll / valueStep);

        objectLinks[i].transform.localScale += Vector3.up / 10f;

        }
    }


}
