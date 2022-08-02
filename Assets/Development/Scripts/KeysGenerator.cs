using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScreen = UnityEngine.Screen;

public class KeysGenerator : MonoBehaviour
{

    #region Inspector
    
    [SerializeField] private List<GameObject> musiciansKeys;
    [SerializeField] private float keyTime;
    [SerializeField] private int minWaitTime;
    [SerializeField] private int maxWaitTime;
    
    #endregion

    #region fields

    private float _maxX ;
    private float _minX ;
    private float _maxY ;
    private float _minY ;

    #endregion
    
    void Start()
    {
        //borders of screen by unity screen code start
        Vector3 upRight = Camera.main.ScreenToWorldPoint(new Vector3(UnityScreen.width, UnityScreen.height, 0));
        _maxX = upRight.x;
        _maxY = upRight.y;
        Vector3 downLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        _minX = downLeft.x;
        _minY = downLeft.y;
        //borders of screen by unity screen code end
        StartCoroutine(AppearsInScreen());
    }
    
    private IEnumerator AppearsInScreen()
    {
        while (true)
        {
            Vector2 position = PickPosition();
            GameObject key = musiciansKeys[Random.Range(0, musiciansKeys.Count)];
            GameObject newObject = Instantiate(key, position, Quaternion.identity);
            newObject.GetComponent<SpriteRenderer>().sortingLayerName = "Objects";
            Destroy(newObject, keyTime);
            yield return new WaitForSeconds(Random.Range(minWaitTime,maxWaitTime));
        }
    }

    private Vector2 PickPosition()
    {
        while (true)
        {
            float posY = Random.Range(_minY,_maxY);
            float posX = Random.Range(_minX,_maxX);
            var intersecting = Physics2D.CircleCast(new Vector2(posX, posY), 1f,Vector2.zero);
            if (intersecting.collider != null)
            {
                continue;
            }
            return new Vector2(posX, posY);
        }

    }
}
