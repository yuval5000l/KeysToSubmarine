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

    private int counter_stations = 0;

    void update()
    {
        
    }

    public void CloseDoor()
    {
        //Debug.Log("CloseDoor()");

        coli.enabled = true;
        sprite.enabled = true;
        //anim.Settrigger("CloseDoor")


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
        if (counter_stations >= 0)
        {
            counter_stations--;
        }
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

        //anim.Settrigger("OpenDoor")

        yield return new WaitForSeconds(xSeconds);
        CloseDoor();
    }

}
