using UnityEngine;
namespace SAEP {
public static class AudioOneShot {
 public static void Play(AudioClip clip,Vector3 position,float volume=1f){if(clip)AudioSource.PlayClipAtPoint(clip,position,volume);}
}
}
