using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    // KeyCodes For Movement And Action
    [SerializeField] private KeyCode up_button = KeyCode.W;
    [SerializeField] private KeyCode down_button = KeyCode.S;
    [SerializeField] private KeyCode left_button = KeyCode.A;
    [SerializeField] private KeyCode right_button = KeyCode.D;

    [SerializeField] private KeyCode player_action_button = KeyCode.R;
    [SerializeField] private UnityEngine.InputSystem.InputAction player_action_button_new;

    [SerializeField] private bool controller_set = false;
    Player1Controls controls;

    // In charge of speed & stuff
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private TMP_Text debug_Text;
    private Vector2 movement;
    // Start is called before the first frame update
    void Awake()
    {
        controls = new Player1Controls();
        if (controller_set)
        {
            EnableController();
        }
    }

    public void EnableController()
    {
        //controls.Gameplay.Action.performed += ctx => Simple();
        player_action_button_new = controls.Gameplay.Action;
        controls.Gameplay.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => movement = Vector2.zero;
        Debug.Log(controls.Gameplay.Move.GetType());
    }

    void Simple()
    {
        //Debug.Log("HEY");
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller_set)
        {
            //Vector2 m = new Vector2(movement.x, movement.y) * Time.deltaTime;
            //transform.Translate(m, Space.World);

        }
        else
        {
            movement_keyboard();
        }
        //movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");
    }
    private void movement_controller()
    {

    }
    private void movement_keyboard()
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
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (movement * moveSpeed));
    }

    public KeyCode GetPlayerActionButton()
    {
        return player_action_button;
    }
    
    public bool is_Controller()
    {
        return controller_set;
    }

    public InputAction GetPlayerActionButtonNew()
    {
        return player_action_button_new;
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
