using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var renderer = GetComponent<Renderer>();
        Color c = renderer.material.color;
        Debug.Log(c.r);
        renderer.material.color = new Color(c.r, c.g, c.b, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
