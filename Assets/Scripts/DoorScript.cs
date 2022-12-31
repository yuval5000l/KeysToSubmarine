using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Collider2D coli;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator anim;

    

    void update()
    {
        
    }

    public void CloseDoor()
    {
        Debug.Log("CloseDoor()");

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
    
    
    public void OpenDoor()
    {
        //anim.Settrigger("OpenDoor")

        coli.enabled = false;
        sprite.enabled = false;

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
