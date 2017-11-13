using UnityEngine;
using System.Collections;

namespace SparkVR
{
    public class Player : MonoBehaviour
    {
        private Transform m_SelfTrans;

        public Transform SelfTrans
        {
            get
            {
                if (m_SelfTrans == null)
                    m_SelfTrans = transform;
                return m_SelfTrans;
            }
        }

        /// <summary>
        /// move to target position in seconds
        /// </summary>
        /// <param name="Destination"></param>
        /// <param name="duration"></param>
        public void MoveToPosition(Vector3 Destination, float duration)
        {
            StartCoroutine(MoveThread(Destination, duration));
        }

        /// <summary>
        /// move to somewhere from position to destination with a stably speed.
        /// </summary>
        /// <param name="fromPos"></param>
        /// <param name="Destination"></param>
        /// <param name="speed"></param>
        public void MoveToPosition(Vector3 fromPos, Vector3 Destination, float speed)
        {
            float dist = Vector3.Distance(fromPos, Destination);
            float duration = dist / speed;
            StartCoroutine(MoveThread(Destination, duration));
        }

        private IEnumerator MoveThread(Vector3 Dest, float dur)
        {
            float startTime = Time.time;
            float passTime = 0.0f;
            Vector3 startPos = SelfTrans.position;
            while (passTime <= dur)
            {
                passTime = Time.time - startTime;
                float t = passTime / dur;
                Vector3 wantedPos = Vector3.Lerp(startPos, Dest, t);
                wantedPos.y = 0f;
                SelfTrans.position = wantedPos;
                yield return null;
            }
        }
    }
}
