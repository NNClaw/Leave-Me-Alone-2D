using UnityEngine;

public class Utils
{
    public static Vector2 ScreenToWorldPoint(Vector2 position, Camera camera)
    {
        return camera.ScreenToWorldPoint(position);
    }
}
