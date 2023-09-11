using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyBulletsList;

    [SerializeField]
    private float startTimeBtwShots;
    private float timeBtwShots;

    void Start()
    {
        timeBtwShots = startTimeBtwShots;
    }

    void Update()
    {
        if (timeBtwShots <= 0)
        {
            int randomInt = Random.Range(0, enemyBulletsList.Length);
            Instantiate(enemyBulletsList[randomInt], transform.position, Quaternion.identity);

            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
