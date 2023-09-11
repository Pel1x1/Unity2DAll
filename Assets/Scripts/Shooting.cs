using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
    }

    private Transform aimTransform;
    private Transform aimGunEndPointPosition;
    private Transform aimGunTargetPointPosition;

    [SerializeField]
    private Animator PistolShootAnimator;

    //private Vector2 cameraSize = new Vector2(18f, 10f);

    [SerializeField]
    private float shotDistance;


    // Start is called before the first frame update
    void Awake()
    {
        PistolShootAnimator = PistolShootAnimator.GetComponent<Animator>();

        aimTransform = transform.Find("AimPistol");
        aimGunEndPointPosition = aimTransform.Find("GunEndPointPosition");
        aimGunTargetPointPosition = aimTransform.Find("GunTargetPointPosition");

        aimGunTargetPointPosition.transform.position = new Vector3(shotDistance, aimGunTargetPointPosition.transform.position.y, aimGunTargetPointPosition.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        HandleShooting();

        //RaycastHit2D targetDistance = Physics2D.Raycast(fromPosition, fromPosition - targetPosition, Mathf.Infinity);
        //Debug.DrawRay(aimGunEndPointPosition.position, (aimGunEndPointPosition.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized * -10000f, Color.red);
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PistolShootAnimator.SetTrigger("Shoot");

            SoundManager.PlaySound(SoundManager.Sound.PlayerShoot);

            //Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //mousePosition = null; //new Vector3(mousePosition.x * (cameraSize.x / Mathf.Abs(mousePosition.x)), mousePosition.y * (cameraSize.y / Mathf.Abs(mousePosition.y)), mousePosition.z);

            OnShoot?.Invoke(this, new OnShootEventArgs
            {
                gunEndPointPosition = aimGunEndPointPosition.position,
                shootPosition = aimGunTargetPointPosition.position,
            });
        }
    }
}
