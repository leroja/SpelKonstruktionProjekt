using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Source.Managers
{
    class AudioManager
    {
        private static AudioManager instance;
        private float prevVol = 1.0f;
        //private float MasterVolume = 1.0f;
        Dictionary<string, Song> songDic = new Dictionary<string, Song>();
        Dictionary<string, SoundEffect> soundEffectDic = new Dictionary<string, SoundEffect>();


        private AudioManager()
        {

        }
        
        public static AudioManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AudioManager();
                }
                return instance;
            }
        }


        public void GlobalMute()
        {
            MediaPlayer.IsMuted = true;
            prevVol = SoundEffect.MasterVolume;
            SoundEffect.MasterVolume = 0;
        }

        public void GlobalUnMute()
        {
            MediaPlayer.IsMuted = false;
            SoundEffect.MasterVolume = prevVol;
        }

        /// <summary>
        /// Changes the volume of the mediaplayer
        /// </summary>
        /// <param name="Volume">
        /// MediaPlayer voume, has to be between 0.0 and 1.0
        /// </param>
        public void ChangeSongVolume(float Volume)
        {
            if(Volume <= 1.0 && Volume >= 0.0)
            {
                MediaPlayer.Volume = Volume;
            }
        }

        /// <summary>
        /// adds the soundEffect to the soundeffect "pool"
        /// </summary>
        /// <param name="effectName">
        /// name of the soundEffect 
        /// </param>
        /// <param name="effect">
        /// the soundEffect
        /// </param>
        public void AddSoundEffect(string effectName, SoundEffect effect)
        {
            if (effect != null)
            {
                if (!soundEffectDic.ContainsKey(effectName))
                {
                    soundEffectDic.Add(effectName, effect);
                }
            }
        }

        /// <summary>
        /// removes the soundeffect from the dictionary
        /// if there is is a soundeffect with that name
        /// </summary>
        /// <param name="effectName">
        /// name of the soundEffect
        /// </param>
        public void RemoveSoundEffect(string effectName)
        {
            if (soundEffectDic.ContainsKey(effectName))
            {
                soundEffectDic.Remove(effectName);
            }
        }

        /// <summary>
        /// Plays a specific soundeffect
        /// </summary>
        /// <param name="SoundEffect">
        /// the name of the soundEffect
        /// </param>
        public void PlaySoundEffect(string SoundEffect)
        {
            if (soundEffectDic.ContainsKey(SoundEffect))
            {
                soundEffectDic[SoundEffect].Play();
            }
        }

        /// <summary>
        /// Changes the Volume of the soundEffects
        /// </summary>
        /// <param name="volume">
        /// soundEffect Volume has to be between 0.0 and 1.0
        /// </param>
        public void ChangeGlobalSoundEffectVolume(float volume)
        {
            if (volume <= 1.0 && volume >= 0.0)
            {
                prevVol = volume;
                SoundEffect.MasterVolume = volume;
            }
        }


        public void StopRepeat()
        {
            MediaPlayer.IsRepeating = false;
        }


        public void StartRepeat()
        {
            MediaPlayer.IsRepeating = true;
        }

        /// <summary>
        /// adds the song to the song "pool"
        /// </summary>
        /// <param name="songName">
        /// name of the song
        /// </param>
        /// <param name="song">
        /// the song
        /// </param>
        public void AddSong(string songName, Song song)
        {
            if(song != null)
            {
                if (!songDic.ContainsKey(songName))
                {
                    songDic.Add(songName, song);
                }
            }
        }

        /// <summary>
        /// remove a specific song
        /// if there is song with that name
        /// </summary>
        /// <param name="songName">
        /// name of the song
        /// </param>
        public void RemoveSong(string songName)
        {
            if (songDic.ContainsKey(songName))
            {
                songDic.Remove(songName);
            }
        }

        /// <summary>
        /// Plays the song if there is one with that name
        /// </summary>
        /// <param name="name">
        /// name of the song
        /// </param>
        public void PlaySong(string name)
        {
            if (songDic.ContainsKey(name))
            {
                MediaPlayer.Play(songDic[name]);
                
            }
        }


        public void StopSong()
        {
            MediaPlayer.Stop();
        }

    }
}
