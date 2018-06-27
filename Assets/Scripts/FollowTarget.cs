﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour 
{
    private Transform player;
    private Vector3 offset;
    private float smoothing = 3;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").transform;
        offset = transform.position - player.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 targetPosition = player.position + player.TransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothing);
        transform.LookAt(player.position);
	}
}
