using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Pathfinding
{

   private const int MOVE_STRAIGHT_COST = 10;
   private const int MOVE_DIAGONAL_COST = 14;

   private Grid<PathNode> grid;
   private List<PathNode> openList;
   private List<PathNode> closedList;
   
   public Pathfinding(int width, int height)
   {
      grid = new Grid<PathNode>(width, height, 10f, Vector2.zero, (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y));
   }

   private List<PathNode> FindPath(int startX, int startY, int endX, int endY)
   {
      PathNode startNode = grid.GetGridObj(startX, startY);
      PathNode endNode = grid.GetGridObj(endX, endY);
      openList = new List<PathNode>{startNode};
      closedList = new List<PathNode>();
      
      //cycle through existing nodes
      for (int x = 0; x < grid.gridArray.GetLength(0); x++)
      {
         for (int y = 0; y < grid.gridArray.GetLength(1); y++)
         {
            PathNode pathNode = grid.GetGridObj(x, y);
            pathNode.gCost = int.MaxValue;
            pathNode.CalculateFCost();
            pathNode.cameFromNode = null;
         }
      }

      startNode.gCost = 0;
      startNode.hCost = CalculateDistanceCost(startNode, endNode);
      startNode.CalculateFCost();

      while (openList.Count > 0)
      {
         PathNode currentNode = GetLowestFCostNode(openList);
         if (currentNode == endNode)
         {
            return CalculatePath(endNode);
         }

         openList.Remove(currentNode);
         closedList.Add(currentNode);
      }

      //delete this once pathfinding is written:
      return null;
   }

   private List<PathNode> GetNeighborList(PathNode currentNode)
   {
      List<PathNode> neighborList = new List<PathNode>();

      if (currentNode.x - 1 >= 0)
      {
         neighborList.Add(GetNode(currentNode.x - 1, currentNode.y));
         if (currentNode.y - 1 >= 0) neighborList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
         if (currentNode.y + 1 < grid.gridArray.GetLength(1)) neighborList.Add(GetNode(currentNode.x - 1, currentNode. y + 1));
      }

      if (currentNode.x + 1 < grid.gridArray.GetLength(0))
      {
         neighborList.Add(GetNode(currentNode.x + 1, currentNode.y));
         if (currentNode.y - 1 >= 0) neighborList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
         if (currentNode.y + 1 < grid.gridArray.GetLength(1)) neighborList.Add(GetNode(currentNode.x + 1, currentNode. y + 1));
      }
      if (currentNode.y - 1 >= 0) neighborList.Add(GetNode(currentNode.x, currentNode.y - 1));
      if (currentNode.y + 1 < grid.gridArray.GetLength(1)) neighborList.Add(GetNode(currentNode.x, currentNode.y - 1));

      return neighborList;
   }

   private PathNode GetNode(int x, int y)
   {
      return grid.GetGridObj(x, y);
   }

   private List<PathNode> CalculatePath(PathNode endNode)
   {
      
      //delete later
      return null;
   }

   private int CalculateDistanceCost(PathNode a, PathNode b)
   {
      int xDistance = Mathf.Abs(a.x - b.x);
      int yDistance = Mathf.Abs(a.y - b.y);
      int remaining = Mathf.Abs(xDistance - yDistance);
      return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
   }

   private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
   {
      PathNode lowestFCostNode = pathNodeList[0];
      for (int i = 0; i < pathNodeList.Count; i++)
      {
         if (pathNodeList[i].fCost < lowestFCostNode.fCost)
         {
            lowestFCostNode = pathNodeList[i];
         }
      }

      return lowestFCostNode;
   }
}
