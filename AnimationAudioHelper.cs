using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class AnimationAudioHelper : MonoBehaviour
{
    [System.Serializable]
    public class MyEvent:UnityEvent<int> {}

    public MyEvent onClick;
    public GameObject[] gameObjectsToFlip;
    public Selectable[] selectables;

    public void PlayClip(AudioClip clip)
    {
        GameManager.GM.PlayClip(clip, .5f);
        //AudioSource.PlayClipAtPoint(clip, GameManager.GM.ears.transform.position, 0.5f);

    }

    public void DisableGO(int which)
    {

        gameObjectsToFlip[which].SetActive(false);
    }
    public void EnableGO(int which)
    {

        gameObjectsToFlip[which].SetActive(true);
    }

    public void SelectThis(int which)
    {
        EventSystem.current.SetSelectedGameObject(selectables[which].gameObject);
        selectables[which].OnSelect(null);

    }

    public void ChangeGameState(int i){
        GameManager.GM.SetGameState(i);
    }

    //using this at the end of an animation, after interactable is turned back on.
    //allows me to set the selected button from another button.
    public void UpdateSelected()
    {
        EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().OnSelect(null);
    }
    public void zOnClick()
    {
        onClick.Invoke(1);
    }

}
