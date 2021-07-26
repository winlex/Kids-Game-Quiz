using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeInFinish : MonoBehaviour
{
    void Update()
    {
        if(this.gameObject.activeSelf)
            this.GetComponent<SpriteRenderer>().DOFade(0.3f, 3);
    }
}
