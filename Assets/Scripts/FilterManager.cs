using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FilterManager : MonoBehaviour
{
    [SerializeField] private Volume renderVolume;
    [SerializeField] private GameObject filter;
    private ColorAdjustments colorAdjustments;
    private List<VolumeComponent> components;


    // Start is called before the first frame update
    void Start()
    {
        components = renderVolume.profile.components;
        foreach (VolumeComponent component in components)
        {
            if (component.name == "ColorAdjustments(Clone)")
                colorAdjustments = (ColorAdjustments)component;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            EnableDisableFiter();
        }
    }

    void EnableDisableFiter()
    {
        if (colorAdjustments == null)
            return;
        
        colorAdjustments.active = !colorAdjustments.active;
        filter.active = !filter.active; 
    }

}
