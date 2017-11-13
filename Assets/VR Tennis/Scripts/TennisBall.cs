using UnityEngine;
using System.Collections;

namespace SparkVR
{
    public class TennisBall : MonoBehaviour
    {
        [HideInInspector]
        public bool Playing = false;
        public AudioSource AdSource;
        public AudioClip HitGroundSound;
        public AudioClip HitRacketSound;
        public AnimationCurve FlyCurve;
        public float BaseModifier = 3f;
        public float UpperModifier = 2f;
        public float ForwardModifier = 5f;
        public float FlySpeedModifier = 1f;

        private bool doFly = false;
        private int bounceCount = 0;

        private Vector3 p0;
        private Vector3 p1;
        private Vector3 p2;
        private Vector3 p3;
        private float StartFlyTime;
        private float StartTime;
        private float Duration;
        [HideInInspector]
        public Vector3 CurrentFlyDir;
        public Vector3 FlyDirProjectOnGround
        {
            get
            {
                return Vector3.ProjectOnPlane(CurrentFlyDir, Vector3.up);
            }
        }

        private Vector3 BallPrePosition;

        private Rigidbody _RigidBody;
        public Rigidbody RigidBody
        {
            get
            {
                if (_RigidBody == null)
                    _RigidBody = GetComponent<Rigidbody>();
                return _RigidBody;
            }
        }

        private Transform _SelfTrans;
        public Transform SelfTrans
        {
            get
            {
                if (_SelfTrans == null)
                    _SelfTrans = transform;
                return _SelfTrans;
            }
        }

        private Material _Mat;
        private Material Mat
        {
            get
            {
                if (_Mat == null)
                    _Mat = GetComponentInChildren<MeshRenderer>().material;
                return _Mat;
            }
        }

        public Vector3 StartFly(Vector3 startPointOnGround, Vector3 startPoint, Vector3 direction, Vector3 endPoint = default(Vector3))
        {
            // clamp the value in property range.
            float inputValueClamp = Mathf.Clamp(10 * (direction.sqrMagnitude - 1), 1f, 1.5f);

            p0 = startPoint;
            p1 = startPoint + direction * inputValueClamp * BaseModifier + Vector3.up * inputValueClamp * UpperModifier;

            if (endPoint != Vector3.zero)
            {
                p3 = endPoint;
            }
            else
            {
                p3 = startPointOnGround + Vector3.ProjectOnPlane(direction, Vector3.up) * ForwardModifier * inputValueClamp;
            }

            p2 = (p1 + p3) * 0.5f;// p2 is half of p1 and p3

            StartTime = Time.time;

            float clampValue = Mathf.Clamp(10 * (direction.sqrMagnitude - 1), 0.8f, 2.3f);
            clampValue = clampValue - 0.8f;// range lock in 0 ~ 1.5f
            clampValue = 1.5f - clampValue;// inverse range.
            clampValue = FlySpeedModifier * clampValue;
            Duration = clampValue + 0.8f;

            RigidBody.useGravity = false;
            doFly = true;
            bounceCount = 0;
            return p3;
        }

        private void DoFly()
        {
            float passTime = Time.time - StartTime;

            if (passTime < Duration - 0.01f)
            {
                float t = passTime / Duration;
                float anim_t = FlyCurve.Evaluate(t);

                SelfTrans.position = BezierHelper.CalculateCubicBezierPoint(anim_t, p0, p1, p2, p3);
                CurrentFlyDir = (SelfTrans.position - BallPrePosition);

                if (BallPrePosition != Vector3.zero)
                {
                    Debug.DrawLine(BallPrePosition, SelfTrans.position, Color.red, 10f);
                }
            }
            else
            {
                doFly = false;

                //RigidBody.AddForce((CurrentFlyDir * 10),ForceMode.VelocityChange);
                //RigidBody.AddForceAtPosition((CurrentFlyDir * 10), SelfTrans.position);
                RigidBody.useGravity = true;
            }

            BallPrePosition = SelfTrans.position;
        }

        private float currentTime = 0.0f;

        void Update()
        {
            if (doFly)
            {
                DoFly();
            }

            CurrentFlyDir = SelfTrans.position - BallPrePosition;
            BallPrePosition = SelfTrans.position;
        }

        void DetectedHit()
        {
            Vector3 moveDelta = SelfTrans.position - BallPrePosition;
            Ray ray = new Ray(SelfTrans.position, moveDelta);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 0.1f))
            {
                SelfTrans.position = hit.point;
            }

            Debug.DrawRay(SelfTrans.position, moveDelta, Color.black, 1f);

            BallPrePosition = SelfTrans.position;
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("GameGround"))
            {
                bounceCount++;
                doFly = true;
                StartTime = Time.time;
                Duration = Mathf.Lerp(Duration, 0f, (bounceCount / 50f));

                //doBounce = true;
                //RigidBody.useGravity = true;

                Vector3 ReflectDir = Vector3.Reflect(CurrentFlyDir.normalized, Vector3.up);

                //ReflectDir /= bounceCount;
                //Vector3 up = Vector3.up / bounceCount;

                //p0 = SelfTrans.position;
                //p1 = p0 + ReflectDir + up;
                //p3 = p0 + Vector3.ProjectOnPlane(ReflectDir, Vector3.up) * 3;
                //p2 = (p1 + p3) * 0.5f;

                Vector3 flyForward = Vector3.ProjectOnPlane(CurrentFlyDir, Vector3.up);

                //float ret = Vector3.Dot(flyForward, Vector3.forward);
                //Vector3 forward = Vector3.zero;


                RigidBody.velocity = (Vector3.up + flyForward.normalized) * Mathf.Lerp(6f, 0f, bounceCount / 5f);
                //RigidBody.AddForce(ReflectDir);
                //Debug.DrawRay(SelfTrans.position, collision.relativeVelocity,Color.blue,100);
                AdSource.clip = HitGroundSound;
                AdSource.Play();
                Debug.DrawRay(SelfTrans.position, ReflectDir, Color.black, 10);
            }


        }


        public void OnHit()
        {
            StartFlyTime = Time.time;
            bounceCount = 0;
            AdSource.clip = HitRacketSound;
            AdSource.Play();
        }

        public void SetColor(Color color)
        {
            Mat.color = color;
        }
    }

}