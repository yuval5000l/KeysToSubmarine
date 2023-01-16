using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Collider2D coli;
    private Animator anim;
    [SerializeField] private bool always_open = false;
    [SerializeField] private int numOfStations = 1;
    [SerializeField] private bool permanent_changes = false;
    private List<GameObject> tiles_active = new List<GameObject>();
    private List<SpriteRenderer> cables = new List<SpriteRenderer>();
    private Color default_color;
    private int counter_stations = 0;
    private bool door_open = false;
    private bool door_idle_open_time = false;
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
        anim = GetComponent<Animator>();

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
        if (tiles_active.Count >= numOfStations)
        {
            if (!door_open && !door_idle_open_time)
            {
                anim.SetTrigger("Door_H");
                anim.SetTrigger("Door_HO");
                anim.SetTrigger("Door_O");

            }
            door_open = true;
            door_idle_open_time = true;

        }
        else if (door_idle_open_time)
        {
            if (!always_open)
            {
                StartCoroutine(OpenDoorFor(time));
            }
            door_idle_open_time = false;
        }
    }
    public bool DoorState()
    {
        return door_open;
    }

    public void CloseDoor()
    {
        if (!door_idle_open_time)
        {
            anim.SetTrigger("Door_OH");
            anim.SetTrigger("Door_H");
            anim.SetTrigger("Door_C");

            coli.enabled = true;
            door_open = false;
        }
    }
    public void StopOpenDoor(GameObject obj)
    {
        tiles_active.Remove(obj);
    }
    public IEnumerator CloseDoorIn(float xSec)
    {
        yield return new WaitForSeconds(xSec);
        coli.enabled = true;
    }
    
    public void StopTouchDoor()
    {
        if (counter_stations >= 0 && !permanent_changes)
        {
            counter_stations--;
        }
    }
    
    public void OpenDoor(GameObject obj, float xSeconds = 1)
    {
        if (tiles_active.Contains(obj))
        {
            return;
        }
        tiles_active.Add(obj);
        time = xSeconds;
    }
    public void ReallyOpenDoor()
    {
        coli.enabled = false;
    }
    public IEnumerator OpenDoorFor(float xSeconds)
    {
        yield return new WaitForSeconds(xSeconds);
        CloseDoor();
    }

}
