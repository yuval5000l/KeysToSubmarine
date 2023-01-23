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
    [SerializeField] private Animator[] players_animators = new Animator[3]; // Pink, Blue, Orange
    [SerializeField] private Button button;
    [SerializeField] private GameObject squareIndicator;


    [Header("ManyManyLists")]
    [SerializeField] List<GameObject> PinkStuff = new List<GameObject>(); // 
    [SerializeField] List<GameObject> BlueStuff = new List<GameObject>();
    [SerializeField] List<GameObject> OrangeStuff = new List<GameObject>();
    List<List<GameObject>> ChooseButtons = new List<List<GameObject>>();
    [SerializeField] List<GameObject> ChooseButtonsPink = new List<GameObject>(); // Space, Q, B, A
    [SerializeField] List<GameObject> ChooseButtonsBlue = new List<GameObject>(); // Space, Q, B, A
    [SerializeField] List<GameObject> ChooseButtonsOrange = new List<GameObject>(); // Space, Q, B, A
    [SerializeField] List<GameObject> ChooseButtonsTemp = new List<GameObject>(); // Space, Q, B, A

    [SerializeField] List<InputDevice> players_devices = new List<InputDevice>();
    List<int> players_devices_num = new List<int>();
    List<int> player_num = new List<int>();
    private bool mainMenuActive = true;
    private int num_players = 0;
    private int num_level = 0;

    private PlayerInput players_input;


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
        num_level = gameManager.Getlevel();
        ChooseButtons.Add(ChooseButtonsPink);
        ChooseButtons.Add(ChooseButtonsBlue);
        ChooseButtons.Add(ChooseButtonsOrange);
        ChooseButtons.Add(ChooseButtonsTemp);

        AddInitialStuff(PinkStuff);
        AddInitialStuff(BlueStuff);
        AddInitialStuff(OrangeStuff);


    }
    private void AddInitialStuff(List<GameObject> stuff)
    {
        foreach (Transform obj in stuff[0].GetComponentsInChildren<Transform>())
        {
            stuff.Add(obj.gameObject);
        }
        stuff.RemoveAt(0);
        stuff.RemoveAt(0);
        foreach (GameObject obj in stuff)
        {
            obj.SetActive(false);
        }
        stuff[0].SetActive(true);
    }
    private void AddPlayerStuff(int device_num, int player_loc)
    {
        if (player_loc == 0)
        {
            AddPlayerStuffHelper(PinkStuff, device_num, true);
        }
        else if (player_loc == 1)
        {
            AddPlayerStuffHelper(BlueStuff, device_num, true);
        }
        else if (player_loc == 2)
        {
            AddPlayerStuffHelper(OrangeStuff, device_num, true);
        }
    }
    private void RemovePlayerStuff(int device_num, int player_loc)
    {
        if (player_loc == 0)
        {
            AddPlayerStuffHelper(PinkStuff, device_num, false);
        }
        else if (player_loc == 1)
        {
            AddPlayerStuffHelper(BlueStuff, device_num, false);
        }
        else if (player_loc == 2)
        {
            AddPlayerStuffHelper(OrangeStuff, device_num, false);
        }
    }

    private void AddPlayerStuffHelper(List<GameObject> stuff,  int device_num, bool Add)
    {
        
        stuff[0].SetActive(!Add);
        stuff[1].SetActive(Add);
        stuff[device_num + 2].SetActive(Add);
        stuff[device_num + 6].SetActive(Add);
        stuff[stuff.Count -1].SetActive(Add);

    }
    private int AddPlayerAnimation()
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
                return cur_num;
            }
        }
        return cur_num;
    }
    private void RemovePlayerAnimation(int counter)
    {
        Color maColor = new Color(1f, 1f, 1f, 0.5f);
        players_animators[player_num[counter]].SetBool("Choosen", false);
        player_num.RemoveAt(counter);

    }
    private void AddPlayer(InputDevice inp, int num, int device_num)
    {
        //gameManager
        if (players_devices.Count < gameManager.getNumOfPlayers())
        {
            players_devices.Add(inp);
            players_devices_num.Add(num);
            int player_loc =  AddPlayerAnimation();
            AddPlayerStuff(device_num, player_loc);
            foreach(GameObject obj in ChooseButtons[player_loc])
            {
                obj.SetActive(false);
            }
        }
    }
    private void RemovePlayer(InputDevice inp, int num, int device_num)
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
            RemovePlayerStuff(device_num, player_num[counter]);
            for (int i = 0; i < ChooseButtons[player_num[counter]].Count; i++)
            {

                if (ChooseButtons[3][i].activeSelf)
                {
                    ChooseButtons[player_num[counter]][i].SetActive(true);
                }
                
            }
            RemovePlayerAnimation(counter);
            
        }
    }
    private void UpdateSquare()
    {
        squareIndicator.GetComponent<SpriteRenderer>().color = Color.green;

        for (int i = 0; i < 3; i++)
        {
            if (!player_num.Contains(i))
            {
                squareIndicator.transform.localPosition = players_animators[i].gameObject.transform.localPosition + Vector3.down *1.5f;
                return;
            }
        }
        squareIndicator.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);

    }
    void OnKeyboard1(InputValue inp) // Q
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
                AddPlayer(Keyboard.current, 2, 1);
                foreach(List<GameObject> lst in ChooseButtons)
                {
                    lst[1].SetActive(false);
                }


            }
            else
            {
                RemovePlayer(Keyboard.current, 2, 1);
                foreach (List<GameObject> lst in ChooseButtons)
                {
                    if (lst[3].activeSelf)
                    {
                        lst[1].SetActive(true);
                    }
                }

            }
        }
    }
    void OnKeyboard2(InputValue inp) // Space
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
                AddPlayer(Keyboard.current, 1, 0);
                foreach (List<GameObject> lst in ChooseButtons)
                {
                        lst[0].SetActive(false);
                }
            }
            else
            {
                RemovePlayer(Keyboard.current, 1, 0);
                foreach (List<GameObject> lst in ChooseButtons)
                {                    
                    if (lst[3].activeSelf)
                    {
                        lst[0].SetActive(true);
                    }
                }

            }
        }
    }
    void OnKeyboard3(InputValue inp) // B
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
                AddPlayer(Keyboard.current, 3, 2);
                foreach (List<GameObject> lst in ChooseButtons)
                {
                  lst[2].SetActive(false);
                }

            }
            else
            {
                RemovePlayer(Keyboard.current, 3, 2);
                foreach (List<GameObject> lst in ChooseButtons)
                {
                    if (lst[3].activeSelf)
                    {
                        lst[2].SetActive(true);
                    }
                }
            }
        }
    }

    void OnGamePad1(InputValue inp) // A
    {
        if (!inp.isPressed)
        {
            
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
                    int player_loc = AddPlayerAnimation();
                    AddPlayerStuff(3, player_loc);
                    foreach(GameObject obj in ChooseButtons[player_loc])
                    {
                        obj.SetActive(false);
                    }
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
                    RemovePlayerStuff(3, player_num[counter]);
                    for (int i = 0; i < ChooseButtons[player_num[counter]].Count; i++)
                    {
                        if (ChooseButtons[3][i].activeSelf)
                        {
                            ChooseButtons[player_num[counter]][i].SetActive(true);
                        }
                    }
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
        UpdateSquare();

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
            gameManager.Nextlevel(- 1);
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

    public void ToLevelSelect()
    {
        SceneManager.LoadScene("ChooseLevelsScreen");
    }
}
