using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    [SerializeField] private UnityEvent onMirrorHit;
    // Start is called before the first frame update
    void Start()
    {
        onMirrorHit.AddListener(MirrorHit);
    }

    // Update is called once per frame
    void Update()
    {
        if( type == mirrorType.Continue) {
            if (Input.GetKeyDown(KeyCode.W)) {
                isPressed = true;
            }

            if (Input.GetKeyUp(KeyCode.W)) {
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

    public Vector2 Reflect(Vector3 inDir, Vector3 inNormal) {
        onMirrorHit?.Invoke();
        //Debug.LogFormat("IN DIR: {0} | IN NORMAL: {1} | NEW DIR: {2}", inDir, inNormal, Vector2.Reflect(inDir, inNormal));
        return Vector2.Reflect(inDir, inNormal);
    }

    private void MirrorHit() {
        Debug.Log("Mirror hit");
    }
}
