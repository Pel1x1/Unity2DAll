using UnityEngine;

public class ParallaxBehaviour : MonoBehaviour
{
    [SerializeField] Transform followingTarget;
    [SerializeField, Range(0f, 1f)] float parallaxStrength = .1f;
    [SerializeField] bool disableVerticalParallax;

    Vector3 targetPreviousPosition;

    void Start()
    {
        if (!followingTarget)
            followingTarget = Camera.main.transform;
        targetPreviousPosition = followingTarget.position;
    }

    void Update()
    {
        Vector3 delta = followingTarget.position - targetPreviousPosition;

        if (disableVerticalParallax)
            delta.y = 0;
        targetPreviousPosition = followingTarget.position;
        transform.position += delta * parallaxStrength;
    }
}