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
    [SerializeField] protected List<KeyCode> players_action_key =new List<KeyCode>(); // All the relevant keys
    
    [SerializeField] protected List<GameObject> players_in_station; // List that holds all the current players

    [SerializeField] protected List<Tuple<PlayerController, bool>> players_controller_in_station = new List<Tuple<PlayerController, bool>>();

    // Is Station Active
    [SerializeField] protected bool station_active = false; // Checks if the station is active (has a mission)

    // Missions
    protected List<Action> missions = new List<Action>(); // List that contains all the functions for the missions
    protected List<int> missionsNumberOfPlayers = new List<int>(); // List that contains the number of players that need to be in the station for each mission
    [SerializeField] protected int mission_index = 0; // The mission we choose for this station

    [SerializeField] protected int press_in_a_row = 0; 
    [SerializeField] protected MissionManager missionManager;

    protected List<InputAction> player_action_controller = new List<InputAction>();

    protected List<bool> action_keys_pressed;

    protected bool action_key_pressed = false;

    
    [SerializeField] protected GameObject stationPopup;
    protected float timeWindowToPress = 0;
    //replaces the variable "count" in pressNKeysInARow
    protected int pressKeysInARowCount = 0;
    //The maximal frame count before we automatically reset pressKeysInARowCount, should find a solution using milliseconds
    //instead of number of frames.
    [SerializeField] protected float maximalTime = 0.25f;

    //[SerializeField] protected TMP_Text playersForMission; 
    [SerializeField] protected int numberOfPlayers;


    // Start is called before the first frame update
    void Start()
    {
        // missions.Add(getAllKeysDown);
        // missionsNumberOfPlayers.Add(1);
        //missions.Add(getKeyDownTwoPlayers);
        // missions.Add(getAllKeysDown);
        // missionsNumberOfPlayers.Add(2);
        // missions.Add(getAllKeysDown);
        // missionsNumberOfPlayers.Add(players_in_station.Count);
        // missions.Add(pressNKeyInARow);
        // missionsNumberOfPlayers.Add(2);
        missions.Add(getKeyDownAllPlayers);
        //ToDo, needs an elegant solution, station shouldn't know how many players are in the game.
        missionsNumberOfPlayers.Add(3);

        stationPopup = Instantiate(Resources.Load("LightBulb")) as GameObject;
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
        if (action_key_pressed)
        {
            int points = (int)(missionsNumberOfPlayers[mission_index] + 1) / 2;

            //Debug.Log("Station getAllKeysDown() Mission Accomplished with " + missionsNumberOfPlayers[mission_index].ToString() + " Players");
            station_active = false; //todo uncomment once we finish testing otherwise annoying
            deActivatePopup();
            missionManager.missionDone(1, points);
        }
        else
        {
            //int count = 0;
            foreach (var action_key in players_action_key)
            {
                if (Input.GetKeyDown(action_key))
                {
                    pressKeysInARowCount += 1;
                }
            }

            int points = (int)(missionsNumberOfPlayers[mission_index] + 1) / 2;

            if (pressKeysInARowCount == missionsNumberOfPlayers[mission_index])
            {
                //Debug.Log("Station getAllKeysDown() Mission Accomplished with " + missionsNumberOfPlayers[mission_index].ToString() + " Players");
                station_active = false; //todo uncomment once we finish testing otherwise annoying
                deActivatePopup();
                missionManager.missionDone((pressKeysInARowCount * 1.5f), points);
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
        Debug.Log(action_key_pressed);
        if (action_key_pressed)
        {
            int points = (int)(missionsNumberOfPlayers[mission_index] + 1) / 2;
            //Debug.Log("Station pressNKeyInARow() +=1 with " + missionsNumberOfPlayers[mission_index].ToString() + " Players");
            //station_active = false;
            press_in_a_row += 1;
            action_key_pressed = false;
            if (press_in_a_row == 5)
            {
                station_active = false; //todo uncomment we finish testing otherwise annoying
                //Debug.Log("Station pressKeyInARow() Mission Accomplished giving: " + missionsNumberOfPlayers[mission_index].ToString() + " points");

                missionManager.missionDone(5, missionsNumberOfPlayers[mission_index] * 2);
                press_in_a_row = 0;
            }

        }
        else
        {
            // foreach (var action_key in players_action_key)
            // {
            //     int points = (int)(missionsNumberOfPlayers[mission_index] + 1) / 2;
            //     Debug.Log("Station pressNKeyInARow() +=1 with " + missionsNumberOfPlayers[mission_index].ToString() + " Players");
            //     //station_active = false;
            //     press_in_a_row += 1;
            //     action_key_pressed = false;
            //     if (press_in_a_row == 5)
            //     {
            //         Debug.Log(pressKeysInARowCount);
            //         pressKeysInARowCount += 1;
            //     }

            // }
        
            // int count = 0;
            foreach (var action_key in players_action_key)
            {
                if (Input.GetKeyDown(action_key))
                {
                    pressKeysInARowCount += 1;
                    timeWindowToPress = 0;
                }
            }

            if (pressKeysInARowCount == missionsNumberOfPlayers[mission_index])
            {
                // Debug.Log("Station pressNKeyInARow() +=1 with " + missionsNumberOfPlayers[mission_index].ToString() + " Players");
                // station_active = false; //todo uncomment once we finish testing otherwise annoying

                //missionManager.missionDone(10, points);
                Debug.Log(press_in_a_row);
                press_in_a_row += 1;
                pressKeysInARowCount = 0;
            }



            if (press_in_a_row == 5)
            {
                station_active = false; //todo uncomment we finish testing otherwise annoying
                deActivatePopup();
                // Debug.Log("Station pressKeyInARow() Mission Accomplished giving: " + missionsNumberOfPlayers[mission_index].ToString() + " points");
                if (press_in_a_row == 5)
                {
                    station_active = false; //todo uncomment we finish testing otherwise annoying
                    // Debug.Log("Station pressKeyInARow() Mission Accomplished giving: " + missionsNumberOfPlayers[mission_index].ToString() + " points");

                    missionManager.missionDone(5, missionsNumberOfPlayers[mission_index] * 2);
                    press_in_a_row = 0;
                }
            }
        }
    }
    

    private void getKeyDownAllPlayers()
    {
        if(players_action_key.Count > 0)
        {
            foreach( var action_key in players_action_key)
            {
                //Debug.Log("In Loop");
                if (!Input.GetKey(action_key))
                {
                    return;
                }
            }

            //Debug.Log("Station Neutralized");
            station_active = false;
            deActivatePopup();
            missionManager.missionDone(5, 1);
        }
    }

    // This checks which player is on the station
    //protected void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player" && players_in_station.Count < missionsNumberOfPlayers[mission_index])
    //    {
    //        //Debug.Log("Player Touched");
    //        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
    //        if (player.is_Controller())
    //        {
    //            //Debug.Log("Player with controller");
    //            int counter = players_controller_in_station.Count;
    //            player_action_controller.Add(player.GetPlayerActionButtonNew());
    //            //player_action_controller[counter] = player.GetPlayerActionButtonNew();
    //            players_controller_in_station.Add(new Tuple<PlayerController, bool>(player, false));
    //            //Debug.Log(player_action_controller);
    //            //Debug.Log("Counter = " + counter);
    //            player_action_controller[counter].started += ctx => players_controller_in_station[counter] = new Tuple<PlayerController, bool>(players_controller_in_station[counter].Item1, true);
    //            //player_action.performed += ctx => action_key_pressed = true;
    //            player_action_controller[counter].canceled += ctx => players_controller_in_station[counter] = new Tuple<PlayerController, bool>(players_controller_in_station[counter].Item1, false);
                
    //        }
    //        else
    //        {
    //            players_action_key.Add(player.GetPlayerActionButton()); // There must be a better way
    //        }
    //        players_in_station.Add(collision.gameObject);

    //    }
    //}

    //protected void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        //Debug.Log("Player Bye");
    //        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
    //        if (players_in_station.Contains(collision.gameObject))
    //        {
    //            if (player.is_Controller())
    //            {
    //                int location = 0;
    //                int counter = 0;
    //                foreach(Tuple<PlayerController, bool> playera in players_controller_in_station)
    //                {
    //                    if (playera.Item1 == player)
    //                    {
    //                        player_action_controller.Remove(player_action_controller[counter]);
    //                        location = counter;
    //                    }
    //                    counter++;
    //                }
    //                players_controller_in_station.Remove(players_controller_in_station[location]);
    //            }
    //            else
    //            {
    //                players_action_key.Remove(player.GetPlayerActionButton());
    //            }
    //            players_in_station.Remove(collision.gameObject);
    //            press_in_a_row = 0; // NOICE
                
    //        }
    //    }

    //}
    
    
    void OnTriggerEnter2D(Collider2D colider) {
        if (colider.gameObject.tag == "Player" && players_in_station.Count < missionsNumberOfPlayers[mission_index])
        {
            //Debug.Log("Player Touched");
            PlayerController player = colider.gameObject.GetComponent<PlayerController>();
            if (player.is_Controller())
            {
                //Debug.Log("Player with controller");
                int counter = players_controller_in_station.Count;
                player_action_controller.Add(player.GetPlayerActionButtonNew());
                //player_action_controller[counter] = player.GetPlayerActionButtonNew();
                players_controller_in_station.Add(new Tuple<PlayerController, bool>(player, false));
                //Debug.Log("Counter = " + counter);
                player_action_controller[counter].started += ctx => players_controller_in_station[counter] = new Tuple<PlayerController, bool>(players_controller_in_station[counter].Item1, true);
                //player_action.performed += ctx => action_key_pressed = true;
                player_action_controller[counter].canceled += ctx => players_controller_in_station[counter] = new Tuple<PlayerController, bool>(players_controller_in_station[counter].Item1, false);
                
            }
            else
            {
                players_action_key.Add(player.GetPlayerActionButton()); // There must be a better way
            }
            players_in_station.Add(colider.gameObject);
        }
        
    }
    
    
    void OnTriggerExit2D(Collider2D colider) {
        if (colider.gameObject.tag == "Player")
        {
            //Debug.Log("Player Bye");
            PlayerController player = colider.gameObject.GetComponent<PlayerController>();
            if (players_in_station.Contains(colider.gameObject))
            {
                if (player.is_Controller())
                {
                    int location = 0;
                    int counter = 0;
                    foreach(Tuple<PlayerController, bool> playera in players_controller_in_station)
                    {
                        if (playera.Item1 == player)
                        {
                            player_action_controller.Remove(player_action_controller[counter]);
                            location = counter;
                        }
                        counter++;
                    }
                    players_controller_in_station.Remove(players_controller_in_station[location]);
                }
                else
                {
                    players_action_key.Remove(player.GetPlayerActionButton());
                }
                players_in_station.Remove(colider.gameObject);
                press_in_a_row = 0; // NOICE
                
            }
        }
        
    }
    



    public virtual void setMissionIndex(int i)
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
        //playersForMission.text = missionsNumberOfPlayers[mission_index].ToString() + " Players";
        stationPopup.SetActive(true);
    }

    public void deActivatePopup()
    {
        //playersForMission.text = "";
        stationPopup.SetActive(false);
    }
    
    public bool hasPlayersInStation()
    {
        return (players_in_station.Count != 0);
    }

}


