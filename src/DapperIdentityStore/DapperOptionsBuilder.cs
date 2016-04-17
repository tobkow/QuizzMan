namespace QuizzMan.IdentityStore.Dapper
{
    public class DapperOptionsBuilder
    {
        private DapperOptions _options;

        public DapperOptionsBuilder()
                : this(new DapperOptions())
        {
        }

        public DapperOptionsBuilder(DapperOptions options)
        {
            _options = options;
        }

        public virtual DapperOptions Options => _options;
    }
}
