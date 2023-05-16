namespace Core.CQRS
{
    public static class ValidationErrorCodes
    {
        public const string CannotBeEmpty = "1";
        public const string GreaterThan = "2";
        public const string LessThan = "3";
        public const string LessThanOrEqual = "4";
        public const string NotEqualTo = "5";
        public const string NotEqualToOrEqual = "6";
        public const string BadValue = "7";
        public const string NotNull = "8";
        public const string ShouldBeNull = "9";
    }
}
