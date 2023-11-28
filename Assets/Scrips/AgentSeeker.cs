using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class AgentSeeker : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] AgentBehaviour agentBehaviour;

    private List<GameObject> food = new List<GameObject>();
    private List<GameObject> poison = new List<GameObject>();

    private int[] dna;

    Agent agent;
    private void Start()
    {
        GameObject[] foodArray = GameObject.FindGameObjectsWithTag("food");
        foreach (GameObject foodObject in foodArray)
        {
            food.Add(foodObject);
        } 
        GameObject[] posionArray = GameObject.FindGameObjectsWithTag("poison");
        foreach (GameObject posionObject in posionArray)
        {
            poison.Add(posionObject);
        }
        dna = new int[2];
        dna[0] = Random.Range(-5, 6);
        dna[1] = Random.Range(-5, 6);

        agent = GetComponent<Agent>();
        agent.setValues(AgentType.Seeker);

    }

    private void FixedUpdate()
    {
        behaviour(food, poison);
        //eat(food);
        //eat(poison);
        if (target == null)
        {
            return;
        }
    }
    private Vector3 eat(List<GameObject> temp)
    {
        
            float record = float.PositiveInfinity;
            int closest = -1;
            for (int i = 0; i < temp.Count; i++)
            {
                float d = Vector3.Distance(transform.position, temp[i].transform.position);
                if (d < record)
                {
                    record = d;
                    closest = i;
                }
            }
           
            target = temp[closest];
            //agent.getRB().velocity = SteeringBehaviours.Seek(transform, target.transform.position);
           return SteeringBehaviours.Seek(transform, target.transform.position);
        
    }


    private void behaviour(List<GameObject> good, List<GameObject> bad)
    {
        var steerG = eat(food);
        var steerB = eat(poison);

        steerG *= dna[0];
        steerB *= dna[1];



        agent.getRB().velocity = steerG;
        agent.getRB().velocity = steerB;

    }
    
    private void OnDrawGizmos()
    {
        if (agent != null && agent.getEyePerception() != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(agent.getEyePerception().position, 5);
        }
    }

  
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("food"))
        {
            GameObject foodToDestroy = collision.gameObject;
            Destroy(foodToDestroy);
            food.Remove(foodToDestroy);
            agent.getHealt(2);
     
        }
        if (collision.gameObject.CompareTag("poison"))
        {
            GameObject foodToDestroy = collision.gameObject;
            Destroy(foodToDestroy);
            poison.Remove(foodToDestroy);
            agent.getDamage(2);
        }
    }
}
