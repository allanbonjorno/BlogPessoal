namespace BlogPessoal.Security
{
    public class Settings
    {
        private static string secret = "656bed301aaf5512cdb5cac10f9213d76ca3da57192d62e15ce0cdad42d46380";
        public static string Secret { get => secret; set => secret = value; }

    }
}
