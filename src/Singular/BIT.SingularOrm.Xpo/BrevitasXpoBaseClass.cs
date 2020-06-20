using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;

namespace BIT.SingularOrm.Xpo
{
    [ModelIgnore(nameof(Session))]
    [ModelIgnore(nameof(Session))]
    [ModelIgnore(nameof(IsLoading))]
    [ModelIgnore(nameof(IsDeleted))]
    [ModelIgnore("Loading")] //XPBaseObject.Loading is an obsolete property
    [ModelIgnore(nameof(Oid))]
    [ModelIgnore(nameof(This))]
    [ModelIgnore(nameof(ClassInfo))]
    [NonPersistent]
    public abstract class BrevitasXpoBaseClass : XPCustomObject
    {
        public BrevitasXpoBaseClass()
        {
        }

        public BrevitasXpoBaseClass(Session session) : base(session)
        {
        }

        public BrevitasXpoBaseClass(Session session, XPClassInfo classInfo) : base(session, classInfo)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
           
        }
#if MediumTrust
		private Guid oid = Guid.Empty;
		[Key(true)]
		public Guid Oid {
			get { return oid; }
			set { oid = value; }
		}
#else
        [PersistentAlias("oid")]
        [field: Persistent("Oid")]
        [field: Key(true)]
        [field: MemberDesignTimeVisibility(false)]
        public Guid Oid { get; } = Guid.Empty;
#endif
    }
}