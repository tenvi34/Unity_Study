using UnityEngine;
using UnityEngine.UI;

public class SliderComponent : MonoBehaviour
{
    [SerializeField] private MyCharacterController character;
    [SerializeField] private Slider slider;

    // Start is called before the first frame update
    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(OneValueChanged);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OneValueChanged(float value)
    {
        character.jumpPower = value * 10.0f;
    }
}