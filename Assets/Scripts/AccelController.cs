using System.Xml.Schema;
using UnityEngine;

public class AccelController : MonoBehaviour
{
    public float speed = 30f;
    public float deadzone = 0.012f;
    public float sleepVel = 0.02f;
    public Vector3 rotFixEuler = new Vector3(90, 0, 0);
    public bool autocalibrationOnStart = true;

    Rigidbody rb;
    Quaternion calib = Quaternion.identity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = 0.1f;
        rb.angularDamping = 0.1f;
        Input.gyro.enabled = true;

        if (autocalibrationOnStart == true)
        {
            calib = GetWorldAttitude();
        }
        
    }

    Quaternion GetWorldAttitude()
    {
        var g = Input.gyro.attitude;
        var q = new Quaternion(g.x, g.y, -g.z, -g.w);
        return Quaternion.Euler(rotFixEuler) * q;
    }

    void ZeroMotion()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        Quaternion worldRot = GetWorldAttitude();
        Quaternion rel = Quaternion.Inverse(calib) * worldRot;

        Vector3 fwd = rel * Vector3.forward;
        Vector3 dir = new Vector3(fwd.x, 0f, fwd.z);

        if (dir.magnitude < deadzone)
        {
            if(rb.linearVelocity.magnitude < sleepVel && rb.angularVelocity.magnitude < sleepVel)
            {
                rb.Sleep();
            }
            return;
        }
        rb.WakeUp();
        rb.AddForce(dir.normalized * dir.magnitude * speed, ForceMode.Acceleration);
    }
}
