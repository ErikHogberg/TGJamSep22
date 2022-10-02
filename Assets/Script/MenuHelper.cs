using UnityEngine;
using UnityEngine.Events;

public class MenuHelper : MonoBehaviour
{

    public UnityEvent<bool> MenuToggle;

    public UnityEvent<bool> SoundToggleOn;
    public UnityEvent<bool> SoundToggleOff;

    public static bool paused = false;
    public static bool sound = true;

    private void Start() {
        MenuToggle.Invoke(paused);
         SoundToggleOn.Invoke(sound);
        SoundToggleOff.Invoke(!sound);
    }

    public void MenuClick(){
        paused = !paused;
        MenuToggle.Invoke(paused);
    }

    public void SoundToggle(){
        sound = !sound;
        SoundToggleOn.Invoke(sound);
        SoundToggleOff.Invoke(!sound);
    }

}
