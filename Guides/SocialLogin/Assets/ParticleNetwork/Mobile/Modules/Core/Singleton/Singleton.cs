
namespace Network.Particle.Scripts.Singleton
{

	public abstract class Singleton<T> where T : class, new()
	{
		protected static T _Instance;

		public static T Instance
		{
			get
			{
				if (_Instance == null)
				{
					_Instance = new T();
				}
				return _Instance;
			}
		}

		protected Singleton()
		{
			if (_Instance != null)
			{
				throw new SingletonException("This " + typeof(T).ToString() + " Singleton Instance is not null !!!");
			}
			Init();
		}

		public virtual void Init()
		{
		}
	}

}

