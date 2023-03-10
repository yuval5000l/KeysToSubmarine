using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreIndicator : MonoBehaviour
{
    public Vector2 orb_loc;
    private Rigidbody2D rb;
    [SerializeField] private float speed = 0.0009f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 dir = (transform.localPosition - new Vector3(orb_loc.x, orb_loc.y, 0)).normalized;
        rb.AddForce(-dir * speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
        //transform.position = Vector2.Lerp(transform.position, orb_loc, Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HUD")
        {
            gameObject.SetActive(false);
        }
    }
}
