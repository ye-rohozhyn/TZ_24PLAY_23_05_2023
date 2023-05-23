using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private bool _isPickuping;

    public void SetIsPickuping(bool value)
    {
        _isPickuping = value;
    }

    public bool GetIsPickuping()
    {
        return _isPickuping;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            Cube cube = other.GetComponent<Cube>();

            if (cube.GetIsPickuping())
            {
                cube.SetIsPickuping(false);
                other.tag = "Untagged";
                CubeStacking.singleton.AddCube(other.transform);
            }
        }
        else if (other.CompareTag("Obstacle"))
        {
            if (!_isPickuping)
            {
                CubeStacking.singleton.RemoveCube(transform, other.transform);
            }
        }
    }
}
