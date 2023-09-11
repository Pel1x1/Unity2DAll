using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;

    private GameObject player;
    private Rigidbody2D rb;

    private PlayerMovement playerScript;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.Find("Player");

        Vector3 direction = player.transform.position - transform.position;

        SoundManager.PlaySound(SoundManager.Sound.EnemyShoot);
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;

        playerScript = player.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SoundManager.PlaySound(SoundManager.Sound.PlayerDamage);

            playerScript.MakePhrase();
        }

        Destroy(this.gameObject);
    }
}
