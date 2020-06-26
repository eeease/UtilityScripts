using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Text))]
public class LoadingScreenTips : MonoBehaviour
{
    Text textBox;
    [Range(0, 100)]
    public int jokeChance; 
    [Tooltip("Set this false to not show a joke")]
    public bool showJoke;
    [TextArea(1, 2)]
    public string[] loadingTipsSerious;
    [TextArea(1, 2)]
    public string[] loadingTipsJoke;

    public float delay;
    // Start is called before the first frame update
    void OnEnable()
    {
        textBox = GetComponent<Text>();
        PickTip();
    }

    public string RandomTip()
    {
        string s = loadingTipsSerious[Random.Range(0, loadingTipsSerious.Length)];
        return s;
    }

    public string RandomJoke()
    {
        string s = loadingTipsJoke[Random.Range(0, loadingTipsJoke.Length)];
        return s;
    }

    public void PickTip()
    {
        int roll = Random.Range(0, 100);
        if (roll <= jokeChance && showJoke)
        {
            StartCoroutine(ShowTip(RandomJoke(),delay));
        }else{
            StartCoroutine(ShowTip(RandomTip(), delay));
        }

    }

    public IEnumerator ShowTip(string tip,float del)
    {
        textBox.text = tip;
        if(del==0)
        {
            yield break;
        }
        yield return new WaitForSecondsRealtime(delay); //using Realtime in case the user pauses timeScale.
        PickTip();
    }
}
