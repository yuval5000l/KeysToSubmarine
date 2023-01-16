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
    [SerializeField] private SpriteRenderer[] players_animators = new SpriteRenderer[3];
    [SerializeField] private Button button;
    
    [SerializeField] List<InputDevice> players_devices = new List<InputDevice>();
    List<int> players_devices_num = new List<int>();
    List<int> player_num = new List<int>();
    int[] player_num_default = new int[3] { 0, 1, 2 };
    private bool mainMenuActive = true;
    private int num_players = 0;
    private KeyCode player_1_action = KeyCode.Q;
    private KeyCode player_2_action = KeyCode.Space;
    private KeyCode player_3_action = KeyCode.B;
    private KeyCode player_4_action = KeyCode.M;
    //[SerializeField] private bool action_gamepad1 = false;
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
    private void AddPlayerAnimation()
    {
        Color goodColor = new Color(1f, 1f, 1f, 1f);
        int cur_num = 0;
        for (int i =0; i < 3; i++)
        {
            if (!player_num.Contains(i))
            {
                player_num.Add(i);
                cur_num = i;
                players_animators[cur_num].color = goodColor;
                return;
            }
        }
    }
    private void RemovePlayerAnimation(int counter)
    {
        Color maColor = new Color(1f, 1f, 1f, 0.5f);
        players_animators[player_num[counter]].color = maColor;
        player_num.RemoveAt(counter);

    }
    private void AddPlayer(InputDevice inp, int num)
    {
        //gameManager
        if (players_devices.Count < gameManager.getNumOfPlayers())
        {
            players_devices.Add(inp);
            players_devices_num.Add(num);
            AddPlayerAnimation();
        }
        
    }
    private void RemovePlayer(InputDevice inp, int num)
    {
        if (players_devices.Count > 0)
        {
            int counter = -1;
            for (int i = 0; i < players_devices.Count; i++)
            {
                if (players_devices[i] == InputSystem.GetDevice<InputDevice>() && players_devices_num[i] == num)
                {
                    counter = i;
                }
            }

            players_devices.Remove(inp);
            players_devices_num.Remove(num);
            RemovePlayerAnimation(counter);

        }
    }
    void OnKeyboard1(InputValue inp)
    {
        if (!inp.isPressed)
        {
            int counter = -1;
            for (int i = 0; i < players_devices.Count; i++)
            {
                if (players_devices[i] == InputSystem.GetDevice<InputDevice>())
                {
                    if (players_devices_num[i] == 2)
                    {
                        counter = i;
                    }
                }
            }
            if (counter == -1)
            {
                AddPlayer(Keyboard.current, 2);
            }
            else
            {
                RemovePlayer(Keyboard.current, 2);
            }
        }
    }
    void OnKeyboard2(InputValue inp)
    {
        if (!inp.isPressed)
        {
            int counter = -1;
            for (int i = 0; i < players_devices.Count; i++)
            {
                if (players_devices[i] == InputSystem.GetDevice<InputDevice>())
                {
                    if (players_devices_num[i] == 1)
                    {
                        counter = i;
                    }
                }
            }
            if (counter == -1)
            {
                AddPlayer(Keyboard.current, 1);
            }
            else
            {
                RemovePlayer(Keyboard.current, 1);
            }
        }
    }
    void OnKeyboard3(InputValue inp)
    {
        if (!inp.isPressed)
        {
                int counter = -1;
                for (int i = 0; i < players_devices.Count; i++)
                {
                    if (players_devices[i] == InputSystem.GetDevice<InputDevice>())
                    {
                        if (players_devices_num[i] == 3)
                    {
                            counter = i;
                        }

                    }
                }
                if (counter == -1)
                {
                    AddPlayer(Keyboard.current, 3);
                }
                else
                {
                    RemovePlayer(Keyboard.current, 3);
                }
        }
    }

    void OnGamePad1(InputValue inp)
    {
        if (!inp.isPressed)
        {
            //Debug.Log(InputSystem.GetDevice<InputDevice>().ToString().Substring(InputSystem.GetDevice<InputDevice>().ToString().Length-1));
            
                if (!players_devices.Contains(InputSystem.GetDevice<InputDevice>()))
                {
                    if (players_devices.Count < gameManager.getNumOfPlayers())
                    {
                        players_devices.Add(InputSystem.GetDevice<InputDevice>());
                        if (InputSystem.GetDevice<InputDevice>().ToString().Substring(InputSystem.GetDevice<InputDevice>().ToString().Length - 1) == "s")
                        {
                            players_devices_num.Add(0);
                        }
                        else
                        {
                            int x = int.Parse(InputSystem.GetDevice<InputDevice>().ToString().Substring(InputSystem.GetDevice<InputDevice>().ToString().Length - 1));
                            players_devices_num.Add(x);
                        }
                        AddPlayerAnimation();
                    }
                }
                else
                {
                    if (players_devices.Count > 0)
                    {
                        int counter = 0;
                        for (int i = 0; i < players_devices.Count; i++)
                        {
                            if (players_devices[i] == InputSystem.GetDevice<InputDevice>())
                            {
                                counter = i;
                            }
                        }
                        players_devices.Remove(InputSystem.GetDevice<InputDevice>());
                        players_devices_num.RemoveAt(counter);
                        RemovePlayerAnimation(counter);
                }
            }
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
        if (players_devices.Count == gameManager.getNumOfPlayers())
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
