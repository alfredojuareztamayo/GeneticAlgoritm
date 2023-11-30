using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Agent : MonoBehaviour 
{
    [SerializeField] float life, vel, armor, strengh;
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform eyePerception;
    public float maxVel, steeringForce;

    public Vector3 aceleration =new Vector3(0,0,0);
    public float maxSpeed = 8;
    public float maxForce = 0.2f;
    public Vector3 velocity =new Vector3(0,0,-2);
    public bool isDead; 



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //private void Update()
    //{
    //    rb.velocity += aceleration;
    //    rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    //    rb.position = rb.velocity;
    //   // aceleration *= 0;
        
    //}
    public void applyForce(Vector3 force)
    {
        aceleration += force;
    }
    public void setValues(AgentType agentType)
    {
        switch (agentType)
        {
            case AgentType.Holder:
                life = 10;
                vel = 15;
                armor = 20;
                strengh = 10;
                maxVel = 5;
                steeringForce = 10;
                break;

            case AgentType.Seeker:
                life = 5;
                vel = 10;
                armor = 10;
                strengh = 20;
                maxVel = 6;
                steeringForce = 15;
                isDead = false;
                break;
        }
    }

    public Rigidbody getRB() { return rb; }
    public void getDamage(float damage)
    {
        life -= damage;
    }
    public void getHealt(float healt)
    {
        life += healt;
    }
    public Transform getEyePerception()
    {
        return eyePerception;
    }

    public void setMaxVel(float _maxVel)
    {
        maxVel = _maxVel;
    }
    public float getMaxVel() { return maxVel; }


    public void seeking(Transform target)
    {
        Vector3 desired = target.position - gameObject.transform.position;
        desired.Normalize();
        desired *= maxSpeed;
        Vector3 steer = desired - rb.velocity;
        steer.Normalize();
        steer *= maxSpeed;

        applyForce(steer);
    }
}

public enum AgentType
{
    Seeker,
    Holder
}
public enum AgentBehaviour
{
    idle,
    hungry,
    seek
}