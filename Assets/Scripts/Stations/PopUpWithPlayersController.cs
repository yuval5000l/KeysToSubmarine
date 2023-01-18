using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpWithPlayersController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] children = new GameObject[4];
    private int numOfchildren = 3;
    private bool redball = true;
    void Awake()
    {

        //int i = 0;
        //foreach (GameObject obj in GetComponentsInChildren<GameObject>())
        //{
        //    children[i] = obj;
        //    i++;
        //}
    }
    public void withoutRedBall()
    {
        children[0].SetActive(false);
        redball = false;
    }
    public void setNumOfChildren(int num)
    {
        numOfchildren = num;
        if (num == 1)
        {
            children[1].SetActive(false);
            children[2].SetActive(true);
            children[3].SetActive(false);
        }
        if (num == 2)
        {
            children[1].SetActive(true);
            children[2].SetActive(false);
            children[3].SetActive(true);
        }
        if (num == 3)
        {
            children[1].SetActive(true);
            children[2].SetActive(true);
            children[3].SetActive(true);
        }
    }

    public void setTriggerToChild(string trig)
    {
        bool delete = false;
        if (trig.Contains("Un"))
        {
            delete = true;
        }
        for (int i = 1; i < 4; i++)
        {
            if (delete)
            {
                if (children[i].activeSelf && trig.Contains(children[i].GetComponent<PopUpController>().getColor()))
                {
                    children[i].GetComponent<PopUpController>().setColor("None");
                    children[i].GetComponent<Animator>().SetTrigger(trig);

                    return;
                }
            }
            else
            {
                if (children[i].activeSelf && children[i].GetComponent<PopUpController>().getColor() == "None")
                {
                    children[i].GetComponent<PopUpController>().setColor(trig);
                    children[i].GetComponent<Animator>().SetTrigger(trig);

                    return;
                }
            }
        }
    }

    public void deActivatePopUp()
    {
        foreach(GameObject obj in children)
        {
            obj.GetComponent<PopUpController>().deActivatePopUp();
        }
    }
    public void ActivatePopUps()
    {
        if (redball)
        {
            children[0].SetActive(true);
        }
        setNumOfChildren(numOfchildren);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
