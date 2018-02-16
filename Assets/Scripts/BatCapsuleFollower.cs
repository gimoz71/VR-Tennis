using UnityEngine;

public class BatCapsuleFollower : MonoBehaviour
{
    private BatCapsule _batFollower;
    private Rigidbody _rigidbody;
    private Vector3 _velocity;
    public float _speed;

    [SerializeField]
    private float _sensitivity = 100f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 destination = _batFollower.transform.position;
		_rigidbody.transform.rotation = _batFollower.transform.rotation;

        _velocity = (destination - _rigidbody.transform.position) * _sensitivity;

        _rigidbody.velocity = _velocity;

        _speed = _velocity.magnitude;

        Debug.Log(_speed);

    }

    public void printSpeed() {
        Debug.Log(_speed);
    }

    public void SetFollowTarget(BatCapsule batFollower)
    {
        _batFollower = batFollower;
    }
}