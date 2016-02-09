using System.Linq;

namespace ModelHouse
{
    class Outside : Location
    {
        private bool hot;
        public bool Hot {  get { return hot; } }

        public Outside(string name, bool hot)
            : base(name)
        {
            this.hot = hot;
        }

        public override string Description
        {
            get
            {
                string NewDescription = base.Description;
                if (hot)
                    NewDescription += " Ti's very hot.";
                return NewDescription;
            }
        }
    }
}