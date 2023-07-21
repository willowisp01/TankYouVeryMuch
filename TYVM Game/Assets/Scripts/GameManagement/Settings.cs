using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    [SerializeField]
    private Slider volumeSlider;
    private float volume;
    private float tempVolume;

    [SerializeField]
    private GameEventListener resumeListener;

    private void Awake() {
        volume = SaveDataManager.GetSaveData().settings.volume;
        tempVolume = volume;
        ChangeVolume();
        volumeSlider.value = volume;
        resumeListener.nextEvent.AddListener(Close);
    }

    public void Open() {
        gameObject.SetActive(true);
    }

    public void Close() {
        gameObject.SetActive(false);
    }

    // This is used when the slider is changed, but the change has not been confirmed
    public void StoreVolume(float volume) {
        tempVolume = volume;
    }

    public void ChangeVolume() {
        AudioListener.volume = tempVolume;
        SaveDataManager.SaveVolume(tempVolume);
    }
}
