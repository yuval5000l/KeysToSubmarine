using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTile : MonoBehaviour
{
    [SerializeField] private int numberOfSeconds = 5;
    [SerializeField] private DoorScript door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player")
        {
            door.OpenDoor(numberOfSeconds);
        }
    }

    // void OnTriggerExit2D(Collider2D coll)
    // {
    //     if(coll.tag == "Player")
    //     {
    //         door.CloseDoor();
    //     }
    // }
}
