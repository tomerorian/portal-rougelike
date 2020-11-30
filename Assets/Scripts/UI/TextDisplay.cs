using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextDisplay : MonoBehaviour
{
    public static TextDisplay Instance { get; private set; }

    [Header("Refs")]
    [SerializeField] GameObject displayTextContainer = null;
    [SerializeField] TextMeshProUGUI displayText = null;

    private void Awake()
    {
        CreateSingleton();
    }

    private void CreateSingleton()
    {
        if (Instance && Instance != this)
        {
            Debug.LogError("Found more than one TextDisplay script instances");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        displayTextContainer.SetActive(false);
    }

    private void Update()
    {
        if (!displayTextContainer.activeSelf) { return; }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.I))
        {
            HideDisplay();
        }
    }

    public void DisplayText(string text, float duration = 0)
    {
        Level.Instance.PauseGame();

        displayText.text = text;
        displayTextContainer.SetActive(true);

        if (duration > 0)
        {
            StartCoroutine(WaitAndHideDisplay(duration));
        }
    }

    private IEnumerator WaitAndHideDisplay(float delay)
    {
        yield return new WaitForSeconds(delay);

        HideDisplay();
    }

    private void HideDisplay()
    {
        displayTextContainer.SetActive(false);

        StartCoroutine(WaitForFrameAndUnpause());
    }

    private IEnumerator WaitForFrameAndUnpause()
    {
        yield return new WaitForEndOfFrame();

        Level.Instance.UnpauseGame();
    }
}
