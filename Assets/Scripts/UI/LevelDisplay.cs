using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText = null;

    private void Start()
    {
        levelText.text = "Level " + GameSession.Instance.GetLevel().ToString();
    }
}
