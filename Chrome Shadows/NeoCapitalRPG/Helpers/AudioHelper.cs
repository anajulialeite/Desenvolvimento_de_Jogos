using NAudio.Wave;
using System;
using System.IO;
using System.Threading;

namespace NeoCapitalRPG.Helpers
{
    internal class AudioHelper
    {
        private static WaveOutEvent outputDevice;
        private static AudioFileReader audioFile;
        private static Thread audioThread;
        private static bool pararMusica = false;

        public static void TocarMusicaEmLoop(string caminhoCompleto, float volume = 1.0f)
        {
            if (!File.Exists(caminhoCompleto))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Arquivo não encontrado: {caminhoCompleto}");
                Console.ResetColor();
                return;
            }

            pararMusica = false;

            audioThread = new Thread(() =>
            {
                try
                {
                    using (audioFile = new AudioFileReader(caminhoCompleto))
                    using (outputDevice = new WaveOutEvent())
                    {
                        outputDevice.Init(audioFile);
                        outputDevice.Volume = volume;

                        while (!pararMusica)
                        {
                            audioFile.Position = 0;
                            outputDevice.Play();
                            while (outputDevice.PlaybackState == PlaybackState.Playing && !pararMusica)
                            {
                                Thread.Sleep(100);
                            }
                        }

                        outputDevice.Stop();
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Erro ao tocar música: {ex.Message}");
                    Console.ResetColor();
                }
            });

            audioThread.IsBackground = true; // garante que feche ao sair do programa
            audioThread.Start();
        }

        public static void PararMusica()
        {
            pararMusica = true;
            if (audioThread != null && audioThread.IsAlive)
            {
                audioThread.Join();
            }
        }
    }
}
