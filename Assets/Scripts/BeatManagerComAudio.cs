using UnityEngine;

public class BeatManagerComAudio : MonoBehaviour
{
    public static BeatManagerComAudio Instance;

    public AudioSource music;
    public float bpm = 100f;

    private float beatInterval;
    private float dspStartTime;
    public float songPosition;

    public delegate void BeatAction();
    public static event BeatAction OnBeat;

    void Start()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        beatInterval = 60f / bpm;
        dspStartTime = (float)AudioSettings.dspTime;
        music.Play();
    }

    void Update()
    {
        songPosition = (float)(AudioSettings.dspTime - dspStartTime);

        if (songPosition >= beatInterval)
        {
            OnBeat?.Invoke();
            beatInterval += 60f / bpm;
        }
    }
}