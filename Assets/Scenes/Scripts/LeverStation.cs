using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class LeverStation : StationScript
{



    void Start()
    {
        missions.Add(getAllKeysDown);
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


    private void getAllKeysDown()
    {
        if (action_key_pressed)
        {
            int points = (int)(missionsNumberOfPlayers[mission_index] + 1) / 2;

            Debug.Log("Station getAllKeysDown() Mission Accomplished with " + missionsNumberOfPlayers[mission_index].ToString() + " Players");
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
                Debug.Log("Station getAllKeysDown() Mission Accomplished with " + missionsNumberOfPlayers[mission_index].ToString() + " Players");
                station_active = false;
                pressKeysInARowCount = 0;
                deActivatePopup();
                missionManager.missionDone((pressKeysInARowCount * 1.5f), points);
            }
        }
    }
}