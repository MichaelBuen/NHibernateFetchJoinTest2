namespace NHibernateFetchJoinTest2.DomainMapping.Mappings
{
    using NHibernate.Mapping.ByCode.Conformist;
    using NHibernateFetchJoinTest2.Domains;

    public class DocumentMapping : ClassMapping<Document>
    {
        public DocumentMapping()
        {
            Id(x => x.Id);

            Property(x => x.DocumentDescription);

            Bag(x => x.Periods, collectionMapping =>
            {
                collectionMapping.Inverse(true);
                collectionMapping.Key(k => k.Column("DocumentId"));

                collectionMapping.Lazy(NHibernate.Mapping.ByCode.CollectionLazy.Lazy);

                // Remove this. This causes Document's Periods to load, even if child collection Periods 
                // is not accessed yet.
                // This is evident in SQL log, it shows LEFT JOIN Period.
                collectionMapping.Fetch(NHibernate.Mapping.ByCode.CollectionFetchMode.Join);
            }, mapping => mapping.OneToMany());
        }
    }

    public class PeriodMapping: ClassMapping<Period>
    {
        public PeriodMapping()
        {
            Id(x => x.Id);
            Property(x => x.PeriodDescription);
        }
    }
}
