using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WhackAMole : MonoBehaviour
{
    [SerializeField] TMP_Text timeText, startResetText;
    [SerializeField] GameObject[] moles;

    public bool whackStarted, isMoleUp;
    public int whackScore;
    public TMP_Text scoreText, highScoreText;

    float whackTimer;

    private IEnumerator MoveObjectRoutine() {
        int randomIndex = Random.Range(0, moles.Length);
        GameObject nextMole = moles[randomIndex];
        nextMole.transform.localPosition = new Vector3(nextMole.transform.localPosition.x, 0.3f, nextMole.transform.localPosition.z);
        isMoleUp = true;

        float targetYUp = nextMole.transform.position.y + 0.2f;
        float initialY = nextMole.transform.position.y;
        float time = 0f;
        float duration = 0.5f;
        while (time < duration) {
            time += Time.deltaTime;
            float newY = Mathf.Lerp(initialY, targetYUp, time / duration);
            nextMole.transform.position = new Vector3(nextMole.transform.position.x, newY, nextMole.transform.position.z);
            yield return null;
        }

        nextMole.transform.localPosition = new Vector3(nextMole.transform.localPosition.x, 0.5f, nextMole.transform.localPosition.z);
        MoleManager moleManager = nextMole.GetComponent<MoleManager>();
        moleManager.isThisMoleUp = true;
        yield return new WaitForSeconds(1f);
        moleManager.isThisMoleUp = false;

        float targetYDown = nextMole.transform.position.y - 0.2f;
        initialY = nextMole.transform.position.y;
        time = 0f;
        while (time < duration) {
            time += Time.deltaTime;
            float newY = Mathf.Lerp(initialY, targetYDown, time / duration);
            nextMole.transform.position = new Vector3(nextMole.transform.position.x, newY, nextMole.transform.position.z);
            yield return null;
        }

        nextMole.transform.localPosition = new Vector3(nextMole.transform.localPosition.x, 0.3f, nextMole.transform.localPosition.z);
        isMoleUp = false;
    }

    void RestartWhackAMole() {
        whackStarted = false;
        startResetText.text = "Start";
        whackScore = 0;
        scoreText.text = "00";
        whackTimer = 59.5f;
        foreach (GameObject m in moles) {
            m.transform.localPosition = new Vector3(m.transform.localPosition.x, 0.3f, m.transform.localPosition.z);       
        }
    }

    // This trigger is StartResetButton
    private void OnTriggerEnter(Collider coll) {
        if (!whackStarted) {
            whackStarted = true;
            startResetText.text = "Reset";
        } else {
            RestartWhackAMole();
        }
    }

    void Start() {
        RestartWhackAMole();
    }

    void Update() {
        if (whackStarted && whackTimer > 0.0f) {
            startResetText.text = "Reset";
            whackTimer -= Time.deltaTime;
            timeText.text = "00:" + (whackTimer % 60).ToString("00");

            if (whackScore > int.Parse(highScoreText.text)) {
                highScoreText.text = whackScore.ToString("00");
            }

            if (!isMoleUp) {
                StartCoroutine(MoveObjectRoutine());
            }
        } else {
            timeText.text = "01:00";
            RestartWhackAMole();
        }
    }
}
