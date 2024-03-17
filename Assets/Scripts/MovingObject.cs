using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
    protected int i;
    protected int j;
    protected int last_i;
    protected int last_j;
    protected bool CoordinateChanged()
    {
        return i != last_i || j != last_j;
    }
    protected void CalculatePosition(ref int i, ref int j)
    {
        i = GameController.gridSize - 1 - (int)((transform.position.z + 5 + GameController.gridSize / 2 * 10) / 10);
        j = (int)((transform.position.x + 5 + GameController.gridSize / 2 * 10) / 10);
    }
    protected void UpdatePosition()
    {
        last_i = i;
        last_j = j;
    }
    protected Vector3 MiddleOfTile(int i, int j, int y)
    {
        int x = (j - GameController.gridSize / 2) * 10;
        int z = (GameController.gridSize - 1 - i - GameController.gridSize / 2) * 10;
        return new Vector3(x, y, z);
    }
}
