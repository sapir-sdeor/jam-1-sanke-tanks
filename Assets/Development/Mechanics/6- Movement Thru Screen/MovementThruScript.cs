using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityScreen = UnityEngine.Screen;
public class MovementThruScript : MonoBehaviour
{
    #region Inspector
    
    public List<GameObject> bandMembers;
    [SerializeField] private float maxXRight;
    [SerializeField] private float maxYUp;
    [SerializeField] private float minXLeft;
    [SerializeField] private float minYDown;
    private bool xTopLeft;
    private bool xTopRight;
    private bool yTopUp;
    private bool yTopDown;
    [SerializeField] private float speed;
    public Camera cam;

    #endregion
    // Start is called before the first frame update
    void Start()
    { 
        Vector3 upRight = Camera.main.ScreenToWorldPoint(new Vector3(UnityScreen.width, UnityScreen.height, 0));
       maxXRight = upRight.x;
       maxYUp = upRight.y;
       Vector3 DownLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
       minXLeft = DownLeft.x;
       minYDown = DownLeft.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > maxXRight)
        {
            xTopRight = true;
        }
        else if (transform.position.x < minXLeft)
        {
            xTopLeft = true;
        }
        if (transform.position.y > maxYUp)
        {
            yTopUp = true;
        }
        else if (transform.position.y < minYDown)
        {
            yTopDown = true;
        }
        
        // for (int i = 0; i < bandMembers.Count; i++)
        // {
            // if (bandMembers[i].transform.position.x >= maxXRight)
            // {
                // bandMembers[i].transform
            // }
        // }
    }

    void FixedUpdate()
    {
        var pos = GetComponent<Transform>().position;
        if (xTopRight)
        {
            transform.position = new Vector3(minXLeft + speed, pos.y, 0f);
            xTopRight = false;
        }
        else if(xTopLeft)
        {
            transform.position = new Vector3(maxXRight - speed, pos.y, 0f);
            xTopLeft = false;
        }

        if (yTopUp)
        {
            transform.position = new Vector3(pos.x, minYDown + speed, 0f);
            yTopUp = false;
        }
        else if (yTopDown)
        {
            transform.position = new Vector3(pos.x, maxYUp - speed, 0f);
            yTopDown = false;
        }
    }
}
