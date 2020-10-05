namespace GameJamStarterKit.Audio
{
    /// <summary>
    /// Simple class to play single background songs.
    /// </summary>
    public class PersistentBackgroundMusic : BackgroundMusic
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }
    }
}