using System;
using UnityEngine;

public class SceneScript : MonoBehaviour
{
    [SerializeField] private int speed;
    void Update()
    {
        var transform1 = transform;
        Vector3 curPos = transform1.position;
        if (curPos.x <= -9)
        {
            curPos.x = 9;
        }
        else
        {
            curPos.x -= speed * Time.deltaTime; 
        }
        transform1.position = curPos;
    }
}
