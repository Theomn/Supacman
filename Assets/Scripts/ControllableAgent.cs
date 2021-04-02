using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class ControllableAgent : MonoBehaviour
{
    public float moveDelay;

    protected LevelController level;
    protected enum State
    {
        Idle,
        Moving
    }
    public int x, y;
    protected State state;
    public IControllerStrategy controllerStrategy;

    public virtual void Start()
    {
        level = GameAccessor.Instance().level;
        state = State.Idle;
        moveDelay = controllerStrategy.GetMoveDelay();
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
                transform.DOMove(target, moveDelay).OnComplete(EndMovement);
            }
        }
    }

    private void EndMovement()
    {
        state = State.Idle;
    }
}
