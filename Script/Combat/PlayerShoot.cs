using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    public static PlayerShoot instance;

    public int DamageOfRangeWeapon = 2;
    [SerializeField] int maxBulletCount = 10;
    [SerializeField] float maxTimerbetweenBullets = .5f;
    [SerializeField] float bulletSpeed = 50f;
    [SerializeField] float dmgPopupTimer = 15f;

    [SerializeField] GameObject bullet;
    [SerializeField] GameObject muzzle;

    [SerializeField] Transform bulletSpawnPos;
    [SerializeField] Transform muzzlePos;

    [SerializeField] Text dmgPicture;
    [SerializeField] GameObject dmgParent;
    [Space]
    [SerializeField] AudioTrigger gunSFX;
    public int currentBulletCount;

    float timer = 0;
    float timerForDmg = Mathf.Infinity;

    int standardDmg;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        dmgParent.SetActive(false);
        currentBulletCount = maxBulletCount;
        standardDmg = DamageOfRangeWeapon;
    }

    void Update()
    {
        if (PlayerMovement.instance.isDead) return;

        timer -= Time.deltaTime;
        timerForDmg -= Time.deltaTime;

        dmgPicture.text = timerForDmg.ToString("0");

        HandleShoot();
    }

    private void HandleShoot()
    {
        if (currentBulletCount > 0 && Input.GetMouseButton(0) && timer <= 0)
        {
            timer = maxTimerbetweenBullets;
            gunSFX.Play(transform.position);
            Shoot();
        }

        if(currentBulletCount <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        Instantiate(muzzle, muzzlePos.position, muzzlePos.rotation);
        GameObject bullets = Instantiate(bullet, bulletSpawnPos.position, bulletSpawnPos.rotation);
        Rigidbody rb = bullets.GetComponent<Rigidbody>();
        rb.AddForce(-bulletSpawnPos.right * bulletSpeed, ForceMode.Impulse);
    }

    public void IncreaseWeaponDmg(int dmg)
    {
        CancelInvoke();
        timerForDmg = dmgPopupTimer;
        dmgParent.SetActive(true);
        DamageOfRangeWeapon = dmg;
        Invoke(nameof(ResetDmg), dmgPopupTimer);
    }

    void ResetDmg()
    {
        dmgParent.SetActive(false);
        DamageOfRangeWeapon = standardDmg;
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1f);
        currentBulletCount = maxBulletCount;
    }

}
