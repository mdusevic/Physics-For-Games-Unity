using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public GameObject checkMark;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        checkMark.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!EndTimer())
        {
            float t = Time.time - startTime;

            float minutes = ((int)t / 60);
            float seconds = (t % 60);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    bool EndTimer()
    {
        int enemyCount = FindObjectsOfType<EnemyTarget>().Length;

        if (enemyCount == 0)
        {
            timerText.color = Color.green;
            Time.timeScale = 0f;
            checkMark.SetActive(true);
            return true;
        }
        return false;
    }
}
