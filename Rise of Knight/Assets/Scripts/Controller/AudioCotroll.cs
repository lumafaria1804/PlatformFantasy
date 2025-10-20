using UnityEngine;

public class AudioCotroll : MonoBehaviour
{
    [SerializeField] private AudioClip bmgMusic;
    private AudioController audioController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioController = FindObjectOfType<AudioController>();

        audioController.PlayBMG(bmgMusic);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
