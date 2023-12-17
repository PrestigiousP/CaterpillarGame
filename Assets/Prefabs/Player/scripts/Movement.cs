using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private int _nbSegments;
    private List<Transform> _segments = new();

    // Start is called before the first frame update
    void Start()
    {
        var transforms = GetComponentsInChildren<Transform>();
        _nbSegments = transforms.Where(s => s.name.Contains("Sphere", StringComparison.Ordinal)).Count();
        _segments = transforms.Where(s => !s.name.Equals("Caterpillar", StringComparison.Ordinal)).ToList();
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
        // quadratic
        var firstSphere = _segments.Find(s => s.name.Equals("Sphere", StringComparison.Ordinal));
        var middleSphere = _segments.Find(s => s.name.Equals("Sphere (10)", StringComparison.Ordinal));
        var lastSphere = _segments.Find(s => s.name.Equals("Sphere (20)", StringComparison.Ordinal));


        for (int i = 0; i < _nbSegments; i++)
        {
            if (i == 0 || i == _nbSegments - 1)
            {
                continue;
            }

            // TODO à tweaker 
            float lerpStep = (i * 0.05f);

            //var vector = CreateCubicVector(firstSphere, middleSphere, secondMiddleSphere, lastSphere, lerpStep, i, translationY);
            var vector = CreateQuadraticVector(firstSphere, middleSphere, lastSphere, lerpStep, i, translationY);

            _segments[i].transform.position = new Vector3(
                vector.x,
                vector.y,
                vector.z);
        }
    }

    private Vector3 CreateQuadraticVector(Transform firstSphere,
        Transform secondSphere,
        Transform thirdSphere,
        float lerpStep,
        int index,
        float translationY)
    {
        var x1 = Mathf.Lerp(firstSphere.position.x, secondSphere.position.x, lerpStep);
        var y1 = Mathf.Lerp(firstSphere.position.y, secondSphere.position.y + (translationY * 4), lerpStep);

        var x2 = Mathf.Lerp(secondSphere.position.x, thirdSphere.position.x, lerpStep);
        var y2 = Mathf.Lerp(secondSphere.position.y + (translationY * 4), thirdSphere.position.y, lerpStep);

        var x = Mathf.Lerp(x1, x2, lerpStep);
        var y = Mathf.Lerp(y1, y2, lerpStep);

        return new Vector3(
                x,
                y,
                _segments[index].transform.position.z);
    }
}
