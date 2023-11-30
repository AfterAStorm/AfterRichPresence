using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AfterRichPresence
{
    internal struct Profile
    {

        #region # - Properties

        public string Name { get; set; } = "Unnamed";
        public ProfileType Type { get; set; } = ProfileType.Static;
        public long ClientId { get; set; } = 0;

        public ActivityInfo StaticProperties { get; set; } = new();
        public ProfileTimestampType StaticTimestampType { get; set; } = ProfileTimestampType.None;

        public DynamicProperties DynamicProperties { get; set; } = new();

        #endregion

        #region # - Constructor

        public Profile()
        {
        }

        #endregion

    }

    internal struct DynamicProperties
    {
        public string Script { get; set; } = @"--[[ GLHF ]]--

print(""Hello World"")

-- presence table, what is displayed :)
presence.details = ""Details""
presence.state = ""State""
presence.largeImage = ""https://upload.wikimedia.org/wikipedia/commons/thumb/c/c0/Unicode_0x004C.svg/1200px-Unicode_0x004C.svg.png""
presence.largeText = ""Large Image Text""
presence.smallImage = ""https://upload.wikimedia.org/wikipedia/commons/thumb/0/08/Unicode_0x0053.svg/1200px-Unicode_0x0053.svg.png""
presence.smallText = ""Small Image Text""
presence.timestampStart = os.time() -- 30 second timer, aka it will display 00:30 left, comment/remove timestampEnd for a elapsed timer
presence.timestampEnd = os.time() + 30

-- media table, if MediaUpdate (or start) trigger is enabled
print(media.title) -- the title of the current playing media
print(media.artist) -- the artist
print(media.position) -- the position (in seconds)
print(media.state) -- the sttate (Playing, Paused, or Stopped)
print(media.length) -- the length (in seconds)";
        public DynamicTriggers Triggers { get; set; } = DynamicTriggers.None;
        public int TimerInterval = 0;
        public TimerUnit TimerUnit;

        public DynamicProperties()
        {
        }
    }

    [Flags]
    internal enum DynamicTriggers
    {
        None = 0,
        Start = 1,
        MediaUpdated = 2,
        Timer = 4,
    }

    internal enum TimerUnit
    {
        Milliseconds = 0,
        Seconds = 1,
        Minutes = 2,
        Hours = 3,
        Days = 4, 
    }

    internal enum ProfileType
    {
        Static = 0,
        Dynamic = 1
    }

    internal enum ProfileTimestampType
    {
        None = 0,
        Custom = 1,
        LocalTime = 2
    }
}
