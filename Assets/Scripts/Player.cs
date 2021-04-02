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

    public override void Start()
    {
        controllerStrategy = new ManualHorizontalStrategy();
        base.Start();
    }
}
