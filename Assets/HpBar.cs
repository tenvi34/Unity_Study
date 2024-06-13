using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Image _background;
    public Image _mask;
    public TextMeshProUGUI _hpStringState;

    public MyCharacterController _characterControllerScript;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateHpBarStatus();
    }

    private void UpdateHpBarStatus()
    {
        var currentHp = _characterControllerScript.CurrentHp;
        var maxHp = _characterControllerScript.MaxHp;
        var lerpSpeed = 5f;

        // 체력 텍스트 표시
        _hpStringState.text = string.Format("{0} / {1}", currentHp, maxHp);

        // // mask의 height는 변하지 않을거라서 그냥 가져온다.
        // float height = _mask.GetComponent<RectTransform>().sizeDelta.y;
        //
        // // background의 width값이 x인데 이게 최대 크기이니까 fullWidth라 명명한다.
        // float fullWidth = _background.GetComponent<RectTransform>().sizeDelta.x;
        //
        // // hp / maxHp => 0~1사이의 값을 갖게되고 0.5 * fullWidth하게 되면 => 절반으로 마스킹 사이즈가 된다.
        // _mask.GetComponent<RectTransform>().sizeDelta = new Vector2(currentHp / maxHp * fullWidth, height);
        var fillAmount = currentHp / maxHp;
        _mask.fillAmount = Mathf.Lerp(_mask.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
    }
}