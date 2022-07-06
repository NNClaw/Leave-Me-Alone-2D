using UnityEngine;

internal interface ISwipeDetection
{
    event SwipeDetection.SwipeDirectionUp DirectionUp;
    event SwipeDetection.SwipeDirectionDown DirectionDown;

    public void DetectSwipe();
    public void SwipeDirection(Vector2 direction);
    public void SwipeEnd(Vector2 position, float time);
    public void SwipeStart(Vector2 position, float time);
}