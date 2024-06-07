using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupAds : MonoBehaviour
{
    public GameObject objectToEnable;
    public TextMeshProUGUI textToUpdate;
    public string[] randomStrings;
    public float minInterval = 1f;
    public float maxInterval = 5f;

    private float nextUpdateTime;
    private bool isEnabled = false;

    void Start()
    {
        nextUpdateTime = Time.time + Random.Range(minInterval, maxInterval);
    }

    void Update()
    {
        if (!isEnabled && Time.time >= nextUpdateTime)
        {
            EnableObject();
        }

        if (isEnabled && Input.GetKeyDown(KeyCode.Escape))
        {
            DisableObject();
        }
    }

    void EnableObject()
    {
        isEnabled = true;
        objectToEnable.SetActive(true);

        // Choose a random string from the array
        int randomIndex = Random.Range(0, randomStrings.Length);
        string randomString = randomStrings[randomIndex];

        // Set the text to the random string
        textToUpdate.text = randomString;

        // Set next update time
        nextUpdateTime = Time.time + Random.Range(minInterval, maxInterval);
    }

    void DisableObject()
    {
        isEnabled = false;
        objectToEnable.SetActive(false);
    }
}
