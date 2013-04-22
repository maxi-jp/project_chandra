using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace IS_XNA_Shooter
{
    public class Audio
    {
        private ContentManager content;

        private static float effectsVolume;
        private static float musicVolume;

        // efectos del menu:
        public static SoundEffect digitalAcent01;

        // efectos del juego:
        public static SoundEffect laserShot01;
        public static SoundEffect brokenBone01, brokenBone02;
        public static SoundEffect tackled1;

        // canciones:
        public static Song music01;

        public Audio(ContentManager content)
        {
            this.content = content;
            effectsVolume = musicVolume = 1;
            MediaPlayer.Volume = musicVolume;
        }

        public void LoadContent(int i)
        {
            // i:
            // 0=Menú principal
            // 1=Juego

            switch (i)
            {
                case 0: // menu:
                    digitalAcent01 = content.Load<SoundEffect>("Audio/FXEffects/digital_acent_01");
                    break;

                case 1: // juego:
                    laserShot01 = content.Load<SoundEffect>("Audio/FXEffects/laser_shot_01");
                    brokenBone01 = content.Load<SoundEffect>("Audio/FXEffects/broken_bone_01");
                    brokenBone02 = content.Load<SoundEffect>("Audio/FXEffects/broken_bone_02");
                    tackled1 = content.Load<SoundEffect>("Audio/FXEffects/tackled_1");

                    music01 = content.Load<Song>("Audio/Music/music_01");
                    break;
            }
        } // LoadContent

        public void UnloadContent(int i)
        {
            // i:
            // 0=Menú principal
            // 1=Juego

            switch (i)
            {
                case 0: // menu:
                    digitalAcent01 = null;
                    break;

                case 1: // juego:
                    laserShot01 = null;
                    brokenBone01 = null;
                    brokenBone02 = null;
                    tackled1 = null;

                    break;
            }
        } // UnloadContent

        public static void PlayEffect(String cad)
        {
            PlayEffect(cad, 1, 0);
        }

        public static void PlayEffect(String cad, float pitch, float pan)
        {
            switch (cad)
            {
                case "digitalAcent01":  digitalAcent01.Play(effectsVolume,pitch,pan);   break;
                case "laserShot01":     laserShot01.Play(effectsVolume, pitch, pan);    break;
                case "brokenBone01":    brokenBone01.Play(effectsVolume, pitch, pan);   break;
                case "brokenBone02":    brokenBone02.Play(effectsVolume, pitch, pan);   break;
                case "tackled1":        tackled1.Play(effectsVolume, pitch, pan);       break;
            }
        } // PlayEffect

        public static void PlayMusic(int i)
        {
            switch (i)
            {
                case 1:
                    MediaPlayer.Play(music01);
                    MediaPlayer.IsRepeating = true;
                    break;
            }
        } // PlayMusic

        public static void SetMusicVolumen(float volume)
        {
            musicVolume = volume;
            MediaPlayer.Volume = musicVolume;
        }

    } // class Audio
}