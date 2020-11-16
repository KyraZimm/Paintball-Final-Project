using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MovementMarker : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform draggedRectTransform;
    private PlayerMovement player;

    void Start()
    {
        player = GetComponentInParent<PlayerMovement>();
    }

    void Update()
    {
        player.markerPosition = draggedRectTransform.transform.position;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        draggedRectTransform.anchoredPosition += eventData.delta;
    }
}
