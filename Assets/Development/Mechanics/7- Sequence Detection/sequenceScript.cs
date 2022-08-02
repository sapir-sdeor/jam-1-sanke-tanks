using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sequenceScript : MonoBehaviour
{
    #region Inspector
    
    [SerializeField] private bool addingMember;
    public List<GameObject> bandMembers;
    public List<GameObject> musiciansSeq;
    [SerializeField] private float bandMembersGap;

    #endregion
    #region MonoBehaviour
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (checkForSequence(musiciansSeq))
        {
            print("the sequence is on");
        }
    }
    #endregion
    
    #region Methods
    
    public bool checkForSequence(List<GameObject> musicians)
    {
        int countSequence = 0;
        if (musicians.Count > bandMembers.Count)
            return false;
        for (int i = 0; i < musicians.Count; i++)
        {
            foreach (var t in bandMembers)
            {
                if (t.CompareTag(musicians[i].tag))
                    countSequence ++;
            }
        }
        return musicians.Count == countSequence;
    }
    #endregion
}
