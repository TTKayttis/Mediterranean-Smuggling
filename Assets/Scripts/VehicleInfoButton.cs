using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VehicleInfoButton : MonoBehaviour
{

    public TextMeshProUGUI costText;

    public string costBaseString;

    public TextMeshProUGUI stabilityText;

    public string stabilityBaseString;

    public TextMeshProUGUI capacityText;

    public string capacityBaseString;

    public Button buyButton;


    public GameObject TargetVehicle;

    public Sprite boatSprite;

    public Image boatImage;

    public void ShowInfo()
    {
        var vehicle = TargetVehicle.GetComponent<Vehicle>();

        costText.text = costBaseString + vehicle.price;
        stabilityText.text = stabilityBaseString + vehicle.stability;
        capacityText.text = capacityBaseString + vehicle.maxRoom;
        boatImage.sprite = boatSprite;
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => GameManager.Instance.BuyVehicle(TargetVehicle, vehicle));


    } 
}
