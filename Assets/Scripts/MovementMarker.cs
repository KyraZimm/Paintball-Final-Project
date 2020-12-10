using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class MovementMarker : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform draggedRectTransform;
    public PlayerMovement player;
    
    CanvasScaler _canvasScaler;
    
    void Start()
    {
        _canvasScaler = GetComponentInParent<CanvasScaler>();
    }

    void Update()
    {
        player.markerPosition = draggedRectTransform.transform.position;
      
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        //draggedRectTransform.anchoredPosition = eventData.position;

        
        draggedRectTransform.anchoredPosition = UnscaleEventDelta(eventData.position);
        Debug.Log("event data: " + eventData.position);
        
        //print("event data: " + eventData.position);
        //print("marker data: " + draggedRectTransform.anchoredPosition);

        /*
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.Normalize();
        draggedRectTransform.anchoredPosition = targetPosition;
        */
        
        

    }
    
    public Vector3 UnscaleEventDelta(Vector3 vec)
    {
        Vector2 referenceResolution = _canvasScaler.referenceResolution;
        Vector2 currentResolution = new Vector2(Screen.width, Screen.height);
 
        float widthRatio = currentResolution.x / referenceResolution.x;
        float heightRatio = currentResolution.y / referenceResolution.y;
        float ratio = Mathf.Lerp(widthRatio, heightRatio, _canvasScaler.matchWidthOrHeight);

        Vector3 tempPosition = (vec / ratio);
        Vector3 finalPosition = Camera.main.ScreenToViewportPoint(tempPosition);
        
        Debug.Log("temporary position: " + tempPosition + ", mouse position: " + Input.mousePosition + ", final position: " + finalPosition);

        return tempPosition;
    }
}
