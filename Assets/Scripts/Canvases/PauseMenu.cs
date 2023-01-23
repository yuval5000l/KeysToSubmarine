using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private OurEventHandler GM;
    public static bool isGamePaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private MainMenuScript MainMenu;
    //[SerializeField] private MaEventHandler GM;

    // Start is called before the first frame update
    void Start()
    {
        GM = FindObjectOfType<OurEventHandler>().GetComponent<OurEventHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //if (MainMenu.getMainMenuActive() == false)
            //{
                if (isGamePaused)
                {
                    Resume();

                }
                else
                {
                    Pause();
                }
            //}
        }
        
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }



    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void Restartlevel()
    {
        Time.timeScale = 1f;
        GM.restartlevel();
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScreen");
        pauseMenuUI.SetActive(false);

    }
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
