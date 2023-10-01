using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(RectTransform))]

public class DragableCustomer : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{


    public TextMeshProUGUI costText;
    public Customer customer;

    void Start()
    {

        costText.text = customer.money.ToString("C0");

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        DragManager.Instance.AddDragObject(gameObject);
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.Translate(eventData.delta);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragManager.Instance.FreeDragObject(gameObject);
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }



    
}
