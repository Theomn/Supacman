using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAccessor
{
    public LevelController level;
    public GameObject camera;
    public Player player;
    public Score score;

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
