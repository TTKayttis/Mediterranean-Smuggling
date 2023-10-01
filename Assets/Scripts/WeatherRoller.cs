using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu()]
public class WeatherRoller : ScriptableObject
{

    public float max = 5;

    [SerializeField] int rollCount = 5;
    [SerializeField] int lowestValues = 5;
    

    public float RollWeather()
    {

        List<float> rolls = new();
        for (int j = 0; j < rollCount; j++)
        {
            rolls.Add(Random.Range(0, max / lowestValues));
        }
        rolls = rolls.OrderBy(x => x).ToList();
        float roll = 0;
        for (int j = 0; j < lowestValues; j++)
        {
            roll += rolls[j];
        }
        return roll;
    }
}
