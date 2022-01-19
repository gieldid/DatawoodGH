using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatawoodGH.Utility;

namespace DatawoodGH.Network.SocketConnection
{
    public class ModFileObject
    {
        public List<string> MovesFull {  get; private set; }
        public Dictionary<string, string> Speeds { get; private set; }
        public Dictionary<string, string> Zones { get; private set; }
        public List<MoveObject> Voves { get; set; } 

        /// <summary>
        /// Reads the modfile and puts the Moves, speeds and zones in their respective variables.
        /// </summary>
        /// <param name="path">path of the modfile</param>
        public ModFileObject(string path) {
            MovesFull = new List<string>();
            Speeds = new Dictionary<string, string>();
            Zones = new Dictionary<string, string>();
            ReadModFile(path);
        }

        private void ReadModFile(string path) {
            string[] lines = System.IO.File.ReadAllLines(path);
            foreach (var line in lines)
            {
                ReadMoves(line);
                ReadSpeeds(line);
                ReadZones(line);
            }
        }

        private void ReadMoves(string line) {
            if (line.Contains("MoveL") || line.Contains("MoveAbsJ"))
            {
                MovesFull.Add(line);
            }
        }

        private void ReadSpeeds(string line) {
            if (line.Contains("speeddata")) {
                var startName = Utils.GetNthIndex(line, 'S', 3);
                var endName = line.IndexOf(':');
                var speedName = line.Substring(startName, endName - startName);

                var startData = line.IndexOf('[');
                var endData = line.IndexOf(']') + 1;

                var speedData = line.Substring(startData, endData - startData);
                
                Speeds.Add(speedName, speedData);
            }
        }

        private void ReadZones(string line) {
            if (line.Contains("zonedata"))
            {
                var startName = line.IndexOf('Z');
                var endName = line.IndexOf(':');
                var zoneName = line.Substring(startName, endName - startName);

                var startData = line.IndexOf('[');
                var endData = line.IndexOf(']') + 1;

                var zoneData = line.Substring(startData, endData - startData);

                Speeds.Add(zoneName, zoneData);
            }
        }
    }
}
