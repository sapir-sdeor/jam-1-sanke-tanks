using System;
using System.Collections.Generic;
using UnityEngine;

public class BandManager1 : MonoBehaviour
{
    #region Inspector

    
    [SerializeField] private bool addingMember;
    public List<GameObject> musicians;
    public List<GameObject> bandMembers;
    [SerializeField] private float bandMembersGap;

    #endregion

    #region Methods

    private void addMemeber(GameObject bandMember)
    {
        addingMember = true;
        var lastBandMemberPos = bandMembers[bandMembers.Count-1].gameObject.transform.position;
        var xOffset = lastBandMemberPos.x + musicians[0].gameObject.transform.localScale.x + bandMembersGap;
        var newMemberPos = lastBandMemberPos;
        newMemberPos.x = xOffset;
        GameObject temp = Instantiate(bandMember,newMemberPos,transform.rotation,transform);
        bandMembers.Add(temp);
    }
    
    public void addMember1()
    {
        addMemeber(musicians[0]);
    }
    
    public void addMember2()
    {
        addMemeber(musicians[1]);
    }
    
    public void addMember3()
    {
        addMemeber(musicians[2]);
    }
    private int CountNumberOfSequence()
    {
        var countSequence = 1;
        for (var i = bandMembers.Count - 1; i > 0; i--)
        {
            if (bandMembers[i].CompareTag(bandMembers[i - 1].tag))
                countSequence++;
            else
                break;
        }
        return countSequence;
    }
    #endregion

    #region MonoBehaviour

    private void Start()
    {
        var transform1 = transform;
        GameObject temp = Instantiate(musicians[0],transform1.position,transform1.rotation,transform1);
        bandMembers.Add(temp);
    }

    private void Update()
    {
        if (addingMember)
        {
            var count = CountNumberOfSequence();
            print("number of sequence: " + count);
            addingMember = false;
        }
    }
    #endregion
}
