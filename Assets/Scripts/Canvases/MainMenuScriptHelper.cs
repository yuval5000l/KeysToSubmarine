using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScriptHelper : MonoBehaviour
{
    [SerializeField] private MainMenuScript MainMenuPapa;


    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        MainMenuPapa.StartGame();
    }
}
