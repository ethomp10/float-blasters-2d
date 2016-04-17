using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class CameraFollow : MonoBehaviour
    {
        public static Transform target;

        private void Start () {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            
            if (player != null) {
                target = player.transform;
            }
        }

        private void Update () {
            if (target != null)
                transform.position = new Vector3 (target.position.x, target.position.y, transform.position.z);
            else {
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 100, Time.deltaTime);
                FindPlayer();
            }
        }
        
        private void FindPlayer () {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            
            if (player != null) {
                target = player.transform;
                transform.parent = null;
            }
        }
    }
}
