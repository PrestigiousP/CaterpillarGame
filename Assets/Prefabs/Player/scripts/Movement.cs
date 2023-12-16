using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private const int NbSegments = 9;
    private List<Transform> _segments = new();

    // Start is called before the first frame update
    void Start()
    {
        _segments = GetComponentsInChildren<Transform>().ToList();
        _segments = _segments.Where(s => !s.name.Contains("Caterpillar", StringComparison.Ordinal)).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        float translationY = Input.GetAxis("Vertical");
        //float translationX = Input.GetAxis("Horizontal");

        SetSegmentsPosition(translationY);
    }

    private void SetSegmentsPosition(float translationY)
    {
        //SetControlsPointPosition();

        var firstSphere = _segments.Find(s => s.name.Contains("FirstSphere", StringComparison.Ordinal));
        var middleSphere = _segments.Find(s => s.name.Contains("MiddleSphere", StringComparison.Ordinal));
        var LastSphere = _segments.Find(s => s.name.Contains("LastSphere", StringComparison.Ordinal));

        for (int i = 0; i < NbSegments; i++)
        {
            if (i == 0 || i == NbSegments - 1)
            {
                continue;
            }

            // TODO modifier 
            float lerpStep = (i * 0.1f) + 0.1f;

            Debug.Log($"i: {i} lerpStep: {lerpStep}");

            var x1 = Mathf.Lerp(firstSphere.position.x, middleSphere.position.x, lerpStep);
            var y1 = Mathf.Lerp(firstSphere.position.y, middleSphere.position.y, lerpStep);

            var x2 = Mathf.Lerp(middleSphere.position.x, LastSphere.position.x, lerpStep);
            var y2 = Mathf.Lerp(middleSphere.position.y, LastSphere.position.y, lerpStep);

            var x = Mathf.Lerp(x1, x2, lerpStep);
            var y = Mathf.Lerp(y1, y2, lerpStep);

            _segments[i].transform.position = new Vector3(
                x,
                y + translationY,
                _segments[i].transform.position.z);

            var vector = new Vector3(x, y, _segments[0].transform.position.z);
            Debug.Log($"{i} {vector}");
            //Debug.Log($"i: {i} vector: {vector}");
            //segment.transform.position = vector;
        }
    }
}
