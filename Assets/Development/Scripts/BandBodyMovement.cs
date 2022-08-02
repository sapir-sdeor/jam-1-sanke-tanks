using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandBodyMovement : MonoBehaviour
{
    private BandBodyManager _bandBodyManager;
    private BandManager _bandManager;
    [SerializeField] private BandHeadMovement bandHeadMovement;
    [SerializeField] private int gap;

    private void Start()
    {
        _bandBodyManager = GetComponent<BandBodyManager>();
        _bandManager = GetComponentInParent<BandManager>();
    }

    public void MoveBandBody()
    {
        int index = 1;
        foreach (var bodyPart in _bandBodyManager.BandMembers)
        {
            var oldPos = bodyPart.transform.position;
            Vector3 point =
                _bandManager.PositionsHistory[Mathf.Min(index * gap, _bandManager.PositionsHistory.Count - 1)];
            bodyPart.transform.position = point;
            if (oldPos.x > point.x) //moving left
                bodyPart.GetComponent<SpriteRenderer>().flipX = false;
            else if (oldPos.x < point.x) //moving right
                bodyPart.GetComponent<SpriteRenderer>().flipX = true;
            index++;
        }
    }
    
    
    
}