using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace SparkVR
{
    public class Racket : MonoBehaviour
    {

        /// <summary>
        /// swipe sensitive
        /// </summary>
        public float SwipeSensitive = 1f;

        /// <summary>
        /// swipe left or right direction modifier
        /// </summary>
        public float SwipeXModifier = 1f;

        /// <summary>
        /// swipe up or down direction modifier
        /// </summary>
        public float SwipeYModifier = 1f;

        /// <summary>
        /// swipe forward or backward direction modifier
        /// </summary>
        public float SwipeZModifier = 1f;

        /// <summary>
        /// swipe power modifier
        /// </summary>
        public float SwipePowerModifier = 1f;

        /// <summary>
        /// SteamVR Controller reference.
        /// </summary>
        public Transform Controller;

        /// <summary>
        /// the racket detected point.
        /// </summary>
        public Transform DetectedTrans;

        /// <summary>
        /// for debug
        /// </summary>
        public float DetectedDist = 0.3f;

        /// <summary>
        /// how many frame count we cached.
        /// </summary>
        public int CachedCount = 5;

        /// <summary>
        /// cache the swipe directions.
        /// </summary>
        //private List<Vector3> CachedSwipeDirection = new List<Vector3>();

        /// <summary>
        /// cache the swipe angles.
        /// </summary>
        private List<Vector3> CachedSwipeAngle = new List<Vector3>();

        /// <summary>
        /// the previous position of the racket.
        /// </summary>
        private Vector3 PrePosition;

        /// <summary>
        /// current tennis ball we handled.
        /// </summary>
        private TennisBall currentTennisBall;

        /// <summary>
        /// the previous detected position of the racket
        /// </summary>
        //private Vector3 PreDetectedPos;

        /// <summary>
        /// the previous forward direction of the racket
        /// </summary>
        private Vector3 PreRocketForward;

        /// <summary>
        /// the previous angle of the racket's x axis.
        /// </summary>
        private float PreAngleX;

        /// <summary>
        /// the previous angle of the racket's y axis.
        /// </summary>
        private float PreAngleY;

        /// <summary>
        /// the previous angle of the racket's z axis.
        /// </summary>
        private float PreAngleZ;

        /// <summary>
        /// the Previous swipe direction of the racket.
        /// </summary>
        //private Vector3 PreSwipeDirection;

        /// <summary>
        /// the previous rotation of the racket.
        /// </summary>
        Quaternion PreRotation;

        private Transform _SelfTrans;

        /// <summary>
        /// Cached self transform.
        /// </summary>
        public Transform SelfTrans
        {
            get
            {
                if (_SelfTrans == null)
                    _SelfTrans = transform;
                return _SelfTrans;
            }
        }

        // Use this for initialization
        void Start()
        {
            PrePosition = DetectedTrans.position;

        }

        public void HandleDayDreamRacket()
        {
            // TODO:if you have a day dream device,you can just use the rotation of daydream controller.
            // for example : 
            // SelfTrans.rotation = DaydreamController.rotation.

            // now we just use pc mouse to simulate the daydream controller.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 targetPoint = ray.origin + ray.direction * 50f;
            SelfTrans.LookAt(targetPoint);
        }

        public void HandleSteamRacket()
        {
            // steam vr controller's rotation
            SelfTrans.rotation = Controller.rotation;
            SelfTrans.position = Controller.position;
        }

        /// <summary>
        /// calculate the racket swipe power and direction.
        /// </summary>
        /// <returns></returns>
        public float CalculateSwipePowerAndDir(out Vector3 swipeDir)
        {
            // calculate current swipe direction.
            Vector3 currentPoint = DetectedTrans.position;
            Vector3 currentSwipeDirection = (currentPoint - PrePosition) * SwipeSensitive;
            PrePosition = DetectedTrans.position;

            // we ignore tiny swipe.
            if (currentSwipeDirection.sqrMagnitude < 0.002f)
            {
                CachedSwipeAngle.Clear();
            }

            swipeDir = Vector3.zero;
            float force = Quaternion.Angle(SelfTrans.rotation, PreRotation);
            PreRotation = SelfTrans.rotation;

            PreAngleX = Vector3.Angle(PreRocketForward, Vector3.right);
            PreAngleY = Vector3.Angle(PreRocketForward, Vector3.up);
            PreAngleZ = Vector3.Angle(PreRocketForward, Vector3.forward);

            float AngleX = Vector3.Angle(SelfTrans.forward, Vector3.right);
            float AngleY = Vector3.Angle(SelfTrans.forward, Vector3.up);
            float AngleZ = Vector3.Angle(SelfTrans.forward, Vector3.forward);

            float deltaAngleX = AngleX - PreAngleX;
            float deltaAngleY = AngleY - PreAngleY;
            float deltaAngleZ = AngleZ - PreAngleZ;

            Vector3 angle = new Vector3(deltaAngleX, deltaAngleY, deltaAngleZ);

            if (CachedSwipeAngle.Count < CachedCount)
            {
                CachedSwipeAngle.Add(angle);
            }
            else
            {
                CachedSwipeAngle.RemoveAt(0);
                CachedSwipeAngle.Add(angle);
            }

            for (int i = 0; i < CachedSwipeAngle.Count; ++i)
            {
                swipeDir += CachedSwipeAngle[i];
            }

            PreRocketForward = SelfTrans.forward;

            swipeDir = -swipeDir;
            swipeDir.x = swipeDir.x * SwipeXModifier;
            swipeDir.y = swipeDir.y * SwipeYModifier;
            swipeDir.z = swipeDir.z * SwipeZModifier;

            // if the swipe direction is backward,we ignore the swipe power.
            if (swipeDir.z < 0)
            {
                //force = 0f;
            }

            return force * SwipePowerModifier;
        }

        /// <summary>
        /// handle serve
        /// </summary>
        /// <param name="power"></param>
        /// <param name="serveDir"></param>
        /// <returns></returns>
        public bool HandleServe(out float power, out Vector3 serveDir)
        {
            power = 0.0f;
            serveDir = Vector3.zero;
            return false;
        }

        public void SetBall(TennisBall ball)
        {
            PrePosition = DetectedTrans.position;
            currentTennisBall = ball;
        }

        public void ResetRacket()
        {
            CachedSwipeAngle.Clear();
            PrePosition = DetectedTrans.position;
            PreRocketForward = SelfTrans.forward;
        }

        private int HitTennisBallFrameCount = int.MinValue;

        void OnDrawGizmos()
        {
            Gizmos.color = new Color(1f, 1f, 1f, 0.5f);
            Gizmos.DrawSphere(DetectedTrans.position, DetectedDist);
        }
    }

}