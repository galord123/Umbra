using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static Vector3 GetMousePosition()
    {

        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            vec.z = 0f;

            return vec;
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            Touch touch = Input.GetTouch(0);

            Vector3 vec = Camera.main.ScreenToWorldPoint(touch.position);
            vec.z = 0f;

            return vec;
        }
        else
        {
            return new Vector3();
        }
    }

    public static double GetDistance(double x1, double y1, double x2, double y2)
    {
        return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
    }
}
