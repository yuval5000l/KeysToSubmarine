using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpController : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator maAnimator;
    void Start()
    {
        maAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void deActivatePopUp()
    {
        maAnimator.SetTrigger("Finish");
    }

    public void DeActivate()
    {
        gameObject.SetActive(false);
    }
}
