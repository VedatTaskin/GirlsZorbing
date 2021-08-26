using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinMenu : MonoBehaviour
{
    public Transform tasinacakElmas;
    public GameObject elmasToplamaObjesi;
    public int rewardCoin=10;
    public Text rewardCoinText;
    public Text coinCountText;
    public int coinCount=0;
    private float range;

    

    // Start is called before the first frame update
    void Start()
    {
        coinCount= PlayerPrefs.GetInt("ElmasScore");
        coinCountText.text = coinCount.ToString();        
        StartCoroutine(ElmasToplamaTekrarla(10));
    }


    IEnumerator ElmasToplamaTekrarla(int length)
    {
        for (int i = 0; i < length; i++)
        {
            range = Random.Range(-200, 200);
            Vector3 rastgelePos = tasinacakElmas.transform.position + Vector3.one * range;            
            yield return new WaitForSeconds(0.1f);
            elmasToplamaObjesi.GetComponent<ElmasControl>().ElmasTopla(rastgelePos, 5);
            rewardCoin -= 5;
            rewardCoinText.text = rewardCoin.ToString();

        }        
    }
}
