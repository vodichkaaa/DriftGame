using UnityEngine;

public enum ControlMode
{
    Keyboard = 1,
    Touch = 2
};

public class InputSystem : MonoBehaviour
{
    public ControlMode controlMode;
    public float acceleration;
    public float steering;
    public GameObject[] uiElements;

    public void AccelerationInput(float input)
    {
        acceleration = input;
    }
    
    public void SteeringInput(float input)
    {
        steering = input;
    }

    private void Update()
    {
        if (controlMode == ControlMode.Keyboard)
        {
            acceleration = Input.GetAxis("Vertical");
            steering = Input.GetAxis("Horizontal");
            foreach (var ui in uiElements)
            {
                ui.SetActive(false);
            }
        }
        else
        {
            foreach (var ui in uiElements)
            {
                ui.SetActive(true);
            }
        }
    }
}
