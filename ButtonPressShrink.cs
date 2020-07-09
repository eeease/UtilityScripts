using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonPressShrink : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    public Vector3 shrinkSize;
    private Vector3 originalSize;
    public AudioSource audSource;
    public AudioClip[] audClips;
    Image img;
    public Color pointerDownColor;
    Color originalColor;
    
	// Use this for initialization
	void Start () {
        originalSize = transform.localScale;
        img = GetComponent<Image>();
        originalColor = img.color;
        if (GetComponent<AudioSource>() != null)
        {
            audSource = GetComponent<AudioSource>();
        }else
        {
            audSource = gameObject.AddComponent<AudioSource>() as AudioSource; //add an audiosource if there isn't one on there already
        }
        audSource.playOnAwake = false; //otherwise it'll play whatever sound is in there
	}
	
	// Update is called once per frame
	void Update () {
     
	}
    public void OnPointerDown(PointerEventData data)
    {
        transform.localScale = shrinkSize;
        audSource.clip = audClips[0];//the click in sfx
        audSource.Play();
        img.color = pointerDownColor;

    }

    public void OnPointerUp(PointerEventData data)
    {
        transform.localScale = originalSize;
        audSource.clip = audClips[1];
        audSource.Play();
        img.color = originalColor;

    }
    public void ShrinkMe()
    {
    }

    public void ResetMySize()
    {
    }
}
