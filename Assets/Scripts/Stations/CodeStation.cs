using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeStation : StationScript

{
    [SerializeField] private SpriteRenderer spriteR;
    [SerializeField] private Sprite idle;
    [SerializeField] private Sprite HoverSprite;


    [SerializeField] private Sprite[] states;
    List<bool> check_pressed_once = new List<bool>() { false, false, false, false };
    [SerializeField] private float bonus_time = 1f;
    [SerializeField] private DoorScript door;
    [SerializeField] private float DoorOpenTime = 1.0f;
    [SerializeField] private GameObject stationExplainer;

    private List<KeyCode> keys_pressed = new List<KeyCode>();
    private bool door_activated = true;

    new void Start()
    {
        base.Start();
        missions.Add(pressNKeyInARow);
        missionsNumberOfPlayers.Add(numberOfPlayers);
        spriteR.sprite = idle;
        stationPopup = Instantiate(Resources.Load("LightBulb")) as GameObject;
        stationPopup.transform.position = gameObject.transform.position + new Vector3(0, 1, 0);
        deActivatePopup();
        
    }

    // Update is called once per frame
    void Update()
    {
        //temporary fix for presKeyInRow, needs better solution
        if (station_active)
        {
            if (stationExplainer)
            {
                stationExplainer.SetActive(true);
            }
            if (players_in_station.Count == missionsNumberOfPlayers[mission_index])
            {
                if (press_in_a_row == 0)
                {
                    spriteR.sprite = HoverSprite;
                }
                missions[mission_index]();
            }
            else
            {
                PlayerAnimationIdle();
                spriteR.sprite = idle;
            }
        }
        else
        {
            if (stationExplainer)
            {
                stationExplainer.SetActive(false);
            }
            PlayerAnimationIdle();
            spriteR.sprite = idle;
        }
        if (timeWindowToPress >= maximalTime)
        {
            timeWindowToPress = 0;
            keys_pressed.Clear();
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
        timeWindowToPress += Time.deltaTime;
    }
    // Bug - player can press multiple times and act like 3 players.
    private void pressNKeyInARow()
    {
        foreach (var action_key in players_action_key)
        {
            if (Input.GetKeyDown(action_key) && !keys_pressed.Contains(action_key))
            {
                timeWindowToPress = 0;
                keys_pressed.Add(action_key);
            }
        }
        int counter = 0;
        foreach (var action_key in players_controller_in_station) // For Controller
        {
            if (action_key.Item2 && check_pressed_once[counter] == false)
            {
                timeWindowToPress = 0;
            }
            counter++;
        }
        if (keys_pressed.Count == missionsNumberOfPlayers[mission_index])
        {
            PlayerAnimationWork();
            press_in_a_row += 1;
            spriteR.sprite = states[press_in_a_row - 1];
            keys_pressed.Clear();
        }


        if (press_in_a_row == 5)
        {
            spriteR.sprite = states[press_in_a_row - 1];
            station_active = false;
            deActivatePopup();
            if (press_in_a_row == 5)
            {
                station_active = false;

                missionManager.missionDone(bonus_time, missionsNumberOfPlayers[mission_index] * 2);
                press_in_a_row = 0;
            }
            if (door && door_activated)
            {
                door.OpenDoor(DoorOpenTime);
                door_activated = false;
            }
        }
    }

    
    public override void setMissionIndex(int i)
    {
        mission_index = i;
        spriteR.sprite = idle;
        station_active = true;
        activatePopup();
    }

}