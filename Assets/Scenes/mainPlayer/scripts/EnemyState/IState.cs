﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    //prepare the state
    void Enter(Enemy parent);

    void Update();

    void Exit();
}
