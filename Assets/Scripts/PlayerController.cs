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

    [SerializeField] private bool controller_set1 = false;
    [SerializeField] private bool controller_set2 = false;

    [SerializeField] private Animator animator;

    Player1Controls controls_1;
    Player2Controls controls;
    // In charge of speed & stuff
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private TMP_Text debug_Text;
    private Vector2 movement;
    [SerializeField] private float radioActivity = 0f;
    private bool[] levelsOfRadioActivity;
    private bool looking_right = true;
    private bool idle = true;
    // Start is called before the first frame update
    void Awake()
    {
        controls = new Player2Controls();
        controls_1 = new Player1Controls();
        if (controller_set1)
        {
            EnableController1();
        }
        else if (controller_set2)
        {
            EnableController2();
        }
        levelsOfRadioActivity = new bool[] {false,false,false};
    }

    public void EnableController1()
    {
        //controls.Gameplay.Action.performed += ctx => Simple();
        player_action_button_new = controls_1.Gameplay.Action;
        controls_1.Gameplay.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls_1.Gameplay.Move.canceled += ctx => movement = Vector2.zero;
        //Debug.Log(controls.Gameplay.Move.GetType());
        Debug.Log("Player1");
    }
    public void EnableController2()
    {
        //controls.Gameplay.Action.performed += ctx => Simple();
        player_action_button_new = controls.Gameplay.Action;
        controls.Gameplay.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => movement = Vector2.zero;
        //Debug.Log(controls.Gameplay.Move.GetType());
        Debug.Log("Player2");

    }
    //void Simple()
    //{
    //    //Debug.Log("HEY");
    //}

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
        if (!(controller_set1 || controller_set2))
        {
            movement_keyboard();
        }

    }

    private void movement_keyboard()
    {
        if (Input.GetKey(right_button))
        {

            //animator.SetTrigger("Run_Right");
            movement.x = 1;
            //if (!looking_right)
            //{
            //    looking_right = true;
            //    transform.rotation = new Quaternion(transform.rotation.x, 90, transform.rotation.z, transform.rotation.w);
            //}
        }
        else if (Input.GetKey(left_button))
        {
            movement.x = -1;
            //animator.SetTrigger("Run_Right");
            //if (looking_right)
            //{
            //    looking_right = false;
            //    transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
            //}
        }
        else
        {
            movement.x = 0;
        }
        if (Input.GetKey(up_button))
        {
            //animator.SetTrigger("Run_Up");
            movement.y = 1;
        }
        else if (Input.GetKey(down_button))
        {
            //animator.SetTrigger("Run_Down");
            movement.y = -1;
        }
        else
        {
            movement.y = 0;
        }
        AnimationIdle();
    }

    private void AnimationDecideState()
    {
        float x1 = rb.velocity.x;
        float y1 = rb.velocity.y;
        if (Mathf.Abs(x1) > Mathf.Abs(y1))
        {
            if (x1 > 0)
            {
                animator.SetTrigger("Run_Right");
                if (!looking_right)
                {
                    looking_right = true;
                    transform.rotation = new Quaternion(transform.rotation.x, 90, transform.rotation.z, transform.rotation.w);
                }
            }
            else
            {
                animator.SetTrigger("Run_Right");
                if (looking_right)
                {
                    looking_right = false;
                    transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
                }
            }
        }
        else
        {
            if (y1 > 0)
            {
                animator.SetTrigger("Run_Up");
            }
            else
            {
                animator.SetTrigger("Run_Down");
            }
        }
    }
    private void FixedUpdate()
    {
        rb.AddForce(movement * moveSpeed, ForceMode2D.Impulse);
        if (!idle)
        {
            AnimationDecideState();
        }

    }

    public KeyCode GetPlayerActionButton()
    {
        return player_action_button;
    }
    
    public bool is_Controller()
    {
        return controller_set1 || controller_set2;
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

    public void AnimationWork(Vector3 otherlocalLocation)
    {
        //PlayerDirectionAnimation(otherlocalLocation);
        animator.SetTrigger("work");
    }
    private void PlayerDirectionAnimation(Vector3 otherlocalLocation)
    {
        float x1 = transform.localPosition.x;
        float y1 = transform.localPosition.y;
        float x2 = otherlocalLocation.x;
        float y2 = otherlocalLocation.y;
        float deg = Mathf.Atan2((y2 - y1), (x2 - x1));
        if (deg > -2.25f && deg <= -0.75f)
        {
            animator.SetTrigger("Run_Down");
        }
        else if(deg > -0.75 && deg <= 0.75f)
        {
            animator.SetTrigger("Run_Right");
            if (!looking_right)
            {
                looking_right = true;
                transform.rotation = new Quaternion(transform.rotation.x, 90, transform.rotation.z, transform.rotation.w);
            }
            
        }
        else if(deg > 0.75f && deg <= 2.25f)
        {
            animator.SetTrigger("Run_Up");
        }
        else
        {
            animator.SetTrigger("Run_Right");
            if (looking_right)
            {
                looking_right = false;
                transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
            }
        }
        //Debug.Log(deg);
    }
    public void AnimationPush(Vector3 otherlocalLocation)
    {
        //PlayerDirectionAnimation(otherlocalLocation);
        if (!idle)
        {
            animator.SetTrigger("push");
        }
    }

    public void AnimationIdle()
    {
        if (Mathf.Abs(rb.velocity.x) <= 0.01f && Mathf.Abs(rb.velocity.y) <= 0.01f && !Input.GetKeyDown(player_action_button))
        {
            animator.SetTrigger("idle");
            idle = true;
        }
        else
        {
            idle = false;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Holdable")
        {
            if(Input.GetKey(player_action_button))
            {
               other.transform.position = gameObject.transform.position + new Vector3(0f,-0.5f,0f); 
            }
        }

        if(other.tag == "RadioActive")
        {
            radioActivity += 4 * other.gameObject.GetComponent<TrashCan>().getRadioActivityLevel();
            checkRadioActivity();
        }
    }

    void checkRadioActivity()
    {
        if(radioActivity >= 10000 && !levelsOfRadioActivity[2])
        {
            levelsOfRadioActivity[2] = true;
            Destroy(gameObject);
        }
        else if (radioActivity >= 6666 && !levelsOfRadioActivity[1])
        {
            moveSpeed = moveSpeed / 2;
            levelsOfRadioActivity[1] = true;
        }
        else if (radioActivity >= 3333 && !levelsOfRadioActivity[0])
        {
            moveSpeed = moveSpeed / 2;
            levelsOfRadioActivity[0] = true;
        }
    }
}
