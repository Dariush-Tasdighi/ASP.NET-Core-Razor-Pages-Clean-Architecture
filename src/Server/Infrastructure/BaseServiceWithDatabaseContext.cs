namespace Infrastructure;

public abstract class BaseServiceWithDatabaseContext : BasePageModel
{
	public BaseServiceWithDatabaseContext
		(Persistence.DatabaseContext databaseContext) : base()
	{
		DatabaseContext = databaseContext;
	}

	protected Persistence.DatabaseContext DatabaseContext { get; }

	protected void DisposeDatabaseContext()
	{
		DatabaseContext?.Dispose();
	}

	protected async
		System.Threading.Tasks.Task DisposeDatabaseContextAsync()
	{
		if (DatabaseContext is not null)
		{
			await DatabaseContext.DisposeAsync();
		}
	}
}
