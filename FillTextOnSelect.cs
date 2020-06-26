using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FillTextOnSelect : MonoBehaviour, ISelectHandler
{
    public Text[] textBoxes;
    [TextArea(8,8)]
    public string[] fillThis;

    //optional just to help save time for me:
    public Enemy eStats;
    // Start is called before the first frame update
    void Start()
    {
        if(fillThis.Length!=textBoxes.Length)
        {
            Debug.Log("Number of texts not equal to number of strings!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnSelect(BaseEventData eventData)
    {
        for (int i = 0; i < textBoxes.Length; i++)
        {
            textBoxes[i].text = fillThis[i];
        }
        if (eStats)
        {


            textBoxes[2].text =
                //"Stats: \n" +
                "Health: " + eStats.health + "\n" +
                "Damage: " + eStats.dmgToGive + "\n" +
                "Attack Delay: " + eStats.attackDelay + "\n" +
                "Range: " + eStats.maxAttackDist + "\n" +
                "Speed: " + eStats.moveSpeed + "\n" +
                "Research Rate: " + eStats.researchSpeed + "\n" +
                "Buffed By: " + eStats.buffedBy;
        }
    }
}
