using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Vilain : ControllableAgent
{
    void Start()
    {
        level = GameAccessor.Instance().level;
        state = State.Idle;
    }

    void FixedUpdate()
    {
        if (state == State.Idle)
        {
            Tuple<int, int> nextPosition = controllerStrategy.CalculateNextPosition(x, y);
            if (nextPosition != null)
            {
                state = State.Moving;
                x = nextPosition.Item1;
                y = nextPosition.Item2;
                Vector3 target = level.LevelToWorldPosition(x, y);
                transform.DOMove(target, 0.5f).OnComplete(EndMovement);
            }
        }
    }

    private void EndMovement()
    {
        state = State.Idle;
    }
}
