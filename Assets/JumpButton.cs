using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpButton : MonoBehaviour
{
    //public GameObject character;
    private Button _jumpButton;

    public MyCharacterController characterComp;
    
    // Start is called before the first frame update
    void Start()
    {
        _jumpButton = GetComponent<Button>();
        _jumpButton.onClick.AddListener(Jump);
        //_jumpButton.onClick.AddListener(() => { Jump(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Jump()
    {
        // if (character != null)
        // {
        //     MyCharacterController script = character.GetComponent<MyCharacterController>();
        //     character.GetComponent<Rigidbody>().AddForce(Vector3.up * script.jumpPower, ForceMode.Impulse);
        // }

        // 위 방식과 다르게 캡슐화 은닉화 상태 , ? => Null 체크
        characterComp?.Jump();
    }
}
