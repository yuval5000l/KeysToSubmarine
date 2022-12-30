using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Collider2D coli;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator anim;

    // private Vector3 pos;
    

    // void Start()
    // {
    //     coli.enabled = true;
    //     pos = transform.position;
    // }

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
        Debug.Log("CloseDoorIn()");
        yield return new WaitForSeconds(xSec);

        coli.enabled = true;
        sprite.enabled = true;


    }
    
    
    public void OpenDoor()
    {
        Debug.Log("OpenDoor()");

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
