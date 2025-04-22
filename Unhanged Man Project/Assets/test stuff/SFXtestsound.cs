using UnityEngine;

public class SFXtestsound : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private AudioSource sfxSource;
    public void TestSFX() {
    
        sfxSource.Play();
    
    }
}
