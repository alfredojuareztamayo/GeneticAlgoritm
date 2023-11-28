using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    public GameObject food;
    AgentSeeker seeker;
    // Start is called before the first frame update
    void Start()
    {
        GameObject seekerGame = GameObject.FindGameObjectWithTag("Player");
        seeker = seekerGame.GetComponent<AgentSeeker>();
        generateFood();
    }

    private void Update()
    {
        if (Random.Range(0, 100) == 10)
        {
            GameObject tempFood = Instantiate(food, new Vector3(Random.Range(-20, 20), 0, Random.Range(20, -20)), Quaternion.identity);
            seeker.food.Add(tempFood);
        }
    }
    private void generateFood()
    {
        for(int i = 0; i < 10; i++)
        {
            GameObject tempFood = Instantiate(food,new Vector3(Random.Range(-20,20),0,Random.Range(20,-20)), Quaternion.identity);
            seeker.food.Add(tempFood);
        }

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("player"))
    //    {
    //        Destroy(food);
    //    }
    //}
}
