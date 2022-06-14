using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    public static void checkDespawn(GameObject obj)
    {
        if (!checkObjectOnScreen(obj, 2))
        {
            Destroy(obj);
        }
    }
    public static bool checkObjectOnScreen(GameObject obj, float deadSpace)
    {
        Vector3 screenPoint = FindObjectOfType<Camera>().WorldToViewportPoint(obj.transform.position);
        if ((screenPoint.x > 0 - deadSpace && screenPoint.x < 1 + deadSpace && screenPoint.y > 0 - deadSpace && screenPoint.y < 1 + deadSpace))
        {
            return true;
        }
        return false;
    }

    public static float getAngle(Vector3 point1, Vector3 point2)
    {
        float instantiationAngle;
        Vector2 A = point1;
        Vector2 B = point2;
        Vector2 C = new Vector2(B.x + 10, B.y);
        instantiationAngle = Vector2.SignedAngle(A - B, C - B);
        if (instantiationAngle < 0)
        {
            instantiationAngle *= -1;
        }
        else
        {
            instantiationAngle = 360 - instantiationAngle;
        }
        return instantiationAngle;
    }
}
