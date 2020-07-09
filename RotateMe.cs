using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMe : MonoBehaviour {
    public float speed, maxSpeed, slowSpeed, slowRate;
    public int counter, maxCounter;
    public bool ridinSpinnas;
    public AudioSource aSource;
    public AudioClip[] clips; //0=start flick; 1=wind down
    public Animator arrowAnim1, arrowAnim2; //lazy but i'm assigning it in inspector and triggering these in code.
    Vector3 startPos;
    public Vector3 offScreenPos;
    public float backTimer, backTimerOG;

    [Header("Particles")]
    public GameObject explosionParticles;
    public GameObject[] fireworks;
    public bool fireworksOn;
    public float timer, min, max;
    public AudioSource fireworkPre, fireworkPost;
    public AudioClip[] fireworkFX;
    Vector3 pos;

    [Header("Music Audio")]
    public AudioSource drumrollSource, musicSource;
    public AudioClip[] drumRolls, musicClips;

    // Use this for initialization
    void Start () {
        //aSource = GetComponent<AudioSource>();
        GetComponent<FloatyMovement>().enabled = false;
        timer = Random.Range(min, max);
        pos = new Vector3(Random.Range(-9, 9), Random.Range(0, 5), Random.Range(5, 20));
        if (!UIManager.UIM.soundsOn)
            musicSource.playOnAwake = false;
    }

    // Update is called once per frame
    void Update () {
        //just rotate the whole time (this is so i don't have to pick and assign the winner to a random face.)
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
        if (speed > slowSpeed)
        {
            speed -= slowRate;
        }
        if (fireworksOn)
        {
            FireWorksLogic();
            BackTimerLogic();
        }
	}

    public void FireWorksLogic()
    {
        //x=-9-9; y= 0-5; z=5-20
        //hardcoding pos cause laziness.
        //pick a random position behind you but on screen:
        timer -= Time.deltaTime;
        
        if (timer <= 0)
        {
            fireworkPre.pitch = Random.Range(.5f, 1.5f);
            if (UIManager.UIM.soundsOn)
                fireworkPre.Play(44100);

            pos = new Vector3(Random.Range(-9, 9), Random.Range(0, 5), Random.Range(5, 20));

            timer = Random.Range(min, max);
            Instantiate(fireworks[Random.Range(0, fireworks.Length)], pos, Quaternion.identity);
            fireworkPost.clip = fireworkFX[Random.Range(0, fireworkFX.Length)];
            if (UIManager.UIM.soundsOn)
                fireworkPost.Play();
        }

    }

    public void BackTimerLogic()
    {
        backTimer -= Time.deltaTime;
        if (backTimer <= 0)
        {
            UIManager.UIM.backButton.onClick.Invoke();
            backTimer = backTimerOG;
        }
    }

    public void StartReallySpinning()
    {
        GetComponent<FloatyMovement>().enabled = false;
        transform.position = startPos; //reset position so it spins in the middle of the screen.
        aSource.clip = clips[0];
        aSource.loop = true;
        if (UIManager.UIM.soundsOn)
            aSource.Play();
        speed = maxSpeed;
        if(UIManager.UIM.animation)
            counter = maxCounter;
        ridinSpinnas = true;
        explosionParticles.SetActive(false);
        fireworksOn = false;
        arrowAnim1.SetBool("isFlicking", true);
        arrowAnim2.SetBool("isFlicking", true);
        musicSource.Stop();
        backTimer = backTimerOG;
        Camera.main.GetComponent<SimpleRotate>().enabled = false;
        Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);

    }

    public void ReturnToSlowSpin()
    {
        speed = slowSpeed;
        counter = maxCounter;
        ridinSpinnas = false;
        GetComponent<FloatyMovement>().enabled = false;
        transform.position = offScreenPos; //reset position so it spins in the middle of the screen.
        fireworksOn = false;
        //musicSource.Stop();
        musicSource.clip = musicClips[0];
        if (UIManager.UIM.soundsOn)
            musicSource.Play();
        //UIManager.UIM.TriggerBGSwapAndMove();
        if (UIManager.UIM.rotationEnabled)
        {
            print("setting cam rotate to true");
            Camera.main.GetComponent<SimpleRotate>().enabled = true;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Counter") && ridinSpinnas)
        {
            if (counter > 0)
            {
                counter--;
                if(counter == 3)
                {
                    if (UIManager.UIM.soundsOn)
                        PlayAudioClipDelay(clips[1], true);
                    //aSource.loop = false;
                    drumrollSource.clip = drumRolls[0];
                    drumrollSource.loop = true;
                    if (UIManager.UIM.soundsOn)
                        drumrollSource.Play();
                }
              
            }
            else
            {
                GetComponent<FloatyMovement>().enabled = true;
                if (UIManager.UIM.soundsOn)
                    PlayAudioClipDelay(clips[2], false);

                speed = 0;
                ridinSpinnas = false;
                //print("back to idle");
                arrowAnim1.ResetTrigger("flickAway");
                arrowAnim2.ResetTrigger("flickAway");

                arrowAnim1.SetTrigger("backToIdle");
                arrowAnim2.SetTrigger("backToIdle");
                //arrowAnim1.SetBool("isFlicking", false);
                //arrowAnim2.SetBool("isFlicking", false);

                explosionParticles.SetActive(true);
                fireworksOn = true;
                UIManager.UIM.EnableBackAndRollAgain();
                drumrollSource.clip = drumRolls[1];
                drumrollSource.loop = false;

                if (UIManager.UIM.soundsOn)
                    drumrollSource.Play();
                musicSource.clip = musicClips[1];
                if (UIManager.UIM.soundsOn)
                    musicSource.Play();
            }
        }
    }

    public void PlayAudioClipDelay(AudioClip whatClip, bool loopOn)
    {
        //tried this with a coroutine on a delay of clip.length but didn't work too well so just doing this.
        aSource.clip = whatClip;
        if (UIManager.UIM.soundsOn)
            aSource.Play();
        if (!loopOn)
        {
            aSource.loop = false;
        }
        else
        {
            aSource.loop = true;
        }
    }

}
