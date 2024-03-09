using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableScreen : IScreen
{
    private GameObject screen;
    public DisableScreen(GameObject screen)
    {
        this.screen = screen;
    }
    public void Execute()
    {
        screen.SetActive(false);
    }
}
