using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerChooser : MonoBehaviour
{
    PlayerChooserPap papa;
    private int player_num = -1;
    private bool in_place = false;
    
    // Start is called before the first frame update
    void Start()
    {
        papa = FindObjectOfType<PlayerChooserPap>();
    }
    public void setPlayerNum(int num)
    {
        player_num = num;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMove(InputValue value)
    {
        Vector2 movement_tmp = value.Get<Vector2>();
        //Debug.Log(movement_tmp);
        if (movement_tmp.x == 1)
        {
            in_place = papa.move_player(2, player_num);
        }
        if (movement_tmp.x == -1)
        {
            in_place = papa.move_player(0, player_num);
        }
        if (Mathf.Abs(movement_tmp.y) == 1)
        {
            in_place = papa.move_player(1, player_num);
        }
    }
    private void OnActionB(InputValue value)
    {
        if (value.isPressed)
        {
            if (in_place)
            {
                papa.joined(player_num);
            }
        }
    }
    public void MoveTo(Vector3 vect)
    {
        transform.position = vect;
    }

}
