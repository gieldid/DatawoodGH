using DatawoodGH.Utility;
using System.Collections.Generic;
using System.Linq;

namespace DatawoodGH.Network.SocketConnection
{
    public class ModFileObject
    {
        private const string MoveL = "MoveL";
        private const string MoveAbsJ = "MoveAbsJ";
        private const string SetDo = "SetDO";
        private const string WaitTime = "WaitTime";

        public Dictionary<string, string> Speeds { get; private set; }
        public Dictionary<string, string> Zones { get; private set; }
        public Dictionary<string, string> WaitTimes { get; private set; }
        public List<CommandObject> Commands { get; set; } 

        /// <summary>
        /// Reads the modfile and puts the Moves, speeds and zones in their respective variables.
        /// </summary>
        /// <param name="path">path of the modfile</param>
        public ModFileObject(string path) {
            Speeds = new Dictionary<string, string>();
            Zones = new Dictionary<string, string>();
            WaitTimes = new Dictionary<string, string>();
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
                ReadWaitTimes(line);
            }
        }

        private void ReadCommands(string line) {
            if (line.Contains(MoveL))
            {
                MoveLObject moveL = new MoveLObject(line, Speeds, Zones);
                Commands.Add(moveL);
            }
            else if (line.Contains(MoveAbsJ))
            {
                MoveAbsJObject moveAbjs = new MoveAbsJObject(line, Speeds, Zones);
                Commands.Add(moveAbjs);
            }
            else if (line.Contains(SetDo))
            {
                DigitalOutpotCommand digitalOutpotCommand = new DigitalOutpotCommand(line);
                Commands.Add(digitalOutpotCommand);
            }
            else if (line.Contains(WaitTime)) { 
                WaitCommand waitCommand = new WaitCommand(line, WaitTimes);
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

        private void ReadWaitTimes(string line) {
            if (line.Contains("PERS num Wait")) {
                string waitValue = new string(line.SkipWhile(c => c != '=')
                    .Skip(1)
                    .TakeWhile(c => c != ';')
                    .ToArray()).Trim();

                var startName = line.IndexOf('W');
                var endName = line.IndexOf(':');

                string waitName = line.Substring(startName, endName - startName);

                WaitTimes.Add(waitName, waitValue);
            }

        }
    }
}
