using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class PlayerLogic : MonoBehaviour
{

   CharacterController m_characterController;

    const float MOVEMENT_SPEED = 5.0f;
    const float GROUND_HEIGHT = 1.58f;
    float m_horizontalInput;
    float m_verticalInput;

    Vector3 m_movement;

    const float GRAVITY = 0.7f;
    const float JUMP_HEIGHT = 0.15f;
    Vector3 m_heightMovement;
    Camera m_camera;
    bool m_isJumping = false;
    bool m_onGround = true;

    int m_score = 0;
    int m_health = 100;
    public int SCORE_IMPROVE = 10;
    [SerializeField]
    TextMeshProUGUI m_scoreTMP;
    [SerializeField]
    TextMeshProUGUI m_healthTMP;
    [SerializeField]
    TextMeshProUGUI m_die2TMP;
    [SerializeField]
    TextMeshProUGUI m_dieTMP;
    [SerializeField]
    AudioClip m_updateScoreSound;
    [SerializeField]
    AudioClip m_healthSound;
    DesireCubeLogic m_desireCubeLogic;
    // Start is called before the first frame update

    AudioSource m_audioSource;
    void Start()
    {
        m_characterController = GetComponent<CharacterController>();
        m_desireCubeLogic = FindObjectOfType<DesireCubeLogic>();
        m_camera = Camera.main;
        m_audioSource = GetComponent<AudioSource>();
        UpdateScoreUI();
        UpdateHealthUI();
    }

    // Update is called once per frame
    void Update()
    {
        m_horizontalInput = Input.GetAxisRaw("Horizontal");
        m_verticalInput = Input.GetAxisRaw("Vertical");

        JumpForOnce();
        RotateTowardsMousePosition();
        if(Input.GetButtonDown("Fire2"))
            Restart();
        if(Input.GetKeyDown(KeyCode.Escape)||Input.GetKeyDown(KeyCode.Home))
            Application.Quit();
    }
    void RotateTowardsMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPos = m_camera.WorldToScreenPoint(transform.position);
        Vector3 direction = mousePos - playerPos;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle + 90.0f, Vector3.up);
    }
    void JumpForOnce(){
        //Debug.Log(this.transform.localPosition.y);
        if (this.transform.localPosition.y == GROUND_HEIGHT){
            m_onGround = true;
        }
        //Debug.Log(m_onGround);
         if(!m_isJumping && Input.GetButtonDown("Jump")&&m_onGround )
        {
            m_isJumping = true;
            m_onGround = false;
        }
    }

    void FixedUpdate()
    {
        m_movement.x = m_horizontalInput * MOVEMENT_SPEED * Time.deltaTime;
        m_movement.z = m_verticalInput * MOVEMENT_SPEED * Time.deltaTime;

        if(m_isJumping)
        {
            m_heightMovement.y = JUMP_HEIGHT;
            m_isJumping = false;
        }

        if(m_heightMovement.y < 0)
        {
            m_heightMovement.y -= GRAVITY * 1.5f * Time.deltaTime;
        }
        else
        {
            m_heightMovement.y -= GRAVITY * Time.deltaTime;
        }

        m_characterController.Move(m_movement + m_heightMovement);
    }
    public void UpdateScore(){
        Debug.Log(m_desireCubeLogic);
        m_score += SCORE_IMPROVE;
        UpdateScoreUI();
        if (m_score >= 30 && m_score < 120){
            SCORE_IMPROVE = 30;
            m_desireCubeLogic.CUBE_COOLDOWN = 2.2f;
        }
        if(m_score >= 120 && m_score < 500){
            SCORE_IMPROVE = 60;
            m_desireCubeLogic.CUBE_COOLDOWN = 1.6f;
        }
        if (m_score >=500){
            SCORE_IMPROVE = 100;
            m_desireCubeLogic.CUBE_COOLDOWN = 1.4f;
        }
    }
    public void UpdateHealth(){
        m_health -= 10;
        UpdateHealthUI();
        PlaySound(m_healthSound);
        if(m_health <= 0){
            Die();
        }
    }
    void UpdateScoreUI(){
        if (m_scoreTMP){
            m_scoreTMP.text = "Score :" + m_score;
            PlaySound(m_updateScoreSound);
        }
    }
    void UpdateHealthUI(){
        if(m_healthTMP){
            m_healthTMP.text = "Mood :" + m_health;
        }
    }
    void Die(){
        m_dieTMP.text = "GG!";
        m_die2TMP.text = "(press right mouse button to restart)";
        Time.timeScale = 0;
    }
    void Restart(){
        Time.timeScale = 1;
        m_score = 0;
        m_health = 100;
        m_desireCubeLogic.CUBE_COOLDOWN = 3f;
        SCORE_IMPROVE = 10;
        UpdateHealthUI();
        UpdateScoreUI();
        HideEndUI();
        m_desireCubeLogic.Restart();
    }
    void HideEndUI(){
        m_dieTMP.text = "";
        m_die2TMP.text ="";
    }
    void PlaySound(AudioClip sound){
        if(m_audioSource && sound){
            m_audioSource.PlayOneShot(sound);
        }
    }
}
