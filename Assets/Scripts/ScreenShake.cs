using UnityEngine;

public class ScreenShake : MonoBehaviour
{

    // Intensity of the screen shake
    public float intensity = 0.1f;

    // Duration of the screen shake
    public float duration = 0.5f;

    // Decay rate of the screen shake
    public float decay = 0.02f;

    // Internal timer to track the screen shake duration
    private float timer = 0;

    // Internal vector to track the screen shake offset
    private Vector3 offset = Vector3.zero;

    void Update()
    {
        // If the screen shake is active
        if (timer > 0)
        {

            // Calculate the new screen shake offset
            offset = Random.insideUnitSphere * intensity;

            // Apply the offset to the camera
            transform.position += offset;

            // Decrement the timer
            timer -= Time.deltaTime * decay ;
        }
        // If the screen shake has finished
        else
        {

            // Reset the screen shake offset
            offset = Vector3.zero;

            // Disable the script
            enabled = false;
        }
    }

    // Public function to trigger the screen shake
    public void Shake()
    {
        // Reset the timer and enable the script
        timer = duration;
        enabled = true;
    }
}
