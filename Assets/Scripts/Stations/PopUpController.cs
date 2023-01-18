using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator maAnimator;
    private string color = "None";
    void Start()
    {
        //maAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActivatePopUp()
    {
        gameObject.SetActive(true);
    }
    public void deActivatePopUp()
    {
        maAnimator.SetTrigger("Finish");
    }

    public void DeActivate()
    {
        gameObject.SetActive(false);
    }

    public string getColor()
    {
        return color;
    }
    public void setColor(string c)
    {
        color = c;
    }
}
