
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Open : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene("Game");
    }
}
