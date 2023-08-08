using UnityEngine;

public class Segment : MonoBehaviour
{
    public Transform previousBone;
    private Transform _previousTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var distanceVec = transform.position - previousBone.position;

        if (distanceVec.magnitude > 0.2f)
        {
            var previousPosVec = transform.position - _previousTransform.position;

            transform.position = new Vector3(
                    transform.position.x + previousPosVec.x,
                    transform.position.y + previousPosVec.y,
                    transform.position.z + previousPosVec.z);

        }

        _previousTransform = transform;
    }
}
