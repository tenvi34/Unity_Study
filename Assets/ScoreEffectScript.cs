using UnityEngine;

public class ScoreEffectScript : MonoBehaviour
{
    // 내 오브젝트가 사라질 때 나타날 효과
    public GameObject HitEffect;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void OnHit()
    {
        // 내 오브젝트가 있는 곳에 HitEffect 게임 오브젝트를 스폰
        Instantiate(HitEffect, transform.position, Quaternion.identity);
        // 충돌 시 아이템 제거
        Destroy(gameObject);
    }
}