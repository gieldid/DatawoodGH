using DatawoodGH.Utility;
using System.Collections.Generic;

namespace DatawoodGH.Network.SocketConnection
{
    public class ModFileObject
    {
        private const string MOVE_L = "MoveL";
        private const string MOVE_ABSJ = "MoveAbsJ";
        private const string SET_DO = "SetDO";
        private const string WAIT_TIME = "WaitTime";

        public Dictionary<string, string> Speeds { get; private set; }
        public Dictionary<string, string> Zones { get; private set; }
        public List<CommandObject> Commands { get; set; } 

        /// <summary>
        /// Reads the modfile and puts the Moves, speeds and zones in their respective variables.
        /// </summary>
        /// <param name="path">path of the modfile</param>
        public ModFileObject(string path) {
            Speeds = new Dictionary<string, string>();
            Zones = new Dictionary<string, string>();
            Commands = new List<CommandObject>();
            ReadModFile(path);
        }

        private void ReadModFile(string path) {
            string[] lines = System.IO.File.ReadAllLines(path);

            foreach (var line in lines)
            {
                ReadCommands(line);
                ReadSpeeds(line);
                ReadZones(line);
            }
        }

        private void ReadCommands(string line) {
            if (line.Contains(MOVE_L))
            {
                MoveLObject moveL = new MoveLObject(line, Speeds, Zones);
                Commands.Add(moveL);
            }
            else if (line.Contains(MOVE_ABSJ))
            {
                MoveAbsJObject moveAbjs = new MoveAbsJObject(line, Speeds, Zones);
                Commands.Add(moveAbjs);
            }
            else if (line.Contains(SET_DO))
            {
                DigitalOutpotCommand digitalOutpotCommand = new DigitalOutpotCommand(line);
                Commands.Add(digitalOutpotCommand);
            }
            else if (line.Contains(WAIT_TIME)) { 
                WaitCommand waitCommand = new WaitCommand();
                Commands.Add(waitCommand);
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

                Zones.Add(zoneName, zoneData);
            }
        }
    }
}
