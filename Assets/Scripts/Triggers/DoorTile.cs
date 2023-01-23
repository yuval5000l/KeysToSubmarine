using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTile : MonoBehaviour
{
    [SerializeField] private float numberOfSeconds = 5;
    [SerializeField] private DoorScript door;
    private Animator MaAnimator;    
    //private bool check_player_pressed = false;
    private List<PlayerController> players = new List<PlayerController>();
    private List<bool> players_pressed = new List<bool>();
    [SerializeField] AudioSource steppedOn;
    [SerializeField] AudioSource colliderEnter;
    // Start is called before the first frame update
    void Start()
    {
        MaAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool is_pressed()
    {
        foreach(bool p in players_pressed)
        {
            if (p)
            {
                return true;
            }
        }
        return false;
    }

    private void player_pressed(PlayerController player)
    {
        if (!is_pressed())
        {
            MaAnimator.SetBool("Hover", false);
            MaAnimator.SetBool("Press", true);
            door.OpenDoor(gameObject, numberOfSeconds);
        }
        for (int i = 0; i < players_pressed.Count; i++)
        {
            if (players[i] == player)
            {
                players_pressed[i] = true;
            }
        
        }
        player.get_on_tile();
    }

    private void player_unpressed(PlayerController player, int i)
    {
        players_pressed[i] = false;
        if (!is_pressed())
        {
            MaAnimator.SetBool("Hover", true);
            MaAnimator.SetBool("Press", false);
            door.StopOpenDoor(gameObject);
        }
        player.get_off_tile();


    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            colliderEnter.Play();
            PlayerController player = coll.gameObject.GetComponent<PlayerController>();
            if (!players.Contains(player))
            {
                if (players.Count == 0)
                {
                    MaAnimator.SetBool("Idle", false);
                    MaAnimator.SetBool("Hover", true);
                }
                players.Add(player);
                players_pressed.Add(false);
            }
            else
            {
                player_pressed(player);
                steppedOn.Play();
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            int counter = 0;
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            for (int i = 0; i < players.Count; i++)
            {
                if (player = players[i])
                {
                    counter = i;
                }
            }
            Debug.Log(players_pressed.Count);
            Debug.Log(players.Count);
            if (players_pressed[counter])
            {
                player_unpressed(player, counter);
            }
            else
            {
                players.RemoveAt(counter);
                players_pressed.RemoveAt(counter);
                if (players.Count == 0)
                {
                    MaAnimator.SetBool("Hover", false);
                    MaAnimator.SetBool("Idle", true);
                }
            }
        }
    }

}
