using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableScreen : IScreen
{
    private GameObject screen;
    public EnableScreen(GameObject screen)
    {
        this.screen = screen;
    }
    public void Execute()
    {
        screen.SetActive(true);
    }
}
