using UnityEngine;

public class MovementScript : MonoBehaviour
{
    #region Inspector

    [SerializeField] private float speed = 1;
    [SerializeField] private PlayerMovementKeys playerKeys;

    #endregion

    #region Fields

    private Vector2 _direction;

    #endregion
    
    void Update()
    {
        if (Input.GetKeyDown(playerKeys.Up))
            _direction = Vector2.up * speed;
        else if (Input.GetKeyDown(playerKeys.Down))
            _direction = Vector2.down * speed;
        else if (Input.GetKeyDown(playerKeys.Left))
            _direction = Vector2.left * speed;
        else if (Input.GetKeyDown(playerKeys.Right))
            _direction = Vector2.right * speed;
    }
    private void FixedUpdate()
    {
        var pos = GetComponent<Transform>().position;
        transform.position = new Vector3(
            pos.x + _direction.x,
            pos.y + _direction.y,
            0f);
        
    }
}