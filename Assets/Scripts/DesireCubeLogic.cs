using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DesireCubeLogic : MonoBehaviour
{
    GameObject m_desireCube1;
    GameObject m_desireCube2;
    GameObject m_desireCube3;
    GameObject m_desireCube4;
    GameObject hiddenCube;
    public float CUBE_COOLDOWN =  3f;
    float m_cubecooldown = 3.0f;
    GameObject m_desireCube;
    Color purple = new Color(1.0f,0.0f,1.0f);
    PlayerLogic m_PlayerLogic;

    GameObject m_shotCube;
    // Start is called before the first frame update
    void Start()
    {
        m_desireCube1 = GameObject.Find("DesireCube1");
        m_desireCube2 = GameObject.Find("DesireCube2");
        m_desireCube3 = GameObject.Find("DesireCube3");
        m_desireCube4 = GameObject.Find("DesireCube4");
        hiddenCube = GameObject.Find("HiddenCube");
        m_PlayerLogic = FindObjectOfType<PlayerLogic>();
        m_desireCube = hiddenCube;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_desireCube != hiddenCube)
            m_desireCube.transform.Rotate(0,2,0);
        if(m_cubecooldown <= 0.0f){
            m_desireCube = RandomCube();
            RandomColor();
            m_cubecooldown += CUBE_COOLDOWN;
        }
        else if(m_desireCube.GetComponent<Renderer>().material.color != Color.white){
            m_cubecooldown -= 0.98f * Time.deltaTime;
        }
        else
            m_cubecooldown -= Time.deltaTime;
    }
    GameObject RandomCube(){
        int temp = Random.Range(0,4);
        switch(temp){
            case 0:
                return m_desireCube1;
                break;
            case 1:
                return m_desireCube2;
                break;
            case 2:
                return m_desireCube3;
                break;
            case 3:
                return m_desireCube4;
                break;
        }
        return null;
    }
    void RandomColor(){
        if(m_desireCube1.GetComponent<Renderer>().material.color != Color.white){
            m_desireCube1.GetComponent<Renderer>().material.color = Color.white;
            m_PlayerLogic.UpdateHealth();
        }
        if(m_desireCube2.GetComponent<Renderer>().material.color != Color.white){
            m_desireCube2.GetComponent<Renderer>().material.color = Color.white;
            m_PlayerLogic.UpdateHealth();
        }
        if(m_desireCube3.GetComponent<Renderer>().material.color != Color.white){
            m_desireCube3.GetComponent<Renderer>().material.color = Color.white;
            m_PlayerLogic.UpdateHealth();
        }
        if(m_desireCube4.GetComponent<Renderer>().material.color != Color.white){
            m_desireCube4.GetComponent<Renderer>().material.color = Color.white;
            m_PlayerLogic.UpdateHealth();
        }
        int temp = Random.Range(0,4);
        switch(temp){
            case 0:
                m_desireCube.GetComponent<Renderer>().material.color = Color.yellow; 
                break;
            case 1:
                m_desireCube.GetComponent<Renderer>().material.color = Color.red;
                break;
            case 2:
                m_desireCube.GetComponent<Renderer>().material.color = Color.green;
                break;
            case 3:
                m_desireCube.GetComponent<Renderer>().material.color = purple;
                break;
        }
    }
    public void UpdateCube(string fruitName,string cubeName){
            switch(cubeName){
                case "MinionTall":
                    m_shotCube = m_desireCube1;
                    break;
                case "MinionTwoEye":
                    m_shotCube = m_desireCube2;
                    break;
                case "MinionTallTwoEye":
                    m_shotCube = m_desireCube3;
                    break;
                case "MinionStandard":
                    m_shotCube = m_desireCube4;
                    break;
           }
           if (m_shotCube == m_desireCube){
               CubeDetect(fruitName);
           }
    }
    void CubeDetect(string fruitName){
        if (fruitName =="Banana" && m_desireCube.GetComponent<Renderer>().material.color == Color.yellow){
            m_PlayerLogic.UpdateScore();
            m_desireCube.GetComponent<Renderer>().material.color = Color.white;
            m_cubecooldown = CUBE_COOLDOWN;
        }
        else if (fruitName =="Apple" && m_desireCube.GetComponent<Renderer>().material.color == Color.red){
            Debug.Log("Apple");
            m_PlayerLogic.UpdateScore();
            m_desireCube.GetComponent<Renderer>().material.color = Color.white;
            m_cubecooldown = CUBE_COOLDOWN;
        }
        else if (fruitName =="Watermelon" && m_desireCube.GetComponent<Renderer>().material.color == Color.green){
            m_PlayerLogic.UpdateScore();
            m_desireCube.GetComponent<Renderer>().material.color = Color.white;
            m_cubecooldown = CUBE_COOLDOWN;
        }
        else if (fruitName =="Grape" && m_desireCube.GetComponent<Renderer>().material.color == purple){
            m_PlayerLogic.UpdateScore();
            m_desireCube.GetComponent<Renderer>().material.color = Color.white;
            m_cubecooldown = CUBE_COOLDOWN;
        }
        else{
            m_PlayerLogic.UpdateHealth();
            m_desireCube.GetComponent<Renderer>().material.color = Color.white;
            m_cubecooldown = CUBE_COOLDOWN;
        }
        m_desireCube = hiddenCube;
    }
    public void Restart(){
        m_cubecooldown = CUBE_COOLDOWN;
        m_desireCube1.GetComponent<Renderer>().material.color = Color.white;
        m_desireCube2.GetComponent<Renderer>().material.color = Color.white;
        m_desireCube3.GetComponent<Renderer>().material.color = Color.white;
        m_desireCube4.GetComponent<Renderer>().material.color = Color.white;
        m_desireCube = hiddenCube;
    }
}
