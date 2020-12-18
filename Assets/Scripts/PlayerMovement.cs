using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 targetPosition;
    private Vector3 currentPos;
    private List<Vector3> playerPath;
    private int currentPathIndex;
    private float speed = 5;

    private bool movementAllowed = false;

    public MovementMarker marker;
    public Vector3 markerPosition;
    private Autofire autofireHandling;

    private Vector2 groundHeight, groundWidth;
    
    private LineRenderer pathVisual;


    void Start()
    {
        currentPos = transform.position;
        pathVisual = gameObject.GetComponent<LineRenderer>();
        
        groundHeight = new Vector2(0, 25);
        groundWidth = new Vector2(0, 45);

        autofireHandling = GetComponent<Autofire>();
    }

   
    void Update()
    {
        if (!autofireHandling.isDead)
        {
        
            if (Input.GetMouseButtonUp(0))
            {
                if (MarkerInBounds(markerPosition) && !MarkerOverlappingObstacles(markerPosition))
                {
                    ShowPath(FetchPlannedPath(markerPosition));
                }
                else
                {
                    print("marker in bounds: " + MarkerInBounds(markerPosition));
                    print("marker overlapping obstacle: " + MarkerOverlappingObstacles(markerPosition));
                    ClearPath();
                    marker.ResetPosition();
                
                    //show error message
                }
            }
        
            if (Input.GetMouseButton(1))
            {
                ClearPath();
                if (MarkerInBounds(markerPosition) && !MarkerOverlappingObstacles(markerPosition))
                {
                    SetTargetPosition(markerPosition);
                }
                else
                {
                    print("marker in bounds: " + MarkerInBounds(markerPosition));
                    print("marker overlapping obstacle: " + MarkerOverlappingObstacles(markerPosition));
                    marker.ResetPosition();
                    
                    //error message
                }
            }
            
            //make player move
            HandleMovement();
        }
        else
        {
            //error message saying player is dead
        }
        
    }
    bool isMarkerHittingObstacle()
    {
        RaycastHit hit;
        bool isObstacle = false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
		{
            Debug.Log("found " + hit.collider.gameObject.name + " at distance: " + hit.distance);
            isObstacle = hit.collider.gameObject.tag == "Obstacle";
        }

        return isObstacle;
    }
    private bool MarkerInBounds(Vector3 markerPos)
    {
        
       //check whether marker is off-screen
        if (markerPos.x >= groundWidth.y || markerPos.x <= groundWidth.x || markerPos.y >= groundHeight.y || markerPos.y <= groundHeight.x)
        {
            return false;
        }
        else
        {
            return true;
        }
        
    }

    private bool MarkerOverlappingObstacles(Vector3 markerPos)
    {
        
        List<PathNode> closedNodes = GameManager.gameManager.closedNodes;
        float cellSize = 0.8f;
        //check whether marker is overlapping obstacle
        for (int i = 0; i < closedNodes.Count; i++)
        {
            if (markerPos.x >= (closedNodes[i].x*cellSize) && markerPos.x <= (closedNodes[i].x*cellSize) + cellSize && markerPos.y >= (closedNodes[i].y*cellSize) && markerPos.y <= (closedNodes[i].y*cellSize) + cellSize)
            {
                return true;
            }
        }

        return false;
    }

    private void HandleMovement()
    {
        if (playerPath != null)
        {
            Vector3 targetPosition = playerPath[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 0.2f) 
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;

                transform.position = transform.position + (moveDir * speed * Time.deltaTime);
                // Zeru's code: facing the marker
                if (!gameObject.GetComponent<Autofire>().isFiring)
                {
                    var dir = new Vector3(targetPosition.x, targetPosition.y, 0) - transform.position;
                    var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                }

            } 
            else {
                currentPathIndex++;
                if (currentPathIndex >= playerPath.Count) {
                    StopMoving();
                }
            }
        }
    }

    private void StopMoving()
    {
        currentPathIndex = 0;
        playerPath.Clear();
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    public void SetTargetPosition(Vector3 targetPosition) {
        currentPathIndex = 0;
        playerPath = Pathfinding.Instance.FindPath(GetPosition(), targetPosition);
        
        //temporary fix for final pathnode:
        //Vector3 finalNode = GameManager.GetMouseWorldPosition();
        //playerPath.Add(finalNode);
        
        if (playerPath != null && playerPath.Count > 1) {
            playerPath.RemoveAt(0);
        }
    }

    private List<Vector3> FetchPlannedPath(Vector3 targetPosition)
    {
        List<Vector3> plannedPath = Pathfinding.Instance.FindPath(GetPosition(), targetPosition);
        return plannedPath;
    }

    private void ShowPath(List<Vector3> futurePath)
    {
        pathVisual.positionCount = futurePath.Count;

        for (int i = 0; i < futurePath.Count; i++)
        {
            pathVisual.SetPosition(i, futurePath[i]);
        }
    }

    private void ClearPath()
    {
        pathVisual.positionCount = 1;
    }
}
