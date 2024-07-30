using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    public Renderer render;

    private Material original;
    public Material highlight;
    private Material[] materials;
    
    public int posicion;

    // Start is called before the first frame update
    void Start()
    {
        //render = GetComponentInChildren<render>();
        Creator creator = this.GetComponent<Creator>();
        if(creator == null)
        {
            materials = render.materials;
            original = materials[posicion];
        }
    }

    public void Highlight()
    {
        materials[posicion] = highlight;
        render.materials = materials;
    }

    public void removeHighlight()
    {
        materials[posicion] = original;
        render.materials = materials;

    }
}
