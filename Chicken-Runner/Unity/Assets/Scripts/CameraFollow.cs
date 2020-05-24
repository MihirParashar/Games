using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    [HideInInspector]
    public Transform target;

    [HideInInspector]
    public bool inCutscene = false;

    public float smoothSpeed = 0.125f;

    void Start()
    {
        Time.timeScale = 1.0f;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.position.x, target.position.y, target.position.z - 1f);
        } 
    }

}
