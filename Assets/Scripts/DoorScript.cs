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

    private void Awake()
    {
        int counter = 0;
        foreach(SpriteRenderer sprite1 in  GetComponentsInChildren<SpriteRenderer>())
        {
            if (counter > 0)
            {
                cables.Add(sprite1);

            }
            counter++;
        }
        if (cables.Count != 0)
        {
            default_color = cables[0].color;
        }

    }
    void update()
    {
    }
    public bool DoorState()
    {
        return door_open;
    }
    public void CloseDoor()
    {
        //Debug.Log("CloseDoor()");

        coli.enabled = true;
        sprite.enabled = true;
        //anim.Settrigger("CloseDoor")
        counter_stations = 0;
        door_open = false;
        foreach (SpriteRenderer sprite1 in cables)
        {
            sprite1.color = default_color;
        }
    }
    
    public IEnumerator CloseDoorIn(float xSec)
    {
        yield return new WaitForSeconds(xSec);
        //anim.Settrigger("CloseOpenDoor")

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
        //anim.Settrigger("OpenDoor")
        counter_stations++;
        //Debug.Log(counter_stations);

        if (always_open)
        {
            if (counter_stations == numOfStations)
            {
                coli.enabled = false;
                sprite.enabled = false;
                door_open = true;
                foreach (SpriteRenderer sprite1 in cables)
                {
                    sprite1.color = new Color(1f, 1f, 1f, 1f);
                }
            }
        }
        else
        {
            if (counter_stations == numOfStations)
            {
                StartCoroutine(OpenDoorFor(xSeconds));
            }
        }

    }
    
    public IEnumerator OpenDoorFor(float xSeconds)
    {

        coli.enabled = false;
        sprite.enabled = false;
        door_open = true;
        //anim.Settrigger("OpenDoor")
        foreach (SpriteRenderer sprite1 in cables)
        {
            sprite1.color = new Color(1f, 1f, 1f, 1f);
        }
        yield return new WaitForSeconds(xSeconds);
        CloseDoor();
    }

}
