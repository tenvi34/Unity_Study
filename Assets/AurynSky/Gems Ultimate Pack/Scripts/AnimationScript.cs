using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public bool isAnimated;

    public bool isRotating;
    public bool isFloating;
    public bool isScaling;

    public Vector3 rotationAngle;
    public float rotationSpeed;

    public float floatSpeed;
    public float floatRate;

    public Vector3 startScale;
    public Vector3 endScale;
    public float scaleSpeed;
    public float scaleRate;
    private float floatTimer;
    private bool goingUp = true;
    private float scaleTimer;

    private bool scalingUp = true;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (isAnimated)
        {
            if (isRotating) transform.Rotate(rotationAngle * rotationSpeed * Time.deltaTime);

            if (isFloating)
            {
                floatTimer += Time.deltaTime;
                var moveDir = new Vector3(0.0f, 0.0f, floatSpeed);
                transform.Translate(moveDir);

                if (goingUp && floatTimer >= floatRate)
                {
                    goingUp = false;
                    floatTimer = 0;
                    floatSpeed = -floatSpeed;
                }

                else if (!goingUp && floatTimer >= floatRate)
                {
                    goingUp = true;
                    floatTimer = 0;
                    floatSpeed = +floatSpeed;
                }
            }

            if (isScaling)
            {
                scaleTimer += Time.deltaTime;

                if (scalingUp)
                    transform.localScale = Vector3.Lerp(transform.localScale, endScale, scaleSpeed * Time.deltaTime);
                else if (!scalingUp)
                    transform.localScale = Vector3.Lerp(transform.localScale, startScale, scaleSpeed * Time.deltaTime);

                if (scaleTimer >= scaleRate)
                {
                    if (scalingUp)
                        scalingUp = false;
                    else if (!scalingUp) scalingUp = true;
                    scaleTimer = 0;
                }
            }
        }
    }
}