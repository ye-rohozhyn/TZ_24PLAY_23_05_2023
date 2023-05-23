using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 0.1f;
    [SerializeField] private float shakeIntensity = 0.5f;

    private float _currentDuration = 0f;

    private void FixedUpdate()
    {
        if (_currentDuration > 0)
        {
            float offsetX = Random.Range(-1f, 1f) * shakeIntensity;
            float offsetY = Random.Range(-1f, 1f) * shakeIntensity;

            transform.localPosition += new Vector3(offsetX, offsetY, 0);

            _currentDuration -= Time.deltaTime;
        }
    }

    public void Shake()
    {
        _currentDuration = shakeDuration;
        Handheld.Vibrate();
    }
}
