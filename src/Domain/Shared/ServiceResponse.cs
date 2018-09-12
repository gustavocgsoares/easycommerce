namespace Easy.Commerce.Domain.Shared
{
    public class ServiceResponse<T>
    {
        public Error Error { get; set; }

        public T Result { get; set; }

        public bool HasError
        {
            get
            {
                return Error != null;
            }
        }

        internal ServiceResponse<T> WithErrorMessage(string message)
        {
            Error = new Error
            {
                Message = message
            };

            return this;
        }
    }
}
