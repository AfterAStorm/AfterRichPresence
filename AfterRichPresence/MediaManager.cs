using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Control;
using System.Windows.Media.Imaging;
using Windows.Storage.Streams;
using System.IO;

namespace AfterRichPresence
{
    internal sealed class MediaManager
    {

        #region # - Properties

        private static GlobalSystemMediaTransportControlsSessionManager sessionManager;
        private static MediaInfo mediaInfo;
        private static MediaState mediaState = MediaState.Stopped;
        private static MediaInfo proposedMediaInfo;
        private static MediaState proposedMediaState = MediaState.Stopped;
        private static System.Windows.Forms.Timer waitTimer = new();
        private static long updateTime = 0;

        #endregion

        #region # - Constructor

        static MediaManager()
        {
            sessionManager = GetSystemMediaTransportControlsSessionManager().Result;
            sessionManager.CurrentSessionChanged += SessionManager_CurrentSessionChanged;
            var session = sessionManager.GetCurrentSession();
            if (session != null)
                session.PlaybackInfoChanged += MediaManager_PlaybackInfoChanged;
            waitTimer.Start();
            waitTimer.Interval = 1;
            waitTimer.Tick += (_, _) =>
            {
                if (updateTime == 75)
                    PublishChange();
                if (updateTime <= 75)
                    updateTime += 1;
            };
            HandleChange();
            updateTime = 751;
            PublishChange();
        }

        #endregion

        #region # - Handling

        private static GlobalSystemMediaTransportControlsSession GetCurrentSession()
        {
            foreach (var session in sessionManager.GetSessions())
            {
                var info = session.GetPlaybackInfo();
                if (info != null && info.PlaybackStatus == GlobalSystemMediaTransportControlsSessionPlaybackStatus.Playing)
                    return session;
            }
            return sessionManager.GetCurrentSession();
        }

        public static async Task<MediaInfo> GetLiveMediaInfo()
        {
            var session = GetCurrentSession();
            if (session == null)
                return new();
            var properties = await GetMediaProperties(session);
            if (properties == null)
                return new();
            var timeline = session.GetTimelineProperties();
            if (timeline == null)
                return new();
            /*IRandomAccessStreamWithContentType thumbStrean = properties.Thumbnail.OpenReadAsync().GetAwaiter().GetResult();
            byte[] fileBytes = new byte[thumbStrean.Size];
            using (DataReader reader = new DataReader(thumbStrean))
            {
                reader.LoadAsync((uint)thumbStrean.Size).GetAwaiter().GetResult();
                reader.ReadBytes(fileBytes);
            }
            BitmapImage image = new BitmapImage();
            using (var ms = new MemoryStream(fileBytes))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
            }*/

            return new()
            {
                Title       = properties.Title,
                Artist      = properties.Artist,
                Position    = timeline.Position,
                Length      = timeline.EndTime,
                //Icon        = properties.Thumbnail
            };
        }

        public static MediaState GetLiveMediaState()
        {
            var session = GetCurrentSession();
            if (session == null)
                return MediaState.Stopped;
            var playbackInfo = session.GetPlaybackInfo();
            if (playbackInfo == null)
                return MediaState.Stopped;
            return playbackInfo.PlaybackStatus switch // intelli sense told me to turn it into an expression, i thought i'd try it out
            {
                GlobalSystemMediaTransportControlsSessionPlaybackStatus.Changing
                or GlobalSystemMediaTransportControlsSessionPlaybackStatus.Paused => MediaState.Paused,

                GlobalSystemMediaTransportControlsSessionPlaybackStatus.Playing => MediaState.Playing,

                _ => MediaState.Stopped,
            };
        }

        private static void MediaManager_PlaybackInfoChanged(GlobalSystemMediaTransportControlsSession sender, PlaybackInfoChangedEventArgs args)
        {
            HandleChange();
        }

        private static void SessionManager_CurrentSessionChanged(GlobalSystemMediaTransportControlsSessionManager sender, CurrentSessionChangedEventArgs args)
        {
            var session = GetCurrentSession();
            if (session != null)
            {
                session.PlaybackInfoChanged -= MediaManager_PlaybackInfoChanged;
                session.PlaybackInfoChanged += MediaManager_PlaybackInfoChanged;
            }
        }

        /*private static void Session_MediaPropertiesChanged(GlobalSystemMediaTransportControlsSession sender, MediaPropertiesChangedEventArgs args)
        {
            HandleChange();
        }*/

        private static async void HandleChange()
        {
            var lastProposedMediaInfo = proposedMediaInfo;
            var lastProposedMediaState = proposedMediaState;
            proposedMediaInfo = await GetLiveMediaInfo();
            proposedMediaState = GetLiveMediaState();
            Debug.WriteLine($"Handling changes! {proposedMediaState} {proposedMediaInfo.Title} --> {proposedMediaInfo.Artist} ({proposedMediaInfo.Position} - {proposedMediaInfo.Length})");
            if (lastProposedMediaInfo == proposedMediaInfo && lastProposedMediaState == proposedMediaState)
                return; // nothings changed, no point to restart the timer!
            Debug.WriteLine($"... {proposedMediaState} {proposedMediaInfo.Title} --> {proposedMediaInfo.Artist} ({proposedMediaInfo.Position} - {proposedMediaInfo.Length})");
            updateTime = 0;
        }

        private static void PublishChange()
        {
            Debug.WriteLine($"Publishing media changes... {proposedMediaState} {proposedMediaInfo.Title} --> {proposedMediaInfo.Artist} ({proposedMediaInfo.Position} - {proposedMediaInfo.Length})");
            if (proposedMediaInfo != mediaInfo)
            {
                mediaInfo = proposedMediaInfo;
                MediaInfoChanged?.Invoke(mediaInfo);
            }
            if (proposedMediaState != mediaState)
            {
                mediaState = proposedMediaState;
                MediaStateChanged?.Invoke(mediaState);
            }
        }

        static async Task<GlobalSystemMediaTransportControlsSessionManager> GetSystemMediaTransportControlsSessionManager() =>
            await GlobalSystemMediaTransportControlsSessionManager.RequestAsync();

        static async Task<GlobalSystemMediaTransportControlsSessionMediaProperties?> GetMediaProperties(GlobalSystemMediaTransportControlsSession session)
        {
            try
            {
                return await session.TryGetMediaPropertiesAsync();
            }
            catch {}
            return null;
        }

        #endregion

        #region # - Events

        internal delegate void MediaInfoChangedHandler(MediaInfo info);
        internal static event MediaInfoChangedHandler? MediaInfoChanged;

        internal delegate void MediaStateChangedHandler(MediaState state);
        internal static event MediaStateChangedHandler? MediaStateChanged;

        #endregion

        #region # - Methods

        public static MediaInfo GetInfo()
        {
            return mediaInfo; // latest media info
        }

        public static MediaState GetState()
        {
            return mediaState;
        }

        #endregion

    }

    internal struct MediaInfo
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Icon { get; set; }

        public TimeSpan Position { get; set; }
        public TimeSpan Length { get; set; }

        public static bool operator ==(MediaInfo l, MediaInfo r) => l.Title == r.Title && l.Artist == r.Artist && l.Length == r.Length && l.Position == r.Position;
        public static bool operator !=(MediaInfo l, MediaInfo r) => l.Title != r.Title || l.Artist != r.Artist || l.Length != r.Length || l.Position != r.Position;

        public override readonly bool Equals(object? obj)
        {
            return obj != null && obj is MediaInfo && this == (MediaInfo)obj;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }

    internal enum MediaState
    {
        Playing,
        Paused,
        Stopped
    }
}
