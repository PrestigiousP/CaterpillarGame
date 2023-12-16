using Assets.Prefabs.scripts;
using UnityEngine;

public class Segment : MonoBehaviour
{
    public GameObject SpherePrefab; // Assign the sphere prefab in the Inspector

    private Rigidbody _rigidbody;
    private Vector3 _initPosition;

    public Segment(Vector3 initialPosition)
    {
        _initPosition = initialPosition;
        _rigidbody = gameObject.AddComponent<Rigidbody>();
        tag = Tags.PlayerSegment;
    }

    // Start is called before the first frame update
    void Start()
    {
        var segment = Instantiate(SpherePrefab,
            new Vector3(_initPosition.x, _initPosition.y, _initPosition.z),
            transform.rotation);

        //_segment.AddComponent(typeof(Rigidbody));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
