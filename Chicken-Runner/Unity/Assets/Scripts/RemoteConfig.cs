using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.RemoteConfig;

public class RemoteConfig : MonoBehaviour
{
    public struct userAttributes { }
    public struct appAttributes { }

    public static bool isJulyHoliday;


    private void Awake()
    {
        ConfigManager.FetchCompleted += SetConfigInfo;
        ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
    }


    void SetConfigInfo(ConfigResponse response)
    {
        isJulyHoliday = ConfigManager.appConfig.GetBool("IsJulyHoliday");
        Debug.Log(response.requestOrigin + " " + isJulyHoliday);
    }


    private void OnDestroy()
    {
        ConfigManager.FetchCompleted -= SetConfigInfo;
    }
}
