using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTowardsPlayer : MonoBehaviour
{
    private int assistantOffset = 90;
    private bool active = true;

    [SerializeField]
    Transform _object;

    [SerializeField]
    MinMaxSliderFloat _headVerticalRotationBounds;

    private void Awake()
    {
        if (_object == null)
            _object = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (active)
        {
            Vector3 direction = transform.position - _object.position;

            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            rotation = Quaternion.Euler(0, rotation.eulerAngles.y - assistantOffset, rotation.eulerAngles.z);

            var desiredAngle = Vector3.SignedAngle(transform.position, direction, Vector3.right);

            Debug.Log("pre-clamp angle: " + desiredAngle);

            desiredAngle = Mathf.Clamp(desiredAngle, _headVerticalRotationBounds.Min, _headVerticalRotationBounds.Max);

            Debug.Log("post-clamp angle: " + desiredAngle);

            rotation = rotation * Quaternion.Euler(0, 0, desiredAngle);

            transform.rotation = rotation;
        }
    }

    public void Active(bool activate)
    {
        active = activate;
    }

}
