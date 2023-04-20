using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{//Handels UI ButtonPressing
    [SerializeField] public string clipdown_name, clipup_name;
    [SerializeField] private AudioClip _clipdown, _clipup;

    [SerializeField] private AudioSource _source;

    public SoundManager SM;

    //Plays a sound when the button is pressed. Will be used for all buttons

    private void Awake()
    {
        SM = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        GetSauce();

    }
    void Start()
    {
        
    }
    
    void Update()
    {
        FindSounds();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _source.PlayOneShot(_clipdown);
        //ClickedDebug();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _source.PlayOneShot(_clipup);
        ClickedDebug();
    }

    public void ClickedDebug()
    {
        Debug.Log("Clicked " + name + "!");
    }

    void FindSounds()
    {
        _clipdown = SM.GetSFX(clipdown_name).sfx;
        _clipup = SM.GetSFX(clipup_name).sfx;
    }

    void GetSauce()//If Sound Manager disappears for some reason, find it again
    {
        if (!SM || !_source)
        {
            SM = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
            _source = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioSource>();
        }
    }

}
