using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GM : MonoBehaviour
{
    public GameObject skillWindow;
    public GameObject hud;
    bool skillWindowup = false;
    //public int points = 10;
    public TMP_Text pointsText;

    public static List<Skill> skills = new List<Skill>();
    public static List<int> skillNumbers = new List<int>();

    public static GM instance;

    public Image healthBar;
    public Image staminaBar;
    public float health, maxHealth;
    float healthRecoverTime = 10f;
    public float stamina, maxStamina;
    float recoverTime = 2f;

    void onEnable()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        pointsText.text = "";
        hud.SetActive(true);
        skillWindow.SetActive(false);
        health = 100;
        maxHealth = 100; ;
        maxStamina = 100f;
        stamina = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        //pointsText.text = points.ToString();
        if (Input.GetKeyDown(KeyCode.P))
        {
            
            skillWindowup = !skillWindowup;
            skillWindow.SetActive(skillWindowup);
            hud.SetActive(!skillWindowup);
        }

        for (int i = 0; i < skills.Count; i++)
        {
            if (skills[i].purchased == true && Input.GetKeyDown(skills[i].number.ToString()) && stamina >= skills[1].staminaCost)
            {
                stamina -= skills[1].staminaCost;
                if (stamina < 0)
                {
                    stamina = 0f;
                }
            }
        }
        //heath recover
        if ((health + healthRecoverTime) > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += healthRecoverTime * Time.deltaTime;
        }
        //stamina recover
        if ((stamina + recoverTime) > maxStamina)
        {
            stamina = maxStamina;
        }
        else
        {
            stamina += recoverTime * Time.deltaTime;
        }

        healthBar.fillAmount = health / maxHealth;
        staminaBar.fillAmount = stamina / maxStamina;
    }
}
