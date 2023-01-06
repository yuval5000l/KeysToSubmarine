using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class LeverStation : StationScript
{

    List<bool> check_pressed_once = new List<bool>() { false,false,false,false};
    [SerializeField] private Animator station_animation;
    [SerializeField] private DoorScript door;
    [SerializeField] private float DoorOpenTime = 1.0f;
    [SerializeField] private bool alwaysActive;
    [SerializeField] private float bonus_time = 1.5f;
    [SerializeField] private GameObject stationExplainer;
    new void Start()
    {
        base.Start();
        missions.Add(getAllKeysDown);
        missionsNumberOfPlayers.Add(numberOfPlayers);
        if (stationPopup == null)
        {
            stationPopup = Instantiate(Resources.Load("LightBulb")) as GameObject;
        }
        stationPopup.transform.position = gameObject.transform.position + new Vector3(0, 0.5f, 0);
        deActivatePopup();

    }

    // Update is called once per frame
    void Update()
    {

        pressKeysInARowCount = 0;
        //Debug.Log(missionsNumberOfPlayers[mission_index]);
        //temporary fix for presKeyInRow, needs better solution
        if (station_active || alwaysActive)
        {
            if (stationExplainer)
            {
                stationExplainer.SetActive(true);
            }
            if (players_in_station.Count == missionsNumberOfPlayers[mission_index])
            {
                missions[mission_index]();
                station_animation.SetTrigger("Hover");
            }
            else
            {
                station_animation.SetTrigger("StopHover");
            }
        }
        else
        {
            if (stationExplainer)
            {
                stationExplainer.SetActive(false);
            }
            station_animation.SetTrigger("StopHover");
            PlayerAnimationIdle();
        }

        for (int i = 0; i < players_controller_in_station.Count; i++)
        {
            if (players_controller_in_station[i].Item2 == false)
            {
                check_pressed_once[i] = false;
            }
            else
            {
                check_pressed_once[i] = true;
            }
        }
    }


    private void getAllKeysDown()
    {
        //if (action_key_pressed)
        //{
        //    int points = (int)(missionsNumberOfPlayers[mission_index] + 1) / 2;

        //    Debug.Log("Station getAllKeysDown() Mission Accomplished with " + missionsNumberOfPlayers[mission_index].ToString() + " Players");
        //    station_active = false; //todo uncomment once we finish testing otherwise annoying
        //    deActivatePopup();
        //    missionManager.missionDone(1, points);
        //}
        //else
        //{
            //int count = 0;
        foreach (var action_key in players_action_key)
        {
            if (Input.GetKeyDown(action_key))
            {
                pressKeysInARowCount += 1;
            }

        }
        int counter = 0;
        foreach (var action_key in players_controller_in_station) // For Controller
        {
            if (action_key.Item2 && check_pressed_once[counter] == false)
            {
                pressKeysInARowCount += 1;
                timeWindowToPress = 0;
            }
            counter++;
        }
        int points = (int)(missionsNumberOfPlayers[mission_index] + 1) / 2;


        if (pressKeysInARowCount == missionsNumberOfPlayers[mission_index])
        {
            //Debug.Log("Station getAllKeysDown() Mission Accomplished with " + missionsNumberOfPlayers[mission_index].ToString() + " Players");
            foreach (PlayerController player in players_in_station)
            {
                player.ForceStop();
            }
            PlayerAnimationWork();
            station_active = false; 
            deActivatePopup();
            //Debug.Log("Did it");
            station_animation.SetTrigger("pullLever");
            if (door != null)
            {
                // door.OpenDoor();
                door.OpenDoor(DoorOpenTime);
            }
            
            if (!alwaysActive) // otherwise they get points for opening the door ... 
            {
                missionManager.missionDone((pressKeysInARowCount * bonus_time), points);
            }
        }
        else
        {
            PlayerAnimationIdle();
        }
    }
    
}