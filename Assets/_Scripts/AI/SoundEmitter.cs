using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    float _soundIntensity = 0f;
    public float soundIntensity
    {
        get { return _soundIntensity; }
        set { _soundIntensity = Mathf.Clamp(value, 0, 100); }
    }

    float _soundFrecuency = 0;
    public float soundFrecuency
    {
        get { return _soundFrecuency; }
        set { _soundFrecuency = Mathf.Clamp(value, 0, 20000); }
    }

    public void MakeSound(float intensity, float frecuency)
    {
        soundIntensity = intensity;
        soundFrecuency = frecuency;
    }

    void MakeActionSound(int actionIndex)
    {
        soundIntensity = actionsSound[actionIndex].soundIntensity;
        soundFrecuency = actionsSound[actionIndex].soundFrecuency;
    }

    public List<SoundAction> actionsSound;

    [System.Serializable]
    public class SoundAction
    {
        public string soundName = "Default";
        public float soundIntensity;
        public float soundFrecuency;
    }

    public bool testing;
    private void Start()
    {
        if (testing)
        {
            MakeActionSound(0);
        }
    }
}
