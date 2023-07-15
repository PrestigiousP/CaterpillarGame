using UnityEngine;

public class ArmMovement : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float translationY = Input.GetAxis("Vertical");
        float translationX = Input.GetAxis("Horizontal");

        var armSegments = GameObject.FindGameObjectsWithTag("Segment");

        Debug.Log(gameObject.transform.rotation.z);

        armSegments[0].transform.rotation = Quaternion.Euler(
            armSegments[0].transform.rotation.z + translationX * 10f,
            armSegments[0].transform.rotation.z + translationY * 10f,
            armSegments[0].transform.rotation.z);
    }
}
