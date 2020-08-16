using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        Vector3 lookToVector = new Vector3();
        public Sprite currentReticle;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dir = Input.mousePosition - objectPos;

            lookToVector = Vector3.zero;
            lookToVector.z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(lookToVector);

        }
    }
}