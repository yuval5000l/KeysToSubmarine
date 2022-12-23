using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationScript : MonoBehaviour
{
    // Player Section
    private List<KeyCode> players_action_key =new List<KeyCode>(); // All the relevant keys
    [SerializeField] private int PlayersInStation = 1; // How many players in this station?
    [SerializeField] private List<GameObject> players_in_station; // List that holds all the current players
    
    // Is Station Active
    [SerializeField] private bool station_active = false; // Checks if the station is active (has a mission)

    // Missions
    private List<Action> missions = new List<Action>(); // List that contains all the functions for the missions
    [SerializeField] private int mission_index = 0; // The mission we choose for this station

    [SerializeField] private MissionManager missionManager;

    // Start is called before the first frame update
    void Start()
    {
        missions.Add(getKeyDown);
    }

    // Update is called once per frame
    void Update()
    {
        if (station_active)
        {
            if (players_in_station.Count == PlayersInStation)
            {
                missions[mission_index](); 
            }
        }
    }


    // Different missions
    private void getKeyDown()
    {
        if (Input.GetKeyDown(players_action_key[0]))
        {
            Debug.Log("Station Nuetralized");
            station_active = false;
            missionManager.AddTime(5);
        }
    }

    // This checks which player is on the station
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && players_in_station.Count < PlayersInStation)
        {
            //Debug.Log("Player Touched");
            players_action_key.Add(collision.gameObject.GetComponent<PlayerController>().GetPlayerActionButton()); // There must be a better way
            players_in_station.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Player Bye");
            if (players_in_station.Contains(collision.gameObject))
            {

                players_action_key.Remove(collision.gameObject.GetComponent<PlayerController>().GetPlayerActionButton());
                players_in_station.Remove(collision.gameObject);
            }
        }
    }

}
