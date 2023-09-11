using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFollowing : MonoBehaviour
{
    public Transform FollowedGameObject;

    void Update()
    {
        this.transform.position = new Vector3(FollowedGameObject.position.x, this.transform.position.y, this.transform.position.z);
    }
}
