using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    [SerializeField] private GameObject stationPopup;
    private float timeWindowToPress = 0;
    //replaces the variable "count" in pressNKeysInARow
    private int pressKeysInARowCount = 0;
    //The maximal frame count before we automatically reset pressKeysInARowCount, should find a solution using milliseconds
    //instead of number of frames.
    private float maximalTime = 0.25f;



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
        stationPopup.transform.position = gameObject.transform.position + new Vector3(0,1,0);
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
        int count = 0;
        foreach (var action_key in players_action_key)
        {
            if (Input.GetKeyDown(action_key))
            {
                count += 1;
            }
        }

        int points = (int) (missionsNumberOfPlayers[mission_index] + 1) / 2;

        if (count == missionsNumberOfPlayers[mission_index])
        {
            Debug.Log("Station getAllKeysDown() Mission Accomplished with " + missionsNumberOfPlayers[mission_index].ToString() + " Players");
            station_active = false; //todo uncomment once we finish testing otherwise annoying
            deActivatePopup();
            missionManager.missionDone(5, points);
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
            if (Input.GetKeyDown(action_key))
            {
                Debug.Log(pressKeysInARowCount);
                pressKeysInARowCount += 1;
            }
        }
        

        int points = (int) (missionsNumberOfPlayers[mission_index] + 1) / 2;

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

            missionManager.missionDone(5, missionsNumberOfPlayers[mission_index] * 2);
            press_in_a_row = 0;
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
            players_action_key.Add(collision.gameObject.GetComponent<PlayerController>().GetPlayerActionButton()); // There must be a better way
            players_in_station.Add(collision.gameObject);
        }
        station_active = true;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            station_active = true;
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
                press_in_a_row = 0;
            }
        }
        station_active = false;
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
        stationPopup.SetActive(true);
    }

    public void deActivatePopup()
    {
        stationPopup.SetActive(false);
    }
    

}
