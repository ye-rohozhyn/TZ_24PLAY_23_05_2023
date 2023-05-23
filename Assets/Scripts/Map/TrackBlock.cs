using UnityEngine;

public class TrackBlock : MonoBehaviour
{
    [SerializeField] private GameObject[] presets;
    [SerializeField] private Transform presetHolder;

    private GameObject _currentPreset;

    public void SetNewPreset()
    {
        if (_currentPreset) Destroy(_currentPreset);

        int index = Random.Range(0, presets.Length);
        _currentPreset = Instantiate(presets[index], presetHolder);
    }
}