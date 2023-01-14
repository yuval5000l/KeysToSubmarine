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
        string[] colors = new string[4] {"Pink", "Blue", "Orange", "Yellow"};
        players_in_game = gameManager.getNumOfPlayers();
        int i = 0;
        foreach(PlayerController player in GetComponentsInChildren<PlayerController>())
        {
            players[i] = player;
            player.setColor(colors[i]);
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
    //    public bool UpdatePlayerControls(int player_num, List<KeyCode> new_controls)
    //    {
    //        if (new_controls.Count != 5 || player_num < 0 || player_num > 3)
    //        {
    //            return false;
    //        }
    //        foreach (PlayerController player in players)
    //        {
    //            foreach(KeyCode key in player.getListControls())
    //            {
    //                if (new_controls.Contains(key))
    //                {
    //                    return false;
    //                }
    //            }
    //        }
    //        players[player_num].updateListControls(new_controls);
    //        return true;
    //    }

    //    public List<List<KeyCode>> GetPlayersControls()
    //    {
    //        List<List<KeyCode>> playersControls= new List<List<KeyCode>>(4);
    //        foreach (PlayerController player in players)
    //        {
    //            playersControls.Add(player.getListControls());
    //        }
    //        return playersControls;
    //    }
}
