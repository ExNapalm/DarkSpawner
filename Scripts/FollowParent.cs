using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowParent : MonoBehaviour
{

    [SerializeField] private Transform parentPosition;


    // Update is called once per frame
    void Update()
    {
        Vector3 reducer = new Vector3(5, 2, 0);
        transform.position = parentPosition.position;
        transform.position -= reducer;//Make the object follow behind the parent


    }
}
