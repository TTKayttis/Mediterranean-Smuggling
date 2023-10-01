using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
[RequireComponent(typeof(Vehicle))]
public class VisualVehicle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragDroppable
{
    public TextMeshProUGUI capacityText;

    private Vehicle vehicle;

    public GameObject LockOverlay;

    void Start()
    {
        vehicle = GetComponent<Vehicle>();
        vehicle.OnLock.AddListener(() => LockOverlay.SetActive(true));
        vehicle.OnUnlock.AddListener(() => LockOverlay.SetActive(false));
    }
    public bool OnDropGameObject(GameObject target)
    {
        if (vehicle.Locked)
        {
            return false;
        }
        if(target.TryGetComponent<DragableCustomer>(out var customer))
        {
            vehicle.AddCustomer(customer.customer);
            AudioEffecPlayer.Instance.PlayAudio(0);
            Destroy(customer.gameObject);

            return true;
        }
        return false;


    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DragManager.Instance.AddHoverObject(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DragManager.Instance.RemoveHoverObject(gameObject);
    }
    private void LateUpdate()
    {
        capacityText.text = $"{vehicle.customers.Count}/{vehicle.maxRoom}";
    }

}
public interface IDragDroppable
{
   
    public bool OnDropGameObject(GameObject target);

   

}
