using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLook : MonoBehaviour
{
    public PlayerController player;
    [SerializeField] Transform cameraSolt;
    [SerializeField] Vector3 offset;
    public float sensitivity = 1f;
    public float minPitch = -30f;
    public float maxPitch = 70f;

    private float yaw;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 lookInput = player.input.Player.Look.ReadValue<Vector2>();
        float pitch = lookInput.y * Time.deltaTime * -1f;
        Vector3 angles = cameraSolt.localEulerAngles;

        float newPitch = angles.x + pitch;

        newPitch = (newPitch > 180) ? newPitch - 360 : newPitch;
        newPitch = Mathf.Clamp(newPitch, minPitch, maxPitch);
        
        cameraSolt.localEulerAngles = new Vector3(newPitch, angles.y, angles.z);


        transform.position = cameraSolt.position + cameraSolt.forward * offset.z;
        transform.LookAt(cameraSolt);
    }
}
