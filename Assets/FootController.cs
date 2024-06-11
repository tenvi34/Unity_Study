using UnityEngine;

public class FootController : MonoBehaviour
{
    public AudioSource FootSoundSource;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void OnFoot()
    {
        if (FootSoundSource != null) FootSoundSource.Play();
    }
}