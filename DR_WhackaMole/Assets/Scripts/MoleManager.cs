using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleManager : MonoBehaviour
{
    [SerializeField] GameObject[] hammerGO;
    [SerializeField] WhackAMole wAM;

    public bool isThisMoleUp;

    void OnTriggerEnter(Collider coll) {
        if (isThisMoleUp && coll.gameObject == hammerGO[0] || coll.gameObject == hammerGO[1]) {
            Debug.Log("hit");
            StopAllCoroutines();
            wAM.whackScore++;
            wAM.scoreText.text = wAM.whackScore.ToString("00");
            this.gameObject.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x, 0.3f, this.gameObject.transform.localPosition.z);
            isThisMoleUp = false;
        }
    }

    void Start() {
        isThisMoleUp = false;
    }
}
