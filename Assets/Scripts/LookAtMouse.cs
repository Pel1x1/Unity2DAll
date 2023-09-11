using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    public float speed = 5f;

    private bool isFlipped = false;


    private void Update()
    {
        float currentRotation = Mathf.Abs(this.gameObject.transform.rotation.z);

        if (currentRotation > 0.707f && isFlipped == false)
        {
            FlipObj();

            isFlipped = true;
        }
        else if (currentRotation <= 0.707f && isFlipped == true)
        {
            FlipObj();

            isFlipped = false;
        }

        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime); 
    }

    private void FlipObj()
    {
        Vector3 newScale = this.transform.localScale;
        newScale.y *= -1;

        this.transform.localScale = newScale;
    }
}