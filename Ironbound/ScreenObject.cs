using System;
using System.Collections.Generic;
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

        public static void Save(ScreenObject screenObject) {

        }
    }

    public class ScreenObject {
        public string name;
        public int x, y, width, height;
        public double lastViewedTime;

        public virtual string Save() {
            return $"{name}|{x},{y}|{width}|{height}|{lastViewedTime}";
        }

        public virtual void LoadFromString(string save) {
            string[] data = save.Split("so(|)".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            // name
            name = data[0];

            // x, y
            string[] pos = data[1].Split(',');
            //if (int.TryParse(pos[0], out int nx))

        }
    }
}
