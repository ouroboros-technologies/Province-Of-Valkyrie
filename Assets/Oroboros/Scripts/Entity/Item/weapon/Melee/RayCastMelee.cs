using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastMelee : MonoBehaviour
{

    //used to calculate blunt or sharp damage
    public bool isBlunt;
    // how far up the blade?
    public float dmg_percent;
    //size of blade
    public float size;

    public Rigidbody OwnerRigidbody;

    Vector3 position;
    Vector3 previousPosition;
    Vector3 velocity;
    Vector3 force;

    private float weight;

    Ray ray;
    RaycastHit hit;

    private bool isEnabled = false;

    void Start()
    {
        position = transform.position;
        previousPosition = transform.position;
    }

    void Update()
    {
        //public static bool Raycast(Vector3 origin, Vector3 direction, float maxDistance = Mathf.Infinity, int layerMask = DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal);
        position = transform.position;
        CalculateVelocity(Time.deltaTime);
        CalculateForce();
        if (isEnabled)
        {
            Ray ray = new Ray(previousPosition, position - previousPosition);
            Debug.DrawRay(previousPosition, position - previousPosition, Color.red, 1f);
            if (Physics.Raycast(previousPosition, position - previousPosition, out hit, ((position - previousPosition).magnitude) + size))
            {
                Collider hitCollider = hit.collider;
                print(hit.collider.gameObject.name + " " + hit.collider.gameObject.layer);
                if (hitCollider.gameObject.layer == 9 && hit.transform.gameObject.GetComponent<Rigidbody>() != OwnerRigidbody)
                {
                    OnObjectHit(hit, hitCollider); // Maybe change this to a layer specific collision function instead of Generic
                                                   // this would allow different types of things to use the core collider that is used for jumping/walking
                                                   // instead of the little ones in the arms/legs for melee combat.
                }
            }
        }
        previousPosition = position;
    }

    private void OnObjectHit(RaycastHit hit, Collider collider)
    {
        //the collider that was hit is not the player using the weapon, and is on the Combat Collision layer
        Collider c = hit.collider.GetComponent<Collider>(); // this should be redundant since I'm passing the collider now.
        GameObject hitGO = hit.transform.gameObject; // saving it in case we need to do something else with this GO in the future, could be redundant.
        hitGO.GetComponent<Stats>().TakeDamage(1); // passing in 1 damage for the time being
        Debug.Log(hitGO.GetComponent<Stats>().cur_health);
    }

    private void CalculateVelocity(float deltaTime)
    {
        velocity = (position - previousPosition) / deltaTime;
    }

    private void CalculateForce()
    {
        force = velocity * weight;
    }

    public Transform GetObjectHit()
    {
        return hit.transform;
    }

    public void SetEnabled(bool enabled)
    {
        isEnabled = enabled;
    }

    public void SetWeight(float weight)
    {
        this.weight = weight;
    }

    private void OnDrawGizmos()
    {
        if (isBlunt)
        {
            Gizmos.color = Color.grey;
        }
        else
        {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawSphere(transform.position, GetComponentInParent<Transform>().localScale.x * size);
    }
}
