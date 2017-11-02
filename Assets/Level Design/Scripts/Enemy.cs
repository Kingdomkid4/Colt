using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    float attackTurnTime = 0.7f;
    int rotateSpeed = 120;
    int attackDistance = 17;
    int extraRunTime = 2;
    int damage = 1;
    int attackSpeed = 1;
    int attackRotateSpeed = 20;
    float idleTime = 1.6f;
    Vector3 punchPosititon = new Vector3((float)0.4, 0, (float)0.7);
    float punchRadius = 1.1f;
    AudioClip idleSound;
    AudioClip attackSound;
    private float attackAngle = 10;
    private bool isAttacking = false;
    private float lastPunchTime = 0;
    Transform target;
    private CharacterController characterController;
    
    void Start ()
    {
        StartCoroutine(StartFunction());
    }
	

    IEnumerator StartFunction()
    {
        characterController.GetComponent<CharacterController>();

        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        GetComponent<Animation>().wrapMode = WrapMode.Loop;
        GetComponent<Animation>().Stop();
        GetComponent<AudioSource>().clip = idleSound;
        yield return new WaitForSeconds(Random.value);
        while (true)
        {
            StartCoroutine(Idle());
            StartCoroutine(Attack());
        }
    }

    IEnumerator Idle()
    {
        GetComponent<Animation>().Play("idle");
        if (idleSound && GetComponent<AudioSource>().clip != idleSound)
        {
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().clip = idleSound;
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().Play();
        }
        yield return new WaitForSeconds((float)idleTime);
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        if (attackSound && GetComponent<AudioSource>().clip != attackSound)
        {
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().clip = attackSound;
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().Play();
        }
        GetComponent<Animation>().CrossFade("Chase");
        float angle = 180.0f;
        float time = 0;
        Vector3 direction;
        while (angle > 5 || time < attackTurnTime)
        {
            time += Time.deltaTime;
            angle = Mathf.Abs(Vector3.Angle(gameObject.transform.position, target.position));
            float move = Mathf.Clamp01((90 - angle) / 90);
            GetComponent<Animation>()["Chase"].weight = GetComponent<Animation>()["Chase"].speed = move;
            direction = transform.TransformDirection(Vector3.forward * attackSpeed * move);
            characterController.SimpleMove(direction);
            yield break;
        }
        float timer = 0.0f;
        bool lostSight = false;
        while (timer < extraRunTime)
        {
            angle = Vector3.Angle(gameObject.transform.position, target.position);
            if (Mathf.Abs(angle) > 40)
            {
                lostSight = true;
            }
            if (lostSight)
            {
                timer += Time.deltaTime;
            }
            direction = transform.TransformDirection(Vector3.forward * attackSpeed);
            characterController.SimpleMove(direction);
            Vector3 pos = transform.TransformPoint(punchPosititon);
            if (Time.time > lastPunchTime + 0.3 && (pos - target.position).magnitude < punchRadius)
            {
                target.SendMessage("ApplyDamage", damage);
                lastPunchTime = Time.time;
            }
            if (characterController.velocity.magnitude < attackSpeed * 0.3)
            {
                yield break;
            }
        }
        isAttacking = false;
        GetComponent<Animation>().CrossFade("idle");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.TransformPoint(punchPosititon), punchRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
