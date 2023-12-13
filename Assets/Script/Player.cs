using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public float LaneChangingOffset;
    public Action OnEndReached;
    
    public Animator animator;
    public enum Lane { Middle, Left, Right }
    Lane CurrentLane;
    public float ForwardSpeed = 10f; // Adjust the speed as needed
    private Vector3 touchStartPos;
    private Vector3 touchEndPos;
    private float swipeThreshold = 50f; // Adjust this threshold as needed
    private float lastTapTime;
    private float doubleTapTimeThreshold = 0.5f; // Adjust the threshold as needed

    private void Start()
    {
        // Get the Animator component (note the corrected GetComponent line)
        animator = GetComponent<Animator>();
        CurrentLane = Lane.Middle;
        Obstacle.OnCollisionWithPlayer += GameOver;
    }
    private void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Check for the beginning of a touch
            if (touch.phase == TouchPhase.Began)
            {
                // Assuming GameManager.IsGameStarted is a boolean variable in the GameManager script
                GameManager.IsGameStarted = true;

                // Set the "IsGameStarted" parameter in the animator to true
                animator.SetBool("IsGameStarted", true);

                // Check for a double tap
                if (Time.time - lastTapTime < doubleTapTimeThreshold)
                {
                    // Double tap detected, trigger jump
                    animator.SetTrigger("Jump");
                }

                lastTapTime = Time.time;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                // Check for a swipe up for jumping
                if (touch.deltaPosition.y > 0)
                {
                    animator.SetTrigger("Jump");
                }

                // Check for a left or right swipe
                float swipeDistance = touch.deltaPosition.x;

                // Check for a left swipe
                if (swipeDistance < -swipeThreshold)
                {
                    SwipeLeft();
                }
                // Check for a right swipe
                else if (swipeDistance > swipeThreshold)
                {
                    SwipeRight();
                }
            }
        }
    }


    //private void Update()
    //{
    //    // Check for touch input
    //    if (Input.touchCount > 0)
    //    {
    //        Touch touch = Input.GetTouch(0);

    //        // Check for the beginning of a touch
    //        if (touch.phase == TouchPhase.Began)
    //        {
    //            // Assuming GameManager.IsGameStarted is a boolean variable in the GameManager script
    //            GameManager.IsGameStarted = true;

    //            // Set the "IsGameStarted" parameter in the animator to true
    //            animator.SetBool("IsGameStarted", true);
    //        }

    //        // Check for a tap or swipe up for jumping
    //        if (touch.phase == TouchPhase.Began || (touch.phase == TouchPhase.Moved && touch.deltaPosition.y > 0))
    //        {
    //            animator.SetTrigger("Jump");
    //        }
    //    }

    //    // Check for touch input
    //    if (Input.touchCount > 0)
    //    {
    //        Touch touch = Input.GetTouch(0);

    //        if (touch.phase == TouchPhase.Began)
    //        {
    //            touchStartPos = touch.position;
    //        }
    //        else if (touch.phase == TouchPhase.Ended)
    //        {
    //            touchEndPos = touch.position;
    //            float swipeDistance = touchEndPos.x - touchStartPos.x;

    //            // Check for a left swipe
    //            if (swipeDistance < -swipeThreshold)
    //            {
    //                SwipeLeft();
    //            }
    //            // Check for a right swipe
    //            else if (swipeDistance > swipeThreshold)
    //            {
    //                SwipeRight();
    //            }
    //        }
    //    }
    //}

    private void SwipeLeft()
    {
        if (CurrentLane == Lane.Middle)
        {
            // Move the player left using DOTween
            Vector3 targetPosition = transform.position - new Vector3(LaneChangingOffset, 0, 0);
            transform.DOMove(targetPosition, 0.1f).SetEase(Ease.Linear);
            CurrentLane = Lane.Left;
        }
        else if (CurrentLane == Lane.Right)
        {
            // Move the player left using DOTween
            Vector3 targetPosition = transform.position - new Vector3(LaneChangingOffset, 0, 0);
            transform.DOMove(targetPosition, 0.1f).SetEase(Ease.Linear);
            CurrentLane = Lane.Middle;
        }
    }

    private void SwipeRight()
    {
        if (CurrentLane == Lane.Middle)
        {
            // Move the player right using DOTween
            Vector3 targetPosition = transform.position + new Vector3(LaneChangingOffset, 0, 0);
            transform.DOMove(targetPosition, 0.1f);
            CurrentLane = Lane.Right;
        }
        else if (CurrentLane == Lane.Left)
        {
            // Move the player right using DOTween
            Vector3 targetPosition = transform.position + new Vector3(LaneChangingOffset, 0, 0);
            transform.DOMove(targetPosition, 0.1f);
            CurrentLane = Lane.Middle;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "RoadSpawnerTag")
        {
            if (OnEndReached != null)
                OnEndReached();
        }
       
    }

    void GameOver()
    {
        animator.SetBool("IsGameEnded", true);
        //animator.SetBool("IsGameStarted", false);
        GameManager.instance.GameOver();
    }
    private void OnDisable()
    {
        Obstacle.OnCollisionWithPlayer -= GameOver;
    }
}
