using System;
using DevExpress.Xpo;

namespace XpoDemoOrm
{
    public class Customer : XPLiteObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Customer(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        [DevExpress.Xpo.Key(false)]
        //HACK Xpo Attributes
        [Indexed(Unique = true)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public Int32 Code
        {
            get => code;
            set => SetPropertyValue(nameof(Code), ref code, value);
        }

        Int32 code;
        decimal maxCredit;
        bool active;
        string name;


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }

        public bool Active
        {
            get => active;
            set => SetPropertyValue(nameof(Active), ref active, value);
        }


        public decimal MaxCredit
        {
            get => maxCredit;
            set => SetPropertyValue(nameof(MaxCredit), ref maxCredit, value);
        }


        [Association("Customer-Invoices")]
        public XPCollection<Invoice> Invoices
        {
            get
            {
                return GetCollection<Invoice>(nameof(Invoices));
            }
        }

    }
}