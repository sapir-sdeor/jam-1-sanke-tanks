using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{
    #region Inspector

    //borders of screen
    
    [SerializeField] private float maxX = 9;
    [SerializeField] private float minX = -9;
    [SerializeField] private float maxY = 5;
    [SerializeField] private float minY = -5;

    // Band Musicians
    [SerializeField] private List<GameObject> musicians;
    
    #endregion
    
    
    

    //needed to create a prefab of the objects that appears
    [SerializeField] private GameObject collectPrefab;


    void Start()
    {
        StartCoroutine(AppearsInScreen());
    }
    
    private IEnumerator AppearsInScreen()
    {
        while (true)
        {
            float posY = Random.Range(minY,maxY);
            float posX = Random.Range(minX,maxX);
            Vector2 position = new Vector2(posX, posY);
            GameObject musician = musicians[Random.Range(0, musicians.Count)];
            GameObject newObject = Instantiate(musician, position, Quaternion.identity);
            Destroy(newObject, 10);  //Should be variables?
            yield return new WaitForSeconds(Random.Range(1,10));
        }
    }
}
