using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AIHorizontalVilainStrategy : IControllerStrategy
{
    private List<List<bool>> levelGrid;

    public Tuple<int, int> CalculateNextPosition(int x, int y)
    {
        Tuple<int, int> outputPosition = new Tuple<int, int>(x, y);
        Player player = GameAccessor.Instance().player;
        var path = AStarAlgorithm.Compute(x, y, player.x, player.y, AStarAlgorithm.MovementType.Horizontal);
        DebugDrawPath(path);
        if (path != null && path.Count >= 2)
        {
            return new Tuple<int, int>(path[1].x, path[1].y);
        }
        return outputPosition;
    }

    private void DebugDrawPath(List<AStarAlgorithm.Node> path)
    {
        var level = GameAccessor.Instance().level;
        foreach (AStarAlgorithm.Node node in path)
        {
            if (node.previousNode != null)
            {
                Debug.DrawLine(level.LevelToWorldPosition(node.x, node.y), level.LevelToWorldPosition(node.previousNode.x, node.previousNode.y), Color.red, 0.5f);
            }
        }
    }
}
