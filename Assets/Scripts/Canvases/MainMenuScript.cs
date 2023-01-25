using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using System;
using UnityEngine.Events;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private TMP_Text num_players_text;
    [SerializeField] private OurEventHandler gameManager;
    [SerializeField] private GameObject MainMenu;
    private List<Button> buttons = new List<Button>();
    // [SerializeField] private AudioSource menuMusic;
    private int num_players;
    //private int index = 0;
    // Start is called before the first frame update

    // public static event Action TutorialEnded;
    public static UnityEvent TutorialEnded = new();

    void Start()
    {
        num_players = gameManager.getNumOfPlayers();
        if (num_players_text != null)
        {
            num_players_text.text = num_players.ToString();
        }
        // menuMusic.Play();
    }
    //private void ChooseButton()
    //{
    //    //buttons[index].onClick = Button.ButtonClickedEvent(true);
    //}
    //private void OnMove(InputValue value)
    //{
    //    Vector2 movement_tmp = value.Get<Vector2>();
    //    //Debug.Log(movement_tmp);
    //    //if (movement_tmp != Vector2.zero)
    //    //{
    //    //    if (!move_pressed_x)
    //    //    {
    //    //        if (Mathf.Abs(movement_tmp.x) == 1)
    //    //        {
    //    //            movement.x = movement_tmp.x;
    //    //            move_pressed_x = true;
    //    //        }
    //    //        else
    //    //        {
    //    //            move_pressed_x = false;
    //    //        }
    //    //    }
    //    //    else
    //    //    {
    //    //        move_pressed_x = false;
    //    //    }
    //    //    if (!move_pressed_y)
    //    //    {
    //    //        if (Mathf.Abs(movement_tmp.y) == 1)
    //    //        {
    //    //            movement.y = -movement_tmp.y;
    //    //            move_pressed_y = true;

    //    //        }
    //    //        else
    //    //        {
    //    //            move_pressed_y = false;

    //    //        }
    //    //    }
    //    //    else
    //    //    {
    //    //        move_pressed_y = false;
    //    //    }

    //    //}
    //    //else
    //    //{
    //    //    move_pressed_x = false;
    //    //    move_pressed_y = false;
    //    //}
    //}
    //private void OnChoose(InputValue value)
    //{
    //    //if (value.isPressed)
    //    //{
    //    //    ChooseButton();
    //    //}
    //}
    //private void OnBackButton(InputValue value)
    //{
    //    //if (value.isPressed)
    //    //{
    //    //    SceneManager.LoadScene("StartGame");
    //    //}
    //}
    public void StartGame()
    {
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

    public void ToLevelSelect()
    {
        SceneManager.LoadScene("ChooseLevelsScreen");
    }

    public void ToFirstLevel()
    {
        SceneManager.LoadScene("Machines");
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
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Alpha2))
            destroyMusic();

#endif
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
    
    public void destroyMusic()
    {
        TutorialEnded?.Invoke();
    }


}
