using UnityEngine;
using System.Collections;
using TMPro;

public class Wheel : MonoBehaviour
{
    [SerializeField]
    float maxRotationSpeed, spinTime, slowRotationSpeed;
    [SerializeField]
    GameObject button;
    [SerializeField]
    GameObject text;

    float rotationSpeed, angle, endRotation;
    bool isSpinning, isSlowingDown, temp;
    string result;
    int rng;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isSpinning = false;
        text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        angle = transform.eulerAngles.z;
        if (isSpinning) {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            button.SetActive(false);
        } else {
            button.SetActive(true);
        }

        if (isSlowingDown) {
            if (maxRotationSpeed < 0) {
                if (rotationSpeed <= -1 * slowRotationSpeed) {
                    rotationSpeed += 1;
                }
            } else {
                if (rotationSpeed >= slowRotationSpeed) {
                    rotationSpeed -= 1;
                }
            }
            if (angle >= endRotation - 5 && angle <= endRotation + 5) {
                isSpinning = false;
                rotationSpeed = 0;
                text.SetActive(true);
                text.GetComponent<TextMeshProUGUI>().text = "You got " + result;
            }
        }
    }

    public void Spin() 
    {
        isSpinning = true;
        isSlowingDown = false;
        text.SetActive(false);
        rng = Random.Range(1, 100);
        if (rng <= 20) { // Life 30 min (20% chance)
            endRotation = 22.5f;
            result = "Infinite Lives (30 min)";
        } else if (rng <= 30) { // Brush 3x (10% chance)
            endRotation = 67.5f;
            result = "3 Brushes";
        } else if (rng <= 40) { // Gems 35 (10% chance)
            endRotation = 112.5f;
            result = "35 Gems";
        } else if (rng <= 50) { // Hammers 3x (10% chance)
            endRotation = 157.5f;
            result = "3 Hammers";
        } else if (rng <= 55) { // Coins 750 (5% chance)
            endRotation = 202.5f;
            result = "750 Coins";
        } else if (rng <= 75) { // Brush 1x (20% chance)
            endRotation = 247.5f;
            result = "1 Brush";
        } else if (rng <= 80) { // Gems 75 (5% chance)
            endRotation = 292.5f;
            result = "75 Gems";
        } else { // Hammers 1x (20% chance)
            endRotation = 337.5f;
            result = "1 Hammer";
        }
        Debug.Log(result);
        rotationSpeed = maxRotationSpeed;
        StartCoroutine(SlowSpin(spinTime));
    }

    IEnumerator SlowSpin(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isSlowingDown = true;
        Quaternion.RotateTowards(transform.rotation, new Quaternion(0, 0, endRotation, 1), rotationSpeed);
    }
}
