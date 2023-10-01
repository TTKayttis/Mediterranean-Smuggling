using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoSingleton<DragManager>
{
    private Vector2 startPosition;

    public GameObject currentDragged;

    public List<GameObject> hoverObjects = new List<GameObject>();

    public void AddDragObject(GameObject dragObject)
    {
        if(currentDragged != null)
        {
            currentDragged.transform.position = startPosition;
        }
        currentDragged = dragObject;
        startPosition = dragObject.transform.position;
    }
    public void FreeDragObject(GameObject dragObject)
    {
        if(currentDragged == dragObject)
        {
            if (DropObject(dragObject))
            {

            }
            else
            {
                currentDragged.transform.position = startPosition;
            }
           
            currentDragged = null;
        }
      

    }
    /// <summary>
    /// Try to drop to each onhover object, returns true if object was acpeted
    /// </summary>
    /// <param name="dragObject"></param>
    /// <returns></returns>
    private bool DropObject(GameObject dragObject)
    {
        hoverObjects.RemoveAll(x => x == null); 
       foreach (var o in hoverObjects)
        {
            
            if(o.TryGetComponent<IDragDroppable>(out var dragDroppable))
            {
                if (dragDroppable.OnDropGameObject(dragObject))
                {
                    return true;

                }
            }
        }
        return false;
    }

    public void AddHoverObject(GameObject gameObject)
    {
        hoverObjects.Add(gameObject);

    }

    public void RemoveHoverObject(GameObject gameObject)
    {

        hoverObjects.Remove(gameObject);

    }
}
