using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Pathfinding pathfinder;
    private PlayerMovement player;
    
    void Start()
    {
        //initialize pathfinding
        pathfinder = new Pathfinding(10, 10);
        
        //locate player
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();

        //function to check for obstacles, take them out of pathfinding

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = GetMouseWorldPosition();
            Debug.Log(mouseWorldPosition);
            
            pathfinder.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            Debug.Log("get XY:" + x + ", " + y);
            
            List<PathNode> path = pathfinder.FindPath(0, 0, x, y);
            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.Log("path node " + i + ": " + path[i]);
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) + Vector3.one, new Vector3(path[i + 1].x, path[i + 1].y) + Vector3.one, Color.green, 100f);
                }
            }
            
            player.SetTargetPosition(mouseWorldPosition);
        }
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
