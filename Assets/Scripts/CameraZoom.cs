using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 12f;
    [SerializeField] private bool ZoomActive = false;
    private Vector3 maZero = new Vector3(0, 0, -10);
    private Camera maCamera;
    [SerializeField] private Vector3 orb_loc = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        maCamera = FindObjectOfType<Camera>();
        orb_loc = FindObjectOfType<TrashCan>().transform.position;
        orb_loc.z = -10;
    }
    public void ActivateZoom(Vector3 loc)
    {
        ZoomActive = true;
        orb_loc = loc;
        orb_loc.z = -10;
    }

    public void DeActivateZoom()
    {
        ZoomActive = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (ZoomActive)
        {
            maCamera.orthographicSize = Mathf.Lerp(maCamera.orthographicSize, 3, zoomSpeed);
            maCamera.transform.position = Vector3.Lerp(maCamera.transform.position, orb_loc, zoomSpeed);
        }
        else
        {
            maCamera.orthographicSize = Mathf.Lerp(maCamera.orthographicSize, 5, zoomSpeed);
            maCamera.transform.position = Vector3.Lerp(maCamera.transform.position, maZero, zoomSpeed);
        }
    }
}
