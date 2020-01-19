using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ironbound {
    public static class DebugManager {
        public static string LogPath => @"logs\";
        public static string currentLog = "log.txt";
        public static string CompleteLogPath => $"{LogPath}{currentLog}";
        static BlockingCollection<object> debugQueue = new BlockingCollection<object>();
        static StreamWriter logWriter;

        static DebugManager() {
            AppDomain.CurrentDomain.ProcessExit += Cleanup; // On exit, the logWriter stream must be disposed of

            if (!Directory.Exists(LogPath))
                Directory.CreateDirectory(LogPath);
            if (File.Exists(CompleteLogPath))
                File.Delete(CompleteLogPath);

            var debugBackgroundThread = new Thread(
                () => {
                    while (true) {
                        Console.WriteLine(debugQueue.Take());
                    }
                }
                ) {
                IsBackground = true
            };
            debugBackgroundThread.Start();
        }

        private static void Cleanup(object sender, EventArgs e) {
            logWriter?.Dispose();
        }

        static void AddMessage(object msg) {
            msg = $"{msg} at [{TimeManager.GlobalTime} time {TimeManager.GlobalDateTime} {(TimeManager.Paused ? "Paused" : "Not paused")}]";
            debugQueue.Add(msg); // Asynchronously record log

            // Write log to file    
            if (logWriter == null)
                logWriter = File.AppendText(CompleteLogPath);

            logWriter.WriteLine(msg);
            logWriter.Flush();
        }

        public static void LogInfo(object info) { AddMessage($" [INFO] {info}"); }
        public static void LogError(object error) { AddMessage($"[ERROR] {error}"); }
    }
}
