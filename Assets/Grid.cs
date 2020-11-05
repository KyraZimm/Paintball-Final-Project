using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
   private int width;
   private int height;
   private float cellSize;
   public int[,] gridArray;
   
   public Grid(int width, int height, float cellSize)
   {
      this.width = width;
      this.height = height;
      this.cellSize = cellSize;
      
      gridArray = new int[width,height];

      for (int x = 0; x < gridArray.GetLength(0); x++) {
         for (int y = 0; y < gridArray.GetLength(1); y++)
         {
            Debug.Log(x + ", " + y);
            Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y+1), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
         }
      }
      Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
      Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
      
   }

   private Vector2 GetWorldPosition(int x, int y)
   {
      return new Vector2(x, y) * cellSize;
   }

   private void GetXY(Vector2 worldPosition, out int x, out int y)
   {
      x = Mathf.FloorToInt(worldPosition.x / cellSize);
      y = Mathf.FloorToInt(worldPosition.y / cellSize);
   }

   public void SetValue(int x, int y, int value)
   {
      if (x >= 0 && y >= 0 && x <= width && y <= height)
      {
         gridArray[x, y] = value;
      }
   }

   public void SetValue(Vector2 worldPosition, int value)
   {
      int x, y;
      GetXY(worldPosition, out x, out y);
      SetValue(x, y, value);
   }
}
