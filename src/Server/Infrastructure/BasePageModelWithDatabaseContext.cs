namespace Infrastructure;

public abstract class BasePageModelWithDatabaseContext : BasePageModel
{
	public BasePageModelWithDatabaseContext
		(Persistence.DatabaseContext databaseContext) : base()
	{
		DatabaseContext = databaseContext;
	}

	protected Persistence.DatabaseContext DatabaseContext { get; }

	//protected readonly Persistence.DatabaseContext DatabaseContext;

	protected void DisposeDatabaseContext()
	{
		if (DatabaseContext is not null)
		{
			DatabaseContext.Dispose();

			//DatabaseContext = null;
		}
	}

	protected async
		System.Threading.Tasks.Task DisposeDatabaseContextAsync()
	{
		if (DatabaseContext is not null)
		{
			await DatabaseContext.DisposeAsync();

			//DatabaseContext = null;
		}
	}
}
