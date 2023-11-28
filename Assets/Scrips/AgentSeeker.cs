using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;


public class AgentSeeker : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] AgentBehaviour agentBehaviour;


    public List<GameObject> food = new List<GameObject>();
    private List<GameObject> poison = new List<GameObject>();

    private int[] dna = new int[2];

    Agent agent;
    private void Start()
    {
        //GameObject[] foodArray = GameObject.FindGameObjectsWithTag("food");
        //foreach (GameObject foodObject in foodArray)
        //{
        //    food.Add(foodObject);
        //} 
        GameObject[] posionArray = GameObject.FindGameObjectsWithTag("poison");
        foreach (GameObject posionObject in posionArray)
        {
            poison.Add(posionObject);
        }
        //dna = 
        dna[0] = Random.Range(-5, 6);
        dna[1] = Random.Range(-5, 6);

        agent = GetComponent<Agent>();
        agent.setValues(AgentType.Seeker);

    }

    private void Update()
    {
        
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
        if(record <5)
        {
            temp.RemoveAt(closest);
        }
       else if (record > -1)
        {
            //agent.seeking(temp[closest].transform);
            return SteeringBehaviours.Seek(transform, target.transform.position);
        }
            //agent.getRB().velocity = SteeringBehaviours.Seek(transform, target.transform.position);
         return Vector3.zero;
        
    }


    private void behaviour(List<GameObject> good, List<GameObject> bad)
    {
        Vector3 steerG = eat(good);
        Vector3 steerB = eat(bad);

        steerG *= dna[0];
        steerB *= dna[1];

        agent.getRB().AddForce(steerG);
        agent.getRB().AddForce(steerB);
        //agent.applyForce(steerB);

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
