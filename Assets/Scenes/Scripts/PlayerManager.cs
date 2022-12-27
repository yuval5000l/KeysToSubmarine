using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private OurEventHandler gameManager;
    private PlayerController[] players = new PlayerController[4];
    private int players_in_game = 2;
    // Start is called before the first frame update
    void Start()
    {
        players_in_game = gameManager.getNumOfPlayers();
        int i = 0;
        foreach(PlayerController player in GetComponentsInChildren<PlayerController>())
        {
            players[i] = player;
            if (players_in_game <= i)
            {
                player.gameObject.SetActive(false);
            }
            i += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //public void AddPlayer()
    //{
    //    if (players_in_game < 4)
    //    {
    //        players[players_in_game].gameObject.SetActive(true);
    //        players_in_game += 1;
    //    }
    //}
    //public void RemovePlayer()
    //{
    //    if (players_in_game > 2)
    //    {
    //        players[players_in_game].gameObject.SetActive(false);
    //        players_in_game -= 1;
    //    }
    //}
    public void UpdatePlayers(int numPlayers)
    {
        players_in_game = numPlayers;
        foreach(PlayerController player in players)
        {
            player.gameObject.SetActive(true);
        }
    }
}
