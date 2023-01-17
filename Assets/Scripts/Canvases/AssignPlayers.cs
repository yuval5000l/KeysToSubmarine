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
    [SerializeField] private OurEventHandler gameManager;
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private Animator[] players_animators = new Animator[3];
    [SerializeField] private Button button;
    
    [SerializeField] List<InputDevice> players_devices = new List<InputDevice>();
    List<int> players_devices_num = new List<int>();
    List<int> player_num = new List<int>();
    private bool mainMenuActive = true;
    private int num_players = 0;
    

    private PlayerInput players_input;

    private IObservable<InputControl> m_ButtonPressListener;

    private InputAction myAction1;
    // Start is called before the first frame update
    void Start()
    {
        num_players = gameManager.getPlayersInGame();
        num_players = 0;
        button.image.color = new Color(1f, 1f, 1f, 0.2f);
        button.enabled = false;
        players_input = GetComponent<PlayerInput>();
        players_input.SwitchCurrentControlScheme("AssignPlayer");
        players_input.SwitchCurrentActionMap("AssignPlayerMap");
        gameManager.ClearLists();

    }
    private void AddPlayerAnimation()
    {
        //Color goodColor = new Color(1f, 1f, 1f, 1f);
        int cur_num = 0;
        for (int i =0; i < 3; i++)
        {
            if (!player_num.Contains(i))
            {
                player_num.Add(i);
                cur_num = i;
                players_animators[cur_num].SetBool("Choosen", true);
                return;
            }
        }
    }
    private void RemovePlayerAnimation(int counter)
    {
        Color maColor = new Color(1f, 1f, 1f, 0.5f);
        players_animators[player_num[counter]].SetBool("Choosen", false);
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

    }

    public void StartGame()
    {
        if(players_devices.Count == 3)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (player_num[j] == i)
                    {
                        gameManager.AddPlayer(players_devices[j], players_devices_num[j]);
                    }
                }
            }
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
    }
    public void RemovePlayer()
    {
        //gameManager.RemovePlayer();
        //num_players = gameManager.getNumOfPlayers();

    }

    public void returnTo3()
    {
        SceneManager.LoadScene("3");
        button.enabled = true;
        button.image.color = new Color(1f, 1f, 1f, 1f);
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
