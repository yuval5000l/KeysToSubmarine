using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System;

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
    [SerializeField] private bool action_gamepad1 = false;
    private bool action_gamepad2 = false;
    private bool action_gamepad3 = false;
    [SerializeField] private bool action_spacebar = false;
    [SerializeField] private bool action_B = false;
    [SerializeField] private bool action_Q = false;
    private PlayerInput players_input;

    private IObservable<InputControl> m_ButtonPressListener;

    private InputAction myAction1;
    // Start is called before the first frame update
    void Start()
    {
        num_players = gameManager.getPlayersInGame();
        num_players = 0;
        if (num_players_text != null)
        {
            num_players_text.text = num_players.ToString();
        }
        button.image.color = new Color(1f, 1f, 1f, 0.2f);
        button.enabled = false;
        players_input = GetComponent<PlayerInput>();
        players_input.SwitchCurrentControlScheme("AssignPlayer");
        players_input.SwitchCurrentActionMap("AssignPlayerMap");
        //var myAction = new InputAction(binding: "<Keyboard>/#(g)");
        //myAction.performed += ((action) => Debug.Log($"Button {action.ToString()} pressed!"));
        //myAction.Enable();
        //myAction1 = myAction;
        //InputSystem.onAnyButtonPress.CallOnce(ctrl => Debug.Log($"Button {ctrl} was pressed"));
    //}
}
    //private void OnEnable()
    //{
    //    myAction1.Enable();
    //}

    private void AddPlayer(string name, string num)
    {
        //gameManager
    }
    private void RemovePlayer(string name, string num)
    {

    }
    void OnKeyboard1(InputValue inp)
    {
        if (inp.isPressed)
        {
            if (action_Q)
            {
                action_Q = false;
                AddPlayer("keyboard", "2");

            }
            else
            {
                action_Q = true;
                RemovePlayer("keyboard", "2");
            }
        }
    }
    void OnKeyboard2(InputValue inp)
    {
        if (inp.isPressed)
        {
            if (action_spacebar)
            {
                action_spacebar = false;
                AddPlayer("keyboard", "1");
            }
            else
            {
                action_spacebar = true;
                RemovePlayer("keyboard", "1");

            }
        }
    }
    void OnKeyboard3(InputValue inp)
    {
        if (inp.isPressed)
        {
            if (action_B)
            {
                action_B = false;
                AddPlayer("keyboard", "3");
            }
            else
            {
                action_B = true;
                RemovePlayer("keyboard", "3");

            }
        }
    }

    void OnGamePad1(InputValue inp)
    {
        if (inp.isPressed)
        {
            if (action_gamepad1)
            {
                action_gamepad1 = false;
            }
            else
            {
                action_gamepad1 = true;
            }
        }
        else
        {
            Debug.Log(InputSystem.GetDevice<InputDevice>());
        }
    }
    void Update()
    {
        //InputSystem.onEvent.Where(e => e.HasButtonPress())
        //.CallOnce(eventPtr =>
        //{
        //    foreach (var button in l.eventPtr.GetAllButtonPresses())
        //        Debug.Log($"Button {button} was pressed");
        //});
        //InputSystem.onAnyButtonPress.Call();
        //m_ButtonPressListener = InputSystem.onAnyButtonPress;
        //Debug.Log(m_ButtonPressListener);
        //Debug.Log(InputSystem.onAnyButtonPress.ToString());
        if (num_players == gameManager.getNumOfPlayers())
        {
            button.enabled = true;
            button.image.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            button.enabled = false;
            button.image.color = new Color(1f, 1f, 1f, 0.2f);
        }

        //if (num_players == 0)
        //{
        //    if (Input.GetKeyDown(player_1_action))
        //    {
        //        num_players += 1;
        //        player1.color = new Color(1f, 1f, 1f, 1f);
        //    }
        //}
        //else if (num_players == 1)
        //{
        //    if (Input.GetKeyDown(player_2_action))
        //    {
        //        num_players += 1;
        //        player2.color = new Color(1f, 1f, 1f, 1f);
        //    }
        //    if (Input.GetKeyDown(player_1_action))
        //    {
        //        num_players -= 1;
        //        player1.color = new Color(1f, 1f, 1f, 0.5f);
        //    }
        //}
        //else if (num_players == 2)
        //{
        //    if (Input.GetKeyDown(player_3_action))
        //    {
        //        num_players += 1;
        //        player3.color = new Color(1f, 1f, 1f, 1f);
        //    }
        //    if (Input.GetKeyDown(player_2_action))
        //    {
        //        num_players -= 1;
        //        player2.color = new Color(1f, 1f, 1f, 0.5f);
        //    }
        //}
        //else if (num_players == 3)
        //{
        //    if (Input.GetKeyDown(player_4_action))
        //    {
        //        num_players += 1;
        //    }
        //    if (Input.GetKeyDown(player_3_action))
        //    {
        //        num_players -= 1;
        //        player3.color = new Color(1f, 1f, 1f, 0.5f);
        //    }
        //}
        //else if (num_players == 3)
        //{
        //    if (Input.GetKeyDown(player_4_action))
        //    {
        //        num_players -= 1;
        //    }
        //}
        //if (num_players >= 2)
        //{
        //    button.enabled = true;
        //    button.image.color = new Color(1f, 1f, 1f, 1f);

        //    while (num_players > gameManager.getNumOfPlayers())
        //    {
        //        gameManager.AddPlayer();
        //    }
        //    while (num_players < gameManager.getNumOfPlayers())
        //    {
        //        gameManager.RemovePlayer();
        //    }
        //}
        //else
        //{
        //    button.enabled = false;
        //    button.image.color = new Color(1f, 1f, 1f, 0.2f);
        //}
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
        //gameManager.AddPlayer();
        //num_players = gameManager.getNumOfPlayers();
        num_players_text.text = num_players.ToString();
    }
    public void RemovePlayer()
    {
        //gameManager.RemovePlayer();
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
