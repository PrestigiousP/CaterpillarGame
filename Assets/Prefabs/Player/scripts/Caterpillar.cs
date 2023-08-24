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
    }

    void Update()
    {
        Move();
        StickSegment();
    }

    private void Move()
    {
        bool w = Input.GetKey(KeyCode.W);
        bool a = Input.GetKey(KeyCode.A);
        bool s = Input.GetKey(KeyCode.S);
        bool d = Input.GetKey(KeyCode.D);

        if (w)
        {
            //Component[] childComponents = transform.GetComponentsInChildren<Component>();

            //foreach (var component in childComponents)
            //{
            //    if (component.name == "Armature")
            //    {
            //        Debug.Log($"Component: {component.name}, Type: {component.GetType()}");


            //    }
            //}
        }

        //if (w)
        //{
        //    var aimVector = playerCamera.transform.position - Segments[7].transform.position;

        //    var speed = 0.01f * Time.deltaTime;

        //    Segments[7].transform.position = new Vector3(aimVector.x + speed, Segments[7].transform.position.y, aimVector.z + speed);
        //}
    }

    private void StickSegment()
    {
        var armature = GetComponentInChildren<Rigidbody>();
        var firstBone = armature.transform.Find("Bone");
        var secondBone = firstBone.transform.Find("Bone.001");

        Debug.Log($"first: {firstBone.transform.rotation} second: {secondBone.transform.rotation}");
    }
}
