using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AIRandomVilainStrategy : IControllerStrategy
{
    private List<List<bool>> levelGrid;
    
    public Tuple<int, int> CalculateNextPosition(int x, int y)
    {
        if (levelGrid == null)
        {
            levelGrid = GameAccessor.Instance().level.grid;
        }
        List<Tuple<int, int>> outputPositions = new List<Tuple<int, int>>();
        if (!levelGrid[y][x + 1])
        {
            outputPositions.Add(new Tuple<int, int>(x + 1, y));
        }
        if (!levelGrid[y][x - 1])
        {
            outputPositions.Add(new Tuple<int, int>(x - 1, y));
        }
        if (!levelGrid[y + 1][x])
        {
            outputPositions.Add(new Tuple<int, int>(x, y + 1));
        }
        if (!levelGrid[y - 1][x])
        {
            outputPositions.Add(new Tuple<int, int>(x, y - 1));
        }
        return outputPositions[UnityEngine.Random.Range(0, outputPositions.Count)];
    }

    public float GetMoveDelay()
    {
        return 0.4f;
    }
}
