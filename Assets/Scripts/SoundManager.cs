using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundToPlay
{
    arrowTrap,
    bearTrap,
    crossbowFire,
    deathSplat,
    doorUp,
    doorDown,
    invisPotion,
    pressurePLateOn,
    pressurePLateOff,
    menuSelect,
    menuSwitch,
    speedPotion,
    spikeUp,
    spikeDown,
    sword,
    treasure1,
    treasure2,
    treasure3,
    treasure4
}

public class SoundManager : MonoBehaviour
{
    private float minSoundDelay = 0.2f;
    public static SoundManager current;

    private void Awake()
    {
        current = this;
    }

    private Dictionary<SoundToPlay, bool> soundBools = new Dictionary<SoundToPlay, bool>()
    {
        {SoundToPlay.arrowTrap, true},
        {SoundToPlay.doorUp, true },
        {SoundToPlay.doorDown, true },
        {SoundToPlay.spikeUp, true },
        {SoundToPlay.spikeDown, true },
        {SoundToPlay.deathSplat, true },
        {SoundToPlay.crossbowFire, true },
        {SoundToPlay.sword, true }
    };


    public bool CanPlaySound(SoundToPlay soundToPlay)
    {
        bool canPlaySound = soundBools[soundToPlay];
        if (canPlaySound == true) { StartCoroutine(DelayUntilNextSound(soundToPlay)); }
        return canPlaySound;
    }

    private IEnumerator DelayUntilNextSound (SoundToPlay SoundToDelay)
    {
        soundBools[SoundToDelay] = false;
        yield return new WaitForSeconds(minSoundDelay);
        soundBools[SoundToDelay] = true;
    }
}
