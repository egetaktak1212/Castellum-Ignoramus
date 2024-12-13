using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.CanvasScaler;


public class EnemyScript : MonoBehaviour
{
    public bool finishedTurn { get; private set; } = false; //i got this one from gpt, google was not enough. i will lament later
    public Camera mainCamera;
    public GameObject damageTextPrefab;
    bool lastRetreat = false;

    public NavMeshAgent nma;

    public Renderer bodyRenderer;

    private NavMeshPath path;
    private GM gameManager;

    public Vector3 destination;

    public bool selected = false;


    float rotateSpeed;

    LayerMask layerMask;

    public int maxHealth = 60;
    public int currentHealth;
    public HealthBarScript healthbar;

    bool retreated = false;

    void OnEnable()
    {
    }

    void OnDisable()
    {
    }

    public void StartTurn()
    {

        //ITS OUR TURN BABY NYEH NYEH NYEH
        finishedTurn = false;
        StartCoroutine(PerformEnemyActions());



    }


    private IEnumerator DoAction() {
        while (true)
        {
            PerformEnemyActions();
            yield return new WaitForSeconds(3);
        }
    
    }



    //WHERE THE MAGIC HAPPENS BOYS
    private IEnumerator PerformEnemyActions()
    {
        if (lastRetreat) {
            lastRetreat = false;
            finishedTurn = true;
            yield break;
        }

        // If the enemy has less than 30% health, it might retreat
        if (currentHealth < maxHealth * 0.3f)
        {
            float rand = Random.value;
            if (rand < 0.5f && !retreated)
            {
                RetreatFromFoes();
                retreated = true;
                finishedTurn = true;
                yield break;
            }
        }

        // Attempt to approach and attack the nearest unit
        if (true /* I made this only to minimize the below*/)
        {
            PlayerControls nearestPlayer = getNearestPlayer();

            if (nearestPlayer != null)
            {
                //Debug.Log("A");
                float distanceToPlayer = Vector3.Distance(transform.position, nearestPlayer.transform.position);
                float attackRange = 2f;
                float moveDistance = 5f; // Set maximum move distance

                if (distanceToPlayer <= attackRange)
                {
                    //Debug.Log("B");
                    // If already in attack range, attack the player
                    Attack(nearestPlayer);
                    finishedTurn = true;
                    yield break; // End turn after attacking
                }
                else
                {

                    if (nma != null)
                    {
                        //Debug.Log("C");
                        nma.isStopped = false;
                        nma.SetDestination(nearestPlayer.transform.position);
                        Vector3 start = transform.position;

                        float distanceMoved = 0f;

                        while (true)
                        {
                            //Debug.Log("D");
                            distanceMoved = Vector3.Distance(start, transform.position);
                            //Debug.Log("E");
                            if (Vector3.Distance(transform.position, nearestPlayer.transform.position) <= attackRange)
                            {
    
                                nma.isStopped = true;
                                Attack(nearestPlayer);
    
                                finishedTurn = true;
                                //Debug.Log("F");
                                yield break;
                            }

                            if (distanceMoved >= moveDistance)
                            {
                                //Debug.Log("G");
                                nma.isStopped = true;  // Stop the enemy from moving.
                                break;                 // Exit the loop as the enemy can't move anymore.
                            }

                            yield return null;
                        }
                    }
                }
            }
        }

        finishedTurn = true;

    }






    private void RetreatFromFoes()
    {
        //if Im low on hp brother, im boutta flee outta here yfeel me. i aint dyin today. But ill only do it once per game cuz i aint a little wuss yfeel
        //this is so sick man
        PlayerControls nearestPlayer = getNearestPlayer();

         if (nearestPlayer != null)
        {
            Vector3 retreatDirection = (transform.position - nearestPlayer.transform.position);
            retreatDirection.Normalize();

            float speed = 7f;
            Vector3 retreatPosition = transform.position + retreatDirection * speed;

            if (nma != null)
            {
                Vector3 pos = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z);

                GameObject DamageText = Instantiate(damageTextPrefab, pos, Quaternion.identity);
                DamageText.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = "RETREAT!";
                nma.isStopped = false;
                nma.SetDestination(retreatPosition);
                lastRetreat = true;
            }



        }


    }

    PlayerControls getNearestPlayer()
    {
        return FindObjectOfType<PlayerControls>();
    }




    // Start is called before the first frame update
    void Start()
    {
        path = new NavMeshPath();
        layerMask = LayerMask.GetMask("wall");

        rotateSpeed = Random.Range(20, 60);

        transform.Rotate(0, Random.Range(0, 360), 0);

        layerMask = LayerMask.GetMask("ground", "unit", "enemy");

        gameManager = GM.instance;


        currentHealth = maxHealth;
        //healthbar.SetMaxHealth(currentHealth);


    }

    private void Update()
    {
        PlayerControls nearestPlayer = getNearestPlayer();
        //Debug.Log(nearestPlayer);
        nma.SetDestination(nearestPlayer.transform.position);

    }

   
    public void TakeDamage(int damage)
    {
        string message;
        if (damage == 999)
        {
            message = "Missed!";
        }
        else
        {
            message = damage.ToString();
            currentHealth -= damage;
            healthbar.setHealth(currentHealth);
        }

        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z);

        GameObject DamageText = Instantiate(damageTextPrefab, pos, Quaternion.identity);
        DamageText.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = message;


        //if dead, tell game manager
        if (currentHealth <= 0)
        {
            //GM.EndEnemyLife(this);
            Destroy(gameObject);
        }

        if (currentHealth < maxHealth * 0.3f)
        {
            float rand = Random.value;
            if (rand < 0.5f && !retreated)
            {
                RetreatFromFoes();
                retreated = true;
                finishedTurn = true;
            }
        }



    }

    void Attack(PlayerControls player)
    {


        int randomValue = Random.Range(5, 15);

        if (Random.value < 0.4f)
        {

            player.TakeDamage(999);
        }
        else
        {

            player.TakeDamage(randomValue);
        }
    }




}
