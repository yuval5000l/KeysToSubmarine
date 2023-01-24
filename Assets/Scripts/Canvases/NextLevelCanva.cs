using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NextLevelCanva : MonoBehaviour
{
    [SerializeField] private OurEventHandler GM;
    private GameObject pauseFirstButton;
    private Button button;
    //private PlayerManager playerManager;
    //private List<List<KeyCode>> playersControl;
    // Start is called before the first frame update
    void Start()
    {
        //playerManager = FindObjectOfType<PlayerManager>().GetComponent<PlayerManager>();
        GM = FindObjectOfType<OurEventHandler>().GetComponent<OurEventHandler>();
        EventSystem.current.SetSelectedGameObject(null);
        pauseFirstButton = GetComponentsInChildren<RectTransform>()[1].GetComponentsInChildren<RectTransform>()[1].gameObject;
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        button = pauseFirstButton.GetComponent<Button>();
        //playersControl = playerManager.GetPlayersControls();
        button.enabled = false;
        button.image.color = new Color(1f, 1f, 1f, 0.2f);
        Time.timeScale = 0.000001f;
        StartCoroutine(PauseWaitResume(1f));
    }
    private IEnumerator PauseWaitResume(float pauseDelay)
    {
        Time.timeScale = .0000001f;
        yield return new WaitForSeconds(pauseDelay * Time.timeScale);
        //yield WaitForSeconds(pauseDelay* Time.timeScale);
        //Time.timeScale = 1f;
        ButtonActive();
    }
    public void ButtonActive()
    {
        button.enabled = true;
        button.image.color = new Color(1f, 1f, 1f, 1f);
    }


    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(playersControl[0][4]) || Input.GetKey(playersControl[1][4]) || Input.GetKey(playersControl[2][4]) || Input.GetKey(playersControl[3][4]))
        //{
        //    NextLevel();
        //}
    }

    public void NextLevel()
    {
        GM.Nextlevel();
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

    }
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
