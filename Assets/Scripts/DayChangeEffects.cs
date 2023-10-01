using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayChangeEffects : MonoBehaviour
{
    [SerializeField] Image background;


    [SerializeField] AudioSource audioSource;

    private Color baseColor;

    public Color darkColor;
    public void Start()
    {
        baseColor = background.color;
        GameManager.Instance.OnDayChange.AddListener(Execute);

    }

    private void Execute()
    {
        audioSource.Play();
        float weatherMod = GameManager.Instance.weather / 5f;
     

        background.color = baseColor - darkColor * weatherMod;
    }
}
