using System.Collections;
using UnityEngine;
using Additional;

public class ShootingEffect : MonoBehaviour
{
    [SerializeField] private Shooting shootScript;
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private Material weaponTracerMaterial;

    //[SerializeField] private Light flashLight;

    [SerializeField]
    private float lightIntensity = 10f;
    [SerializeField]
    private float flashDuration = 0.1f;

    // Start is called before the first frame update
    private void Start()
    {
        shootScript.OnShoot += action_OnShoot;
    }

    // Update is called once per frame
    private void action_OnShoot(object sender, Shooting.OnShootEventArgs e)
    {
        CreateWeaponTracer(e.gunEndPointPosition, e.shootPosition);

        //StartCoroutine(Flash());

        //CreateShootFlash(e.gunEndPointPosition);
    }

    private void CreateWeaponTracer(Vector3 fromPosition, Vector3 targetPosition)
    {
        Vector3 dir = (targetPosition - fromPosition).normalized;

        RaycastHit2D targetDistanceRay = Physics2D.Raycast(fromPosition, dir, Mathf.Infinity);
        if (targetDistanceRay.collider != null)
        {
            targetPosition = targetDistanceRay.point;

            if (targetDistanceRay.collider.gameObject.CompareTag("Enemy"))
            {
                playerMovement.DeathEnemy(targetDistanceRay.collider.gameObject);
            }
        }

        float eurlerZ = Utils.GetAngleFromVectorFloat(dir) - 90;
        float distance = Vector3.Distance(fromPosition, targetPosition);

        Vector3 tracerSpawnPosition = fromPosition + dir * distance * .5f;

        Material tmpWeaponTracerMaterial = new Material(weaponTracerMaterial);
        //tmpWeaponTracerMaterial.SetTextureScale("_MainTex", new Vector2(1f, .99f));
        //tmpWeaponTracerMaterial.SetTextureOffset("_MainTex", new Vector2(0f, 0f));

        World_Mesh worldMesh = World_Mesh.Create(tracerSpawnPosition, eurlerZ, 6f, distance, tmpWeaponTracerMaterial, null, 10000);

        float timer = .1f;
        FunctionUpdater.Create(() =>
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                //DestroySelfInvisible(tmpWeaponTracerMaterial);
                worldMesh.DestroySelf();
                return true;
            }
            return false;
        });
    }

    IEnumerator DestroySelfInvisible(Material traceMaterial)
    {
        for (float f = .05f; f <= 1; f += .05f)
        {
            Color color = traceMaterial.color;
            color.a = f;
            traceMaterial.color = color;

            yield return new WaitForSeconds(0.05f);
        }
    }

    /*IEnumerator Flash()
    {
        flashLight.intensity = lightIntensity;

        yield return new WaitForSeconds(flashDuration);

        flashLight.intensity = 0;
    }*/

    /*private CreateShootFlash(Vector3 spawnPosition)
    {
        World_Sprite worldSprite = World_Sprite.Create(spawnPosition, shootFlashSprite);

        FunctionTimer.Create(worldSprite.DestroySelf, .1f);
    }*/
}