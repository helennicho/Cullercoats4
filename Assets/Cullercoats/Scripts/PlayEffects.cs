﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEffects : MonoBehaviour {

    // for videos played from Assets - for streamed videos  ref: PlayVideoEffects.cs & VideoURLCOntroller
    // displys special effects such as game objects and particles on videos as they are playing;
    // Calls currentTime from VideoController script

    public GameObject vidObject;
    public int startTime;
    public int endTime;

    // for streaming videos
    //private VideoURLController videoController;

    // for playing videos from Assets
    private VideoController videoController;

    // Use this for initialization
    void Start()
    {
        // gets components (parameters etc.) from script
        // streaming videos
        //videoController = GetComponent<VideoURLController>();
        //videos from Assets
        videoController = GetComponent<VideoController>();
    }

    // Update is called once per frame
    void Update()
    {

        if ((vidObject != null) && (startTime == videoController.currentTime))
        {
            vidObject.SetActive(true);
        }
        else if ((vidObject != null) && (endTime == videoController.currentTime))
        {
            vidObject.SetActive(false);
        }
    }
}
