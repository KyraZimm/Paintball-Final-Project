using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Pathfinding pathfinder;
    private PlayerMovement player;

    private bool checkedObstacles = false;
    
    void Start()
    {
        //initialize pathfinding
        pathfinder = new Pathfinding(20, 11);
        
        //locate player
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();

        //function to check for obstacles, take them out of pathfinding

    }

    void Update()
    {
        
        //check to see where obstacles are on map, and mark those as nodes to skip
        if (!checkedObstacles)
        {
            int obstacles = LayerMask.GetMask("Obstacles");
            float distToEdge = pathfinder.GetGrid().GetCellSize()/2;
            float cellSize = pathfinder.GetGrid().GetCellSize();

            for (int x = 0; x < pathfinder.GetGrid().GetWidth(); x++)
            {
                for (int y = 0; y < pathfinder.GetGrid().GetHeight(); y++)
                {
                    RaycastHit2D checkObstacleUp =  Physics2D.Raycast(new Vector2(((x*cellSize) + distToEdge), ((y*cellSize) + distToEdge)), Vector2.up*distToEdge, obstacles);
                    Debug.DrawRay(new Vector2(((x*cellSize) + distToEdge), ((y*cellSize) + distToEdge)), Vector2.up * distToEdge, Color.red, 100f);

                    Debug.Log(checkObstacleUp.collider);
                    
                    if (checkObstacleUp.collider != null)
                    {
                        pathfinder.GetNode(x, y).isWalkable = false;
                        Debug.Log("pathnode " + x + ", " + y + " is not walkable");
                    }
                }
            }

            checkedObstacles = true;
        }
        
        /*
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = GetMouseWorldPosition();
            pathfinder.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            
            List<PathNode> path = pathfinder.FindPath(0, 0, x, y);
            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) + Vector3.one, new Vector3(path[i + 1].x, path[i + 1].y) + Vector3.one, Color.green, 100f);
                }
            }
        }*/
    }
    
    //mouse position functions
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
