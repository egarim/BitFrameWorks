
namespace BIT.SingularOrm
{
    public static class DataSpaceEntityHelper
    {
        public static void Link(ref object Instance, IDataSpace DataSpace)
        {
            var IBrevitasEntityDataSpace = Instance as IBrevitasEntityDataSpace;
            if (IBrevitasEntityDataSpace != null) IBrevitasEntityDataSpace.DataSpace = DataSpace;
        }

        public static void Link<T>(ref T Instance, IDataSpace DataSpace)
        {
            var IBrevitasEntityDataSpace = Instance as IBrevitasEntityDataSpace;
            if (IBrevitasEntityDataSpace != null) IBrevitasEntityDataSpace.DataSpace = DataSpace;
        }
    }
}