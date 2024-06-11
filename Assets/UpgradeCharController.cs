using UnityEngine;

public delegate void HpStatusBroadCast(float currentHp, float maxHp);

public class UpgradeCharController : MonoBehaviour
{
    public float MoveSpeed = 5.0f;
    public float RotSpeed = 180.0f;
    public float JumpPower = 5;

    public UpgradeHpBar hpBarComp;

    private float currentHp;

    public HpStatusBroadCast HpStatusBroadCastDelegates;
    private float maxHp;

    private Vector3 MoveDirection = Vector3.zero;
    private Vector3 RotDirection = Vector3.zero;

    [field: SerializeField]
    private float CurrentHp
    {
        get => currentHp;
        set
        {
            currentHp = value;
            HpStatusBroadCastDelegates?.Invoke(CurrentHp, MaxHp);
        }
    }

    [field: SerializeField]
    private float MaxHp
    {
        get => maxHp;
        set
        {
            maxHp = value;
            HpStatusBroadCastDelegates?.Invoke(CurrentHp, MaxHp);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        CurrentHp = 400;
        MaxHp = 400;

        HpStatusBroadCastDelegates += hpBarComp.UpdateHpStatus;
    }

    // Update is called once per frame
    private void Update()
    {
        var Vertical = Input.GetAxis("Vertical");
        var Horizontal = Input.GetAxis("Horizontal");

        MoveDirection = transform.forward * Vertical;
        RotDirection = transform.right * Horizontal;

        GetComponent<Animator>().SetFloat("Speed", Vertical);

        if (Input.GetKeyDown(KeyCode.P)) CurrentHp -= 10;
    }

    private void FixedUpdate()
    {
        if (MoveDirection != Vector3.zero)
        {
            var nextPosition = Vector3.MoveTowards(transform.position,
                transform.position + MoveDirection * 1000.0f,
                Time.fixedDeltaTime * MoveSpeed);

            GetComponent<Rigidbody>().MovePosition(nextPosition);
        }

        if (RotDirection != Vector3.zero)
        {
            var nextRotaton = Vector3.RotateTowards(transform.forward,
                RotDirection, Time.fixedDeltaTime * RotSpeed * Mathf.Deg2Rad,
                0.0f);

            GetComponent<Rigidbody>()
                .MoveRotation(Quaternion.LookRotation(nextRotaton));
        }
    }

    public void Jump()
    {
        GetComponent<Rigidbody>()
            .AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
    }
}