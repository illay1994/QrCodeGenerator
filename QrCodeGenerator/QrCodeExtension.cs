namespace QrCodeGenerator
{
    internal static class QrCodeExtension
    {
        public static string ToQrCode(this long data)
        {
            return $"MQ{data:D12}";
        }
    }
}