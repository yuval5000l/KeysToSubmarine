using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerManager : MonoBehaviour
{
    [SerializeField] private OurEventHandler gameManager;
    private PlayerController[] players = new PlayerController[4];
    private static bool[] players_assigned_controllers = new bool[4] { false, false, false, false };
    private static List<InputDevice> players_devices = new List<InputDevice>();
    private static List<int> players_nums = new List<int>();

    private int players_in_game = 3;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(players_devices.Count);
        players_in_game = gameManager.getNumOfPlayers();
        setColors();
        if (players_devices.Count == 0)
        {
            default_settings();
        }
        else
        {
            not_default_settings();
        }
    }
    private void setColors()
    {
        string[] colors = new string[4] { "Pink", "Blue", "Orange", "Yellow" };
        int i = 0;
        foreach (PlayerController player in GetComponentsInChildren<PlayerController>())
        {
            players[i] = player;
            player.setColor(colors[i]);
            if (players_in_game <= i)
            {
                player.gameObject.SetActive(false);
            }
            i += 1;
        }

    }
    private void default_settings()
    {

        int i = 0;
        InputDevice[] inp = new InputDevice[1];
        inp[0] = Keyboard.current;
        foreach (PlayerController player in GetComponentsInChildren<PlayerController>())
        {
            players[i] = player;
            if (players_in_game <= i)
            {
                player.gameObject.SetActive(false);
            }
            else
            {
                PlayerInput inputya = player.GetComponent<PlayerInput>();
                inputya.SwitchCurrentControlScheme("keyboard" + (i + 1).ToString(), inp);
            }
            i += 1;
        }
    }

    private void not_default_settings()
    {
        for (int i = 0; i < 3; i++)
        {
            SetPlayer(players_devices[i], players_nums[i]);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayer(InputDevice inp, int num)
    {
        InputDevice[] inp2 = new InputDevice[1];
        inp2[0] = inp;
        for (int i = 0; i < 3; i++) 
        {
            if (players_assigned_controllers[i])
            {
                PlayerInput inputya = players[i].GetComponent<PlayerInput>();
                Debug.Log("Player " + i);
                Debug.Log(inp);
                if (inp == Keyboard.current)
                {
                    

                    inputya.SwitchCurrentControlScheme("keyboard" + (i + 1).ToString(), inp);
                }
                else
                {
                    inputya.SwitchCurrentControlScheme("Gamepad", inp);
                }

            }
        }
    }

    public void AddToLists(InputDevice inp, int num)
    {
        for (int i = 0; i < 3; i++)
        {
            if (!players_assigned_controllers[i])
            {
                players_devices.Add(inp);
                players_nums.Add(num);
                players_assigned_controllers[i] = true;
            }
        }
    }
    
    public void RemovePlayer(InputDevice inp, int num)
    {
        for (int i = 0; i < 3; i++)
        {
            if (players_devices[i] == inp && players_nums[i] == num)
            {
                players_assigned_controllers[i] = false;
                players_devices.RemoveAt(i);
                players_nums.RemoveAt(i);
                return;
            }
        }
    }
    //public void AddPlayer()
    //{
    //    if (players_in_game < 4)
    //    {
    //        players[players_in_game].gameObject.SetActive(true);
    //        players_in_game += 1;
    //    }
    //}
    //public void RemovePlayer()
    //{
    //    if (players_in_game > 2)
    //    {
    //        players[players_in_game].gameObject.SetActive(false);
    //        players_in_game -= 1;
    //    }
    //}
    public void UpdatePlayers(int numPlayers)
    {
        players_in_game = numPlayers;

        foreach (PlayerController player in players)
        {
            player.gameObject.SetActive(true);
            
            //InputSystem.SetDeviceUsage(InputDevice)
        }

    }
    //    public bool UpdatePlayerControls(int player_num, List<KeyCode> new_controls)
    //    {
    //        if (new_controls.Count != 5 || player_num < 0 || player_num > 3)
    //        {
    //            return false;
    //        }
    //        foreach (PlayerController player in players)
    //        {
    //            foreach(KeyCode key in player.getListControls())
    //            {
    //                if (new_controls.Contains(key))
    //                {
    //                    return false;
    //                }
    //            }
    //        }
    //        players[player_num].updateListControls(new_controls);
    //        return true;
    //    }

    //    public List<List<KeyCode>> GetPlayersControls()
    //    {
    //        List<List<KeyCode>> playersControls= new List<List<KeyCode>>(4);
    //        foreach (PlayerController player in players)
    //        {
    //            playersControls.Add(player.getListControls());
    //        }
    //        return playersControls;
    //    }
}
