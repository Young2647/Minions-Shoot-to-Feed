using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PistolLogic : MonoBehaviour
{
   [SerializeField] // Start is called before the first frame update
    GameObject m_bananaPrefab;
       [SerializeField] // Start is called before the first frame update
    GameObject m_watermelonPrefab;
       [SerializeField] // Start is called before the first frame update
    GameObject m_applePrefab;
       [SerializeField] // Start is called before the first frame update
    GameObject m_grapePrefab;
    [SerializeField]
    Transform m_bulletSpawnTransform;
    const float MAX_SHOT_COOLDOWN = 0.2f;
    float m_shotCooldown = 0.0f;

    [SerializeField]
    TextMeshProUGUI m_weaponTMP;
    public enum WeaponType
{
    Apple,
    Banana,
    Watermelon,
    Grape
}
WeaponType m_weapontype = WeaponType.Banana;
    AudioSource m_audioSource;

    [SerializeField]
    AudioClip m_appleShotSound;
        [SerializeField]
    AudioClip m_bananaShotSound;
        [SerializeField]
    AudioClip m_grapeShotSound;
        [SerializeField]
    AudioClip m_watermelonShotSound;

    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        UpdateWeaponUI("Banana");
    }

    // Update is called once per frame
    void Update()
    {
        CheckWeaponType();
        if(Input.GetButtonDown("Fire1") && m_shotCooldown <= 0.0f){
            switch(m_weapontype){
                case(WeaponType.Banana):
                    Shoot(m_bananaPrefab,m_bananaShotSound);
                    break;
                case(WeaponType.Apple):
                    Shoot(m_applePrefab,m_appleShotSound);
                    break;
                case(WeaponType.Watermelon):
                    Shoot(m_watermelonPrefab,m_watermelonShotSound);
                    break;
                case(WeaponType.Grape):
                    Shoot(m_grapePrefab,m_grapeShotSound);
                    break;
            }
        }
        if(m_shotCooldown > 0.0f){
            m_shotCooldown -= Time.deltaTime;
        }

    }
    void CheckWeaponType(){
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            m_weapontype = WeaponType.Banana;
            UpdateWeaponUI("Banana");
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            m_weapontype = WeaponType.Apple;
            UpdateWeaponUI("Apple");
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            m_weapontype = WeaponType.Watermelon;
            UpdateWeaponUI("WaterMelon");
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)){
            m_weapontype = WeaponType.Grape;
            UpdateWeaponUI("Grape");
        }
    }
    void Shoot(GameObject prefab, AudioClip sound){
        m_shotCooldown = MAX_SHOT_COOLDOWN;
        Instantiate(prefab, m_bulletSpawnTransform.position,m_bulletSpawnTransform.rotation);

        PlaySound(sound);
    }
    void UpdateWeaponUI(string fruitName){
        if (m_weaponTMP){
            m_weaponTMP.text = "Current Fruit :" + fruitName;
        }
    }
    void PlaySound(AudioClip sound){
        if(m_audioSource && sound){
            m_audioSource.PlayOneShot(sound);
        }
    }
}
