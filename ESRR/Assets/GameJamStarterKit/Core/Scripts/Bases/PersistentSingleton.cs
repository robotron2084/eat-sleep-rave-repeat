namespace GameJamStarterKit
{
    public class PersistentSingleton<T> : Singleton<T> where T : PersistentSingleton<T>, new()
    {
        protected PersistentSingleton() { }
    }
}