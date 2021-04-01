using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Player : ControllableAgent
{
    private void Awake()
    {
        GameAccessor.Instance().player = this;
    }

    void Start()
    {
        level = GameAccessor.Instance().level;
        controllerStrategy = new ManualHorizontalStrategy();
        x = 1;
        y = 1;
        //transform.position = level.LevelToWorldPosition(x, y);
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
                transform.DOMove(target, 0.3f).OnComplete(EndMovement);
            }
        }
    }

    private void EndMovement()
    {
        state = State.Idle;
    }
}
