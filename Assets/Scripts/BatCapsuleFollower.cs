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
        transform.rotation = _batFollower.transform.rotation;

        // registro nella variabile speed il valore della velocità
        _speed = _velocity.magnitude * 10;

    }


    // Setto gli scaglioni di velocità all'impatto
    public static string GetSpeedKey(float speed)
    {
        Debug.Log(speed);

        if (speed < 40f)
        {
            return "Lento";
        }
        if (speed > 40f && speed < 60f)
        {
            return "Medio";
        }
        if (speed > 60f)
        {
            return "Veloce";
        }

        return "Lento";
    }

    public void SetFollowTarget(BatCapsule batFollower)
    {
        _batFollower = batFollower;
    }
}