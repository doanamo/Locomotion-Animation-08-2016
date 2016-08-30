using UnityEngine;

public class ControllerPID
{
    public float pFactor;
    public float iFactor;
    public float dFactor;

    private float integral;
    private float previousError;

    public ControllerPID()
    {
        this.pFactor = 1.0f;
        this.iFactor = 0.0f;
        this.dFactor = 0.0f;
    }

    public ControllerPID(float pFactor, float iFactor, float dFactor)
    {
        this.pFactor = pFactor;
        this.iFactor = iFactor;
        this.dFactor = dFactor;
    }

    public float Update(float currentError, float deltaTime)
    {
        float proportional = currentError;
        integral += proportional * deltaTime;
        float derivative = (proportional - previousError) / deltaTime;
        previousError = currentError;

        return proportional * pFactor + integral * iFactor + derivative * dFactor;
    }
}
