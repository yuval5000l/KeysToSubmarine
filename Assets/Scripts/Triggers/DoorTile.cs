using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTile : MonoBehaviour
{
    [SerializeField] private int numberOfSeconds = 5;
    [SerializeField] private DoorScript door;
    private Animator MaAnimator;    
    private bool check_player_pressed = false;
    private PlayerController player = null;
    // Start is called before the first frame update
    void Start()
    {
        MaAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void player_pressed()
    {

        MaAnimator.SetBool("Hover", false);
        MaAnimator.SetBool("Press", true);
        door.OpenDoor(gameObject, numberOfSeconds);
        check_player_pressed = true;
        player.get_on_tile();
    }

    private void player_unpressed()
    {
        MaAnimator.SetBool("Hover", true);
        MaAnimator.SetBool("Press", false);
        door.StopOpenDoor(gameObject);
        check_player_pressed = false;
        player.get_off_tile();


    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            if (player == null)
            {
                MaAnimator.SetBool("Idle", false);
                MaAnimator.SetBool("Hover", true);
                player = coll.gameObject.GetComponent<PlayerController>();
            }
            else if (coll.gameObject.GetComponent<PlayerController>() == player)
            {
                player_pressed();
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            if (!check_player_pressed)
            {
                MaAnimator.SetBool("Hover", false);
                MaAnimator.SetBool("Idle", true);
                player = null;
            }
            else if (collision.gameObject.GetComponent<PlayerController>() == player)
            {
                player_unpressed();
            }
        }
    }

}
