using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followX : MonoBehaviour
{
    public Transform objectToFollow;
    public bool followOnlyWithX = true;
    private float xChange;
    private Vector3 originalPosition;
    private float calcOriginalPosition;
    private void Start()
    {
        originalPosition = gameObject.transform.position;
    }
    private void Update()
    {
        if (followOnlyWithX)
        {
            transform.position = new Vector3(objectToFollow.position.x, originalPosition.y, originalPosition.z);
        }
    }

    public void MoveY(float amountmoved)
    {
        originalPosition.y -= amountmoved;
    }
}
