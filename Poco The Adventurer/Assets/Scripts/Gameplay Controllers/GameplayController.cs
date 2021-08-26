using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    private static GameplayController m_Instance;
    public static GameplayController Instance { get { return m_Instance; } }
    public bool runOnMobile = false;

    void Awake()
    {
        MakeSingletgon();
        ConfigureRuntimePlatform();
    }
    void MakeSingletgon()
    {
        if (m_Instance != null)
            Destroy(gameObject);
        else
        {
            m_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void ConfigureRuntimePlatform()
    {
        runOnMobile = ( Application.platform == RuntimePlatform.Android ||
                        Application.platform == RuntimePlatform.IPhonePlayer) ? true : false;
        //runOnMobile = true;
        if (!runOnMobile)
            GameObject.Find("Fixed Joystick").gameObject.SetActive(false);
    }
}
