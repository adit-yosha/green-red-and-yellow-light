using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhaseManager : MonoBehaviour
{
    public enum Phase { Green, Red, Yellow }
    public Phase currentPhase;

    public float phaseDuration = 3f;
    private float timer;

    public TextMeshProUGUI phaseText;
    public Camera mainCamera;

    void Start()
    {
        ChangePhase();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= phaseDuration)
        {
            ChangePhase();
            timer = 0f;
        }
    }

    void ChangePhase()
    {
        int randomPhase = Random.Range(0, 3);
        currentPhase = (Phase)randomPhase;

        UpdateVisual();
    }

    void UpdateVisual()
    {
        switch (currentPhase)
        {
            case Phase.Green:
                phaseText.text = "GREEN - MOVE!";
                phaseText.color = Color.green;
                mainCamera.backgroundColor = new Color(0.6f, 1f, 0.6f);
                break;

            case Phase.Red:
                phaseText.text = "RED - STOP!";
                phaseText.color = Color.red;
                mainCamera.backgroundColor = new Color(1f, 0.6f, 0.6f);
                break;

            case Phase.Yellow:
                phaseText.text = "YELLOW - JUMP!";
                phaseText.color = Color.yellow;
                mainCamera.backgroundColor = new Color(1f, 1f, 0.6f);
                break;
        }
    }
}