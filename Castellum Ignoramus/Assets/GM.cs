using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GM : MonoBehaviour
{
    public GameObject skillWindow;
    public GameObject hud;
    bool skillWindowup = false;
    //public int points = 10;
    public TMP_Text pointsText;


    public static int points = 0;
    public static GM instance;

    public Image healthBar;
    public Image staminaBar;
    public float health, maxHealth;
    float healthRecoverTime = 10f;
    public float stamina, maxStamina;
    float recoverTime = 2f;

    public GameObject WinCamera;
    public GameObject WinCanvas;
    public GameObject MenuCamera;
    public GameObject MenuCanvas;

    //list to store all the game objects im gonna disable/enable when ending starting the game
    public GameObject player;
    public GameObject cams;
    public GameObject mainCamera;
    public GameObject mainHud;
    public GameObject cursor;

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
        pointsText.text = points.ToString();
        if (Input.GetKeyDown(KeyCode.P))
        {
            
            skillWindowup = !skillWindowup;
            skillWindow.SetActive(skillWindowup);
            hud.SetActive(!skillWindowup);
        }

        //for (int i = 0; i < skills.Count; i++)
        //{
        //    if (skills[i].purchased == true && Input.GetKeyDown(skills[i].number.ToString()) && stamina >= skills[1].staminaCost)
        //    {
        //        stamina -= skills[1].staminaCost;
        //        if (stamina < 0)
        //        {
        //            stamina = 0f;
        //        }
        //    }
        //}
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

    public void Win() {
        //disable the player, the two cameras, the inputs, all of it. enable ending camera
        Debug.Log("Winer");
        mainCamera.SetActive(false);
        cursor.SetActive(false);
        player.SetActive(false);
        cams.SetActive(false);
        mainHud.SetActive(false);
        WinCamera.SetActive(true);
        WinCanvas.SetActive(true);
    }

    public void MenuStart() {
        cursor.SetActive(true);
        player.SetActive(true);
        cams.SetActive(true);
        mainHud.SetActive(true);
        MenuCamera.SetActive(false);
        MenuCanvas.SetActive(false);
    }


}
