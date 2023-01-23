using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderScript : MonoBehaviour
{
    private bool slideOut;
    // Start is called before the first frame update
    void Start()
    {
        slideOut = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(slideOut)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 21;
            gameObject.transform.localScale = gameObject.transform.localScale 
            - new Vector3(0, Time.deltaTime, 0);
        }
        if(gameObject.transform.localScale.y <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SlideOut()
    {
        slideOut = true;
    }
}
