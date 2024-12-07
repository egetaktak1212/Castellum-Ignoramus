using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GM : MonoBehaviour
{
    public GameObject skillWindow;
    bool skillWindowup = false;
    public int points = 10;
    public TMP_Text pointsText;

    public static List<Skill> skills = new List<Skill>();

    public static GM instance;

    void onEnable()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        skillWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        pointsText.text = points.ToString();
        if (Input.GetKeyDown(KeyCode.P))
        {
            skillWindowup = !skillWindowup;
            skillWindow.SetActive(skillWindowup);
        }
    }
}
