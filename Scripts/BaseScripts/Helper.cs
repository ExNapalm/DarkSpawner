using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour//List of helpful custom Methods
{
    public GameObject FindChildWithTag(GameObject parent, string tag)//Find specific child within gameobject
    {
        GameObject child = null;

        foreach (Transform transform in parent.transform)
        {
            if (transform.CompareTag(tag))
            {
                child = transform.gameObject;
                break;
            }
        }

        return child;
    }

    public GameObject FindChildGameObject(GameObject fromGameObject, string name)
    {
        Transform[] child = fromGameObject.transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in child) if (t.gameObject.name == name)
            {
               return t.gameObject;
            }
                
        return null;
    }
}
