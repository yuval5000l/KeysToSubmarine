using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class ShowerStation : StationScript
{

    [SerializeField] private float holdTime = 0;
    [SerializeField] private GameObject indicator;
    [SerializeField] private Animator station_animation;

    private float timeHeld = 0;

    void Start()
    {
        missions.Add(HoldKey);
        missionsNumberOfPlayers.Add(numberOfPlayers);

        stationPopup = Instantiate(Resources.Load("StationPopup")) as GameObject;
        stationPopup.transform.position = gameObject.transform.position + new Vector3(0,1.5f,0);
        deActivatePopup();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeWindowToPress >= maximalTime)
        {
            timeWindowToPress = 0;
            pressKeysInARowCount = 0;
        }
        //temporary fix for presKeyInRow, needs better solution
        if (station_active)
        {
            if (players_in_station.Count == missionsNumberOfPlayers[mission_index])
            {
                missions[mission_index]();
                station_animation.SetTrigger("Touched");
            }
            else
            {
                station_animation.SetTrigger("idle");
            }
        }
        else
        {
            station_animation.SetTrigger("idle");
        }
        timeWindowToPress += Time.deltaTime;
    }


    private void HoldKey()
    {
       foreach( var action_key in players_action_key)
       {
            if (Input.GetKey(action_key))
            {
                station_animation.SetTrigger("StartShower");
                timeHeld += Time.deltaTime;
                indicator.transform.localScale = new Vector3((timeHeld)*1f, indicator.transform.localScale.y);
            }
            else
            {
                timeHeld = 0;
            }
       }
       foreach(var action_key in players_controller_in_station)
        {
            if (action_key.Item2 == true)
            {
                station_animation.SetTrigger("StartShower");
                timeHeld += Time.deltaTime;
                indicator.transform.localScale = new Vector3((timeHeld) * 1f, indicator.transform.localScale.y);
            }
        }
        if (timeHeld >= holdTime) 
       {
            station_animation.SetTrigger("EndShower");
            timeHeld = 0;
        station_active = false;
        indicator.transform.localScale = new Vector3(0.01f, indicator.transform.localScale.y);
        deActivatePopup();
        missionManager.missionDone(1, 1);
       }
    }
}

