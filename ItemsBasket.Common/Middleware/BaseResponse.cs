namespace ItemsBasket.Common.Middleware
{
    public class BaseResponse<T>
    {
        public T Item { get; }
        public bool IsSuccessful { get; }
        public string ErrorMessage { get; }

        protected BaseResponse(T item, bool isSuccessful, string errorMessage)
        {
            Item = item;
            IsSuccessful = isSuccessful;
            ErrorMessage = errorMessage;
        }
    }
}