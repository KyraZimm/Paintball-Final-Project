using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
        draggedRectTransform.anchoredPosition = eventData.position;

        /*
        draggedRectTransform.anchoredPosition = UnscaleEventDelta(eventData.position);
        
        print(eventData.position);
        print(draggedRectTransform.anchoredPosition);*/

    }
    
    public Vector3 UnscaleEventDelta(Vector3 vec)
    {
        Vector2 referenceResolution = _canvasScaler.referenceResolution;
        Vector2 currentResolution = new Vector2(Screen.width, Screen.height);
 
        float widthRatio = currentResolution.x / referenceResolution.x;
        float heightRatio = currentResolution.y / referenceResolution.y;
        float ratio = Mathf.Lerp(widthRatio, heightRatio, _canvasScaler.matchWidthOrHeight);
 
        return vec /ratio;
    }
}
