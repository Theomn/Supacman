using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAccessor
{
    public LevelLoader levelLoader;
    public GameObject camera;

    private static GameAccessor instance;

    public static GameAccessor Instance()
    {
        if (instance == null)
        {
            instance = new GameAccessor();
        }
        return instance;
    }
}
