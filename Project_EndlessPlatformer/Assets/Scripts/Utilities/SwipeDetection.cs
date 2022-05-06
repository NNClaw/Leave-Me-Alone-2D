using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : Singleton<SwipeDetection>
{
    [SerializeField] float minimumDistance = .2f;
    [SerializeField] float maximumTime = 1f;
    [SerializeField, Range(0f, 1f)] float directionThreshold = .9f;
    [SerializeField] GameObject trail;

    private InputManager inputManager;

    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;

    private Coroutine swipeCoroutine;

    public delegate void SwipeDirectionUp();
    public event SwipeDirectionUp DirectionUp;

    public delegate void SwipeDirectionDown(bool isCrouching);
    public event SwipeDirectionDown DirectionDown;

    private void Awake()
    {
        inputManager = InputManager.Instance;
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
        trail.SetActive(true);
        trail.transform.position = position;
        swipeCoroutine = StartCoroutine(Trail());
    }

    private IEnumerator Trail()
    {
        while (true)
        {
            trail.transform.position = inputManager.PrimaryPosition();
            yield return null;
        }
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        trail.SetActive(false);
        StopCoroutine(swipeCoroutine);
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if(Vector2.Distance(startPosition, endPosition) >= minimumDistance && 
            (endTime - startTime) <= maximumTime) {

            Vector2 direction = (endPosition - startPosition).normalized;

            SwipeDirection(direction);
        }
    }

    private void SwipeDirection(Vector2 direction)
    {
        if(Vector2.Dot(Vector2.up, direction) > directionThreshold)
        {
            DirectionUp?.Invoke();
        }
        if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            StartCoroutine(CrouchDelay());
        }
        if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {

        }
        if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {

        }
    }

    private IEnumerator CrouchDelay()
    {
        DirectionDown?.Invoke(true);
        yield return new WaitForSeconds(1f);
        DirectionDown?.Invoke(false);
    }
}
