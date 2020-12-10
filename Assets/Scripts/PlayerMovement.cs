using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 targetPosition;
    private Vector3 currentPos;
    private List<Vector3> playerPath;
    private int currentPathIndex;
    private float speed = 5;

    private bool movementAllowed = false;

    public Vector3 markerPosition;

    private Vector2 groundHeight, groundWidth;
    

    private LineRenderer pathVisual;

    void Start()
    {
        currentPos = transform.position;
        pathVisual = gameObject.GetComponent<LineRenderer>();
        
        groundHeight = new Vector2(0, 5);
        groundWidth = new Vector2(0, 10);
    }

   
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ShowPath(FetchPlannedPath(markerPosition));
        }
        
        if (Input.GetMouseButton(1))
        {
            ClearPath();
            SetTargetPosition(markerPosition);
        }
        
        HandleMovement();
    }

    private bool CheckMarkerBoundaries(Vector3 markerPos)
    {
        //check whether marker is off-screen
        if (markerPos.x >= groundWidth.y || markerPos.x <= groundWidth.y || markerPos.y >= groundHeight.y || markerPos.y <= groundHeight.x)
        {
            return false;
        }
        else
        {
            return true;
        }
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
        playerPath = null;
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
