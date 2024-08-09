using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject winTxt;
    [SerializeField] GameObject loseTxt;
    [SerializeField] GameObject timerTxtObject;
    [SerializeField] TextMeshProUGUI timerTxt;
    [SerializeField] public bool isAlertON;
    [SerializeField] public bool isAlerted;
    [SerializeField] float alertTimer;

    private float elapsedTime = 0f;

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlertON) 
        {
            timerTxtObject.SetActive(true);
            StartCoroutine(AlertTimer());
        }
        ResetGame();
    }

    IEnumerator AlertTimer()
    {
        while (elapsedTime < alertTimer)
        {
            elapsedTime += Time.deltaTime;
            int remainingTime = Mathf.CeilToInt(alertTimer - elapsedTime)/60;
            timerTxt.text = remainingTime.ToString();
            yield return null;
        }
        isAlerted = true;
    }

    public void DoWin()
    {
        winTxt.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void DoLose()
    {
        loseTxt.SetActive(true);
        Time.timeScale = 0f;
    }

    void ResetGame()
    {
        if(Input.GetKeyUp(KeyCode.Escape)) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1f;
        }
    }
}
