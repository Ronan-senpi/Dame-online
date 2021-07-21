using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void QuitGame()
    {
#if UNITY_WEBPLAYER
     public static string webplayerQuitURL = "http://ronan-dhersignerie.fr/";
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
#else
         Application.Quit();
#endif
    }
}
