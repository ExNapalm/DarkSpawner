using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Healthbar : MonoBehaviour
{

    [SerializeField] HealthScript HealthShealth;
    public Image hbar;
    public float target = 1;
    private Camera _cam;
    public float currentHealth;

    void Start()
    {
        if(!HealthShealth)
        HealthShealth = GetComponent<HealthScript>();
        if(!_cam)
        _cam = Camera.main;

        

    }

    // Update is called once per frame
    public void initializeHealthBar(float MaxHealth, float currenthealth)
    {
        hbar.fillAmount = MaxHealth;
        target = MaxHealth;//value of a full health bar


    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position = transform.position);
        hbar.fillAmount = Mathf.MoveTowards(hbar.fillAmount, target, Time.deltaTime);

        target = HealthShealth.SetcurrentHealth();//set next health change
    }
}
