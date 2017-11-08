using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReciever : MonoBehaviour {

    float hitpoints = 100.0f;
    float damage = 5.0f;
    int detonationDelay = 0;
    Transform explosion;
    Rigidbody deadReplacement;
    AudioClip deathSound;
    ParticleSystem emitter;

    void ApplyDamage(float damage)
    {
        if (hitpoints <= 0)
        {
            return;
        }
        hitpoints -= damage;
        if (hitpoints <= 0)
        {
            ParticleSystem emitter = GetComponentInChildren<ParticleSystem>();
        }
        if (emitter)
        {
            emitter.Play();
        }
        Invoke("DelayedDetonate", detonationDelay);
    }

    void DelayedDetonate()
    {
        BroadcastMessage("Detonate");
    }

    void Detonate()
    {
        Destroy(gameObject);
        if (explosion)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        if (deadReplacement)
        {
            Rigidbody dead = Instantiate(deadReplacement, transform.position, transform.rotation);
            dead.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
            dead.angularVelocity = GetComponent<Rigidbody>().angularVelocity;
        }
        ParticleSystem emitter = GetComponentInChildren<ParticleSystem>();
        if (emitter)
        {
            emitter.Stop();
            emitter.transform.parent = null;
        }
    }

	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
