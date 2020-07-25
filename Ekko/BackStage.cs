using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DirectShowLib.DES;
using ScreenRecorderLib;
//using Splicer.Renderer;
//using Splicer.Timeline;

namespace Ekko
{
    public class BackStage
    {
        public static Recorder recorder;
        public static Stream OutStream;
        public static string LastPath;
        public static string CurrentPath;


        static TimeSpan startTimeSpan = TimeSpan.FromMinutes(0);
        static TimeSpan periodTimeSpan = TimeSpan.FromMinutes(1);
        static string folderPath = @"C:\EKKO_TESTS";

        public static void StartTimer()
        {
            Timer timer = new System.Threading.Timer((e) =>
            {
                StopRecord();
                StartRecord();
            }, null, startTimeSpan, periodTimeSpan);
        }


        public static void StartRecord()
        {
            string videoName = DateTime.Now.ToFileTime() + ".mp4";
            string videoPath = Path.Combine(folderPath, videoName);
            CurrentPath = videoPath;
            recorder = Recorder.CreateRecorder();
            recorder.OnRecordingComplete += OnRecordingComplete;
            recorder.OnRecordingFailed += OnRecordingFailed;
            recorder.OnStatusChanged += OnStatusChanged;
            recorder.Record(videoPath);
        }

        public static void StopRecord()
        {
            if(recorder != null) { 
            recorder.Stop();
            LastPath = CurrentPath;
            CurrentPath = null;
                }
        }

        public static void SaveMoment()
        {
            recorder.Stop();
            MergeCurrentAndLast();
            StartRecord();
        }

        private static void MergeCurrentAndLast()
        {
            //using (ITimeline timeline = new DefaultTimeline())
            //{
            //    IGroup group = timeline.AddVideoGroup(32, 720, 576);

            //    var firstVideoClip = group.AddTrack().AddClip(LastPath);
            //    var secondVideoClip = group.AddTrack().AddVideo(CurrentPath, firstVideoClip.Duration);

            //    using (AviFileRenderer renderer = new AviFileRenderer(timeline, "output.avi"))
            //    {
            //        renderer.Render();
            //    }
            //}
        }

        public static void DeleteLastRecord()
        {
            if (LastPath != null)
            {
                File.Delete(LastPath);
            }
        }

        private static void OnRecordingComplete(object sender, RecordingCompleteEventArgs e)
        {
            string path = e.FilePath;
            OutStream?.Dispose();
        }
        private static void OnRecordingFailed(object sender, RecordingFailedEventArgs e)
        {
            string error = e.Error;
            OutStream?.Dispose();
        }
        private static void OnStatusChanged(object sender, RecordingStatusEventArgs e)
        {
            RecorderStatus status = e.Status;
        }
    }
}
