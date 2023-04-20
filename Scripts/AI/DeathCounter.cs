using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCounter : MonoBehaviour
{
    [SerializeField] public int death;
    [SerializeField] public int deathTimer;

    [SerializeField] public int deathEnti;
    [SerializeField] public int deathTimerEnti;

    [SerializeField] bool End;
    [SerializeField] bool Fail;
    [SerializeField] bool Started;
    // Start is called before the first frame update
    void Start()
    {
        Started = false;
        death = 0;
        deathTimer = 4;

        deathEnti = 0;
        deathTimerEnti = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (death >= deathTimer)
        {
            StartCoroutine(EndLevel());
        }

        if (Started)
        {
            if (deathEnti >= deathTimerEnti)
            {
                StartCoroutine(FailLevel());
            }
        }

    }

    public void ConsumeDeath()
    {
        death++;
    }

    public void ConsumeEntity()
    {
        deathEnti++;
    }

    public void SetDeath(int amount)
    {
        deathTimer = amount;
    }

    public void SetEnti()
    {
        deathTimerEnti += 1;
    }

    public void SetStarted()
    {
        Started = true;
    }

    IEnumerator EndLevel()//Loop until SceneChanges has Started
    {

        yield return new WaitForSeconds(10);

        if (deathTimer <= death)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//Go to the next scene

    }

    IEnumerator FailLevel()//Loop until SceneChanges has Started
    {

        yield return new WaitForSeconds(3);

        if (deathTimer <= death)
            SceneManager.LoadScene("Lose");//Go to the next scene

    }
}
