using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerController : MonoBehaviour
{

    // KeyCodes For Movement And Action
    [SerializeField] private KeyCode up_button = KeyCode.W;
    [SerializeField] private KeyCode down_button = KeyCode.S;
    [SerializeField] private KeyCode left_button = KeyCode.A;
    [SerializeField] private KeyCode right_button = KeyCode.D;

    [SerializeField] private KeyCode player_action_button = KeyCode.R;


    // In charge of speed & stuff
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private TMP_Text debug_Text;
    private Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(right_button))
        {
            movement.x = 1;
        }
        else if (Input.GetKey(left_button))
        {
            movement.x = -1;
        }
        else
        {
            movement.x = 0;
        }
        if (Input.GetKey(up_button))
        {
            movement.y = 1;
        }            
        else if (Input.GetKey(down_button))
        {            
            movement.y = -1;
        }
        else
        {
            movement.y = 0;
        }
        //movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (movement * moveSpeed));
    }

    public KeyCode GetPlayerActionButton()
    {
        return player_action_button;
    }
    void PlayerMovement()
    {

    }

    public KeyCode[] GetKeys()
    {
        KeyCode[] arr_keys = { up_button, down_button, left_button, right_button, player_action_button };
        return arr_keys;
    }

    // Switching Keys

    public void setUpKey(KeyCode new_key)
    {
        up_button = new_key;
    }
    public void setDownKey(KeyCode new_key)
    {
        down_button = new_key;
    }
    public void setRightKey(KeyCode new_key)
    {
        right_button = new_key;
    }
    public void setLeftKey(KeyCode new_key)
    {
        left_button = new_key;
    }
    public void setActionKey(KeyCode new_key)
    {
        player_action_button = new_key;
    }
}
