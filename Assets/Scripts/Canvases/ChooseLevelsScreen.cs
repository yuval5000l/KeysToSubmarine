using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ChooseLevelsScreen : MonoBehaviour
{
    [SerializeField] private OurEventHandler GM;
    // Start is called before the first frame update
    private bool move_pressed_x = false;
    private bool move_pressed_y = false;

    private int time_to_press = 1;
    private float mini_counter_x = 0f;
    private float mini_counter_y = 0f;

    private float counter_move_pressed_x = 0f;
    private float counter_move_pressed_y = 0f;
    private Vector2 hold_movement = Vector2.zero;
    private Vector2 movement = Vector2.zero;
    private int index = 0;
    [SerializeField] int num_levels_in_game = 12;
    private int total_buttons = 22;
    private Transform[] levels = new Transform[30];
    void Start()
    {
        bool check = false;
        int i = 0;
        foreach (Transform button in GetComponentsInChildren<Transform>())
        {
            if (!check)
            {
                check = true;
            }
            else
            {
                levels[i] = button;
                i++;
            }
            
        }
        for (int j = num_levels_in_game + 5; j < total_buttons; j++)
        {
            levels[j].gameObject.SetActive(false);   
        }
        update_button(true);
    }
    private void update_index(int num)
    {
        update_button(false);
        index = (index + num) % (num_levels_in_game + 4);
        if (index < 0)
        {
            index =( num_levels_in_game + 4 )+ index;
        }
        update_button(true);
    }

    private void update_button(bool choosed)
    {
        if (choosed)
        {
            levels[index].GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else
        {
            levels[index].GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    // Update is called once per frame
    void Update()
    {
        movement_x_handler();
        movement_y_handler();
    }

    private void movement_x_handler()
    {
        if (Mathf.Abs(movement.x) == 1)
        {
            if (counter_move_pressed_x >= time_to_press)
            {
                movement.x = hold_movement.x;
                mini_counter_x += Time.deltaTime;
                if (mini_counter_x >= ((float)time_to_press) / 6)
                {
                    mini_counter_x = 0;
                    update_index((int) movement.x);
                }
            }
            else
            {
                update_index((int)movement.x);
                hold_movement.x = movement.x;
                movement.x = 0;
            }
        }
        if (move_pressed_x)
        {
            counter_move_pressed_x += Time.deltaTime;
            if (counter_move_pressed_x >= time_to_press)
            {
                movement.x = 1;
            }
        }
        else
        {
            counter_move_pressed_x = 0f;
            mini_counter_x = 0f;
        }
    }

    private void movement_y_handler()
    {
        if (Mathf.Abs(movement.y) == 1)
        {
            if (counter_move_pressed_y >= time_to_press)
            {
                movement.y = hold_movement.y;
                mini_counter_y += Time.deltaTime;
                if (mini_counter_y >= ((float)time_to_press) / 6)
                {
                    mini_counter_y = 0;
                    update_index((int)movement.y * 6);
                }
            }
            else
            {
                update_index((int)movement.y * 6);
                hold_movement.y = movement.y;
                movement.y = 0;
            }
        }
        if (move_pressed_y)
        {
            counter_move_pressed_y += Time.deltaTime;
            if (counter_move_pressed_y >= time_to_press)
            {
                movement.y = hold_movement.y;
            }
        }
        else
        {
            counter_move_pressed_y = 0f;
            mini_counter_y = 0f;
        }
    }

    private void ChooseLevel()
    {
        if (index <= 3)
        {
            GM.setTut(true);
        }
        else
        {
            GM.setTut(false);
            index = index - 3;
        }
        //Debug.Log(index);
        GM.Setlevel(index);
        SceneManager.LoadScene("Janitor select");

    }

    private void OnMove(InputValue value)
    {
        Vector2 movement_tmp = value.Get<Vector2>();
        //Debug.Log(movement_tmp);
        if (movement_tmp != Vector2.zero)
        {
            if (!move_pressed_x)
            {
                if (Mathf.Abs(movement_tmp.x) == 1)
                {
                    movement.x = movement_tmp.x;
                    move_pressed_x = true;
                }
                else
                {
                    move_pressed_x = false;
                }
            }
            else
            {
                move_pressed_x = false;
            }
            if (!move_pressed_y)
            { 
                if (Mathf.Abs(movement_tmp.y) == 1)
                {
                    movement.y = -movement_tmp.y;
                    move_pressed_y = true;

                }
                else
                {
                    move_pressed_y = false;

                }
            }
            else
            {
                move_pressed_y = false;
            }

        }
        else
        {
            move_pressed_x = false;
            move_pressed_y = false;
        }
    }
    private void OnChoose(InputValue value)
    {
        if (value.isPressed)
        {
            ChooseLevel();
        }
    }
    private void OnBackButton(InputValue value)
    {
        if (value.isPressed)
        {
            SceneManager.LoadScene("StartGame");
        }
    }
}
