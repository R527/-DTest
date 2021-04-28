using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public GameObject particale;
    
    AudioSource audioSource;
    bool erasing;


    private void Start() {
        TryGetComponent(out audioSource);
    }
    // Update is called once per frame
    void Update()
    {
        if (!erasing) return;

        if(!audioSource.isPlaying) {
            Instantiate(particale, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void AddEffect() {
        audioSource.Play();
        erasing = true;
    }
}
