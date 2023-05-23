using System.Collections.Generic;
using UnityEngine;

public class CubeStacking : MonoBehaviour
{
    [SerializeField] private GameObject pelvis;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float yStep;
    [SerializeField] private Transform cubeHolder;
    [SerializeField] private LayerMask pickupObjects;
    [SerializeField] private List<Transform> _cubes = new();

    public static CubeStacking singleton;

    private void Awake()
    {
        singleton = this;
    }

    public void AddCube(Transform newCube)
    {
        foreach (Transform cube in _cubes) 
        {
            cube.position += Vector3.up * yStep;
        }

        newCube.SetParent(cubeHolder);
        newCube.localPosition = Vector3.zero;
        _cubes.Add(newCube);

        playerAnimator.SetTrigger("Jump");
    }

    public void RemoveCube(Transform cube, Transform newParent)
    {
        _cubes.Remove(cube);
        cube.SetParent(newParent);

        if (_cubes.Count == 0)
        {
            playerAnimator.enabled = false;
            pelvis.SetActive(true);
            print("Lose");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            AddCube(other.transform);
        }
    }
}
