using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class Restart : MonoBehaviour
{
    public UnityEvent restart;

    void Start()
    {
        restart?.Invoke();
    }

    public void RestartGame()
    {
        Destroy(this.gameObject);
    }
}
