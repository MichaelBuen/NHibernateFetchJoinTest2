namespace NHibernateFetchJoinTest2.DomainMapping
{
    using NHibernateFetchJoinTest2.DomainMapping.Mappings;

    using NHibernate.Cfg;

    using System.Linq;


    public static class Mapper
    {


        static NHibernate.ISessionFactory _sessionFactory = Mapper.BuildSessionFactory();


        // call this on production
        public static NHibernate.ISessionFactory SessionFactory
        {
            get { return _sessionFactory; }
        }


        // Call this on unit testing, so we can test caching on each test method independently
        public static NHibernate.ISessionFactory BuildSessionFactory(bool useUnitTest = false)
        {
            var mapper = new NHibernate.Mapping.ByCode.ModelMapper();

            mapper.AddMappings
                (
                    typeof(Mapper).Assembly.GetTypes()
                          .Where(x => x.BaseType.IsGenericType
                                      && x.BaseType.GetGenericTypeDefinition() == typeof(NHibernate.Mapping.ByCode.Conformist.ClassMapping<>))
                );

            // Or you can manually add the mappings
            // mapper.AddMappings(new[]
            //    {
            //        typeof(PersonMapping)
            //    });


            var cfg = new NHibernate.Cfg.Configuration();

            // .DatabaseIntegration! Y U EXTENSION METHOD?
            cfg.DataBaseIntegration(c =>
            {
                var cs = "Server=localhost;Database=test;User Id=sa;password=opensesame93*#;MultipleActiveResultSets=True";

                // SQL Server
                c.Driver<NHibernate.Driver.SqlClientDriver>();
                c.Dialect<NHibernate.Dialect.MsSql2012Dialect>();
                c.ConnectionString = cs;


                c.LogSqlInConsole = true;
                c.LogFormattedSql = true;

            });

            var domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();


            // // AsString is an extension method from NHibernate.Mapping.ByCode:
            // string mappingXml = domainMapping.AsString();

            // // Life without Resharper. 
            // string mappingXml = NHibernate.Mapping.ByCode.MappingsExtensions.AsString(domainMapping);

            // System.Console.WriteLine(mappingXml);


            cfg.AddMapping(domainMapping);


            if (useUnitTest)
                cfg.SetInterceptor(new NHSqlInterceptor());

            var sf = cfg.BuildSessionFactory();




            //using (var file = new System.IO.FileStream(@"c:\x\ddl.txt",
            //       System.IO.FileMode.Create,
            //       System.IO.FileAccess.ReadWrite))
            //using (var sw = new System.IO.StreamWriter(file))
            //{
            //    new NHibernate.Tool.hbm2ddl.SchemaUpdate(cfg)
            //        .Execute(scriptAction: sw.Write, doUpdate: false);
            //}

            return sf;
        }

        class NHSqlInterceptor : NHibernate.EmptyInterceptor
        {
            // http://stackoverflow.com/questions/2134565/how-to-configure-fluent-nhibernate-to-output-queries-to-trace-or-debug-instead-o
            public override NHibernate.SqlCommand.SqlString OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
            {

                Mapper.NHibernateSQL = sql.ToString();
                return sql;
            }

        }

        public static string NHibernateSQL { get; set; }


    } // Mapper



}
