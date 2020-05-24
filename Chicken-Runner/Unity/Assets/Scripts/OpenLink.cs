using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLink : MonoBehaviour
{
    private void Start()
    {
#if UNITY_IOS
        gameObject.SetActive(false);
#endif
    }
    public void OpenSite()
    {
        Application.OpenURL("https://meeruparashar.wixsite.com/chickenrunner");
    }
}
