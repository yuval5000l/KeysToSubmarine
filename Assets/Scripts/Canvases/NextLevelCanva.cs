using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelCanva : MonoBehaviour
{
    [SerializeField] private OurEventHandler GM;
    private PlayerManager playerManager;
    private List<List<KeyCode>> playersControl;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>().GetComponent<PlayerManager>();
        GM = FindObjectOfType<OurEventHandler>().GetComponent<OurEventHandler>();
        //playersControl = playerManager.GetPlayersControls();
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
