using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine.Samples;
using UnityEngine;
using UnityEngine.UI;

public class SkillHotbar : MonoBehaviour
{
    public TMP_Text numberText;
    int number;
    public static Action<SkillHotbar> UnSelectUI;
    public Image skillImage;
    public Sprite noSkillSprite;
    public Sprite skillSprite;
    public Sprite selectedSprite;
    public GameObject shootManager = null;
    SimplePlayerShoot shootScript;
    bool open = false;
    // Start is called before the first frame update
    void Start()
    {



  
        number = Convert.ToInt32(numberText.text);
        skillImage.sprite = noSkillSprite;

        //if this is for fireball, specifically make it fireball script. I know this sucks ok
        if (number == 1) {
            shootScript = shootManager.GetComponent<SimpleFireball>();
        } else if (number == 2)
        {
            shootScript = shootManager.GetComponent<SimplePlayerShoot>();
        }

    }

    private void OnEnable()
    {
        UnSelectUI += unSelectUI;
    }

    void unSelectUI(SkillHotbar obj) {
        if (obj != this) {
            unselectSkill();
        }
    }

    // Update is called once per frame
    void Update()
    {
        KeyCode[] list = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4 };
        if (Input.GetKeyDown(list[number - 1])) {
            selectSkill();
            UnSelectUI?.Invoke(this);

        }


    }

    public void selectSkill()
    {
        if (open && shootManager != null)
        {
            skillImage.sprite = selectedSprite;
            shootScript.selected = true;
        }
    }

    public void unselectSkill() {
        if (open && shootManager != null) {
            skillImage.sprite = skillSprite;
            shootScript.selected = false;
        }

    }

    public void setOpen() {
        Debug.Log("A");
        skillImage.sprite = skillSprite;
        open = true;
    }



}
