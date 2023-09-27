/*****************************************************************************
// File Name :         State.cs
// Author :            Nick Grinstead
//
// Brief Description :  Interface for the various game states.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface State
{
    public abstract void ShowInfo();

    public abstract void ChangeState();
}
