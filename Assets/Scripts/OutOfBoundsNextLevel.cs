using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsNextLevel : MonoBehaviour
{
    [SerializeField] private OurEventHandler GM;
    private int counter = 0;
    private int NumPlayers;
    // Start is called before the first frame update
    void Start()
    {
        NumPlayers = GM.getNumOfPlayers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        counter++;
        if (counter == NumPlayers)
        {
            GM.Nextlevel();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        counter--;
    }
}
