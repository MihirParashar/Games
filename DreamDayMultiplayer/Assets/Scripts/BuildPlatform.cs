using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPlatform : MonoBehaviour
{
    public enum DeviceTypes
    {
        Computer,
        Mobile
    }

    public static DeviceTypes deviceType;

    public static DeviceTypes GetDeviceType()
    {
        return deviceType;
    }
}
