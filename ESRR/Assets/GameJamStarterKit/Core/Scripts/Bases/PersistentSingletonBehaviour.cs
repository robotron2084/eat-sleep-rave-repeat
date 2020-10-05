namespace GameJamStarterKit
{
    public abstract class PersistentSingletonBehaviour<T> : SingletonBehaviour<T>
        where T : PersistentSingletonBehaviour<T>
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }
    }
}