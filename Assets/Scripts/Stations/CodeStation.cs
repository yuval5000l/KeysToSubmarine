using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class CodeStation : StationScript

{
    [SerializeField] private SpriteRenderer spriteR;
    [SerializeField] private Sprite vSprite;
    [SerializeField] private Sprite xSprite;
    List<bool> check_pressed_once = new List<bool>() { false, false, false, false };


    void Start()
    {
        missions.Add(pressNKeyInARow);
        missionsNumberOfPlayers.Add(numberOfPlayers);
        spriteR.sprite = xSprite;
        stationPopup = Instantiate(Resources.Load("StationPopup")) as GameObject;
        stationPopup.transform.position = gameObject.transform.position + new Vector3(0, 1.5f, 0);
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
        if (timeWindowToPress >= maximalTime)
        {
            timeWindowToPress = 0;
            pressKeysInARowCount = 0;
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

    private void pressNKeyInARow()
    {
        foreach (var action_key in players_action_key)
        {
            if (Input.GetKeyDown(action_key))
            {
                pressKeysInARowCount += 1;
                timeWindowToPress = 0;
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
        if (pressKeysInARowCount == missionsNumberOfPlayers[mission_index])
        {

            //Debug.Log(press_in_a_row);
            press_in_a_row += 1;
            pressKeysInARowCount = 0;
        }



        if (press_in_a_row == 5)
        {
            spriteR.sprite = vSprite;
            station_active = false;
            deActivatePopup();
            if (press_in_a_row == 5)
            {
                station_active = false; 

                missionManager.missionDone(5, missionsNumberOfPlayers[mission_index] * 2);
                press_in_a_row = 0;
            }
        }
    }

    // public new void activatePopup()
    // {
    //     playersForMission.text = missionsNumberOfPlayers[mission_index].ToString() + " Players";
    //     stationPopup.SetActive(true);


    // }

    
    public override void setMissionIndex(int i)
    {
        mission_index = i;
        spriteR.sprite = xSprite;
        station_active = true;
        activatePopup();
    }

}