using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Cell : MonoBehaviour
{
    private GameObject Cells;
    private GameObject sprite;
    private bool Done = false;
    private float time = 1.8f; //Задержка для проигрывания анимация, а потом для смены уровня
    
    void Start()
    {
        Cells = GameObject.Find("Cells");
        sprite = this.GetComponent<Transform>().GetChild(1).gameObject;
        if (Cells.GetComponent<Generate>().Difficulty == difficulty.easy)
            sprite.GetComponent<Transform>().DOPunchScale(new Vector3(0.2f, 0.2f, 0), 2.0f, vibrato: 2, elasticity: 0);
    }

    void Update()
    {
        if (Done)
            time -= Time.deltaTime;
        if (time < 0)
            Cells.GetComponent<Generate>().NextLevel();
    }

    void OnMouseDown()
    {
        if(Cells.GetComponent<Generate>().CheckAnswer(this.name))
        {
            sprite.GetComponent<Transform>().DOPunchScale(new Vector3(0.2f, 0.2f, 0), 2.0f, vibrato: 2, elasticity: 0);
            Done = true;
        }
        else
        {
            sprite.GetComponent<Transform>().DOShakePosition(2.0f, strength: new Vector3(0.2f, 0, 0), vibrato: 5, randomness: 1, snapping: false, fadeOut: true);
        }
    }
}
