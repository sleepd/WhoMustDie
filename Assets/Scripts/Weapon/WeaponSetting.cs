using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Game/Weapon Setting", order = 0)]
public class WeaponSetting : ScriptableObject
{
    [Header("Basic Info")]
    public string weaponName = "New Weapon";
    public Sprite icon;
    public GameObject prefab;
    public WeaponType weaponType = WeaponType.Ranged;

    [Header("Stats")]
    public int damage = 10;
    public float fireRate = 0.5f;
    public float range = 50f;
    public int magazineSize = 30;
    public float reloadTime = 2f;

    [Header("Recoil & Spread")]
    public float recoil = 1f;
    public float accuracy = 0.9f;   // 命中率/散射

    [Header("Audio & VFX")]
    public AudioClip fireSound;
    public AudioClip reloadSound;
    public VisualEffect muzzleFlashVFX;
    public GameObject hitEffectVFX;
    public GameObject bloodVFX;

    [Header("Animation")]
    public string fireAnimTrigger = "Fire";
    public string reloadAnimTrigger = "Reload";
}

public enum WeaponType
{
    Melee,
    Ranged,
    Magic,
    Other
}