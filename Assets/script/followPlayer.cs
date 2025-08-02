// CameraFollow.cs
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public Transform target;       // The object to follow (your ball)
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;  
    }
    void LateUpdate()
    {
        transform.position = target.position + offset;
    }

}
