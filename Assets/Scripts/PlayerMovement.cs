using System.Collections;
using UnityEngine;
using TMPro;
using Additional;

public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    public float jump;

    private float Move;

    public Rigidbody2D rb;

    [SerializeField]
    private bool isJumping;

    public GameObject Enemy;
    public float EnemySpawnInterval;
    public int MaxEnemies;
    private int currentEnemies = 0;
    private GameObject EnemyPackage;

    [SerializeField]
    private string[] playerQuotes;
    private TextMeshPro cloudText;
    private Animator cloudTextAnim;
    private Animator messageCloudAnim;
    [SerializeField]
    private float startTextTimer;
    private float textTimer;
    private bool isTextAliveFlag = false;

    void Start()
    {
        cloudText = GameObject.Find("CloudText").GetComponent<TextMeshPro>();
        messageCloudAnim = GameObject.Find("MessageCloud").GetComponent<Animator>();

        EnemyPackage = GameObject.Find("Enemies");

        StartCoroutine(SpawnEnemy());
    }

    void Update()
    {
        Move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(Speed * Move, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isJumping == false)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
        }

        if (textTimer <= 0 && isTextAliveFlag == true)
        {
            messageCloudAnim.SetTrigger("Down");

            isTextAliveFlag = false;
        }

        textTimer -= Time.deltaTime;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            SoundManager.PlaySound(SoundManager.Sound.PlayerJumpEnd);

            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            SoundManager.PlaySound(SoundManager.Sound.PlayerJumpStart);

            isJumping = true;
        }
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(EnemySpawnInterval);

        if (currentEnemies < MaxEnemies)
        {
            Vector3 playerPos = this.gameObject.transform.position;

            GameObject EnemyPref = Instantiate(Enemy, new Vector3(Random.Range(playerPos.x - 17f, playerPos.x + 17f), Random.Range(playerPos.y + 4f, playerPos.y + 10f), playerPos.z), Quaternion.identity);
            EnemyPref.transform.parent = EnemyPackage.transform;

            SoundManager.PlaySound(SoundManager.Sound.EnemySpawn);

            currentEnemies += 1;
        }

        StartCoroutine(SpawnEnemy());
    }

    public void DeathEnemy(GameObject EnemyPrefab)
    {
        EnemyPrefab.GetComponent<Animator>().SetTrigger("Death");

        SoundManager.PlaySound(SoundManager.Sound.EnemyDeath);

        Destroy(EnemyPrefab, 0.6f);
        currentEnemies -= 1;
    }

    public void MakePhrase()
    {
        if (isTextAliveFlag == false)
        {
            messageCloudAnim.SetTrigger("Come");
        }
        
        int randomInt = Random.Range(0, playerQuotes.Length);
        cloudText.text = playerQuotes[randomInt];

        isTextAliveFlag = true;
        textTimer = startTextTimer;
    }
}