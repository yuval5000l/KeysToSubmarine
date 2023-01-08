using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class AssignPlayers : MonoBehaviour
{
    [SerializeField] private TMP_Text num_players_text;
    [SerializeField] private OurEventHandler gameManager;
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private SpriteRenderer player1;
    [SerializeField] private SpriteRenderer player2;
    [SerializeField] private SpriteRenderer player3;
    [SerializeField] private Button button;
    private bool mainMenuActive = true;
    private int num_players = 0;
    private KeyCode player_1_action = KeyCode.Q;
    private KeyCode player_2_action = KeyCode.Space;
    private KeyCode player_3_action = KeyCode.B;
    private KeyCode player_4_action = KeyCode.M;

    // Start is called before the first frame update
    void Start()
    {
        num_players = 0;
        if (num_players_text != null)
        {
            num_players_text.text = num_players.ToString();
        }
        button.image.color = new Color(1f, 1f, 1f, 0.2f);
        button.enabled = false;
    }

    void Update()
    {
        if (num_players == 0)
        {
            if (Input.GetKeyDown(player_1_action))
            {
                num_players += 1;
                player1.color = new Color(1f, 1f, 1f, 1f);
            }
        }
        else if (num_players == 1)
        {
            if (Input.GetKeyDown(player_2_action))
            {
                num_players += 1;
                player2.color = new Color(1f, 1f, 1f, 1f);
            }
            if (Input.GetKeyDown(player_1_action))
            {
                num_players -= 1;
                player1.color = new Color(1f, 1f, 1f, 0.5f);
            }
        }
        else if (num_players == 2)
        {
            if (Input.GetKeyDown(player_3_action))
            {
                num_players += 1;
                player3.color = new Color(1f, 1f, 1f, 1f);
            }
            if (Input.GetKeyDown(player_2_action))
            {
                num_players -= 1;
                player2.color = new Color(1f, 1f, 1f, 0.5f);
            }
        }
        else if (num_players == 3)
        {
            if (Input.GetKeyDown(player_4_action))
            {
                num_players += 1;
            }
            if (Input.GetKeyDown(player_3_action))
            {
                num_players -= 1;
                player3.color = new Color(1f, 1f, 1f, 0.5f);
            }
        }
        else if (num_players == 3)
        {
            if (Input.GetKeyDown(player_4_action))
            {
                num_players -= 1;
            }
        }
        if (num_players >= 2)
        {
            button.enabled = true;
            button.image.color = new Color(1f, 1f, 1f, 1f);

            while (num_players > gameManager.getNumOfPlayers())
            {
                gameManager.AddPlayer();
            }
            while (num_players < gameManager.getNumOfPlayers())
            {
                gameManager.RemovePlayer();
            }
        }
        else
        {
            button.enabled = false;
            button.image.color = new Color(1f, 1f, 1f, 0.2f);
        }
        num_players_text.text = num_players.ToString();


    }

    public void StartGame()
    {
        if(num_players >= 2)
        {
            gameManager.Setlevel(1);
            gameManager.Nextlevel(-1);
        }
    }
    public void nextAfterLore()
    {
        SceneManager.LoadScene("ChooseNumPlayers");
    }

    public void ToLore()
    {
        SceneManager.LoadScene("Lore");
    }
    public void AddPlayer()
    {
        gameManager.AddPlayer();
        //num_players = gameManager.getNumOfPlayers();
        num_players_text.text = num_players.ToString();
    }
    public void RemovePlayer()
    {
        gameManager.RemovePlayer();
        //num_players = gameManager.getNumOfPlayers();
        num_players_text.text = num_players.ToString();

    }

    public bool getMainMenuActive()
    {
        return mainMenuActive;
    }
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}