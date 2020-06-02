using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    private static Music _instance;

    void Awake()
    {
        //if we don't have an [_instance] set yet
        if (!_instance)
        {
            _instance = this;
        }

        //otherwise, if we do, DESTROY this thing
        /*
     +--^----------,--------,-----,--------^-,
     | |||||||||   `--------'     |          O
     `+---------------------------^----------|
       `\_,---------,---------,--------------'
         / XXXXXX /'|       /'
        / XXXXXX /  `\    /'
       / XXXXXX /`-------'
      / XXXXXX /
     / XXXXXX /
    (________(                
     `------'              
   */
        else
        {
            Destroy(this.gameObject);
        }


        DontDestroyOnLoad(this.gameObject);
    }
}
