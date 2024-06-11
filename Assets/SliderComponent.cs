using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderComponent : MonoBehaviour
{
    [SerializeField] private MyCharacterController character;
    [SerializeField] private Slider slider;
    
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(OneValueChanged);
    }

    void OneValueChanged(float value)
    {
        character.jumpPower = value * 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
