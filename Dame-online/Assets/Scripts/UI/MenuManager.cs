using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    [SerializeField]
    MenuController[] menus;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < menus.Length; i++)
            if (menus[i].MenuName.ToLower() == menuName.ToLower())
                OpenMenu(menus[i]);
    }
    public void OpenMenu(MenuController menu)
    {
        for (int i = 0; i < menus.Length; i++)
            if (menus[i].IsOpen)
                CloseMenu(menus[i]);
        menu.Open();
    }
    public void CloseMenu(MenuController menu)
    {
        menu.Close();
    }
}
