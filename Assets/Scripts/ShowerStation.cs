using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class ShowerStation : StationScript
{
    // Player Section
    // [SerializeField] private List<KeyCode> players_action_key =new List<KeyCode>(); // All the relevant keys
    
    // [SerializeField] private List<GameObject> players_in_station; // List that holds all the current players
    
    // // Is Station Active
    // [SerializeField] private bool station_active = false; // Checks if the station is active (has a mission)

    // // Missions
    // private List<Action> missions = new List<Action>(); // List that contains all the functions for the missions
    // private List<int> missionsNumberOfPlayers = new List<int>(); // List that contains the number of players that need to be in the station for each mission
    // [SerializeField] private int mission_index = 0; // The mission we choose for this station

    // [SerializeField] private int press_in_a_row = 0; 
    // [SerializeField] private MissionManager missionManager;

    // private InputAction player_action;

    // private bool action_key_pressed = false;

    
    // [SerializeField] private GameObject stationPopup;
    [SerializeField] private float holdTime = 0;
    //replaces the variable "count" in pressNKeysInARow
    // private int pressKeysInARowCount = 0;
    // //The maximal frame count before we automatically reset pressKeysInARowCount, should find a solution using milliseconds
    // //instead of number of frames.
    // [SerializeField] private float maximalTime = 0.25f;

    // [SerializeField] private TMP_Text playersForMission; 

    // Start is called before the first frame update
    void Start()
    {
        missions.Add(HoldKey);
        missionsNumberOfPlayers.Add(1);

        stationPopup = Instantiate(Resources.Load("StationPopup")) as GameObject;
        stationPopup.transform.position = gameObject.transform.position + new Vector3(0,2,0);
        deActivatePopup();
    }

    // Update is called once per frame
    void Update()
    {
        //temporary fix for presKeyInRow, needs better solution
        if (station_active)
        {
            if (players_in_station.Count == missionsNumberOfPlayers[mission_index])
            {
                missions[mission_index]();
                 
            }
        }

    }


    private void HoldKey()
    {
       foreach( var action_key in players_action_key)
       {
            if (Input.GetKey(action_key))
            {
                holdTime += Time.deltaTime;
            }
            else
            {
                holdTime = 0;
            }
       }
       if (holdTime >= 1f) 
       {
        holdTime = 0;
        station_active = false;
        deActivatePopup();
        missionManager.missionDone(1, 1);
       }
    }
}

    // This checks which player is on the station
    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.tag == "Player" && players_in_station.Count < missionsNumberOfPlayers[mission_index])
    //     {
    //         //Debug.Log("Player Touched");
    //         if (collision.gameObject.GetComponent<PlayerController>().is_Controller())
    //         {
    //             player_action = collision.gameObject.GetComponent<PlayerController>().GetPlayerActionButtonNew();
    //             player_action.started += ctx => action_key_pressed = true;
    //             //player_action.performed += ctx => action_key_pressed = true;
    //             player_action.canceled += ctx => action_key_pressed = false;
    //         }
    //         else
    //         {
    //             players_action_key.Add(collision.gameObject.GetComponent<PlayerController>().GetPlayerActionButton()); // There must be a better way
    //         }
    //         players_in_station.Add(collision.gameObject);
            
    //     }
    // }

    // private void OnCollisionExit2D(Collision2D collision)
    // {
    //     if (collision.gameObject.tag == "Player")
    //     {
    //         //Debug.Log("Player Bye");
    //         if (players_in_station.Contains(collision.gameObject))
    //         {
    //             if (collision.gameObject.GetComponent<PlayerController>().is_Controller())
    //             {
    //                 player_action = null;
    //             }
    //             else
    //             {
    //                 players_action_key.Remove(collision.gameObject.GetComponent<PlayerController>().GetPlayerActionButton());
    //             }
    //             players_in_station.Remove(collision.gameObject);
    //             press_in_a_row = 0; // NOICE
                
    //         }
    //     }

    // }



    // public void setMissionIndex(int i)
    // {
        
    //     mission_index = i;
    //     station_active = true;
        
    //     activatePopup();
    // }

    // public bool getStationActiveState()
    // {
    //     return station_active;
    // }
    
    // public int getMissionsCount()
    // {
    //     return missions.Count;
    // }

    // public void activatePopup()
    // {
    //     playersForMission.text = missionsNumberOfPlayers[mission_index].ToString() + " Players";
    //     stationPopup.SetActive(true);
    // }

    // public void deActivatePopup()
    // {
    //     playersForMission.text = "";
    //     stationPopup.SetActive(false);
    // }
    
    // public bool hasPlayersInStation()
    // {
    //     return (players_in_station.Count != 0);
    // }


