using System.Collections;
using UnityEngine;

public class TrackGenerator : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject trackBlockPrefab;
    [SerializeField] private Transform trackBlockParent;
    [SerializeField] private float blockLength = 30f;
    [SerializeField] private Vector3 spawnPosition;

    private int _trackBlocksCount = 5;
    private GameObject[] _trackBlocks;

    private int _lastBlockIndex = 0;
    private Vector3 _spawnOffset = new(0f, -400f, 0f);
    private float _blockSpeed = 500.0f;

    private void Awake()
    {
        GenerateStartBlocks();
    }

    private void FixedUpdate()
    {
        if (player.position.z > spawnPosition.z - (_trackBlocksCount - 2) * blockLength)
        {
            GenerateNextBlock();
        }
    }

    private void GenerateStartBlocks()
    {
        _trackBlocks = new GameObject[_trackBlocksCount];

        for (int i = 0; i < _trackBlocksCount; i++)
        {
            _trackBlocks[i] = Instantiate(trackBlockPrefab, trackBlockParent);
            _trackBlocks[i].transform.localPosition = spawnPosition;
            _trackBlocks[i].GetComponent<TrackBlock>().SetNewPreset();
            spawnPosition.z += blockLength;
        }
    }

    private void GenerateNextBlock()
    {
        _trackBlocks[_lastBlockIndex].GetComponent<TrackBlock>().SetNewPreset();

        StartCoroutine(MoveFromTo(spawnPosition + _spawnOffset, spawnPosition));

        spawnPosition.z += blockLength;
        _lastBlockIndex = (_lastBlockIndex + 1) % _trackBlocksCount;
    }

    private IEnumerator MoveFromTo(Vector3 startPosition, Vector3 targetPosition)
    {
        Transform lastBlock = _trackBlocks[_lastBlockIndex].transform;
        lastBlock.localPosition = startPosition;

        float distance = Vector3.Distance(startPosition, targetPosition);
        float startTime = Time.time;

        while (lastBlock.localPosition != targetPosition)
        {
            float timeElapsed = Time.time - startTime;
            float t = Mathf.Clamp01(timeElapsed * _blockSpeed / distance);

            lastBlock.localPosition = Vector3.Lerp(startPosition, targetPosition, t);

            yield return null;
        }
    }
}
