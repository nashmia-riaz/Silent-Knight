using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDecal : MonoBehaviour
{
    [SerializeField]
    GameObject decal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecalOn()
    {
        decal.SetActive(true);
    }

    public void DecalOff()
    {
        decal.SetActive(false);
    }
}
