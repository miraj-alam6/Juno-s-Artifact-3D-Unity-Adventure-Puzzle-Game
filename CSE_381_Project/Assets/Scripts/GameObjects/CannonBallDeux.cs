﻿using UnityEngine;
using System.Collections;

public class CannonBallDeux : CannonBall {

	
	// Use this for initialization
	void Start () {
        startPosition = transform.position;
        this.GetComponent<Rigidbody>().velocity = fireDirection * travelSpeed;
        rb = GetComponent<Rigidbody>();
        puScript =  Camera.main.GetComponent<PickUp> ();
        player = Camera.main.transform.parent.GetComponent<FPSController>();
        GameManager.instance.addCannonBall(this);
	}


    // Update is called once per frame
    void FixedUpdate() {
        //Note: if this way is too slow, do it by adding cannonball to a list, and 
        //incrementing the list in game manager. But you need to remove it as well
        //because cannon balls can disappear

        //At high speeds, onTriggerEnter will not handle the cannon collision
        //Inefficient, will swap for raycasting later on
        Collider[] manualCollsionCheck = Physics.OverlapSphere(transform.position, this.GetComponent<SphereCollider>().radius);
        if (manualCollsionCheck.Length > 0)
        {
            string triggerTag = manualCollsionCheck[0].transform.gameObject.tag;
            if (triggerTag.Equals("Player") || triggerTag.Equals("Artifact")) {  
                OnTriggerEnter(manualCollsionCheck[0]);
            }
        }

        
		if (Vector3.Distance(startPosition, transform.position) >= maxDistance) {
            destroySelf();
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag.Equals ("Player") || other.tag.Equals("MainCamera")) {
            Vector3 forceDirection = (other.gameObject.transform.position - transform.position);
            float facingCheck = Vector3.Dot(fireDirection.normalized, forceDirection.normalized);
            if (facingCheck >= 0)
            {
                forceDirection.y = 0.0f;
                forceDirection.Normalize();
                forceDirection *= playerImpactMagnitude;
                if(puScript.heldObject != null)
                    puScript.dropObject();
                player.push(forceDirection);
                destroySelf();
                
            }
        }


        if (other.tag.Equals("Artifact"))
        {
            Vector3 forceDirection = (other.gameObject.transform.position - transform.position);

            //Check if the artifact collided with the front of the cannonBall, not the back 
            float facingCheck = Vector3.Dot(fireDirection.normalized, forceDirection.normalized);
            if (facingCheck >= 0)
            {
                //Drop the artifact if it is held
                bool isHeld = other.gameObject.GetComponent<Artifact>().isHeld;
                if (isHeld)
                {
                    puScript.dropObject();
                }
                Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

                //forceDirection is the direction of the collision, set y to 0 to avoid edge case direct-up forces and normalize
                forceDirection.y = 0.0f;
                forceDirection.Normalize();
                //Multiply forceDirection by impactMagnitude 
                rb.AddForce(forceDirection * artifactImpactMagnitude, ForceMode.Impulse);
                destroySelf();

            }
        }
        else if (other.tag.Equals("DestroyBall")){
            destroySelf();
        }
	}
    
}
