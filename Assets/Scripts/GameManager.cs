using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }
    private MenuController pauseMenu;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        pauseMenu = MenuManager.Instance.GetMenu(MenuType.Pause);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.IsOpen)
                MenuManager.Instance.CloseMenu(pauseMenu);
            else
                MenuManager.Instance.OpenMenu(pauseMenu);
        }
    }
    public void QuitGame()
    {
        RoomManager.Instance.OnPlayerLeave();
        MenuManager.Instance.OpenMenu(MenuType.Loading);
    }
}
