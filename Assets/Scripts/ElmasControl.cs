using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ElmasControl : MonoBehaviour
{
    public GameObject elmasImagePrefab;
    public int UretilecekElmasImageSayisi;
    [SerializeField] Transform elmasinYeri;
    List<GameObject> elmasImages = new List<GameObject>();
    Vector3 elmasPos=new Vector3(0,0,0);
    int counter = 0;
    Sequence elmasAnimation;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < UretilecekElmasImageSayisi; i++)
        {
            GameObject elmasImage = Instantiate(elmasImagePrefab, elmasPos, Quaternion.identity,transform.parent);
            elmasImage.SetActive(false);
            elmasImages.Add(elmasImage);
        }
    }

    public void ElmasTopla (Vector3 pos,int elmasDeger)
    {      

        if (counter == 9)
        {            
            counter = 0;
        }
        elmasImages[counter].transform.position = pos;
        elmasImages[counter].SetActive(true);
        ElmasToplamaAnimasyonu(counter,elmasDeger);
        counter++;
        
    }


    private void ElmasToplamaAnimasyonu(int i, int elmasDeger)
    {
        elmasAnimation = DOTween.Sequence();

        elmasAnimation.Append(elmasImages[i].transform.DOMove(elmasinYeri.position, 1.5f)
            .SetEase(Ease.InFlash))
            .Join(elmasImages[i].transform.DOScale(Vector3.one * 0.5f, 1.5f))
            .OnComplete(() =>
            {                
                UIController.Instance.SetElmasCount(elmasDeger);
                elmasImages[i].SetActive(false);
                elmasImages[i].transform.DOScale(Vector3.one * 2, 1);
                
            });
    }
}
