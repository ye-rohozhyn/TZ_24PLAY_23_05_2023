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
    [SerializeField] private GameObject failScreen;
    [SerializeField] private ScreenShake screenShake;
    [SerializeField] private ParticleSystem addCubeEffect;
    [SerializeField] private Animator collectCubeTextAnimator;

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
        addCubeEffect.Play();
        collectCubeTextAnimator.SetTrigger("CollectCube");
    }

    public void RemoveCube(Transform cube)
    {
        int index = _cubes.IndexOf(cube);
        _cubes.Remove(cube);
        cube.SetParent(null);

        if (_cubes.Count == 0 || index == 0)
        {
            PlayerMovement.singleton.GameOver();
            playerAnimator.enabled = false;
            pelvis.SetActive(true);
            failScreen.SetActive(true);
        }
        else
        {
            Destroy(cube.gameObject, 2f);
        }

        screenShake.Shake();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            AddCube(other.transform);
        }
    }
}
