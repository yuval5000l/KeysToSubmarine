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
    private GameObject pauseFirstButton;
    //[SerializeField] private MainMenuScript MainMenu;
    //[SerializeField] private MaEventHandler GM;

    // Start is called before the first frame update
    void Start()
    {
        GM = FindObjectOfType<OurEventHandler>().GetComponent<OurEventHandler>();
        //bool first = true;
        //Debug.Log(GetComponentsInChildren<RectTransform>()[0].name);
        //Debug.Log(GetComponentsInChildren<RectTransform>()[0].GetComponentsInChildren<RectTransform>()[1].name);
        
        //foreach (GameObject obj in GetComponentsInChildren<GameObject>())
        //{
        //    if (first)
        //    {
        //        first = false;
        //    }
        //    else
        //    {
        //        first = true;
        //        foreach (GameObject ob in obj.GetComponentsInChildren<GameObject>())
        //        {
        //            if (first)
        //            {
        //                first = false;
        //            }
        //            else
        //            {
        //                pauseFirstButton = ob;
        //            }
        //        }
        //    }
        //}
        //pauseFirstButton = GetComponentInChildren<>
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
        if (pauseFirstButton == null)
        {
            pauseFirstButton = pauseMenuUI.GetComponentsInChildren<RectTransform>()[1].gameObject;
        }
        Time.timeScale = 0f;
        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
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
