using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitDetection : MonoBehaviour
{
    public Vector3 vel;

    [SerializeField] GameObject[] hammerGO;
    [SerializeField] GameObject sphereGO;
    [SerializeField] TMP_Text velText;

    LinkedList<float> yBuffer;
    Vector3 previousL, previousR;
    bool isHit;
    float hammerVelocity;
    int velCounter;

    void Start() {
        sphereGO.transform.localPosition = new Vector3(0, -0.9f, 0);
        previousL = Vector3.zero;
        previousR = Vector3.zero;
        yBuffer = new LinkedList<float>();
        isHit = false;
        velCounter = 0;
    }

    void OnTriggerEnter(Collider coll) {
        if (coll.gameObject == hammerGO[0] && !isHit) {
            hammerVelocity = (coll.gameObject.transform.position - previousL).magnitude / Time.deltaTime;
            vel = new Vector3(0, Mathf.Abs(hammerVelocity), 0);
            velText.text = vel.y.ToString("f2");
            sphereGO.GetComponent<Rigidbody>().velocity = vel;
            isHit = true;
        }

        if (coll.gameObject == hammerGO[1] && !isHit) {
            hammerVelocity = (coll.gameObject.transform.position - previousR).magnitude / Time.deltaTime;
            vel = new Vector3(0, Mathf.Abs(hammerVelocity), 0);
            velText.text = vel.y.ToString("f2");
            sphereGO.GetComponent<Rigidbody>().velocity = vel;
            isHit = true;
        }
    }

    void Update() {
        HashSet<float> ySet = new HashSet<float>();

        if (velCounter >= 10) {
            previousL = hammerGO[0].transform.position;
            previousR = hammerGO[1].transform.position;
            velCounter = 0;
        } else {
            velCounter++;
        }

        foreach (float yPos in yBuffer) {
            ySet.Add(yPos);
        }

        if (ySet.Count == 1) {
            sphereGO.GetComponent<Rigidbody>().useGravity = true;
        }

        if (sphereGO.transform.localPosition.y < -0.9f) {
            sphereGO.transform.localPosition = new Vector3(0, -0.9f, 0);
            sphereGO.GetComponent<Rigidbody>().useGravity = false;
            isHit = false;
        } else if (sphereGO.transform.localPosition.y > 0.9f) {
            sphereGO.transform.localPosition = new Vector3(0, 0.9f, 0);
            sphereGO.GetComponent<Rigidbody>().useGravity = true;
        }

        yBuffer.AddLast(sphereGO.transform.localPosition.y);

        if (yBuffer.Count > 5) {
            yBuffer.RemoveFirst();
        }
    }
}
