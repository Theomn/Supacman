using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private string testLevel =
        ".X.X.\n" +
        "X..*X\n" +
        "..o..\n" +
        "X*..X\n" +
        ".X.X.";

    private void Awake()
    {
        GameAccessor.Instance().camera = Camera.main.gameObject;
    }
    void Start()
    {
        GameAccessor.Instance().levelLoader.Load(testLevel);
    }
}
