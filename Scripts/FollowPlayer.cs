using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Vector3 offSet;

    [SerializeField] private float smoothSpeed = 0.125f;

    [SerializeField] [Range(0.01f , 1f)]

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
       Vector3 desiredPosition = target.position + offSet;
       transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
    }
}
