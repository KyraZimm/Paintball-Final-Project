using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Grid<TGridObject>
{
   private int width;
   private int height;
   private float cellSize;
   public TGridObject[,] gridArray;
   private Vector2 origin;

   public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged; 
   public class OnGridValueChangedEventArgs : EventArgs
   {
      public int x;
      public int y;
   }
   
   public Grid(int width, int height, float cellSize, Vector2 origin, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject)
   {
      this.width = width;
      this.height = height;
      this.cellSize = cellSize;
      this.origin = origin;
      
      gridArray = new TGridObject[width,height];

      for (int x = 0; x < gridArray.GetLength(0); x++) {
         for (int y = 0; y < gridArray.GetLength(1); y++)
         {
            //debug
            Debug.Log(x + ", " + y);
            Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y+1), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            
            //initialize grid objects
            gridArray[x, y] = createGridObject(this, x, y);
         }
      }
      Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
      Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
      
   }

   private Vector2 GetWorldPosition(int x, int y)
   {
      return (new Vector2(x, y) * cellSize) + origin;
   }

   private void GetXY(Vector2 worldPosition, out int x, out int y)
   {
      x = Mathf.FloorToInt(worldPosition.x - origin.x/ cellSize);
      y = Mathf.FloorToInt(worldPosition.y - origin.y/ cellSize);
   }

   public void SetGridObj(int x, int y, TGridObject value)
   {
      if (x >= 0 && y >= 0 && x <= width && y <= height)
      {
         gridArray[x, y] = value;
      }
   }

   public void SetGridObj(Vector2 worldPosition, TGridObject value)
   {
      int x, y;
      GetXY(worldPosition, out x, out y);
      SetGridObj(x, y, value);
   }

   public void TriggerGridObjectChanged(int x, int y)
   {
      if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs {x=x, y=y});
   }

   public TGridObject GetGridObj(int x, int y)
   {
      if (x >= 0 && y >= 0 && x <= width && y <= height)
      {
         return gridArray[x, y];
      }
      else
      {
         return default(TGridObject);
      }
   }

   public TGridObject GetGridObj(Vector3 worldPosition)
   {
      int x, y;
      GetXY(worldPosition, out x, out y);
      return GetGridObj(x, y);
   }
}
