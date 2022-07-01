using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : Singleton<SwipeDetection>
{
    [SerializeField] float minimumDistance = .2f;
    [SerializeField] float maximumTime = 1f;
    [SerializeField, Range(0f, 1f)] float directionThreshold = .9f;
    [SerializeField] GameObject swipeTrail;

    private PlayerInputManager inputManager;

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
        inputManager = PlayerInputManager.Instance;
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
        swipeTrail.SetActive(true);
        swipeTrail.transform.position = position;
        swipeCoroutine = StartCoroutine(Trail());
    }

    private IEnumerator Trail()
    {
        while (true)
        {
            swipeTrail.transform.position = inputManager.PrimaryPosition();
            yield return null;
        }
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        swipeTrail.SetActive(false);
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
            StartCoroutine(SwipeDownActivationFreeze());
        }
        if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            // do smthng when swiped left
        }
        if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            // do smthng when swiped right
        }
    }

    private IEnumerator SwipeDownActivationFreeze()
    {
        DirectionDown?.Invoke(true);
        yield return new WaitForSeconds(1f);
        DirectionDown?.Invoke(false);
    }
}
