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
    //[SerializeField] protected List<KeyCode> players_action_key =new List<KeyCode>(); // All the relevant keys
    [Header("Mission Manager")]
    [SerializeField] protected MissionManager missionManager;

    [Header("Station State")]
    [SerializeField] protected List<PlayerController> players_in_station; // List that holds all the current players
    // Is Station Active
    [SerializeField] protected bool station_active = false; // Checks if the station is active (has a mission)
    [SerializeField] protected bool always_active = false;
    // Missions
    protected List<Action> missions = new List<Action>(); // List that contains all the functions for the missions
    protected List<int> missionsNumberOfPlayers = new List<int>(); // List that contains the number of players that need to be in the station for each mission
    /*[SerializeField]*/ protected int mission_index = 0; // The mission we choose for this station

    /*[SerializeField]*/ protected int press_in_a_row = 0;
    [SerializeField] protected int points_award = 0;

    protected List<InputAction> player_action_controller = new List<InputAction>();

    protected List<bool> action_keys_pressed;

    protected bool action_key_pressed = false;

    

    protected float timeWindowToPress = 0;
    //replaces the variable "count" in pressNKeysInARow
    protected int pressKeysInARowCount = 0;
    //The maximal frame count before we automatically reset pressKeysInARowCount, should find a solution using milliseconds
    //instead of number of frames.
    [SerializeField] protected int numberOfPlayers;
    [SerializeField] protected GameObject stationPopup;
    [SerializeField] protected float maximalTime = 0.25f;

    //[SerializeField] protected TMP_Text playersForMission; 

    [Header("Station Sound")]
    [SerializeField] protected AudioSource yellowOrbAppear;
    [SerializeField] protected AudioSource yellowOrbFade;
    [SerializeField] protected AudioSource colliderSound;
    //protected GameObject[] numOfPlayersIndicator = new GameObject[4];

    // Start is called before the first frame update
    protected void Start()
    {
        

        stationPopup = Instantiate(Resources.Load("AdvancedRedBall")) as GameObject;
        stationPopup.transform.position = gameObject.transform.position + new Vector3(0, 0.5f, 0);

        stationPopup.GetComponent<PopUpWithPlayersController>().setNumOfChildren(numberOfPlayers);
        stationPopup.SetActive(false);
        yellowOrbAppear.playOnAwake = false;
        yellowOrbFade.playOnAwake = false;
        colliderSound.playOnAwake = false;

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


    
    
    public bool isAlwaysActive()
    {
        return always_active;
    }
    
    
    void OnTriggerEnter2D(Collider2D colider) {
        if (colider.gameObject.tag == "Player") // && players_in_station.Count < missionsNumberOfPlayers[mission_index])
        {
            colliderSound.Play();
            //Debug.Log("Player Touched");
            PlayerController player = colider.gameObject.GetComponent<PlayerController>();
            if (!players_in_station.Contains(player))
            {
                
                //players_action_key.Add(player.GetPlayerActionButton()); // There must be a better way
                stationPopup.GetComponent<PopUpWithPlayersController>().setTriggerToChild(player.getColor());
                players_in_station.Add(player);
            }
            
        }
    }
    
    
    void OnTriggerExit2D(Collider2D colider) {
        if (colider.gameObject.tag == "Player")
        {
            //Debug.Log("Player Bye");
            PlayerController player = colider.gameObject.GetComponent<PlayerController>();
            if (players_in_station.Contains(player))
            {
               
                
                //players_action_key.Remove(player.GetPlayerActionButton());
                stationPopup.GetComponent<PopUpWithPlayersController>().setTriggerToChild("Un" + player.getColor());

                players_in_station.Remove(player);
                press_in_a_row = 0; // NOICE
                player.AnimationIdle();
                player.StopAnimationWork(Vector3.zero);
                player.StopAnimationPush(Vector3.zero);


            }
        }
        
    }


    protected void PlayerAnimationWork()
    {
        Vector3 loc = gameObject.transform.localPosition;
        foreach (PlayerController player in players_in_station)
        {
            player.AnimationWork(loc);
        }
    }

    protected void PlayerAnimationIdle()
    {
        foreach (PlayerController player in players_in_station)
        {
            player.AnimationIdle();
        }
    }

    public virtual void setMissionIndex(int i)
    {
        
        mission_index = i;
        if (!station_active)
        {
            station_active = true;
        }
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
        if (stationPopup != null) 
        {
            stationPopup.SetActive(true);
            stationPopup.GetComponent<PopUpWithPlayersController>().ActivatePopUps();
            yellowOrbAppear.Play();

        }
        else
        {
            Debug.Log("There isn't any station PopUp for this station");
        }
    }

    public void deActivatePopup()
    {
        //playersForMission.text = "";
        if (stationPopup)
        {
            if(!always_active)
            {
                stationPopup.GetComponent<PopUpWithPlayersController>().deActivatePopUp();
                yellowOrbFade.Play();
            }
        }
        else
        {
            Debug.Log("There isn't any station PopUp for this station");
        }
    }
    
    public bool hasPlayersInStation()
    {
        return (players_in_station.Count != 0);
    }

}


