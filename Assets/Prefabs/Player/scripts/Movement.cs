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
        //Debug.Log($"nbsegs = {_nbSegments}");
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
        var firstSphere = _segments.Find(s => s.name.Equals("Sphere", StringComparison.Ordinal));
        var middleSphere = _segments.Find(s => s.name.Equals("Sphere (9)", StringComparison.Ordinal));
        var LastSphere = _segments.Find(s => s.name.Equals("Sphere (19)", StringComparison.Ordinal));

        for (int i = 0; i < _nbSegments; i++)
        {
            if (i == 0 || i == _nbSegments - 1)
            {
                continue;
            }

            // TODO modifier 
            float lerpStep = (i * 0.05f) + 0.05f;

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

            //var vector = new Vector3(x, y, _segments[0].transform.position.z);
            //Debug.Log($"{i} {vector}");
        }
    }
}
