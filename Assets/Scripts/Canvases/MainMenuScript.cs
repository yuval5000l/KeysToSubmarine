using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private GameObject MainMenu;
    private bool mainMenuActive = true;
    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.isGamePaused = true;
        Time.timeScale = 0f;

    }
    public void StartGame()
    {
        PauseMenu.isGamePaused = false;
        Time.timeScale = 1f;
        MainMenu.SetActive(false);
        mainMenuActive = false;
        //Tutorial.SetActive(false);
    }

    public void AddPlayer()
    {
        playerManager.AddPlayer();
    }
    public void RemovePlayer()
    {
        playerManager.RemovePlayer();
    }

    public bool getMainMenuActive()
    {
        return mainMenuActive;
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.anyKey)
        //{
        //    if (startPage)
        //    {
        //        startPage = false;
        //        //GoToLore();
        //    }
        //}
    }

    //public void GoToLore()
    //{
    //    mainmenu.setactive(false);
    //    lore.setactive(true);
    //}

    //public void gotomainmenu()
    //{
    //    mainmenu.setactive(true);
    //    lore.setactive(false);
    //    startpage = true;

    //}
    //public void gototutorial()
    //{
    //    lore.setactive(false);
    //    tutorial.setactive(true);
    //}
    //public void startanimation()
    //{
    //    fade_out.settrigger("fadeout");
    //}

    public void ReturnToMainMenu()
    {
        MainMenu.SetActive(true);
        mainMenuActive = true;

        Time.timeScale = 0f;
        PauseMenu.isGamePaused = true;

    }
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
