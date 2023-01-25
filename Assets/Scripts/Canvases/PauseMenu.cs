using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private OurEventHandler GM;
    public static bool isGamePaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    // [SerializeField] private AudioSource menuMusic;
    private GameObject pauseFirstButton;
    private BackGroundScript BG;
    //[SerializeField] private MainMenuScript MainMenu;
    //[SerializeField] private MaEventHandler GM;

    // Start is called before the first frame update
    void Start()
    {
        GM = FindObjectOfType<OurEventHandler>().GetComponent<OurEventHandler>();
    }
    public void press()
    {
        if (isGamePaused)
        {
            Resume();

        }
        else
        {
            Pause();
        }
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
        BG.StopMenuMusic();
        BG.PlayLevelMusic();
    }



    void Pause()
    {
        pauseMenuUI.SetActive(true);
        if (pauseFirstButton == null)
        {
            pauseFirstButton = pauseMenuUI.GetComponentsInChildren<RectTransform>()[1].gameObject;
        }
        Time.timeScale = 0f;
        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        isGamePaused = true;
        BG.PlayMenuMusic();
        BG.PauseLevelMusic();
    }

    public void Restartlevel()
    {
        Time.timeScale = 1f;
        GM.restartlevel();
        BG.StopMenuMusic();
        BG.StopLevelMusic();
        BG.PlayLevelMusic();
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScreen");
        pauseMenuUI.SetActive(false);
        BG.StopMenuMusic();
        BG.StopLevelMusic();
    }
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
