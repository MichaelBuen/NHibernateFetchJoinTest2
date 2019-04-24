namespace NHibernateFetchJoinTest2
{
    using System;

    using NHibernateFetchJoinTest2.DomainMapping;
    using NHibernateFetchJoinTest2.Domains;

    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (var session = Mapper.SessionFactory.OpenSession())
            {
                Console.WriteLine("SQL produced: ");

                var d = session.Get<Document>(1);

                Console.ReadLine();

                Console.WriteLine("Document's periods: ");

                foreach (var period in d.Periods)
                {
                    Console.WriteLine($"* {period.PeriodDescription}");
                }

                Console.ReadLine();
            }
        }
    }
}
