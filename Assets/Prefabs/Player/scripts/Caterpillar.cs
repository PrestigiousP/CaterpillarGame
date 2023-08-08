using UnityEngine;

public class Caterpillar : MonoBehaviour
{
    public PlayerCamera playerCamera;
    public int numberOfSegments;
    public float length;
    public float width;
    public GameObject[] Segments { get; private set; }

    private float _segmentLength;

    void Start()
    {
        Segments = new GameObject[numberOfSegments];
        _segmentLength = length / numberOfSegments;


        InstantiateCaterpillar();
    }

    void Update()
    {
        //Move();
        StickSegment();
    }

    //private void Move()
    //{
    //    bool w = Input.GetKey(KeyCode.W);
    //    bool a = Input.GetKey(KeyCode.A);
    //    bool s = Input.GetKey(KeyCode.S);
    //    bool d = Input.GetKey(KeyCode.D);

    //    if (w)
    //    {
    //        //Component[] childComponents = transform.GetComponentsInChildren<Component>();

    //        //foreach (var component in childComponents)
    //        //{
    //        //    if (component.name == "Armature")
    //        //    {
    //        //        Debug.Log($"Component: {component.name}, Type: {component.GetType()}");


    //        //    }
    //        //}
    //    }

    //    //if (w)
    //    //{
    //    //    var aimVector = playerCamera.transform.position - Segments[7].transform.position;

    //    //    var speed = 0.01f * Time.deltaTime;

    //    //    Segments[7].transform.position = new Vector3(aimVector.x + speed, Segments[7].transform.position.y, aimVector.z + speed);
    //    //}
    //}

    private void StickSegment()
    {
        transform.position = new Vector3(
                transform.position.x + 0.001f,
                transform.position.y + 0.001f,
                transform.position.z + 0.001f
            );

        var seg1 = Segments[0].GetComponent<Rigidbody>();
        var seg2 = Segments[1].GetComponent<Rigidbody>();

        seg1.MovePosition(seg1.position + new Vector3(0.001f, .0001f, 0));

        Debug.Log($"seg1 pointb: {seg1} {seg2}");
        //for (int i = 0; i < numberOfSegments; i++)
        //{
        //    if (i != 0)
        //    {
        //        var previousSegment = Segments[i - 1].GetComponent<Segment>();
        //        var segment = Segments[i].GetComponent<Segment>();

        //        //segment.PointA.transform.position = previousSegment.PointB.transform.position;

        //        Segments[i].transform.position = new Vector3(
        //                1,
        //                1,
        //                1
        //            );

        //        Debug.Log($"Point A: {segment.PointA.position} Point B: {segment.PointB.position}");

        //        //var position = previousSegment.PointB.position - segment.PointA.position;

        //        //float yVector = 0;
        //        //float xVector = 0;
        //        //float zVector = 0;

        //        //if (position.x > 0.2f)
        //        //{
        //        //    xVector = 0.2f;
        //        //}
        //        //if (position.y > 0.2f)
        //        //{
        //        //    yVector = 0.2f;
        //        //}
        //        //if (position.z > 0.2f)
        //        //{
        //        //    zVector = 0.2f;
        //        //}

        //        //var vector = new Vector3(xVector, yVector, zVector);

        //        //previousSegment.transform.position = vector;
        //    }
        //}
    }

    //private void InstantiateCaterpillar()
    //{
    //    var currentPos = gameObject.transform.position;

    //    for (int i = 0; i < numberOfSegments; i++)
    //    {
    //        Segments[i] = Instantiate(cylinderPrefab,
    //            new Vector3(currentPos.x, currentPos.y, currentPos.z),
    //            new Quaternion());

    //        Segments[i].transform.localScale = new Vector3(0, 0, 0);


    //        if (i != 0)
    //        {
    //            var previousCylinder = Segments[i - 1];

    //            Segments[i] = Instantiate(cylinderPrefab,
    //                new Vector3(currentPos.x,
    //                    previousCylinder.transform.position.y + previousCylinder.transform.localScale.y + _segmentLength,
    //                    currentPos.z),
    //                new Quaternion());

    //            Segments[i].transform.localScale = new Vector3(width, _segmentLength, width);

    //        }
    //    }
    //}
}
