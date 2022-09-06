using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum mirrorType {
    Continue,
    Acoup
}
public class Mirror : MonoBehaviour
{
    [SerializeField] private mirrorType type;
    [SerializeField] private float rotationContinueSpeed;
    [SerializeField] private float rotationAcoupValue;
    private bool isPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( type == mirrorType.Continue) {
            if (Input.GetKeyDown(KeyCode.A)) {
                isPressed = true;
            }

            if (Input.GetKeyUp(KeyCode.A)) {
                isPressed = false;
            }

            if (isPressed) {
                Vector3 rotation = new Vector3(0, 0, 0);
                rotation.z = transform.eulerAngles.z + rotationContinueSpeed * Time.deltaTime;
                transform.eulerAngles = rotation;
            }

        } else {
            if (Input.GetKeyDown(KeyCode.B)) {
                Vector3 rotation = new Vector3(0, 0, 0);
                rotation.z = transform.eulerAngles.z + rotationAcoupValue;
                transform.eulerAngles = rotation;
            }
        }
    }
}
