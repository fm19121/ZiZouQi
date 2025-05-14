using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor
{

    public static Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, float.MaxValue);
        return hit.point;
    }
}
