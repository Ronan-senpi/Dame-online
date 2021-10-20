using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLoader : MonoBehaviour
{
    void Start()
    {
        MenuManager.Instance.OpenMenu(MenuType.Game);
    }
}
