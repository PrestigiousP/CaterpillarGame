using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private int _nbSegments;
    private Transform _caterpillar;
    private List<Transform> _segments = new();

    void Start()
    {
        _caterpillar = GetComponent<Transform>();
        var transforms = GetComponentsInChildren<Transform>();
        _nbSegments = transforms.Where(s => s.name.Contains("Sphere", StringComparison.Ordinal)).Count();
        _segments = transforms.Where(s => !s.name.Equals("Caterpillar", StringComparison.Ordinal)).ToList();
    }

    void Update()
    {
        float translationY = Input.GetAxis("Vertical");
        //float translationX = Input.GetAxis("Horizontal");

        SetSegmentsPosition(translationY);
        //Debug.Log($"rotation: {GetComponent<Transform>().transform.rotation}");
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

            float lerpStep = i * 0.05f;

            _segments[i].transform.position = CreateQuadraticVector(firstSphere, middleSphere, lastSphere, lerpStep, i, translationY);
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
                GetYPos(x, y),
                _segments[index].transform.position.z);

        //return new Vector3(
        //    x,
        //    y,
        //    _segments[index].transform.position.z);
    }

    private float GetYPos(float x, float y)
    {
        float result = GetHypothenus(x, y) * _caterpillar.rotation.z;
        Debug.Log($"y: {result}, caterpillar rotation: {_caterpillar.rotation.z}");
        return result;
    }

    private float GetHypothenus(float a, float b) => (float)Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
}
