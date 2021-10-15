using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace meshmath
{
        public class MeshMath : MonoBehaviour
    {
        public static Vector3 GetVectorFromAngle(float angle)
        {
            float angleRand = angle * (Mathf.PI / 180f);
            return new Vector3(Mathf.Cos(angleRand), Mathf.Sin(angleRand));
        }
        public static float GetAngleFromVectorFloat(Vector3 dir)
        {
            dir = dir.normalized;
            float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360;

            return n;
        }

        public static int GetAngleFromVectorInt(Vector3 dir)
        {
            dir = dir.normalized;
            float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360;

            return (int)n;
        }

    }
}
