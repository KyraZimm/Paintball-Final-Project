using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager gameManager;

	private Pathfinding pathfinder;

	private bool checkedObstacles = false;
	public bool isPaused = false;
	public List<PathNode> closedNodes;

	[SerializeField] GameObject youLose;
	[SerializeField] GameObject youWin;

	void Awake()
	{
		gameManager = this;
	}
  
	void Start()
	{
		Instance = this;
		
		//initialize pathfinding
		pathfinder = new Pathfinding(20, 11);
	}

	void Update()
	{

		// pause game
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Log("space pressed");
			isPaused = !isPaused;
			if (isPaused)
			{
				Time.timeScale = 0f;
			}
			else if (!isPaused)
			{
				Time.timeScale = 1f;
			}
		}


		//check to see where obstacles are on map, and mark those as nodes to skip
		if (!checkedObstacles)
		{
			int obstacles = LayerMask.GetMask("Obstacles");
			float distToEdge = pathfinder.GetGrid().GetCellSize() / 2;
			float cellSize = pathfinder.GetGrid().GetCellSize();

			for (int x = 0; x < pathfinder.GetGrid().GetWidth(); x++)
			{
				for (int y = 0; y < pathfinder.GetGrid().GetHeight(); y++)
				{
					//raycast upwards
					RaycastHit2D checkObstacleUp = Physics2D.Raycast(new Vector2(((x * cellSize) + distToEdge), ((y * cellSize) + distToEdge)), Vector2.up * distToEdge, obstacles);
					Debug.DrawRay(new Vector2(((x * cellSize) + distToEdge), ((y * cellSize) + distToEdge)), Vector2.up * distToEdge, Color.red, 100f);

					//raycast to the right
					RaycastHit2D checkObstacleRight = Physics2D.Raycast(new Vector2(((x * cellSize) + distToEdge), ((y * cellSize) + distToEdge)), Vector2.right * distToEdge, obstacles);
					Debug.DrawRay(new Vector2(((x * cellSize) + distToEdge), ((y * cellSize) + distToEdge)), Vector2.right * distToEdge, Color.red, 100f);

					//raycast down
					RaycastHit2D checkObstacleDown= Physics2D.Raycast(new Vector2(((x * cellSize) + distToEdge), ((y * cellSize) + distToEdge)), Vector2.down * distToEdge, obstacles);
					Debug.DrawRay(new Vector2(((x * cellSize) + distToEdge), ((y * cellSize) + distToEdge)), Vector2.down * distToEdge, Color.red, 100f);
					
					//raycast left
					RaycastHit2D checkObstacleLeft = Physics2D.Raycast(new Vector2(((x * cellSize) + distToEdge), ((y * cellSize) + distToEdge)), Vector2.left * distToEdge, obstacles);
					Debug.DrawRay(new Vector2(((x * cellSize) + distToEdge), ((y * cellSize) + distToEdge)), Vector2.left * distToEdge, Color.red, 100f);
					
					//check  raycast results
					if (checkObstacleUp.collider != null || checkObstacleDown.collider != null || checkObstacleRight.collider != null || checkObstacleLeft.collider != null)
					{
						pathfinder.GetNode(x, y).isWalkable = false;
						Debug.Log("pathnode " + x + ", " + y + " is not walkable");
						closedNodes.Add(pathfinder.GetNode(x, y));
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


	public void Lose()
	{
		youLose.SetActive(true);
		Time.timeScale = 0f;
	}
	public void Win()
	{
		youWin.SetActive(true);
		Time.timeScale = 0f;
	}
	public void Restart()
	{
		SceneManager.LoadScene(0);
	}
}
