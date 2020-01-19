using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ironbound {
    // Objects which exist onscreen and are abstracted away offscreen.
    // Some maximum number of objects, past that any attempting to enter the screen remain abstract
    // Store last time onscreen for update purposes

    public static class ScreenObjectManager {
        public static string SavePath => @"save\entities\";
        public static int MaxOnscreen = 1024;
        public static List<ScreenObject> ScreenObjects => screenObjects;
        static List<ScreenObject> screenObjects = new List<ScreenObject>();

        static ScreenObjectManager() {
            if (!Directory.Exists(SavePath))
                Directory.CreateDirectory(SavePath);
        }

        static void TryAddScreenObject(ScreenObject screenObject) {
            if (screenObjects.Count >= MaxOnscreen) { // Is there room?
                DebugManager.LogError($"Attempt to add ScreenObject {screenObject} failed: No room.");
                return;
            }

            screenObjects.Add(screenObject);
            DebugManager.LogInfo($"Successfully added ScreenObject {screenObject}.");
        }
        static void TryRemoveScreenObject(ScreenObject screenObject) {
            if (!screenObjects.Contains(screenObject)) { // Can the object be removed?
                DebugManager.LogError($"ScreenObject {screenObject} cannot be removed because it does not exist.");
                return;
            }
            screenObjects.Remove(screenObject);
        }

        public static bool IsSaved(string name) {
            return File.Exists($"{SavePath}\\{name}.ieo");
        }

        public static string[] GetSavedObjects() {
            string[] paths = Directory.GetFiles(SavePath);
            string[] names = new string[paths.Length];

            for (int i = 0; i < paths.Length; i++)
                names[i] = paths[i].Trim(SavePath.ToCharArray());

            return names;
        }

        static string GetSavePathFromName(string name) => $"{SavePath}\\{name}.ieo";
        public static void Save(ScreenObject screenObject) {
            File.WriteAllText(GetSavePathFromName(screenObject.name), screenObject.Save());
        }
        public static ScreenObject Load(string name) {
            ScreenObject load = new ScreenObject();
            load.LoadFromString(File.ReadAllText(GetSavePathFromName(name)));

            return load;
        }
        public static void LoadAndAdd(string name) {
            TryAddScreenObject(Load(name));
        }
    }

    public class ScreenObject {
        public string name;
        public int x, y, width, height;
        public double lastViewedTime;
        protected int SaveLength => 4;

        public ScreenObject() {
            lastViewedTime = TimeManager.GlobalTime;
        }
        public ScreenObject(string name, int x, int y, int width, int height) : this() {
            this.name = name;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public virtual string Save() => $"{name}|{x},{y}|{width},{height}|{lastViewedTime}";

        public virtual void LoadFromString(string save) {
            try {
                string[] data = save.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (data.Length != SaveLength)
                    DebugManager.LogError("Failed to load entity: Actual length did not match expected length.");

                // name
                name = data[0];

                // x, y
                string[] pos = data[1].Split(',');
                if (int.TryParse(pos[0], out int nx) && int.TryParse(pos[1], out int ny)) {
                    x = nx;
                    y = ny;
                }
                else {
                    DebugManager.LogError($"Failed to load entity {name} at position ({pos[0]},{pos[1]}).");
                }

                // width, height
                string[] dims = data[2].Split(',');
                if (int.TryParse(dims[0], out int nw) && int.TryParse(dims[1], out int nh)) {
                    width = nw;
                    height = nh;
                }
                else {
                    DebugManager.LogError($"Failed to load entity {name} at dimensions ({dims[0]},{dims[1]}).");
                }

                // last viewed time
                if (double.TryParse(data[3], out double lvt)) {
                    lastViewedTime = lvt;
                }
                else {
                    DebugManager.LogError($"Failed to load entity {name} at lastViewedTime ({data[3]}).");
                }
            }
            catch (Exception e) {
                DebugManager.LogError($"Generic failiure loading entity: {e}");
            }
        }

        public override string ToString() => Save();
    }
}
