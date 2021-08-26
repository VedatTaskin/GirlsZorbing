using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIController : MonoBehaviour
{
    private static UIController instance;
    public static UIController Instance { get => instance; set => instance = value; }


    [Header("GamePlay variables")]
    [SerializeField] GameObject gamePlayMenu;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI elmasCounterText;    
    [SerializeField] TextMeshProUGUI ballPickedUpText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI girlCountText;
    [SerializeField] GameObject warningToTouchScreen;
    [SerializeField] GameObject plusOneText;
    [SerializeField] GameObject emojiImage;
 


    [Header("LooseMenu variables")]
    [SerializeField] GameObject looseMenu;   

    [Header("WinMenu variables")]
    [SerializeField] GameObject winMenu;
    [SerializeField] Text winMenuElmasCountText;

    IEnumerator waitForWarning;

    private int elmasScore = 0;
    private int newBallCount = 0;
    private int newGirlCount = 0;
 

    private void Awake()
    {
        if (instance == null)
            instance = this;

        waitForWarning = WaitForWarning();
        elmasScore= PlayerPrefs.GetInt("ElmasScore");
        elmasCounterText.text = elmasScore.ToString();

    }



    internal void SetSliderValue(float value)
    {
        slider.value = value;
        slider.maxValue = 1f;        
    }

    internal void SetElmasCount(int elmasDeger)
    {
        if (elmasDeger == 1)
        {
            elmasScore += elmasDeger;
            elmasCounterText.text = elmasScore.ToString();
        }
        if (elmasDeger == 5)
        {
            elmasScore += elmasDeger;
            winMenuElmasCountText.text = elmasScore.ToString();
            PlayerPrefs.SetInt("ElmasScore", elmasScore);
        }        
    }


    internal void SetBallPickedUp(int ballCount)
    {
        newBallCount += ballCount;
        ballPickedUpText.text = newBallCount.ToString();
    }

    internal void SetLevelText(int seviye)
    {
        seviye = seviye + 1;
        levelText.text = seviye.ToString();
    }

    internal void SetGirlCount(int girlCount)
    {
        newGirlCount += girlCount;
        girlCountText.text = newGirlCount.ToString();
    }
    internal int GetGirlCount()
    {
        return newGirlCount;
    }
    internal void TouchWarning(bool onClick)
    {        
        if (onClick)
        {            
            StopCoroutine(waitForWarning);
            warningToTouchScreen.SetActive(false);
        }
        else if(!onClick) 
        {
            StartCoroutine(waitForWarning);            
        }
    }

    internal void LooseMenu()
    {        
        StartCoroutine(WaitForLooseMenu());
    }

    public void ShowPlusOne(Vector3 position)
    {
        plusOneText.transform.position =Camera.main.WorldToScreenPoint(position);        
        StartCoroutine(WaitForPlusOneText());
    }


    internal void WinMenu()
    {
        StartCoroutine(WaitForWinMenu());        
    }


    IEnumerator WaitForWarning()
    {
        yield return new WaitForSeconds(2);
        warningToTouchScreen.SetActive(true);             
        
    }

    IEnumerator WaitForLooseMenu()
    {
        yield return new WaitForSeconds(1.5f);
        looseMenu.SetActive(true);
        gamePlayMenu.SetActive(false);
    }

    IEnumerator WaitForWinMenu()
    {
        yield return new WaitForSeconds(1.5f);
        winMenu.SetActive(true);
        PlayerPrefs.SetInt("ElmasScore", elmasScore);
        gamePlayMenu.SetActive(false);      
    }


    IEnumerator WaitForPlusOneText()
    {
        plusOneText.SetActive(true);        
        yield return new WaitForSeconds(0.5f);        
        plusOneText.SetActive(false);
    }
    
}
