using System.Collections.Generic;
using System.Text;

namespace Greenbacks.Models
{
    public class ConnectionProperties
    {
        private string _app;

        public string App
        {
            get { return _app; }
            set { _app = value; }
        }

        private string _attachDbFileName;

        public string AttachDbFileName
        {
            get { return _attachDbFileName; }
            set { _attachDbFileName = value; }
        }

        private int _connectTimeout;

        public int ConnectTimeout
        {
            get { return _connectTimeout; }
            set { _connectTimeout = value; }
        }

        private string _dataSource;

        public string DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; }
        }

        private string _initialCatalog;

        public string InitialCatalog
        {
            get { return _initialCatalog; }
            set { _initialCatalog = value; }
        }

        private bool _integratedSecurity;

        public bool IntegratedSecurity
        {
            get { return _integratedSecurity; }
            set { _integratedSecurity = value; }
        }

        private string _metadata;

        public string Metadata
        {
            get { return _metadata; }
            set { _metadata = value; }
        }

        private bool _multipleActiveResultSets;

        public bool MultipleActiveResultSets
        {
            get { return _multipleActiveResultSets; }
            set { _multipleActiveResultSets = value; }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _provider;

        public string Provider
        {
            get { return _provider; }
            set { _provider = value; }
        }

        private string _username;

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string ConnectionString
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                if (!string.IsNullOrEmpty(_metadata))
                {
                    builder.Append("metadata=" + _metadata + ";");
                }

                if (!string.IsNullOrEmpty(_provider))
                {
                    builder.Append("provider=" + _provider + ";");
                }

                builder.Append("provider connection string=';");
                builder.Append("data source=" + _dataSource + ";");
                builder.Append("attachdbfilename=" + _attachDbFileName + ";");

                if (!string.IsNullOrEmpty(_initialCatalog))
                {
                    builder.Append("Initial Catalog=" + _initialCatalog + ";");
                }

                builder.Append("integrated security=" + _integratedSecurity.ToString() + ";");

                if (!string.IsNullOrEmpty(_username))
                {
                    builder.Append("uid" + _username + ";");
                    builder.Append("password=" + _password + ";");
                }

                if (ConnectTimeout > 0)
                {
                    builder.Append("connect timeout" + ConnectTimeout.ToString() + ";");
                }

                builder.Append("MultipleActiveResultSets=" + _multipleActiveResultSets.ToString() + ";");

                if (!string.IsNullOrEmpty(App)) { App = "EntityFramework"; }
                builder.Append("App=" + _app + "';");

                return builder.ToString();
            }
        }

        // <add name="GreenbacksEntities" connectionString="metadata=res://*/Greentacks.csdl|res://*/Greentacks.ssdl|res://*/Greentacks.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\DefaultDB\Greenbacks.mdf;integrated security=True;connect timeout=30;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />

        public ConnectionProperties()
        {

        }

        private void SetDefaults()
        {
            _app = "EntityFramework";
            _attachDbFileName = @"|DataDirectory|\DefaultDB\Greenbacks.mdf";
            _connectTimeout = 30;
            _dataSource = @"(LocalDB)\MSSQLLocalDB";
            _initialCatalog = "";
            _integratedSecurity = true;
            _metadata = "res://*/Greentacks.csdl|res://*/Greentacks.ssdl|res://*/Greentacks.msl";
            _multipleActiveResultSets = true;
            _provider = "System.Data.SqlClient";
        }

        public void ParseProperties(string connectionString)
        {
            var props = connectionString.Split(';');

            List<ConnectionProperty> properties = new List<ConnectionProperty>();
            foreach (var prop in props)
            {
                if (!string.IsNullOrEmpty(prop))
                {
                    string[] items = prop.Split('=');
                    properties.Add(new ConnectionProperty
                    {
                        Property = items[0],
                        Value = items[1]
                    });
                }
            }

            int intVal = 0;
            bool boolVal = false;

            foreach (var prop in properties)
            {
                switch (prop.Property)
                {
                    case "metadata":
                        Metadata = prop.Value;

                        break;
                    case "provider":
                        Provider = prop.Value;

                        break;
                    case "data source":
                        DataSource = prop.Value;

                        break;
                    case "initial catalog":
                        InitialCatalog = prop.Value;

                        break;
                    case "attachdbfilename":
                        AttachDbFileName = prop.Value;

                        break;
                    case "integrated security":
                        if (bool.TryParse(prop.Value, out boolVal))
                        {
                            IntegratedSecurity = boolVal;
                        }

                        break;
                    case "connect timeout":
                        if (int.TryParse(prop.Value, out intVal))
                        {
                            ConnectTimeout = intVal;
                        }

                        break;
                    case "MultipleActiveResultSets":
                        if (bool.TryParse(prop.Value, out boolVal))
                        {
                            MultipleActiveResultSets = boolVal;
                        }

                        break;
                    case "App":
                        App = prop.Value;

                        break;
                    default:
                        break;
                }
            }
        }
    }
}
