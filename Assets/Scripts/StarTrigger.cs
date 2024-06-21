using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarTrigger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ScoreValue;
    [SerializeField] private float t = 0;
    [SerializeField] private float Amplitude = 0.25f;
    [SerializeField] private float Frequency = 2;
    [SerializeField] private float Offset = 0;
    [SerializeField] private Vector3 StartPos;
    private void Start()
    {
        StartPos = transform.position;
        ScoreValue = GameObject.FindGameObjectWithTag("ScoreLevel").GetComponent<TextMeshProUGUI>();
        ScoreValue.text = 0.ToString();
    }
    private void FixedUpdate()
    {
        t += Time.fixedDeltaTime;
        Offset = Amplitude * Mathf.Sin(t * Frequency);

        transform.position = StartPos + new Vector3(0, Offset, 0);

        transform.Rotate(new Vector3(0, 0, 0.5f));
    }
    private void OnTriggerEnter(Collider other)
    {
        MazeGenerator.LevelScore++;

        ScoreValue.text = MazeGenerator.LevelScore.ToString();

        Destroy(gameObject);
    }
}
