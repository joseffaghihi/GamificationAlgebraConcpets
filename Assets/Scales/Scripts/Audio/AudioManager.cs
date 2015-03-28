using UnityEngine;
using System.Collections;

public class AudioManager
{
    private AudioClip[] clip; //Audio clip array to hold each clip loaded in

    /** @brief Default Constructor
     */
    public AudioManager()
    {}

    /** @brief Overload Contructor - Loads Audio Clip
     * @param path - specifies the path to load the
     *               Audio clips from
     */
    public AudioManager(string path)
    {
        clip = Resources.LoadAll<AudioClip>(path);
    }

    /** @brief Loads Audio Clip
     * @param path - specifies the path to load the
     *               Audio clips from
     * @return Void
     */
    public void LoadAudio(string path)
    {
        clip = Resources.LoadAll<AudioClip>(path);
    }

    /** @brief Returns Audio Clip
     * @param name - specifies the name of the clip
     * @return AudioClip
     */
    public AudioClip GetClip(string name)
    {
        foreach (AudioClip newClip in clip)
        {
            if (newClip.name == name)
                return newClip;
        }
        return null;
    }

    public AudioClip GetClip(int index)
    {
        if (clip.Length > index)
            return clip[index];
        else
            return null;
    }
}
