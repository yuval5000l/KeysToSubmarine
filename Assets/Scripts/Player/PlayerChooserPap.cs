using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerChooserPap : MonoBehaviour
{

    List<Vector3> positions = new List<Vector3>();
    int[] player_loc = new int[] { -1, -1, -1 };
    bool[] player_joined = new bool[] { false, false, false };
    PlayerChooser[] players = new PlayerChooser[3];
    InputDevice[] devices = new InputDevice[3];
    private OurEventHandler GM;
    [SerializeField] private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button.enabled = false;
        button.image.color = new Color(1f, 1f, 1f, 0.2f);
        button.enabled = false;
        GM = FindObjectOfType<OurEventHandler>();
        int i = 0;
        foreach (PlayerChooser playa in GetComponentsInChildren<PlayerChooser>())
        {
            players[i] = playa;
            players[i].setPlayerNum(i);
            positions.Add(playa.gameObject.transform.localPosition);
            i++;
        }

        positions.Add(new Vector3(-4.31f, 0.26f)); // Left
        positions.Add(new Vector3(0f, 1.26f)); // Up
        positions.Add(new Vector3(4.66f, 0.26f)); // Up
        var arr = InputSystem.devices.ToArray();
        int x = arr.Length - 1;
        for (int j = 0; j < 3; j++)
        {
            if (x - j > 0)
            {
                devices[j] = arr[x - j];
            }
            else
            {
                devices[j] = arr[0];
            }
        }

        AssignPlayerNumAndDevice();
        
    }
    private void AssignPlayerNumAndDevice()
    {
        int counter_keyboard = 0;
        InputDevice[] inp = new InputDevice[1];

        for (int i = 0; i < 3; i++)
        {
            inp[0]  = devices[i];
            PlayerInput inputya = players[i].GetComponent<PlayerInput>();
            if (devices[i] == Keyboard.current)
            {
                inputya.SwitchCurrentControlScheme("keyboard" + (counter_keyboard + 1).ToString(), inp);
                counter_keyboard++;
            }
            else
            {
                inputya.SwitchCurrentControlScheme("Gamepad", inp);
            }
            
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public bool move_player(int num, int num_player)
    {
        // Num: 0 left, 1 up, 2 right
        bool occupaied = false;
        for (int i = 0; i < 3; i++)
        {
            if (player_loc[i] == num)
            {
                occupaied = true;
            }
        }
        if (player_loc[num_player] == -1 && !occupaied)
        {
            player_loc[num_player] = num;
            players[num_player].MoveTo(positions[num + 3]);
            return true;
        }
        else if (!player_joined[num_player]) 
        {
            player_loc[num_player] = -1;
            players[num_player].MoveTo(positions[num_player]);
            return false;

        }
        return player_loc[num_player] != -1;
    }

    private bool is_all_joined()
    {
        for (int i = 0; i < 3; i++)
        {
            if (!player_joined[i])
            {
                return false;
            }
        }
        return true;
    }
    public void joined(int num)
    {

        //(player_joined[num]) ? player_joined[num] = true ? player_joined[num] = false;
        if (!player_joined[num])
        {
            // ADD INDICATION JOINED
            player_joined[num] = true;

        }
        else
        {
            // REMOVE INDICATION JOINED
            player_joined[num] = false;
        }
        if (is_all_joined())
        {
            GM.ClearLists();
            button.image.color = new Color(1f, 1f, 1f, 1f);
            button.enabled = true;

            for (int j = 0; j < 3; j++)
            {
                int controller_num = player_loc[j];
                GM.AddPlayer(devices[controller_num], j+1);
            }
        }
        else
        {
            button.image.color = new Color(1f, 1f, 1f, 0.2f);
            button.enabled = false;
        }
        //Debug.Log(player_joined[num]);

    }
}
