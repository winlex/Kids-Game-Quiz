using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Loading : MonoBehaviour
{
    private float timer; //Таймер окна загрузки
    private bool status = true;
    
    void Update()
    {
        if(this.gameObject.activeSelf)
            this.GetComponent<SpriteRenderer>().DOFade(1, 3);
        if (this.gameObject.activeSelf && status)
        {
            timer = 2.5f;
            status = false;
        }
        timer -= Time.deltaTime;
        Debug.Log(Time.deltaTime);
        if (timer < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
