using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeUIscreen : MonoBehaviour
{


    [SerializeField] private GameObject[] CanvasScreen;
    [SerializeField] private GameObject Open, Close;

    // Update is called once per frame
    void Update()
    {
        GetSauce();
    }

    void Awake()
    {
        GetSauce();
        SetSubMenutoOff();

    }

    void SetSubMenutoOff()
    {
        foreach (GameObject Scn in CanvasScreen)
        {
            if (Scn.activeSelf)
            {
                Scn.SetActive(false);
            }

        }

        Open.SetActive(true);
    }

    public void disableMenu()
    {
        SetSubMenutoOff();
    }
    public void enableMenu()
    {
        foreach (GameObject Scn in CanvasScreen)
        {
            if (Scn.name == "GameMenu" || Scn.name == "Main")
            {
                Scn.SetActive(true);
            }

        }

        Open.SetActive(false);
    }

    void GetSauce()//If Menu Opener and closer disappears for some reason, find it again
    {
        if (!Open || !Close)
        {
            Open = GameObject.FindGameObjectWithTag("m_opener").GetComponent<GameObject>();
            Close = GameObject.FindGameObjectWithTag("m_closer").GetComponent<GameObject>();
        }
    }

}
