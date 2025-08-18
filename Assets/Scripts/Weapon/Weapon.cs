using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class Weapon : MonoBehaviour
{
    public PlayerController Player;
    [field: SerializeField]
    public WeaponSetting Settings { get; private set; }

    public event Action OnFired;
    public event Action OnAmmoChanged;
    public int CurrentAmmo { get; private set; }
    public bool IsReloading { get; private set; }
    private float nextFireTime;
    private VisualEffect hitVfx;
    private VisualEffect bloodVfx;
    [SerializeField] private VisualEffect muzzleFlashVfx; //Todo: Instantiate a prefab at the muzzle vfx solt
    int layerMask;

    void Start()
    {
        SetAmmo(Settings.magazineSize);

        if (Settings.hitEffectVFX != null) hitVfx = Instantiate(Settings.hitEffectVFX, transform).GetComponent<VisualEffect>();
        if (Settings.bloodVFX != null) bloodVfx = Instantiate(Settings.bloodVFX, transform).GetComponent<VisualEffect>();
        layerMask = ~LayerMask.GetMask("Player");
    }


    public void Fire()
    {
        if (IsReloading) return;
        if (CurrentAmmo <= 0) return;
        if (Time.time < nextFireTime) return;

        nextFireTime = Time.time + Settings.fireRate;
        SetAmmo(CurrentAmmo-1);

        muzzleFlashVfx.SendEvent("Shoot");
        HitCheck();
        OnFired?.Invoke();
        Player.animator.SetTrigger("Fire");
        Player.audioSource.PlayOneShot(Settings.fireSound);
    }


    private void HitCheck()
    {
        if (Physics.Raycast(Player.mainCamera.transform.position,
                            Player.mainCamera.transform.forward,
                            out RaycastHit hit,
                            Settings.range, layerMask))
        {
            IDamageable target = hit.collider.GetComponent<IDamageable>();
            if (target != null)
            {
                target.TakeDamage(Settings.damage);
                bloodVfx.transform.position = hit.point;
                bloodVfx.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                bloodVfx.SendEvent("OnBloodSplash");
            }


            if (hitVfx != null)
            {
                hitVfx.transform.position = hit.point;
                hitVfx.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                hitVfx.SendEvent("OnHit");
            }
        }
    }


    public void Reload()
    {

        StartCoroutine(ReloadCorotine());
    }


    IEnumerator ReloadCorotine()
    {
        Player.audioSource.PlayOneShot(Settings.reloadSound);
        IsReloading = true;
        yield return new WaitForSeconds(Settings.reloadTime);
        IsReloading = false;
        SetAmmo(Settings.magazineSize);
    }

    void SetAmmo(int newAmmo)
    {
        CurrentAmmo = newAmmo;
        OnAmmoChanged?.Invoke();
    }
}
