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

        public static ScreenObject[] ScreenObjects => screenObjects;
        static int _screenObjects_LastIndex = -1;
        static ScreenObject[] screenObjects = new ScreenObject[1024];
        static void TryAddScreenObject(ScreenObject screenObject) {
            int tryAddIndex = _screenObjects_LastIndex + 1;
            if (tryAddIndex >= screenObjects.Length) { // Is there room?
                DebugManager.LogError($"Attempt to add ScreenObject {screenObject} failed: No room.");
                return;
            }

            screenObjects[tryAddIndex] = screenObject;
            _screenObjects_LastIndex = tryAddIndex;
            DebugManager.LogInfo($"Successfully added ScreenObject {screenObject}.");
        }
        static void TryRemoveScreenObject(ScreenObject screenObject) {
            if (!screenObjects.Contains(screenObject)) { // Can the object be removed?
                DebugManager.LogError($"ScreenObject {screenObject} cannot be removed because it does not exist.");
                return;
            }

            // Find largest index
            int removalIndex = Array.IndexOf(screenObjects, screenObject);

            if (removalIndex != _screenObjects_LastIndex) { // Not the last item, needs more processing.
                for (int i = removalIndex; i < _screenObjects_LastIndex - 1; i++) { // Move all objects after it down an index
                    screenObjects[i] = screenObjects[i + 1];
                }
                _screenObjects_LastIndex--;
            }

            // It's the last item, very simple to remove.
            screenObjects[removalIndex] = null; // remove the object
            _screenObjects_LastIndex = removalIndex - 1; // set the last index to the previous object
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
