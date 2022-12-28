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
        if(timeWindowToPress >= maximalTime)
        {
            timeWindowToPress = 0;
            pressKeysInARowCount = 0;
        }

    }


    private void pressNKeyInARow()
    {
        //Debug.Log(action_key_pressed);
        if (action_key_pressed)
        {
            int points = (int)(missionsNumberOfPlayers[mission_index] + 1) / 2;
            Debug.Log("Station pressNKeyInARow() +=1 with " + missionsNumberOfPlayers[mission_index].ToString() + " Players");
            //station_active = false;
            press_in_a_row += 1;
            action_key_pressed = false;
            if (press_in_a_row == 5)
            {
                station_active = false; //todo uncomment we finish testing otherwise annoying
                Debug.Log("Station pressKeyInARow() Mission Accomplished giving: " + missionsNumberOfPlayers[mission_index].ToString() + " points");

                missionManager.missionDone(5, missionsNumberOfPlayers[mission_index] * 2);
                press_in_a_row = 0;
            }

        }
        else
        {

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

                Debug.Log(press_in_a_row);
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