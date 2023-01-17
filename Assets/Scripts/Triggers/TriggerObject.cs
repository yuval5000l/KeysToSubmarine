using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int numOfPlayers = 1;
    [SerializeField] protected List<PlayerController> players_in_station;
    private DoorScript door;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (players_in_station.Count >= numOfPlayers)
        {
            door.OpenDoor(0.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Player Bye");
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (!players_in_station.Contains(player))
            {
                players_in_station.Add(player);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Player Bye");
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (players_in_station.Contains(player))
            {
                players_in_station.Remove(player);
            }
        }
    }
}
