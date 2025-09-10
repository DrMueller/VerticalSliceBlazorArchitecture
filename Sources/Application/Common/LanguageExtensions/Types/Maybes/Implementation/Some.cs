namespace VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Maybes.Implementation
{
    public class Some<T>(T content) : Maybe<T>
    {
        private readonly T _content = content;

        public static implicit operator T(Some<T> value)
        {
            return value._content;
        }

        public override bool Equals(Maybe<T>? other)
        {
            return Equals(other as Some<T>);
        }

        public override bool Equals(T? other)
        {
            return ContentEquals(other);
        }

        public override int GetHashCode()
        {
            return _content!.GetHashCode();
        }

        private bool ContentEquals(T? other)
        {
            if (_content is null && other is null)
            {
                return true;
            }

            if (_content is not null && _content.Equals(other))
            {
                return true;
            }

            return false;
        }

        private bool Equals(Some<T>? other)
        {
            return other is not null &&
                   ContentEquals(other._content);
        }
    }
}