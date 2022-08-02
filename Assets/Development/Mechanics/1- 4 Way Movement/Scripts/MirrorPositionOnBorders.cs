using UnityEngine;

public class MirrorPositionOnBorders : MonoBehaviour
{
    [SerializeField] private float maxX;
    [SerializeField] private float minX;
    [SerializeField] private float maxY;
    [SerializeField] private float minY;
    void Update()
    { 
        //go throw the screen by change the positions
        var tran = transform;
        Vector3 pos = tran.position;
        if (pos.x < minX)
            pos.x = maxX;
        else if (pos.x > maxX)
            pos.x = minX;
        else if (pos.y < minY)
            pos.y = maxY;
        else if (pos.y > maxY)
            pos.y = minY;
        transform.position = pos;
    }
}
