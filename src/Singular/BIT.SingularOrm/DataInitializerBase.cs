
namespace BIT.SingularOrm
{
    public abstract class DataInitializerBase : IDataInitializer
    {
        public abstract void BeforeUpdateSchema(IDataSpace DataSpace);

        public abstract void DataInitialization(IDataSpace DataSpace);
    }
}