using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathLib : MonoBehaviour
{
    
    static float minAngle = -Mathf.PI/4;
    static float maxAngle = Mathf.PI/4;
    static float minRadius = 100;
    static float maxRadius = 200;

    public static Vector3 getEnemyPosition()
    {
        float theta = Random.Range(minAngle, maxAngle);
        float r = Random.Range(minRadius, maxRadius);

        float x = r * Mathf.Sin(theta);
        float z = r * Mathf.Cos(theta);

        return new Vector3(-x, 0, z);
    }

}
