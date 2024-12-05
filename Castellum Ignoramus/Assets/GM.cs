using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    public GameObject skillWindow;
    bool skillWindowup = false;

    public static List<Skill> skills = new List<Skill>();

    // Start is called before the first frame update
    void Start()
    {
        skillWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            skillWindowup = !skillWindowup;
            skillWindow.SetActive(skillWindowup);
        }
    }
}
