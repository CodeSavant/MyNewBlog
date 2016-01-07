using System.Web;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using SimpleBlog.Models;

namespace SimpleBlog
{
    public static class Database
    {
        private const string SessionKey = "SimpleBlog.Database.SessionKey";
 
        private static ISessionFactory _sessionFactory;


        // to give access to the session.
        public static ISession Session
        {
            get { return (ISession) HttpContext.Current.Items[SessionKey]; }

        }
        public static void Configure() // Invoked when the application starts.
        {
            var config = new Configuration();
            // configure the connnection string
            config.Configure();

            // add our mapping
            var mapper = new ModelMapper();
            mapper.AddMapping<UserMap>();

            config.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

            // create our session factory
            _sessionFactory = config.BuildSessionFactory();
        }

        public static void OpenSession() // Invoked when the request occurs.
        {
            // for the sake of thread safety of the IIS. use Item[] with a unique key.
            // for multiple requests at the same time.
            HttpContext.Current.Items[SessionKey] = _sessionFactory.OpenSession();
        }

        public static void CloseSession() //  Invoked at the end of the request.
        {
            var session = HttpContext.Current.Items[SessionKey] as ISession;
            if (session != null)
            {
                session.Close();
            }

            HttpContext.Current.Items.Remove(SessionKey);
        }
    }

}