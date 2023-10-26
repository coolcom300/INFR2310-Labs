using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class navControl : MonoBehaviour
{
    Transform trans;
    public float faceDirection, accuracy;
    public GameObject Target;
    Animator animator;
    NavMeshAgent agent;
    bool isWalking = true;
    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        { 
            agent.destination = Target.transform.position;
            
        }
        var aimDirection = new Vector3(Target.transform.position.x - trans.position.x, 0f, Target.transform.position.z - trans.position.z).normalized;
        faceDirection = Vector3.Angle(new Vector3(0, 0, 1), aimDirection);
        if (aimDirection.x < 0)
        {
            faceDirection = faceDirection * -1;
        }
        trans.rotation = Quaternion.Euler(0, Mathf.LerpAngle(trans.rotation.eulerAngles.y, faceDirection, accuracy), 0);
        //    Vector3 targetDir = agent.transform.position - Target.transform.position;
        //    Vector3 faceDir = Vector3.RotateTowards(transform.forward, targetDir, 1*Time.deltaTime, 0.0f);
        //    agent.transform.rotation = Quaternion.LookRotation(faceDir);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "enemy")
        {
            isWalking = false;
            animator.SetTrigger("attack");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "enemy")
        {
            isWalking = true;
            animator.SetTrigger("walk");
        }
    }
}
