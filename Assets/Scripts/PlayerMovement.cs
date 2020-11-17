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

    void Start()
    {
        currentPos = transform.position;
    }

   
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            SetTargetPosition(markerPosition);
        }
        
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (playerPath != null)
        {
            Vector3 targetPosition = playerPath[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 0.02f) {
                Vector3 moveDir = (targetPosition - transform.position).normalized;

                transform.position = transform.position + (moveDir * speed * Time.deltaTime);
                
            } else {
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
}
