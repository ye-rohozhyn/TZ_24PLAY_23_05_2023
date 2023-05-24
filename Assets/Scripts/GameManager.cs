using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private int targetFrameRate = 60;

    private void Awake()
    {
        ApplySettings();
    }

    private void ApplySettings()
    {
        Application.targetFrameRate = targetFrameRate;
    }
}
