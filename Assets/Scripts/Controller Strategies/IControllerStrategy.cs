using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IControllerStrategy
{
    /// <summary>
    /// Returns the next position this agent should go to, considering its current position and the level layout.
    /// </summary>
    /// <param>Current position of the agent (x,y)</param>
    /// <returns>New agent position (x,y)</returns>
    Tuple<int, int> CalculateNextPosition(int x, int y);
}
