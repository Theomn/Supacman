using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private void Awake()
    {
        GameAccessor.Instance().camera = Camera.main.gameObject;
    }
    void Start()
    {
        GameAccessor.Instance().level.Load();
    }
}
