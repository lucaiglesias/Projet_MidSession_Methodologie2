using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{

    //based on https://www.youtube.com/watch?v=ulxXGht5D2U&ab_channel=TurboMakesGames
   
    public int countdownTime;
    public TMP_Text countdownDisplay;
    public float fadeTime = 0.1f;
    private Color finalColor;
    public int pause = 0;

    public static CountDown Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        //Time.timeScale = 0;
        while(countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        countdownDisplay.text = "Kill !!!";
        pause = 1;
        Spawner.Instance.startCo();
        yield return new WaitForSeconds(1f);

        while(countdownDisplay.color.a > 0)
        {
            countdownDisplay.color = Color.Lerp(countdownDisplay.color, finalColor, fadeTime * Time.deltaTime);
            yield return null;
        }

        countdownDisplay.gameObject.SetActive(false);
    }
}
