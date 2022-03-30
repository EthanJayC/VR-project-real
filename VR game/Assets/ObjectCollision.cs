using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    void OnCollisionStay(UnityEngine.Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Obstacle")
        {
            Debug.Log("We hit an obstacle");
        }
    }
}
