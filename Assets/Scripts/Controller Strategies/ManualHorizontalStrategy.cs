using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ManualHorizontalStrategy : IControllerStrategy
{
    private List<List<bool>> levelGrid;

    public Tuple<int, int> CalculateNextPosition(int x, int y)
    {
        if (levelGrid == null)
        {
            levelGrid = GameAccessor.Instance().level.grid;
        }
        Tuple<int, int> outputPosition = null;
        if(Input.GetAxis("Horizontal") > 0f && !levelGrid[y][x + 1])
        {
            outputPosition = new Tuple<int, int>(x+1, y);
        }
        else if (Input.GetAxis("Horizontal") < 0f && !levelGrid[y][x - 1])
        {
            outputPosition = new Tuple<int, int>(x - 1, y);
        }
        else if (Input.GetAxis("Vertical") < 0f && !levelGrid[y + 1][x])
        {
            outputPosition = new Tuple<int, int>(x, y + 1);
        }
        else if (Input.GetAxis("Vertical") > 0f && !levelGrid[y - 1][x])
        {
            outputPosition = new Tuple<int, int>(x, y - 1);
        }
        return outputPosition;
    }

    public float GetMoveDelay()
    {
        return 0.3f;
    }
}
