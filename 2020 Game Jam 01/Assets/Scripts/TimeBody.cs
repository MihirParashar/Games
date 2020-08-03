using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController2D controller;

    [SerializeField] private float recordTime = 5f;

    private Rigidbody2D rb;


    private List<PointInTime> pointsInTime;

    [HideInInspector] public static bool isRewinding = false;

    private void Start()
    {
        pointsInTime = new List<PointInTime>();

        rb = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        } else
        {
            Record();
        }
    }

    public void Record()
    {
        //We only want to have a certain amount of points in time, so the player
        //Can't go too far back.
        if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            //Start removing our points.
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }

        //Add our current position to the list.
        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }

    public void Rewind()
    {
        //If we have enough positions left, keep rewinding,
        //otherwise stop rewinding.
        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];

            //Settting the values to the values in that point in time.
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;

            //Remove that position because we already used it.
            pointsInTime.RemoveAt(0);

        }
        else
        {
            StopRewind();
        }
    }


    public void StartRewind()
    {
        isRewinding = true;
    }
    public void StopRewind()
    {
        isRewinding = false;
    }
}
