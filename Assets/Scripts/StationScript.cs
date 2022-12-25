using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class StationScript : MonoBehaviour
{
    // Player Section
    private List<KeyCode> players_action_key =new List<KeyCode>(); // All the relevant keys
    
    [SerializeField] private List<GameObject> players_in_station; // List that holds all the current players
    
    // Is Station Active
    [SerializeField] private bool station_active = false; // Checks if the station is active (has a mission)

    // Missions
    private List<Action> missions = new List<Action>(); // List that contains all the functions for the missions
    private List<int> missionsNumberOfPlayers = new List<int>(); // List that contains the number of players that need to be in the station for each mission
    [SerializeField] private int mission_index = 0; // The mission we choose for this station

    [SerializeField] private int press_in_a_row = 0; 
    [SerializeField] private MissionManager missionManager;

    private InputAction player_action;

    private bool action_key_pressed = false;

    
    [SerializeField] private GameObject stationPopup;
    private float timeWindowToPress = 0;
    //replaces the variable "count" in pressNKeysInARow
    private int pressKeysInARowCount = 0;
    //The maximal frame count before we automatically reset pressKeysInARowCount, should find a solution using milliseconds
    //instead of number of frames.
    [SerializeField] private float maximalTime = 0.25f;

    [SerializeField] private TMP_Text playersForMission; 

    // Start is called before the first frame update
    void Start()
    {
        missions.Add(getAllKeysDown);
        missionsNumberOfPlayers.Add(1);
        // missions.Add(getKeyDownTwoPlayers);
        // missionsNumberOfPlayers.Add(2);
        // missions.Add(pressNKeyInARow);
        // missionsNumberOfPlayers.Add(2); 
        stationPopup = Instantiate(Resources.Load("StationPopup")) as GameObject;
        stationPopup.transform.position = gameObject.transform.position + new Vector3(0,2,0);
        deActivatePopup();
    }

    // Update is called once per frame
    void Update()
    {
        //temporary fix for presKeyInRow, needs better solution
        if(timeWindowToPress >= maximalTime)
        {
            timeWindowToPress = 0;
            pressKeysInARowCount = 0;
        }
        if (station_active)
        {
            if (players_in_station.Count == missionsNumberOfPlayers[mission_index])
            {
                missions[mission_index]();
                 
            }
        }
        timeWindowToPress += Time.deltaTime;
    }


    // Different missions
    // private void getKeyDown()
    // {
    //     if (Input.GetKeyDown(players_action_key[0]))
    //     {
    //         Debug.Log("Station getKeyDown() Mission Accomplished with ");
    //         // station_active = false;
    //         missionManager.missionDone(5, 1);
    //     }
    // }
    
    
    private void getAllKeysDown()
    {
        if (action_key_pressed)
        {
            int points = (int)(missionsNumberOfPlayers[mission_index] + 1) / 2;

            Debug.Log("Station getAllKeysDown() Mission Accomplished with " + missionsNumberOfPlayers[mission_index].ToString() + " Players");
            station_active = false; //todo uncomment once we finish testing otherwise annoying
            deActivatePopup();
            missionManager.missionDone(5, points);
        }
        else
        {
            int count = 0;
            foreach (var action_key in players_action_key)
            {
                if (Input.GetKeyDown(action_key))
                {
                    count += 1;
                }
            }

            int points = (int)(missionsNumberOfPlayers[mission_index] + 1) / 2;

            if (count == missionsNumberOfPlayers[mission_index])
            {
                Debug.Log("Station getAllKeysDown() Mission Accomplished with " + missionsNumberOfPlayers[mission_index].ToString() + " Players");
                station_active = false; //todo uncomment once we finish testing otherwise annoying
                missionManager.missionDone(5, points);
            }
        }
    }
    
    
    // private void pressKeyInARow()
    // {
    //     if (Input.GetKeyDown(players_action_key[0]))
    //     {
    //         Debug.Log("Player Pressed Right Key! +=1 to press in a row!");
    //         press_in_a_row += 1;
    //     }
    //
    //     if (press_in_a_row == 5)
    //     {
    //         station_active = false; //todo uncomment once we finish testing otherwise annoying
    //         Debug.Log("Station pressKeyInARow() Mission Accomplished");
    //
    //         missionManager.missionDone(5, 2);
    //         press_in_a_row = 0;
    //     }
    // }
    
    
    
    private void pressNKeyInARow()
    {
     
        
        foreach (var action_key in players_action_key)
        {
            int points = (int)(missionsNumberOfPlayers[mission_index] + 1) / 2;
            Debug.Log("Station pressNKeyInARow() +=1 with " + missionsNumberOfPlayers[mission_index].ToString() + " Players");
            //station_active = false;
            press_in_a_row += 1;
            action_key_pressed = false;
            if (press_in_a_row == 5)
            {
                Debug.Log(pressKeysInARowCount);
                pressKeysInARowCount += 1;
            }

        }
            int count = 0;
            foreach (var action_key in players_action_key)
            {
                if (Input.GetKeyDown(action_key))
                {
                    count += 1;
                }
            }

        if (pressKeysInARowCount == missionsNumberOfPlayers[mission_index])
        {
            Debug.Log("Station pressNKeyInARow() +=1 with " + missionsNumberOfPlayers[mission_index].ToString() + " Players");
            // station_active = false; //todo uncomment once we finish testing otherwise annoying

            //missionManager.missionDone(10, points);
            press_in_a_row += 1;
            pressKeysInARowCount = 0;
        }
        
       

        if (press_in_a_row == 5)
        {
            station_active = false; //todo uncomment we finish testing otherwise annoying
            deActivatePopup();
            Debug.Log("Station pressKeyInARow() Mission Accomplished giving: " +  missionsNumberOfPlayers[mission_index].ToString() +" points");
            if (press_in_a_row == 5)
            {
                station_active = false; //todo uncomment we finish testing otherwise annoying
                Debug.Log("Station pressKeyInARow() Mission Accomplished giving: " + missionsNumberOfPlayers[mission_index].ToString() + " points");

                missionManager.missionDone(5, missionsNumberOfPlayers[mission_index] * 2);
                press_in_a_row = 0;
            }
        }
        
    }
    
    

    private void getKeyDownTwoPlayers()
    {
        if (Input.GetKey(players_action_key[0]) && Input.GetKey(players_action_key[1]))
        {
            Debug.Log("Station Neutralized");
            station_active = false;
            deActivatePopup();
            missionManager.AddTime(5);
        }
    }


    // This checks which player is on the station
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && players_in_station.Count < missionsNumberOfPlayers[mission_index])
        {
            //Debug.Log("Player Touched");
            if (collision.gameObject.GetComponent<PlayerController>().is_Controller())
            {
                player_action = collision.gameObject.GetComponent<PlayerController>().GetPlayerActionButtonNew();
                player_action.started += ctx => action_key_pressed = true;
                //player_action.performed += ctx => action_key_pressed = true;
                player_action.canceled += ctx => action_key_pressed = false;
            }
            else
            {
                players_action_key.Add(collision.gameObject.GetComponent<PlayerController>().GetPlayerActionButton()); // There must be a better way
            }
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
                if (collision.gameObject.GetComponent<PlayerController>().is_Controller())
                {
                    player_action = null;
                }
                else
                {
                    players_in_station.Remove(collision.gameObject);
                }
                players_action_key.Remove(collision.gameObject.GetComponent<PlayerController>().GetPlayerActionButton());
                press_in_a_row = 0; // NOICE
                
            }
        }

    }



    public void setMissionIndex(int i)
    {
        
        mission_index = i;
        station_active = true;
        
        activatePopup();
    }

    public bool getStationActiveState()
    {
        return station_active;
    }
    
    public int getMissionsCount()
    {
        return missions.Count;
    }

    public void activatePopup()
    {
        playersForMission.text = missionsNumberOfPlayers[mission_index].ToString() + " Players";
        stationPopup.SetActive(true);
    }

    public void deActivatePopup()
    {
        playersForMission.text = "";
        stationPopup.SetActive(false);
    }
    
    public bool hasPlayersInStation()
    {
        return (players_in_station.Count != 0);
    }

}
