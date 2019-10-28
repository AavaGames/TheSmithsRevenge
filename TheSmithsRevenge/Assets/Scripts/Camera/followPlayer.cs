using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public Transform objectToFollow;
    public bool followOnlyWithX = true;
    private float xChange = 0f;
    private Vector3 originalPosition;
    private float calcOriginalPosition = 0f;
    private float destination = 0f;
    private float destinationY = 0f;
    private int transition = -1;
    private int transitionY = -1;
    public float camTransitionSpeed = 2f;
    public bool done = true;
    private void Start()
    {
        originalPosition = gameObject.transform.position;
        calcOriginalPosition = (objectToFollow.position.x + originalPosition.x) * -1f;
    }
    private void Update()
    {
        xChange = 0f;
        if (followOnlyWithX)
        {
            xChange = calcOriginalPosition + objectToFollow.position.x;
            transform.position = new Vector3(xChange, originalPosition.y, originalPosition.z);
        }

        if (transition == 0)
        {
            if (calcOriginalPosition < destination)
            {
                calcOriginalPosition += camTransitionSpeed;
                if (calcOriginalPosition >= destination)
                {
                    calcOriginalPosition = destination;
                    transition = -1;
                    done = true;
                }
            }
        }
        else if (transition == 1)
        {
            if (calcOriginalPosition > destination)
            {
                calcOriginalPosition -= camTransitionSpeed;
                if (calcOriginalPosition <= destination)
                {
                    calcOriginalPosition = destination;
                    transition = -1;
                    done = true;
                }
            }
        }

        if (transitionY == 0)
        {
            if (originalPosition.y > destinationY)
            {
                originalPosition.y -= camTransitionSpeed;
                if (originalPosition.y <= destinationY)
                {
                    originalPosition.y = destinationY;
                    transitionY = -1;
                }
            }
        }
        //not working and not needed
        else if (transitionY == 1)
        {
            if (originalPosition.y > destinationY)
            {
                originalPosition.y += camTransitionSpeed;
                if (originalPosition.y <= destinationY)
                {
                    originalPosition.y = destinationY;
                    transitionY = -1;
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            MoveY(-35,true);
        }
    }

    public void AimDirection(bool aimRight)
    {
        if (aimRight == true)
        {
            destination = calcOriginalPosition * -1f;
            transition = 0;
            done = false;
        }
        else
        {
            destination = calcOriginalPosition * -1f;
            transition = 1;
            done = false;
        }
    }
    public void MoveY(float amountmoved, bool movingDown)
    {
        if (movingDown == true)
        {
            destinationY = originalPosition.y + amountmoved;
            transitionY = 0;
        }
        else
        {
            destinationY = originalPosition.y + amountmoved;
            transitionY = 1;
        }
    }
}
