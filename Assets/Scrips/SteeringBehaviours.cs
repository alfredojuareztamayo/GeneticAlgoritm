using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SteeringBehaviours
{

    private static Vector3 truncate(Vector3 vector, float maxValue)
    {
        if (vector.magnitude <= maxValue)
        {
            return vector;
        }
        vector.Normalize();
        return vector *= maxValue;
    }

    public static Vector3 Flee(Transform agent, Vector3 target)
    {
        Agent agentBasic = agent.GetComponent<Agent>();
        Rigidbody agentRB = agent.GetComponent<Rigidbody>();

        Vector3 desiredVel = agent.position - target; // Calcular la dirección opuesta
                                                      // Vector3 desiredVel = target - agent.position;
        desiredVel.Normalize();
        desiredVel *= agentBasic.maxVel;
        Vector3 steering = desiredVel - agentRB.velocity;
        steering = truncate(steering, agentBasic.steeringForce);
        steering /= agentRB.mass;
        steering += agentRB.velocity;
        steering = truncate(steering, agentBasic.steeringForce);
        steering.y = 0;
        return steering;
    }
    public static Vector3 Seek(Transform agent, Vector3 target)
    {

        Agent agentBasic = agent.GetComponent<Agent>();
        Rigidbody agentRB = agent.GetComponent<Rigidbody>();

        Vector3 desiredVel = target - agent.position;
        desiredVel.Normalize();
        desiredVel *= agentBasic.maxVel;
        Vector3 steering = desiredVel - agentRB.velocity;
        steering = truncate(steering, agentBasic.steeringForce);
        steering /= agentRB.mass;
        steering += agentRB.velocity;
        steering = truncate(steering, agentBasic.steeringForce);
        steering.y = 0;
        return steering;
    }
}
