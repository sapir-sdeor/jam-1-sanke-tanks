using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityScreen = UnityEngine.Screen;

public class RandomBarriersGenerator : MonoBehaviour
{
    #region Inspector


    [SerializeField] private GameObject barriersParent;
    
    // borders of barriers
    [SerializeField] private float maxBariersU = 7f;
    [SerializeField] private float maxBariersD = -7f;
    [SerializeField] private float maxBariersL = -7f;
    [SerializeField] private float maxBariersR = 7f;
    

    // Number of items to generate
    [SerializeField] private int maxCons = 3;
    [SerializeField] private int minCons = 1;
    [SerializeField] private int maxSideWall = 3;
    [SerializeField] private int minSideWall = 1;
    [SerializeField] private int maxHorzWall = 3;
    [SerializeField] private int minHorzWall = 1;
    
    // Prefabs
    [SerializeField] private GameObject trafficBarrier;
    [SerializeField] private GameObject trafficCone;
    [SerializeField] private GameObject trafficBarrierSide;
    
    #endregion

    #region Fields

    private List<Vector3> gridPositons = new List<Vector3>(); 
    
    // borders of screen
    private float _maxX;
    private float _minX;
    private float _maxY;
    private float _minY;

    #endregion
    
    void Start()
    {
        Vector3 upRight = 
            Camera.main.ScreenToWorldPoint(new Vector3(UnityScreen.width, UnityScreen.height, 0));
        _maxX = upRight.x;
        _maxY = upRight.y;
        Vector3 downLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        _minX = downLeft.x;
        _minY = downLeft.y;
        
        
        InitialiseList();

        //Randomly generate trafficBarrier
        LayoutObjectAtRandom(trafficBarrier, minHorzWall, maxHorzWall);

        //Randomly generate trafficBarrierSide
        LayoutObjectAtRandom(trafficBarrierSide, minSideWall, maxSideWall);

        //Randomly generate cons
        LayoutObjectAtRandom(trafficCone, minCons, maxCons);
        
    }

    void InitialiseList()
    {
        for (int x = (int) _minX; x < _maxX; x++)
        {
            for (int y = (int) _minY; y < _maxY; y++)
            {
                //x in gridPositions,Enter the value of y
                gridPositons.Add(new Vector3(x, y, 0));
            }
        }
    }

    //Get random positions from gridPositions
    Vector3 RandomPosition()
    {
        //Declare randomIndex and randomly enter a number from the number of gridPositions
        int randomIndex = Random.Range(0, gridPositons.Count);

        //Declare randomPosition and set it to randomIndex of gridPositions
        Vector3 randomPosition = gridPositons[randomIndex];

        //Remove used gridPositions element
        gridPositons.RemoveAt(randomIndex);

        return randomPosition;
    }
    
    //Randomly place arguments on the Map(Enemies, walls, items)
    void LayoutObjectAtRandom(GameObject tile, int minimum, int maximum)
    {
        //Randomly determine the number of items to generate from the minimum and maximum values, and set it to objectCount.
        int objectCount = Random.Range(minimum, maximum);

        //Loop for a few minutes of the object to be placed
        int i = 0;
        while (i < objectCount)
        {
            //Get a random position where no object is currently placed
            Vector3 randomPosition = RandomPosition();
            if (checkIfPosEmpty(randomPosition))
            {
                Instantiate(tile, randomPosition,Quaternion.identity,barriersParent.transform);
                i++;
            }
        }
    }

    private bool checkIfPosEmpty(Vector3 targetPos)
    {
        
        var intersecting = 
            Physics2D.CircleCast(new Vector2(targetPos.x, targetPos.y), 2f,Vector2.zero);
        if (intersecting.collider != null)
        {
            return false;
        }
        
        if (((targetPos.x <= maxBariersL) || (targetPos.x >= maxBariersR)) ||
            ((targetPos.y >= maxBariersU) || (targetPos.y <= maxBariersD)))
            return false;

        return true;
    }
}