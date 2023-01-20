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
    // [SerializeField] private AudioSource menuMusic;
    private int num_players;
    // Start is called before the first frame update
    void Start()
    {
        num_players = gameManager.getNumOfPlayers();
        if (num_players_text != null)
        {
            num_players_text.text = num_players.ToString();
        }
        // menuMusic.Play();
    }
    public void StartGame()
    {
        gameManager.Setlevel(1);
        gameManager.Nextlevel(-1);

    }
    public void nextAfterLore()
    {
        SceneManager.LoadScene("ChooseNumPlayers");
    }

    public void To1()
    {
        SceneManager.LoadScene("1");
    }
    public void To2()
    {
        SceneManager.LoadScene("2");
    }
    public void To3()
    {
        SceneManager.LoadScene("3");
    }
    public void ToJanitorSelect()
    {
        SceneManager.LoadScene("Janitor select");
    }
    public void ToLore()
    {
        SceneManager.LoadScene("Lore");
    }
    public void AddPlayer()
    {
        //gameManager.AddPlayer();
        num_players = gameManager.getNumOfPlayers();
        num_players_text.text = num_players.ToString();


    }
    public void RemovePlayer()
    {
        //gameManager.RemovePlayer();
        num_players = gameManager.getNumOfPlayers();
        num_players_text.text = num_players.ToString();

    }

    // Update is called once per frame
    void Update()
    {
    }


    public void Resume()
    {
        MainMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("StartScreen");

    }
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
