using UnityEngine;

public class CrowdScript : MonoBehaviour
{
    [SerializeField] private GameObject waypoint;
    [SerializeField] private float speed;

    private Vector3 _target;
    private void Start()
    {
        _target = waypoint.gameObject.transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,_target,speed*Time.deltaTime);
    }
}
