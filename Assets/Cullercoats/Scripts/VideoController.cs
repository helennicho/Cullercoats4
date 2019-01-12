
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoController: MonoBehaviour
{
    // VideoPlayer & Controls code from Unity/Tutorial
    // https://unity3d.com/learn/tutorials/topics/graphics/playing-and-pausing

    // inherits from PlayHeadMover.cs

    public Material playButtonMaterial;
    public Material pauseButtonMaterial;
    //public Renderer playButtonRenderer;

    // allow videos to be streamed - uses anindex array to allow an array of video clips to be set up
// controls for video player include play/pause, reset/restart, got to Home screen
// at end of video canvases are displayed to allow user choice of endings 
// co-routine set up to allow option of endings to be played without reloading a new scene

// technical
// need to set up a VideoClip else can't assign a VideoComponent to Play/Pause/Next control buttons and get error message on play

// as generalisation extension could set public string videoURL, URL2, URL£ to a string of arrays  (public string[] videoURL

    public VideoClip[] videoClips;
    private VideoPlayer videoPlayer;
    public int vIndex;

    // if want to move audio source about then set up a GameObject to hold Audio Source and then can move GameObject about
    // public GameObject AudioHolder;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public int aIndex;



    public int currentTime;
    private double fraction;
 

    // to improve mobile performance don't display current or total time of video clip

    public Text currentMinutes;
    public Text currentSeconds;
    public Text totalMinutes;
    public Text totalSeconds;

    public GameObject LoadingObject;
    //public GameObject Ending1hotspot;
    //public GameObject Ending2hotspot;
    //public GameObject Ending1FF;
    //public GameObject Ending2FF;

    public GameObject ChooseEnding; 
    public GameObject NextClipButton;

    // to improve mobile performance don't display video  playhead
    public PlayHeadMover playHeadMover;

    // declare assets used in video effects
    public GameObject videoObject;


    void Awake()
    {
        // place to put get components clauses
        videoPlayer = GetComponent<VideoPlayer>();
        audioSource = GetComponent<AudioSource>();
       
        // note videoPlayer and audioSource set to play on Awake

     
    }

    // Use this for initialization
    void Start()
    {
        if (LoadingObject != null) { LoadingObject.SetActive(true); };
        Debug.Log("in VideoController  ");
        // makes sure last frame of last video doesn’t hang about when switching to next video clip
        videoPlayer.targetTexture.Release();

        videoPlayer.clip = videoClips[0];
        SetTotalTimeUI();

        audioSource.clip = audioClips[0]; 
        audioSource.Play();


    }

    // Update is called once per frame
    void Update()
    {

        // settings for video pause and play
        if (videoPlayer.isPlaying)
        {
            // once intital video clip is playing stop displaying "Loading" object
            if ((vIndex == 0) && (LoadingObject != null) && (currentTime == 1))
            {

                LoadingObject.SetActive(false);
            }

            // set up start time, video length and playhead mover
            SetCurrentTimeUI();
            SetTotalTimeUI();
            playHeadMover.MovePlayhead(CalculatePlayedFraction());

            fraction  = CalculatePlayedFraction();
            // Debug.Log("fraction of video played = " + fraction);
            if ((CalculatePlayedFraction()) > 0.95)
            {
                // can display canvases at this point
            }


            // NB could control display special effects for each video in separate script VideoEffects.cs- call compnents from this script in VideoEffects.cs using parameters VideoIndex and CurrentTime
            //videoEffects.SpecialEffects();
        }

        // when video has stopped playing and audio has stopped then play next video & matching audio clip
        if ((!audioSource.isPlaying) & (!videoPlayer.isPlaying))
        {
            //NB can get audio length using AudioSource.clip.length
            switch (vIndex)
            {
                case 0:
                    // at end of video clip display >> to allow  option to play next clip
                    SetNextClip();
                    //NextClipButton.SetActive(true);
                    //Ending1hotspot.SetActive(true);
                    //Ending2hotspot.SetActive(true);

                    // method 2 using instantiate
                    //GameObject Ending1 = Instantiate(Ending1hotspot);
                    //GameObject Ending2 = Instantiate(Ending2hotspot);

                    break;
                case 1:
                    SetNextClip();
                    // at end of video clip display >> canvas to allow  option to play next clip
                    //NextClipButton.SetActive(true);
                    break;

                case 2:
                    SetNextClip();
                    // at end of video clip display >> canvas to allow  option to play next clip
                    //NextClipButton.SetActive(true);

                    break;

                case 3:
                    SetNextClip();
                    // at end of video clip display >> canvas to allow  option to play next clip
                    // NextClipButton.SetActive(true);
                    break;

                case 4:
                    SetNextClip();
                    // at end of video clip display >> canvasto allow  option to play next clip
                    //NextClipButton.SetActive(true);
                    break;

                case 5:
                    SetNextClip();
                    // at end of video clip display >> canvas to allow  option to play next clip
                    //NextClipButton.SetActive(true);
                    break;

                case 6:
                    // at end of last video clip allow user to choose  ending 1 or 2
                    ChooseEnding.SetActive(true);
                    break;

                default:
                    break;
            }
        }
    }


    public void SetNextClip()
    {
        // plays next video clip and audio clip
        // NB as using video clips that are shorter than audio for optimisation then need to check out if both video clip and audio clips are playing and reset indexes
        vIndex++; 

        // Debug.Log("Index of video  " + vIndex );
        // Debug.Log("Index of  audio " +  aIndex);

        // if trying to access a clip greater than clip array then loop
        if (vIndex >= videoClips.Length)
        {
            vIndex = vIndex % videoClips.Length;
        }

        // play next video clip
        videoPlayer.clip = videoClips[vIndex];
        SetTotalTimeUI();
        videoPlayer.Play();

        aIndex++;
        // if trying to access a clip greater than clip array then loop
        if (aIndex >= audioClips.Length)
        {
            aIndex = aIndex % audioClips.Length;
        }

        // play next audio clip
        //audioSource.clip = audioClips[aIndex];
        //audioSource.PlayOneShot();

        audioSource.PlayOneShot(audioClips[aIndex], 0.7f);

    }

   public void PlayPause()
    {
        

        {
            //Disable Play on Awake for both Video and Audio
            videoPlayer.playOnAwake = false;
            audioSource.playOnAwake = false;

            if ((videoPlayer.isPlaying) | (audioSource.isPlaying))
            {
                Debug.Log("in VideoController - PlayPause -pause");
                videoPlayer.Pause();
                audioSource.Stop();
                //playButtonRenderer.material = playButtonMaterial;

            }
            else
            {
                Debug.Log("in VideoController - PlayPause -play");
                videoPlayer.clip = videoClips[vIndex];
                videoPlayer.Play();
                audioSource.PlayOneShot(audioClips[aIndex], 0.9f);
                //audioSource.Play();
            }
            //playButtonRenderer.material = pauseButtonMaterial;
        }
    }

    public void OnRestart()
    {
        vIndex = 0; aIndex = 0;
        videoPlayer.clip = videoClips[vIndex];
        videoPlayer.Play();
       // SetTotalTimeUI();
       // SetCurrentTimeUI();
        audioSource.PlayOneShot(audioClips[aIndex], 0.9f);

    }

  


    // to play ending option 1

    public void PlayEnding1()
    {
        // may need to set video index for ending 1 video here
        //vIndex = lastbutone;

        // if trying to access a clip greater than clip array then loop

        if (vIndex >= videoClips.Length)
        {
            vIndex = vIndex % videoClips.Length;
        }

        videoPlayer.clip = videoClips[vIndex];

        SetTotalTimeUI();

        // set active UI's
        //Ending1hotspot.SetActive(false);
       // Ending2hotspot.SetActive(false);
        //Ending1FF.SetActive(true);
        //Ending2FF.SetActive(true);

        videoPlayer.Play();

    }


    // to play ending option 2

    public void PlayEnding2()
    {
        // may need to set video index for ending 2 video here
        // vIndex = last;
        // if trying to access a clip greater than clip array then loop

        if (vIndex >= videoClips.Length)
        {
            vIndex = vIndex % videoClips.Length;
        }

        videoPlayer.clip = videoClips[vIndex];

        SetTotalTimeUI();

        // set active UI's
        //Ending1hotspot.SetActive(false);
        //Ending2hotspot.SetActive(false);
        //Ending1FF.SetActive(true);
        //Ending2FF.SetActive(true);
        videoPlayer.Play();

    }



    void SetCurrentTimeUI()
    {
        currentTime = (int)videoPlayer.time;
      //Debug.Log("SetCurrentTime" + currentTime);
        string minutes = Mathf.Floor((int)videoPlayer.time / 60).ToString("00");
        string seconds = ((int)videoPlayer.time % 60).ToString("00");

         currentMinutes.text = minutes;
         currentSeconds.text = seconds;

    }

    void SetTotalTimeUI()
    {
      //  Debug.Log("Total time video" + videoPlayer.clip.length);
        string minutes = Mathf.Floor((int)videoPlayer.clip.length / 60).ToString("00");
        string seconds = ((int)videoPlayer.clip.length % 60).ToString("00");

       totalMinutes.text = minutes;
       totalSeconds.text = seconds;
    }

    double CalculatePlayedFraction()
    {
        double fraction = (double)videoPlayer.frame / (double)videoPlayer.clip.frameCount;
        return fraction;
    }


   
}