using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllableAgent : MonoBehaviour
{
    protected LevelController level;
    protected enum State
    {
        Idle,
        Moving
    }
    public int x, y;
    protected State state;
    public IControllerStrategy controllerStrategy;
}
