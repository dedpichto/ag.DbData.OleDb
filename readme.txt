
***************************************************************************************************

// add section to settings file (optional)
{
  "OleDbDbDataSettings": {
    "AllowExceptionLogging": false, // optional, default is "true"
    "ConnectionString": "YOUR_CONNECTION_STRING" // optional
  }
}

***************************************************************************************************

// add appropriate usings
using ag.DbData.OleDb.Extensions;
using ag.DbData.OleDb.Factories;

***************************************************************************************************

// register services with extension method

		// ...
		services.AddAgOleDb();
		// or
		services.AddAgOleDb(config.GetSection("OleDbDbDataSettings"));
		// or
		services.AddAgOleDb(opts =>
        {
            opts.AllowExceptionLogging = false; // optional
			opts.ConnectionString = YOUR_CONNECTION_STRING; // optional
        });

***************************************************************************************************

// inject IOleDbDbDataFactory into your classes

        private readonly IOleDbDbDataFactory _oleDbFactory;

        public MyClass(IOleDbDbDataFactory oleDbFactory)
        {
            _oleDbFactory = oleDbFactory;
        }

***************************************************************************************************

// OleDbDataObject implements IDisposable interface, so use it into 'using' directive

        using (var oleDbDbData = _oleDbFactory.Create(YOUR_CONNECTION_STRING))
        {
            using (var t = oleDbDbData.FillDataTable("SELECT * FROM YOUR_TABLE"))
            {
                foreach (DataRow r in t.Rows)
                {
                    Console.WriteLine(r[0]);
                }
            }
        }

// in case you have defined connection string in configuration setting you may call Create() method
// without parameter

        using (var oleDbDbData = _oleDbFactory.Create())
        {
            using (var t = oleDbDbData.FillDataTable("SELECT * FROM YOUR_TABLE"))
            {
                foreach (DataRow r in t.Rows)
                {
                    Console.WriteLine(r[0]);
                }
            }
        }

***************************************************************************************************