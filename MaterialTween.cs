using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MaterialTween : MonoBehaviour
{
    [System.Serializable]
    public class TweenSet
    {
        public float delay;
        public float startVal, endVal;
        public string attribute;
        public LoopType loopT;
        [Tooltip("-1 = infinite")]
        public int numOfLoops = -1;
    }

    public Material originalMat;
    public Image mainImage;
    public TweenSet[] tweens;
    Material cloneToDie;

    private void Awake()
    {
        mainImage = GetComponent<Image>();
        originalMat = mainImage.material;

        
    }
    // Start is called before the first frame update
    void Start()
    {
        Material m = Instantiate(originalMat);
        cloneToDie =m;

        mainImage.material = m;
        for (int i=tweens.Length-1; i>=0; i--)
        {
            //tweens[i].originalMat = tweens[i].mainImage.material;
            //Material m = Instantiate(tweens[i].originalMat);
            
            //tweens[i].mainImage.material = m;
            LoopTween(m, tweens[i].delay, tweens[i].attribute, tweens[i].startVal, tweens[i].endVal, tweens[i].loopT, tweens[i].numOfLoops);
        }

    }

    //if we change a value in code, update the tweening.
    private void OnValidate()
    {
        if (mainImage != null)
        {


            Material m = mainImage.material;
            m.DOKill();
            for (int i = tweens.Length - 1; i >= 0; i--)
            {
                //don't need to make a new instance here since we're changing the clone:
                //Material m = tweens[i].mainImage.material;
                LoopTween(m, tweens[i].delay, tweens[i].attribute, tweens[i].startVal, tweens[i].endVal, tweens[i].loopT, tweens[i].numOfLoops);
            }
        }
    }


    public void LoopTween(Material m, float delay, string attribute, float startVal, float endVal, LoopType lt, int nOfLoops)
    {
        m.SetFloat(attribute, startVal);
        m.DOFloat(endVal, attribute, delay).SetLoops(nOfLoops, lt);
    }


    private void OnApplicationQuit()
    {
        mainImage.material = originalMat;

        //for (int i = tweens.Length - 1; i >= 0; i--)
        //{
        //    //add the original material back to the images:
        //    tweens[i].mainImage.material = originalMat;
        //}

        Destroy(cloneToDie);
        //foreach (Material mm in clonesToDie)
        //{
        //    print("Destroying " + mm.name);
        //    Destroy(mm);
        //}
    }
}
