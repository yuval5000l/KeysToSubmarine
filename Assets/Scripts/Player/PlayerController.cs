using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    // Constants
    private float radioActiveDeathLim = 10000;

    // KeyCodes For Movement And Action
    private string color;


    
    // Animation
    [SerializeField] private Animator animator;
    private float RunAnimationThreshold = 0.1f;
    private float RunAnimationJoystickThreshold = 0.3f;
    private bool looking_right = false;
    private bool idle = true;

    // In charge of speed & stuff
    [Header("Player Speed")]

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 movement;

    //[SerializeField] private TMP_Text debug_Text;
    [Header("Player RadioActive")]
    [SerializeField] private float radioActivity = 0f;
    // private bool[] levelsOfRadioActivity;
    private bool radio_active_state = false;
    private SpriteRenderer sprite;
    private SpriteRenderer shadow_sprite;
    private int behind_counter = 0;
    private Material default_material;
    private Material radio_active_material;
    [SerializeField] private Image radioActiveIndicator;



    [SerializeField] private Camera mCamera;
    List<Rigidbody2D> playersITouch = new List<Rigidbody2D>();
    private int frames_for_one_time_press = 1;
    private int frame_counter_for_one_time_press = 0;
    private bool action_pressed;
    private bool one_time_action_pressed;
    [SerializeField] private AudioSource breathing;
    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        default_material = sprite.material;
        radio_active_material = Resources.Load<Material>("Radioactive_player");
        // levelsOfRadioActivity = new bool[] {false,false,false};
        radioActiveIndicator = Instantiate(Resources.Load<Image>("Image")) as Image;
        radioActiveIndicator.transform.position = Vector3.zero;
        radioActiveIndicator.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        radioActiveIndicator.rectTransform.position = new Vector3(2.7f,1f,0f);
        radioActiveIndicator.rectTransform.localScale = new Vector3(0.3f,0.3f,0.3f);


        //Debug.Log(inputya.devices);
        //inputya.SwitchCurrentControlScheme("keyboard2");
    }
    private void Start()
    {
        shadow_sprite = GetComponentsInChildren<SpriteRenderer>()[1];

    }
    public void OnMove(InputValue value)
    {
        Vector2 movement_tmp = value.Get<Vector2>();
        //movement = movement_tmp;

        if (movement_tmp.x > RunAnimationJoystickThreshold)
        {
            movement.x = 1;
        }
        else if (movement_tmp.x < -RunAnimationJoystickThreshold)
        {
            movement.x = -1;
        }
        else
        {
            movement.x = 0;
        }
        if (movement_tmp.y > RunAnimationJoystickThreshold)
        {
            movement.y = 1;
        }
        else if (movement_tmp.y < -RunAnimationJoystickThreshold)
        {
            movement.y = -1;
        }
        else
        {
            movement.y = 0;
        }

        AnimationIdle();

    }

    private void OnActionB(InputValue value)
    {
        if (value.isPressed)
        {
            //Debug.Log("Pressed");
            action_pressed = true;
            one_time_action_pressed = true;
        }
        else
        {
            //Debug.Log("UnPressed");
            action_pressed = false;
        }
    }
    public bool playerPressed()
    {
        return action_pressed;
    }

    public bool playerPressedOneTime()
    {
        return one_time_action_pressed;
    }


    public void get_on_tile()
    {
        rb.AddForce(Vector2.up* 1f, ForceMode2D.Impulse);
    }
    public void get_off_tile()
    {
        rb.AddForce(Vector2.down * 1f, ForceMode2D.Impulse);
    }
    // Update is called once per frame
    void Update()
    {

        if (one_time_action_pressed)
        {
            if (frame_counter_for_one_time_press >= frames_for_one_time_press)
            {
                frame_counter_for_one_time_press = 0;
                one_time_action_pressed = false;
            }
            else
            {
                frame_counter_for_one_time_press++;
            }

        }

        Vector2 pos = gameObject.transform.position;
        Vector2 viewportPoint = Camera.main.WorldToViewportPoint(pos);
        radioActiveIndicator.rectTransform.anchorMin = viewportPoint;
        radioActiveIndicator.rectTransform.anchorMax = viewportPoint;
        AnimationIdle();
        
        if (radio_active_state)
        {
            radioActivity += 10 * 1f * Time.deltaTime * 60f;
            radioActiveIndicator.fillAmount += 230f * Time.deltaTime * 0.0003f;
            checkRadioActivity();
        }

        //if (action_pressed) ButtHead
        //{
        //    foreach (Rigidbody2D rb in playersITouch)
        //    {
        //        rb.AddForce(movement * moveSpeed * 20f, ForceMode2D.Impulse);
        //    }
        //}

    }



    private void AnimationDecideState()
    {
        float x1 = rb.velocity.x;
        float y1 = rb.velocity.y;
        if (x1 == 0 && y1 == 0)
        {
            return;
        }
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


    public void AnimationWork(Vector3 otherlocalLocation)
    {
        //PlayerDirectionAnimation(otherlocalLocation);
        animator.SetBool("work", true);
    }
    public void StopAnimationWork(Vector3 otherlocalLocation)
    {
        //PlayerDirectionAnimation(otherlocalLocation);
        animator.SetBool("work", false);
    }
    public void AnimationPush(Vector3 otherlocalLocation)
    {
        //PlayerDirectionAnimation(otherlocalLocation);
        animator.SetBool("push", true);
    }
    public void StopAnimationPush(Vector3 otherlocalLocation)
    {
        //PlayerDirectionAnimation(otherlocalLocation);
        animator.SetBool("push", false);
    }


    public void AnimationIdle()
    {
        if (Mathf.Abs(rb.velocity.x) <= RunAnimationThreshold && Mathf.Abs(rb.velocity.y) <= RunAnimationThreshold && !animator.GetBool("work") && !animator.GetBool("push"))
        {
            animator.SetTrigger("idle");
            idle = true;

        }
        else
        {
            idle = false;
        }
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
        else if (deg > -0.75 && deg <= 0.75f)
        {
            animator.SetTrigger("Run_Right");
            if (!looking_right)
            {
                looking_right = true;
                transform.rotation = new Quaternion(transform.rotation.x, 90, transform.rotation.z, transform.rotation.w);
            }

        }
        else if (deg > 0.75f && deg <= 2.25f)
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

    }

    public void ForceStop()
    {
        rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Border")
        {
            sprite.sortingOrder = 1;
            //shadow_sprite.enabled = false;
            shadow_sprite.sortingOrder = 0;

            behind_counter += 1;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Holdable")
        {
            if(action_pressed)
            {
               other.transform.position = gameObject.transform.position + new Vector3(0f,-0.5f,0f); 
            }
        }

        if(other.tag == "RadioActive")
        {
            radioActivity += 4 * other.gameObject.GetComponent<TrashCan>().getRadioActivityLevel();
            checkRadioActivity();
            sprite.material = radio_active_material;
            radio_active_state = true;
        }

        if(other.tag == "ActiveShower")
        {
            if (radioActivity > 0)
            {
                radioActivity -= 50 * Time.deltaTime * 60;
                radioActiveIndicator.fillAmount -= 12.5f * 240f * Time.deltaTime * 0.0003f;              
            if (radioActivity < 0)
                {
                    radioActivity = 0;
                }
            }
            checkRadioActivity();
            sprite.material = default_material;
            radio_active_state = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Border")
        {
            behind_counter -= 1;
            if (behind_counter == 0)
            {
                sprite.sortingOrder = 4;
                //shadow_sprite.enabled = true;

                shadow_sprite.sortingOrder = 3;
            }
        }
    }
    void checkRadioActivity()
    {
        if(radioActivity >= radioActiveDeathLim)
        {
            Destroy(gameObject);
            Destroy(radioActiveIndicator);
        }
        // else if (radioActivity >= 6666 && !levelsOfRadioActivity[1])
        // {
        //     moveSpeed = moveSpeed / 2;
        //     levelsOfRadioActivity[1] = true;
        //     radioActiveIndicator.fillAmount = 0;
        // }
        // else if (radioActivity >= 3333 && !levelsOfRadioActivity[0])
        // {
        //     moveSpeed = moveSpeed / 2;
        //     levelsOfRadioActivity[0] = true;
        //     radioActiveIndicator.fillAmount = 0;
        // }
        // else if(3333 <= radioActivity && radioActivity <6666 && levelsOfRadioActivity[1])
        // {
        //     moveSpeed = moveSpeed * 2;
        //     levelsOfRadioActivity[1] = false;
        //     radioActiveIndicator.fillAmount = 0;
        // }
        // else if(radioActivity < 3333 && levelsOfRadioActivity[0])
        // {
        //     moveSpeed = moveSpeed * 2;
        //     levelsOfRadioActivity[0] = false;
        //     radioActiveIndicator.fillAmount = 0;
        // }
    }
    public void setColor(string name)
    {
        color = name;
    }
    public string getColor()
    {
        return color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (!playersITouch.Contains(rb))
            {
                playersITouch.Add(collision.gameObject.GetComponent<Rigidbody2D>());
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playersITouch.Contains(rb))
            {
                playersITouch.Remove(collision.gameObject.GetComponent<Rigidbody2D>());
            }
        }
    }



    //private void movement_keyboard()
    //{
    //    if (Input.GetKey(right_button))
    //    {
    //        movement.x = 1;
    //    }
    //    else if (Input.GetKey(left_button))
    //    {
    //        movement.x = -1;
    //    }
    //    else
    //    {
    //        movement.x = 0;
    //    }
    //    if (Input.GetKey(up_button))
    //    {
    //        movement.y = 1;
    //    }
    //    else if (Input.GetKey(down_button))
    //    {
    //        movement.y = -1;
    //    }
    //    else
    //    {
    //        movement.y = 0;
    //    }
    //    AnimationIdle();
    //}
    //public KeyCode GetPlayerActionButton()
    //{
    //    return player_action_button;
    //}

    //public bool is_Controller()
    //{
    //    return controller_set3 || controller_set4;
    //}

    //public InputAction GetPlayerActionButtonNew()
    //{
    //    return player_action_button_new;
    //}

    //public KeyCode[] GetKeys()
    //{
    //    KeyCode[] arr_keys = { up_button, down_button, left_button, right_button, player_action_button };
    //    return arr_keys;
    //}

    //// Switching Keys

    //public List<KeyCode> getListControls()
    //{
    //    List<KeyCode> a = new List<KeyCode>();
    //    a.Add(up_button);
    //    a.Add(down_button);
    //    a.Add(left_button);
    //    a.Add(right_button);
    //    a.Add(player_action_button);
    //    return a;
    //}

    //public void updateListControls(List<KeyCode> new_controls)
    //{
    //    setUpKey(new_controls[0]);
    //    setDownKey(new_controls[1]);
    //    setLeftKey(new_controls[2]);
    //    setRightKey(new_controls[3]);
    //    setActionKey(new_controls[4]);
    //}

    //public void setUpKey(KeyCode new_key)
    //{
    //    up_button = new_key;
    //}
    //public void setDownKey(KeyCode new_key)
    //{
    //    down_button = new_key;
    //}
    //public void setRightKey(KeyCode new_key)
    //{
    //    right_button = new_key;
    //}
    //public void setLeftKey(KeyCode new_key)
    //{
    //    left_button = new_key;
    //}
    //public void setActionKey(KeyCode new_key)
    //{
    //    player_action_button = new_key;
    //}
}
