using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject[] screens;
    public List<DraggedIconHolder> IconHolders = new List<DraggedIconHolder>();
    public int currentScreenIndex = 0;
    private CommandInvoker commandInvoker = new CommandInvoker();

    private void OnEnable()
    {
        // DraggedIconHolder.isIconDropped += EnableScreen;
    }

    private void OnDisable()
    {
        // DraggedIconHolder.isIconDropped -= EnableScreen;
    }
    void Start()
    {
        for (int i = 1; i < screens.Length; i++)
        {
            screens[i].SetActive(false);
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            OnBackButtonClick();
        }
    }
    public void EnableScreen(int screenIndex)
    {
        commandInvoker.SetCommand(new DisableScreen(screens[currentScreenIndex]));
        commandInvoker.ExecuteCommand();

        commandInvoker.SetCommand(new EnableScreen(screens[screenIndex]));
        commandInvoker.ExecuteCommand();

        currentScreenIndex = screenIndex;
    }

    public void OnBackButtonClick()
    {
        foreach (var holder in IconHolders)
        {
            holder.ResetHolderData();
        }
        int previousScreenIndex = Mathf.Max(0, currentScreenIndex - 1);
        EnableScreen(previousScreenIndex);

    }
}

