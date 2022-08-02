using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SeveralNodesMovement : MonoBehaviour
{
    public List<Transform> bodyParts = new List<Transform>();
    public float speed = 0.1f;
    public Vector2 direction;

    void Update()
    {
        Player1Move();
    }
    
    private void FixedUpdate()
    {
        //Move all nodes according each other, like snake
        for (int i = bodyParts.Count - 1; i > 0 ; i--)
        {
            bodyParts[i].position = bodyParts[i - 1].position ;
        }
        var pos = GetComponent<Transform>().position;
        transform.position = new Vector3(
            pos.x + direction.x,
            pos.y + direction.y, 0f);
    }
    

    private void Player1Move()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            direction = Vector2.right * speed;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            direction = Vector2.left * speed;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            direction = Vector2.down * speed;
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            direction = Vector2.up * speed;
    }
}
