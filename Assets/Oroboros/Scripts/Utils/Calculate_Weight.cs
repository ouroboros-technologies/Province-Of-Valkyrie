using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Calculate_Weight : MonoBehaviour
{
    //in kg
    public float weight_per_mmm;

    Mesh mesh;
    float volume;

    // Start is called before the first frame update
    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        volume = Justin_Math.VolumeOfObject(gameObject);
        //Debug.Log(weight_per_mmm * volume + " : " + gameObject.name);

        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().mass = weight_per_mmm * volume;
        }
    }

    public float getWeight()
    {
        return weight_per_mmm * volume;
    }
}
