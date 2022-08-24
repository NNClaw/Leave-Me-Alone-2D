using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static Vector2 ScreenToWorldPoint(Vector2 position, Camera camera)
    {
        return camera.ScreenToWorldPoint(position);
    }

    /// <summary>
    /// Manipulates index so that it resets and not trigger the IndexOutOfRange exception
    /// </summary>
    /// <typeparam name="T">Type of list</typeparam>
    /// <param name="indexToManipulte">An index value, which should be used for manipulation</param>
    /// <param name="listToManipulate">The list, which will be manipulated</param>
    /// <returns></returns>
    public static int IndexManipulator<T>(int indexToManipulte, List<T> listToManipulate)
    {
        indexToManipulte = (indexToManipulte >= listToManipulate.Count - 1) ? 0 : ++indexToManipulte;

        return indexToManipulte;
    }
}
