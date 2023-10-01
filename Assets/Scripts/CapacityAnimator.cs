using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapacityAnimator : MonoBehaviour
{
    [SerializeField] RectTransform target;

    Vehicle vehicle;

    public float rotationAmount = 25f;

    private float rotatedAmount = 0;

    [SerializeField] float animationSpeed = 1;
    private void Start()
    {
        vehicle = GetComponent<Vehicle>();
    }
    public void LateUpdate()
    {
        float diff = (float)vehicle.customers.Count / vehicle.maxRoom;

        if (diff > 1)
        {
            if(diff > 3)
            {
                diff = 3;
            }
            float rot = diff * diff * Time.deltaTime * animationSpeed;
            target.Rotate(new Vector3(0,0, rot));
            rotatedAmount += Mathf.Abs(rot);
            if(rotationAmount < rotatedAmount)
            {
                rotatedAmount = -rotationAmount;
                animationSpeed = -animationSpeed;
            }
           
        }
        else
        {
            target.rotation = Quaternion.identity;
        }
    }
}
