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
        public static SoundEffect laserShotRed;
        public static SoundEffect SoundPowerUpRed;
        public static SoundEffect SoundPowerUpGreen;
        public static SoundEffect SoundPowerUpBlue;
        public static SoundEffect SoundPowerUpOrange;

        // canciones:
        public static Song music01;
        public static Song music_menu;
        public static Song music_evolution;
        public static Song music_street;

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
                    music_menu = content.Load<Song>("Audio/Music/music_menu_28-Metempsychosis");
                    digitalAcent01 = content.Load<SoundEffect>("Audio/FXEffects/digital_acent_01");
                    break;

                case 1: // juego:
                    laserShot01 = content.Load<SoundEffect>("Audio/FXEffects/laser_shot_01");
                    brokenBone01 = content.Load<SoundEffect>("Audio/FXEffects/broken_bone_01");
                    brokenBone02 = content.Load<SoundEffect>("Audio/FXEffects/broken_bone_02");
                    tackled1 = content.Load<SoundEffect>("Audio/FXEffects/tackled_1");
                    LoadSoundPowerUps();
                    music_evolution = content.Load<Song>("Audio/Music/music_evolution_02-TakeOff");
                    music_street = content.Load<Song>("Audio/Music/music_04-Street");
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
                    music_menu = null;
                    digitalAcent01 = null;
                    break;

                case 1: // juego:
                    laserShot01 = null;
                    brokenBone01 = null;
                    brokenBone02 = null;
                    tackled1 = null;
                    laserShotRed = null;
                    SoundPowerUpRed = null;
                    SoundPowerUpGreen = null;
                    SoundPowerUpBlue = null;
                    SoundPowerUpOrange = null;
                    music_evolution = null;
                    music_street = null;
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
                case "laser_shot_red":  laserShotRed.Play(effectsVolume, pitch, pan); break;
                case "PowerUpRed":      SoundPowerUpRed.Play(effectsVolume, pitch, pan); break;
                case "PowerUpBlue":     SoundPowerUpBlue.Play(effectsVolume, pitch, pan); break;
                case "PowerUpGreen":    SoundPowerUpGreen.Play(effectsVolume, pitch, pan); break;
                case "PowerUpOrange":   SoundPowerUpOrange.Play(effectsVolume, pitch, pan); break;
            }
        } // PlayEffect

        public static void PlayMusic(int i)
        {
            switch (i)
            {
                case 1: // music01
                    MediaPlayer.Stop();
                    MediaPlayer.Play(music01);
                    MediaPlayer.IsRepeating = true;
                    break;
                case 2: // menu song
                    MediaPlayer.Stop();
                    MediaPlayer.Play(music_menu);
                    MediaPlayer.IsRepeating = true;
                    break;
                case 3: // evolution screen song
                    MediaPlayer.Stop();
                    MediaPlayer.Play(music_evolution);
                    MediaPlayer.IsRepeating = true;
                    break;
                case 4: // street music
                    MediaPlayer.Stop();
                    MediaPlayer.Play(music_street);
                    MediaPlayer.IsRepeating = true;
                    break;
            }
        } // PlayMusic

        public static void SetMusicVolumen(float volume)
        {
            musicVolume = volume;
            MediaPlayer.Volume = musicVolume;
        }

        public static void StopMusic()
        {
            MediaPlayer.Stop();
        }

        public static void PauseMusic()
        {
            MediaPlayer.Pause();
        }

        public static void ResumeMusic()
        {
            MediaPlayer.Resume();
        }

        private void LoadSoundPowerUps()
        {
            laserShotRed = content.Load<SoundEffect>("Audio/FXEffects/laser_shot_red");
            SoundPowerUpRed = content.Load<SoundEffect>("Audio/FXEffects/PowerUpRed");
            SoundPowerUpGreen = content.Load<SoundEffect>("Audio/FXEffects/PowerUpGreen");
            SoundPowerUpBlue = content.Load<SoundEffect>("Audio/FXEffects/PowerUpBlue");
            SoundPowerUpOrange = content.Load<SoundEffect>("Audio/FXEffects/PowerUpOrange");
        }


    } // class Audio
}