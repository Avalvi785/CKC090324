using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    private IScreen screen;

    public void SetCommand(IScreen screen)
    {
        this.screen = screen;
    }

    public void ExecuteCommand()
    {
        screen?.Execute();
    }
}
