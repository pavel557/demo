using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPlayer : MonoBehaviour
{
    [SerializeField] private FixedJoystick fixedJoystick;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float reloading;
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private Transform creationPoint;
    [SerializeField] private AudioSource gunAudio;
    [SerializeField] private ParticleSystem gunShotEffect;
    [SerializeField] private GameObject aim;

    private Vector2 direction;

    public float Reloading { get => reloading; set => reloading = value; }

    private void Start()
    {
        StartCoroutine(Fire());
    }

    private void Update()
    {
        if ((fixedJoystick.Horizontal != 0)||(fixedJoystick.Vertical != 0))
        {
            
            ControlTurret();
            if (aim.activeInHierarchy == true)
            {
                return;
            }
            aim.SetActive(true);
        }
        else
        {
            if (aim.activeInHierarchy == false)
            {
                return;

            }
            aim.SetActive(false);
        }
    }

    public void ControlTurret()
    {
        direction = fixedJoystick.Direction;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, angle), Time.deltaTime * rotateSpeed);
    }

    private void CreateBullet()
    {
        GameObject bulletObj = Instantiate(bulletPref, creationPoint.position, transform.rotation);
        MovementController bulletÌÑ = bulletObj.GetComponent<MovementController>();
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bulletÌÑ.DirectionMovement = DirectionMovement();
        bullet.Team = Team.Player;
        gunAudio.Play();
        gunShotEffect.Play();
    }

    private Vector2 DirectionMovement()
    {
        float y = 1f;
        float x = 1f;
        if ((transform.rotation.eulerAngles.z > 90) && (transform.rotation.eulerAngles.z < 270))
        {
            x = -1f;
            y = Mathf.Tan(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * -1f;
        }
        if ((transform.rotation.eulerAngles.z < 90) || (transform.rotation.eulerAngles.z > 270))
        {
            x = 1f;
            y = Mathf.Tan(transform.rotation.eulerAngles.z * Mathf.Deg2Rad);
        }
        if (transform.rotation.eulerAngles.z == 90)
        {
            x = 0f;
            y = float.MaxValue;
        }
        if (transform.rotation.eulerAngles.z == 270)
        {
            x = 0f;
            y = float.MinValue;
        }

        Vector2 direction = new Vector2(x, y);
        direction.Normalize();
        return direction;
    }

    public void StartFire()
    {
        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        while (true)
        {
            if ((fixedJoystick.Horizontal != 0) || (fixedJoystick.Vertical != 0))
            {
                CreateBullet();
            }
            yield return new WaitForSeconds(reloading);
        }
    }
}
