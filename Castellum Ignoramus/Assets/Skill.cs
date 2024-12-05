using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skill : MonoBehaviour
{
    public int cost = 0;
    public string skillname = "something";
    public bool purchased = false;

    public TMP_Text nameText;
    public TMP_Text costText;
    public Image skillImage;
    public int c = 0;
    // Start is called before the first frame update
    void Start()
    {
        GM.skills.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        nameText.text = skillname;
        costText.text = cost.ToString();
        
        if (purchased == true)
        {
            costText.text = "";
        }
    } 

    public void setPurchased(bool purchased)
    {
        this.purchased = purchased;
    }
}
