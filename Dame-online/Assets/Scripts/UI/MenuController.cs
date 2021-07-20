using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    string menuName;
    public string MenuName { get { return menuName; } }
    public bool IsOpen { get; private set; }
    public void Open()
    {
        IsOpen = true;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        IsOpen = false;
        gameObject.SetActive(false);
    }
}
