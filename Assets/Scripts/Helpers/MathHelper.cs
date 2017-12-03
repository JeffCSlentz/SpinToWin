using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathHelper {
    public static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }

    public static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(-Mathf.Sin(radian), Mathf.Cos(radian));
    }

    public static Vector2 GameObjectsRotation(GameObject go)
    {
        return DegreeToVector2(go.transform.rotation.eulerAngles.z);
    }

    public static float AngleBetween(GameObject player, GameObject target)
    {
        return Vector2.SignedAngle(GameObjectsRotation(player), target.transform.position - player.transform.position);
    }
}
