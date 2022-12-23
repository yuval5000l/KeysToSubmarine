using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerController : MonoBehaviour
{

    // In charge of speed & stuff
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private TMP_Text debug_Text;
    private Vector2 movement;
    [SerializeField] private KeyCode player_action_button = KeyCode.M;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
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
}
