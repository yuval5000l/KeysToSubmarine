using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerController[] players = new PlayerController[4];
    private int players_in_game = 3;
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        foreach(PlayerController player in GetComponentsInChildren<PlayerController>())
        {
            players[i] = player;
            i += 1;
        }
        players[3].gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddPlayer()
    {
        if (players_in_game < 4)
        {
            players[3].gameObject.SetActive(true);
            players_in_game += 1;
        }
    }
    public void RemovePlayer()
    {
        if (players_in_game > 3)
        {
            players[3].gameObject.SetActive(false);
            players_in_game -= 1;
        }
    }
}
