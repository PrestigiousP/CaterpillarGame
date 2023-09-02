using UnityEngine;

public class Bezier : MonoBehaviour
{
    public GameObject spherePrefab; // Assign the sphere prefab in the Inspector
    public float length;

    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private Vector3 _firstControlPoint;
    private Vector3 _secondControlPoint;
    private GameObject[] _segments;

    // Start is called before the first frame update
    void Start()
    {
        _segments = new GameObject[10];

        _startPosition = transform.position;
        _endPosition = new Vector3(
                transform.position.x + length,
                transform.position.y,
                transform.position.z
            );
        _firstControlPoint = new Vector3(
                Mathf.Lerp(transform.position.x, transform.position.x + length, 0.3f),
                transform.position.y,
                transform.position.z
            );
        _secondControlPoint = new Vector3(
            Mathf.Lerp(transform.position.x, transform.position.x + length, 0.7f),
            transform.position.y,
            transform.position.z
            );

        SetSegmentPositions(true);
    }

    // Update is called once per frame
    void Update()
    {
        MoveControlPoints();
        SetSegmentPositions(false);
    }

    private void MoveControlPoints()
    {
        bool w = Input.GetKey(KeyCode.W);
        if(w)
        {
            _firstControlPoint = new Vector3(
                    _firstControlPoint.x,
                    _firstControlPoint.y + Time.deltaTime + 0.1f,
                    _firstControlPoint.z
                );
            Debug.Log($"Pressed W: {_firstControlPoint}");
        }
    }

    private void SetSegmentPositions(bool isInstantiating)
    {
        for (float i = 0; i <= 1; i += 0.1f)
        {
            var x1 = Mathf.Lerp(_startPosition.x, _firstControlPoint.x, i);
            var y1 = Mathf.Lerp(_startPosition.y, _firstControlPoint.y, i);

            //var x2 = Mathf.Lerp(FirstControlPoint.x, SecondControlPoint.x, i);
            //var y2 = Mathf.Lerp(FirstControlPoint.y, SecondControlPoint.y, i);
            var x2 = Mathf.Lerp(_firstControlPoint.x, _endPosition.x, i);
            var y2 = Mathf.Lerp(_firstControlPoint.y, _endPosition.y, i);

            var x3 = Mathf.Lerp(_secondControlPoint.x, _endPosition.y, i);
            var y3 = Mathf.Lerp(_secondControlPoint.y, _endPosition.y, i);

            var x = Mathf.Lerp(x1, x2, i);
            var y = Mathf.Lerp(y1, y2, i);

            //var xx = Mathf.Lerp(x2, x3, i);
            //var yy = Mathf.Lerp(y2, y3, i);

            //var xxx = Mathf.Lerp(x, xx, i);
            //var yyy = Mathf.Lerp(y, yy, i);

            if (!isInstantiating)
            {
                var segment = _segments[(int)i * 10];
                segment.transform.position = new Vector3(x, y, transform.position.z);
                continue;
            }

            var spherePos = new Vector3(x, y, transform.position.z);
            _segments[(int)i * 10] = Instantiate(spherePrefab, spherePos, transform.rotation);
        }
    }
}
