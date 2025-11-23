using NAudio.Wave;
using System;
using System.Threading;

namespace NeoCapitalRPG
{
    public class AudioService
    {
        private IWavePlayer waveOut;
        private WaveStream audioStream;
        private AudioFileReader fileReader;
        private Thread musicaThread;
        private bool pararRequisitado = false;

        public void TocarMusica(string caminho, bool loop = true, float volume = 0.8f)
        {
            Parar();
            pararRequisitado = false;

            musicaThread = new Thread(() =>
            {
                waveOut = new WaveOutEvent();
                fileReader = new AudioFileReader(caminho);

                audioStream = loop ? new LoopStream(fileReader) : fileReader;

                waveOut.Init(audioStream);
                waveOut.Volume = volume;
                waveOut.Play();

                
                while (!pararRequisitado && waveOut.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(100);
                }

               
                waveOut?.Stop();
            });

            musicaThread.IsBackground = true;
            musicaThread.Start();
        }

        public void Parar()
        {
            pararRequisitado = true;

            try
            {
                waveOut?.Stop();
                waveOut?.Dispose();
                audioStream?.Dispose();
                fileReader?.Dispose();
            }
            catch { }

            waveOut = null;
            audioStream = null;
            fileReader = null;

            
            musicaThread = null;
        }
    }

    
    public class LoopStream : WaveStream
    {
        private readonly WaveStream source;

        public LoopStream(WaveStream source)
        {
            this.source = source;
        }

        public override WaveFormat WaveFormat => source.WaveFormat;
        public override long Length => long.MaxValue;

        public override long Position
        {
            get => source.Position;
            set => source.Position = value;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int read = source.Read(buffer, offset, count);

            if (read == 0)
            {
                source.Position = 0;
                read = source.Read(buffer, offset, count);
            }

            return read;
        }
    }
}
