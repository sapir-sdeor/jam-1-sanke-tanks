using UnityEngine;

public class MusicNotesUiScript : MonoBehaviour
{
    #region Inspector

    [SerializeField] private float speed;
    [SerializeField] private GameObject endPoint;
    [SerializeField] private GameObject startPoint;

    #endregion
    
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x -= speed * Time.deltaTime;
        transform.position = pos;
        if (transform.position.x <= endPoint.gameObject.transform.position.x)
            transform.position = startPoint.gameObject.transform.position;

    }
    
}