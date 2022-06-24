namespace jtcWeather;

public partial class App : Application
{

	public static DataRepository DataRepo { get; private set; }

	public App(DataRepository repo)
	{
		InitializeComponent();

		DataRepo = repo;

		MainPage = new AppShell();
	}
}
