using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : TriggerObject, Radioactivity
{
    [SerializeField] private float radioActivityLevel;
    // Start is called before the first frame update
    void Start()
    {
        radioActivityLevel = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public new void DoAction(GameObject otherObject)
    {
        Destroy(otherObject);
    }

    public float getRadioActivityLevel()
    {
        return radioActivityLevel;
    }
}
