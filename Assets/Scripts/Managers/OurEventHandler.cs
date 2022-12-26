using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class OurEventHandler : MonoBehaviour
{
    private static int NumberOfPlayers = 2;
    [SerializeField] private PlayerManager playerManager;
    private int levelNumber = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddPlayer()
    {
        if (NumberOfPlayers < 4)
        {
            NumberOfPlayers += 1;
        }
    }
    public void RemovePlayer()
    {
        if (NumberOfPlayers > 2)
        {
            NumberOfPlayers -= 1;
        }
    }

    public void Nextlevel(int numlevel = 0)
    {
        levelNumber += numlevel;
        //playermanager.updateplayers(numberofplayers);
        SceneManager.LoadScene("level " + levelNumber);
        levelNumber += 1;
    }

    public int getNumOfPlayers()
    {
        return NumberOfPlayers;
    }
}
