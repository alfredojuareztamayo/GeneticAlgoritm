using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Agent : MonoBehaviour 
{
    [SerializeField] float life, vel, armor, strengh;
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform eyePerception;
    public float maxVel, steeringForce;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
                life = 10;
                vel = 10;
                armor = 10;
                strengh = 20;
                maxVel = 6;
                steeringForce = 15;
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