using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{

    Camera playerCam;

    private RaycastHit hiter;
    public LayerMask Tilemask;
    // Start is called before the first frame update
    void Start()
    {
        playerCam = Camera.main;
        Debug.Log("Current Active Camera: " + playerCam.name);

    }

    // Update is called once per frame
    void Update()
    {
        FindEntity();

    }

    public void FindEntity()
    {
        Vector3 mousePos = Input.mousePosition;

        mousePos.z = 100f;

        mousePos = playerCam.ScreenToWorldPoint(mousePos);

        Debug.DrawRay(transform.position, mousePos - transform.position, Color.red);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.DrawRay(ray.direction, mousePos, Color.red);
            if (Physics.Raycast(ray, out hit, 1000, Tilemask))
            {
                Debug.Log(hit.transform.name);
            }
            else
            {
            }
        }
        

    }
}

