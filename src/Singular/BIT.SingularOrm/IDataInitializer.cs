namespace BIT.SingularOrm
{
    public interface IDataInitializer
    {
        void BeforeUpdateSchema(IDataSpace DataSpace);

        void DataInitialization(IDataSpace DataSpace);
    }
}