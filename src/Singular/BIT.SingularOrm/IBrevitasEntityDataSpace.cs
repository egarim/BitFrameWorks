namespace BIT.SingularOrm
{
    /// <summary>
    ///     Implemented by business entity that provide a reference to an associated Data Space.
    /// </summary>
    public interface IBrevitasEntityDataSpace
    {
        /// <summary>
        ///     Specifies an Data Space associated with the current IBrevitasEntityDataSpace object.
        /// </summary>
        IDataSpace DataSpace { get; set; }
    }
}