using UnityEngine;
using System.Collections;
namespace SparkVR
{
    public class BezierHelper
    {
        /// <summary>
        /// generate a Bezier curve.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        public static Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            Vector3 p = uuu * p0;
            p += 3 * uu * t * p1;
            p += 3 * u * tt * p2;
            p += ttt * p3;

            return p;
        }

        /// <summary>
        /// Get a point on the Bezier curve.
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static Vector3[] GetBezierCurvePoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, int count)
        {
            Vector3[] result = new Vector3[count];
            float t = 0.0f;

            for (int i = 0; i < count; ++i)
            {
                t = i / (float)count;

                result[i] = CalculateCubicBezierPoint(t, p0, p1, p2, p3);
            }

            return result;
        }

        /// <summary>
        /// Predict Trajectory
        /// </summary>
        /// <param name="rigidbody"></param>
        /// <param name="startPos"></param>
        /// <param name="velocity"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static Vector3[] PredictTrajectory(Rigidbody rigidbody, Vector3 startPos, Vector3 velocity, int step)
        {
            Vector3[] results = new Vector3[step];
            float timestep = 0.1f;
            Vector3 gravityAccel = Physics.gravity * timestep * timestep;
            Vector3 moveStep = velocity * timestep;

            for (int i = 0; i < step; ++i)
            {
                moveStep += gravityAccel;
                startPos += moveStep;
                results[i] = startPos;
            }

            return results;
        }

        /// <summary>
        /// get ray cast on ground.
        /// </summary>
        /// <param name="trackPoins"></param>
        /// <returns></returns>
        public static Vector3 RayCastGround(Vector3[] trackPoins)
        {
            RaycastHit hitInfo;

            for (int i = 0; i < trackPoins.Length - 1; ++i)
            {
                Vector3 origin = trackPoins[i];
                Vector3 nextPoint = trackPoins[i + 1];
                Debug.DrawLine(origin, nextPoint, Color.cyan, 5f);
                Vector3 dir = (nextPoint - origin).normalized;
                float maxDistance = Vector3.Distance(origin, nextPoint);

                if (Physics.Raycast(origin, dir, out hitInfo, maxDistance))
                {
                    return hitInfo.point;
                }
            }
            return Vector3.zero;
        }

        public static Vector3 GetTargetPoint(Rigidbody rig, Vector3 startPos, Vector3 velocity, int step)
        {
            return RayCastGround(PredictTrajectory(rig, startPos, velocity, step));
        }

        /// <summary>
        /// Calculate the velocity of the tennis ball
        /// </summary>
        /// <returns></returns>
        static public Vector3 CalulateStartVelocity(float flyTime, Vector3 target, Vector3 startPoint)
        {
            Vector3 gravity = Physics.gravity;
            Vector3 upVelocity = Vector3.zero;
            Vector3 forwardVelocity = Vector3.zero;

            // up velocity
            upVelocity = -gravity * 0.5f * flyTime;
            // forward velocity
            forwardVelocity = (target - startPoint) / flyTime;
            Debug.DrawRay(startPoint, upVelocity, Color.green, 10f);
            Debug.DrawRay(startPoint, forwardVelocity, Color.red, 10f);
            Vector3 result = upVelocity + forwardVelocity;
            return result;
        }
    }

}
