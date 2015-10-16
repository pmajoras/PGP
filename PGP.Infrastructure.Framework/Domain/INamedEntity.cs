
using PGP.Infrastructure.Framework.Domain;

namespace GoImage.PortalFotografico.Infrastructure.Framework.Domain
{
    /// <summary>
    /// Define the interface of an entity that has a name property.
    /// </summary>
    public interface INamedEntity : IEntity
    {
        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }
        #endregion
    }
}
