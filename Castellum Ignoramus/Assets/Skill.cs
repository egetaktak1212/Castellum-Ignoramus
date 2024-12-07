using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skill : MonoBehaviour
{
    public int number = 0;
    public string skillname = "something";
    public bool purchased = false;
    public float staminaCost = 10;

    public TMP_Text nameText;
    public TMP_Text numberText;
    public Image skillImage;
    //public GameObject game;
    
    bool changenumber = false;

    // Start is called before the first frame update
    void Start()
    {
        GM.skills.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        nameText.text = skillname;
        numberText.text = number.ToString();
        
        if (purchased == false)
        {
            numberText.text = "";
        }

        if (changenumber && purchased)
        {
            for (int i = 1; i <= 9; i++)
            {
                if (Input.GetKeyDown(i.ToString()) && !GM.skillNumbers.Contains(i))
                {
                    number = i;
                    GM.skillNumbers.Add(i);
                    changenumber = false;
                }
            }
        }

    }

    public void clicked()
    {
        if (purchased == true)
        {
            this.changenumber = true;
        }
        return;
    }

    public void setNumber(int number)
    {
        this.number = number;
    }
    public void setPurchased(bool purchase)
    {
        this.purchased = purchase;
    }
}
