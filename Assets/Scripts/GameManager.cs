using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private Pathfinding playerPathfinder;
    
    void Start()
    {
        //initialize pathfinding
        playerPathfinder = new Pathfinding(10, 10);
        
        //function to check for obstacles, take them out of pathfinding
       
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
        }

        if (Input.GetMouseButtonDown(1))
        {
           
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
