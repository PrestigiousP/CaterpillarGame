using Assets.Prefabs.scripts;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject spherePrefab; // Assign the sphere prefab in the Inspector
    public int length; // 5
    public int nbOfSegments; // 10
    public float maxCurve; // 2.5f
    public float stretchSpeed; // 0.01f

    private Vector3 _startPoint;
    private Vector3 _endPoint;
    private Vector3 _firstControlPoint;
    private Vector3 _secondControlPoint;
    private GameObject[] _segments;

    // Start is called before the first frame update
    void Start()
    {
        InstantiateBezierPoints();
        SetInitialPosition();
        DisableInterSegmentPhysics();
    }

    // Update is called once per frame
    void Update()
    {
        //DisableInterSegmentPhysics();
        //MoveControlPoints();
        //SetSegmentPositions();
    }

    // Empêche que les segments entre eux
    // soient soumis à de la physique
    private void DisableInterSegmentPhysics()
    {
        for(int i = 0; i < _segments.Length; i++)
        {
            if (i != 0)
            {
                // Check for collisions between obj1 and obj2
                Collider collider1 = _segments[i - 1].GetComponent<Collider>();
                Collider collider2 = _segments[i].GetComponent<Collider>();

                if (collider1 != null && collider2 != null)
                {
                    if (collider1.bounds.Intersects(collider2.bounds))
                    {
                        // Collision detected between obj1 and obj2
                        Debug.Log(_segments[i].name + " and " + _segments[i - 1].name + " are colliding.");
                        Physics.IgnoreCollision(collider1, collider2);
                    }
                }
            }
        }
    }

    private void InstantiateBezierPoints()
    {
        _segments = new GameObject[10];

        _startPoint = transform.position;

        _endPoint = new Vector3(
                transform.position.x + length,
                transform.position.y,
                transform.position.z);
        _firstControlPoint = new Vector3(
                Mathf.Lerp(transform.position.x, transform.position.x + length, 0.3f),
                transform.position.y,
                transform.position.z);
        _secondControlPoint = new Vector3(
            Mathf.Lerp(transform.position.x, transform.position.x + length, 0.7f),
            transform.position.y,
            transform.position.z);
    }

    private void MoveControlPoints()
    {
        bool w = Input.GetKey(KeyCode.W);
        if(w)
        {
            var yPos = Mathf.Clamp(_startPoint.y + Time.deltaTime + stretchSpeed,
                _segments[_segments.Length / 2].transform.position.y - maxCurve,
                _segments[_segments.Length / 2].transform.position.y + maxCurve);

            _startPoint = new Vector3(
                    _startPoint.x,
                    yPos,
                    _startPoint.z);
        }
        // TODO
        //else if ()
        //{
        //    var yPos = Mathf.Clamp(_startPoint.y + Time.deltaTime - stretchSpeed,
        //        _segments[_segments.Length / 2].transform.position.y,
        //        _segments[_segments.Length / 2].transform.position.y + maxCurve);

        //    _startPoint = new Vector3(
        //        _startPoint.x,
        //        yPos,
        //        _startPoint.z);
        //}
    }

    private void SetInitialPosition()
    {
        for (int i = 0; i < nbOfSegments; i++)
        {
            var xPos = Mathf.Lerp(_startPoint.x, _startPoint.x + length, (float)i / nbOfSegments);
            var segment = Instantiate(spherePrefab,
                new Vector3(xPos, _startPoint.y, transform.position.z),
                transform.rotation);

            segment.AddComponent(typeof(Rigidbody));
            segment.tag = Tags.PlayerSegment;
            //segment.AddComponent(typeof(SphereCollider));

            _segments[i] = segment;
        }
    }

    private void SetSegmentPositions()
    {
        //float lerpStep = 0;
        float lerpStep;

        for (int i = 0; i < nbOfSegments; i++)
        {
            //lerpStep += 0.1f;
            lerpStep = (float) i / nbOfSegments;
            lerpStep = lerpStep == 0 ? 0.01f : lerpStep;

            var x1 = Mathf.Lerp(_startPoint.x, _firstControlPoint.x, lerpStep);
            var y1 = Mathf.Lerp(_startPoint.y, _firstControlPoint.y, lerpStep);

            var x2 = Mathf.Lerp(_firstControlPoint.x, _secondControlPoint.x, lerpStep);
            var y2 = Mathf.Lerp(_firstControlPoint.y, _secondControlPoint.y, lerpStep);

            var x3 = Mathf.Lerp(_secondControlPoint.x, _endPoint.y, lerpStep);
            var y3 = Mathf.Lerp(_secondControlPoint.y, _endPoint.y, lerpStep);

            var x = Mathf.Lerp(x1, x2, lerpStep);
            var y = Mathf.Lerp(y1, y2, lerpStep);

            var xx = Mathf.Lerp(x2, x3, lerpStep);
            var yy = Mathf.Lerp(y2, y3, lerpStep);

            var xxx = Mathf.Lerp(x, xx, lerpStep);
            var yyy = Mathf.Lerp(y, yy, lerpStep);

            var segment = _segments[i];
            segment.transform.position = new Vector3(xxx, yyy, segment.transform.position.z);            
        }
    }
}
