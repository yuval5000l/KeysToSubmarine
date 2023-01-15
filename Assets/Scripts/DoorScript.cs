using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Collider2D coli;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator anim;
    [SerializeField] private bool always_open = false;
    [SerializeField] private int numOfStations = 1;
    [SerializeField] private bool permanent_changes = false;
    private List<SpriteRenderer> cables = new List<SpriteRenderer>();
    private Color default_color;
    private int counter_stations = 0;
    private bool door_open = false;
    private bool[] stationsList;
    private float time = 0f;
    private float triggerTimer = 0f;
    private void Awake()
    {
        int counter = 0;
        stationsList = new bool[numOfStations];
        foreach(SpriteRenderer sprite1 in  GetComponentsInChildren<SpriteRenderer>())
        {
            if (counter > 0)
            {
                cables.Add(sprite1);
            }
            counter++;
        }
        for(int i = 0; i < numOfStations; i++)
        {
            stationsList[i] = false;
        }
        if (cables.Count != 0)
        {
            default_color = cables[0].color;
        }

    }
    void Update()
    {
        triggerTimer += Time.deltaTime;

        if (triggerTimer >= 0.25f)
        {
            triggerTimer = 0f;
            for(int i = 0; i < numOfStations; i++)
            {
                stationsList[i] = false;
            }
            counter_stations = 0;
        }
    }
    public bool DoorState()
    {
        return door_open;
    }
    public void CloseDoor()
    {
        //Debug.Log("CloseDoor()");
        anim.SetTrigger("Door_OH");
        anim.SetTrigger("Door_H");
        anim.SetTrigger("Door_C");

        coli.enabled = true;
        //sprite.enabled = true;
        //anim.Settrigger("CloseDoor")
        counter_stations = 0;
        door_open = false;
        //foreach (SpriteRenderer sprite1 in cables)
        //{
        //    sprite1.color = default_color;
        //}
        for(int i = 0; i < numOfStations; i++)
        {
            stationsList[i] = false;
        }
    }
    
    public IEnumerator CloseDoorIn(float xSec)
    {
        yield return new WaitForSeconds(xSec);
        //anim.SetTrigger("CloseOpenDoor");

        coli.enabled = true;
        sprite.enabled = true;
    }
    
    public void StopTouchDoor()
    {
        if (counter_stations >= 0 && !permanent_changes)
        {
            counter_stations--;
        }
        //Debug.Log(counter_stations);
    }
    
    public void OpenDoor(float xSeconds = 1)
    {
        
        stationsList[counter_stations] = true;
        counter_stations++;
        bool openFlag = true;
        triggerTimer = 0f;
        time = xSeconds;
        //Debug.Log(counter_stations);
        for (int i = 0; i < numOfStations; i++)
        {

            if(!stationsList[i])
            {
                openFlag = false;
            }
        }
        if(openFlag)
        {
            anim.SetTrigger("Door_H");
            anim.SetTrigger("Door_HO");
            anim.SetTrigger("Door_O");
            //if (always_open)
            //{
            //    coli.enabled = false;
            //    sprite.enabled = false;
            //    door_open = true;
            //    foreach (SpriteRenderer sprite1 in cables)
            //    {
            //        sprite1.color = new Color(1f, 1f, 1f, 1f);
            //    }
            //}
            //else
            //{
            //    StartCoroutine(OpenDoorFor(xSeconds));
            //}
        }
    }
    public void ReallyOpenDoor()
    {
        coli.enabled = false;
        //sprite.enabled = false;
        door_open = true;
        if (!always_open)
        {
            StartCoroutine(OpenDoorFor(time));
        }
        
    }
    public IEnumerator OpenDoorFor(float xSeconds)
    {

        //coli.enabled = false;
        //sprite.enabled = false;
        //door_open = true;
        //anim.Settrigger("OpenDoor")
        //foreach (SpriteRenderer sprite1 in cables)
        //{
        //    sprite1.color = new Color(1f, 1f, 1f, 1f);
        //}
        yield return new WaitForSeconds(xSeconds);
        CloseDoor();
    }

}
