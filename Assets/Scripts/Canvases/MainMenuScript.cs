using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private TMP_Text num_players_text;
    [SerializeField] private OurEventHandler gameManager;
    [SerializeField] private GameObject MainMenu;
    private bool mainMenuActive = true;
    private int num_players;
    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.isGamePaused = true;
        //Time.timeScale = 0f;
        num_players = gameManager.getNumOfPlayers();
        num_players_text.text = num_players.ToString();
    }
    public void StartGame()
    {
        PauseMenu.isGamePaused = false;
        //Time.timeScale = 1f;
        MainMenu.SetActive(false);
        mainMenuActive = false;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        gameManager.Nextlevel();
        //Tutorial.SetActive(false);
    }

    public void AddPlayer()
    {
        gameManager.AddPlayer();
        num_players = gameManager.getNumOfPlayers();
        num_players_text.text = num_players.ToString();


    }
    public void RemovePlayer()
    {
        gameManager.RemovePlayer();
        num_players = gameManager.getNumOfPlayers();
        num_players_text.text = num_players.ToString();

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

    //public void ReturnToMainMenu()
    //{
    //    MainMenu.SetActive(true);
    //    mainMenuActive = true;

    //    Time.timeScale = 0f;
    //    PauseMenu.isGamePaused = true;

    //}
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
