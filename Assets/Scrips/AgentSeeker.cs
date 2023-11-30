using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;


public class AgentSeeker : MonoBehaviour {
    [SerializeField] GameObject target;
    [SerializeField] AgentBehaviour agentBehaviour;

    private Animator animador;
    private List<GameObject> food = new List<GameObject>();
    private List<GameObject> poison = new List<GameObject>();

    private int[] dna = new int[2];


    Agent agent;
    private void Start() {
        GameObject[] foodArray = GameObject.FindGameObjectsWithTag("food");
        foreach (GameObject foodObject in foodArray) {
            food.Add(foodObject);
        }
        GameObject[] posionArray = GameObject.FindGameObjectsWithTag("poison");
        foreach (GameObject posionObject in posionArray) {
            poison.Add(posionObject);
        }
        //dna = 
        dna[0] = Random.Range(-5, 6);
        dna[1] = Random.Range(-5, 6);

        agent = GetComponent<Agent>();
        animador = GetComponent<Animator>();
        agent.setValues(AgentType.Seeker);

    }

    private void Update() {

    }
    private void FixedUpdate() {
        behaviour(food, poison);
        //eat(food);
        //eat(poison);
       
    }
    private Vector3 eat(List<GameObject> temp) {
        
        float record = float.PositiveInfinity;
        GameObject closest = null;
        foreach (GameObject foodObject in temp) {
            float dist = Vector3.Distance(transform.position, foodObject.transform.position);
            if (dist < record) {
                record = dist;
                closest = foodObject;
            }
        }
        
        target = closest;
        if (target == null)
        {
            animador.SetBool("isWalking", false);
            return Vector3.zero;
        }
        if (record < 5) {
       
           temp.Remove(closest);
        } else if (record > -1) {

            animador.SetBool("isWalking", true);
            return SteeringBehaviours.Seek(transform, target.transform.position);

        }
       

        return Vector3.zero;
    }


    private void behaviour(List<GameObject> good, List<GameObject> bad) {
        Vector3 steerG = eat(good);
        Vector3 steerB = eat(bad);

        steerG *= dna[0];
        steerB *= dna[1];

        agent.getRB().velocity *= 0.5f;
        agent.getRB().AddForce(steerG);
        agent.getRB().AddForce(steerB);

        //agent.applyForce(steerB);

    }

    private void OnDrawGizmos() {
        if (agent != null && agent.getEyePerception() != null) {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(agent.getEyePerception().position, 5);
        }
    }


    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("food")) {
            GameObject foodToDestroy = collision.gameObject;
            //Destroy(foodToDestroy);
            foodToDestroy.SetActive(false);
            food.Remove(foodToDestroy);
            agent.getHealt(2);

        }
        if (collision.gameObject.CompareTag("poison")) {
            GameObject foodToDestroy = collision.gameObject;
            //Destroy(foodToDestroy);
            foodToDestroy.SetActive(false);
            poison.Remove(foodToDestroy);
            agent.getDamage(2);
        }
    }
}
