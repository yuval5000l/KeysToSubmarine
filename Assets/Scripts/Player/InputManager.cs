using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    [SerializeField] public bool action_pressed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //public void OnActionB(InputAction.CallbackContext context)
    //{
    //    if (context.started || context.performed)
    //    {
    //        //Debug.Log("Action");
    //        action_pressed = true;
    //    }
    //    if (context.canceled)
    //    {
    //        //Debug.Log("ActionCanceled");

    //        action_pressed = false;
    //    }
    //}
}
