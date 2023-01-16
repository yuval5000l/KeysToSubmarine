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
    List<PlayerController> players_pressed = new List<PlayerController>();
    [SerializeField] private Animator MaAnimator;
    [SerializeField] private float bonus_time = 1f;
    [SerializeField] private DoorScript door;
    [SerializeField] private float DoorOpenTime = 1.0f;
    [SerializeField] private GameObject stationExplainer;
    [SerializeField] private int pressToFinish = 5;
    private bool door_activated = true;

    void Awake()
    {
        missions.Add(pressNKeyInARow);
        missionsNumberOfPlayers.Add(numberOfPlayers);
        //spriteR.sprite = idle;
    }
    private new void Start()
    {
        base.Start();
        stationPopup.transform.position = gameObject.transform.position + new Vector3(0, 1f, 0);
        if (always_active)
        {
            numOfPlayersIndicator[numberOfPlayers - 1].SetActive(true);
        }
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
            if (players_in_station.Count > 0)
            {
                //spriteR.sprite = HoverSprite;
                MaAnimator.SetBool("Idle", false);
                MaAnimator.SetBool("Hover", true);
            }
            else
            {
                MaAnimator.SetBool("Idle", true);
            }
            if (players_in_station.Count == missionsNumberOfPlayers[mission_index])
            {
                //if (press_in_a_row == 0)
                //{

                //}
                missions[mission_index]();
            }
            else
            {
                PlayerAnimationIdle();
                //MaAnimator.SetTrigger("idle");
                //spriteR.sprite = idle;
            }
        }
        else
        {
            if (stationExplainer)
            {
                stationExplainer.SetActive(false);
            }
            PlayerAnimationIdle();
            //spriteR.sprite = idle;
        }
        if (timeWindowToPress >= maximalTime)
        {
            timeWindowToPress = 0;
            players_pressed.Clear();
        }

        timeWindowToPress += Time.deltaTime;
    }
    // Bug - player can press multiple times and act like 3 players.
    private void pressNKeyInARow()
    {
        foreach (PlayerController player in players_in_station)
        {
            if (player.playerPressedOneTime() && !players_pressed.Contains(player))
            {
                timeWindowToPress = 0;
                players_pressed.Add(player);
            }
        }

        if (players_pressed.Count == missionsNumberOfPlayers[mission_index])
        {
            PlayerAnimationWork();
            press_in_a_row += 1;
            MaAnimator.SetTrigger("Press");
            MaAnimator.SetBool("Idle", true);
            MaAnimator.SetBool("Hover", false);

            //spriteR.sprite = states[press_in_a_row - 1];
            players_pressed.Clear();
        }

        if (press_in_a_row == pressToFinish)
        {
            spriteR.sprite = states[press_in_a_row - 1];
            station_active = false;
            deActivatePopup();
            if (press_in_a_row == pressToFinish)
            {
                station_active = false;

                missionManager.missionDone(bonus_time, points_award);
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