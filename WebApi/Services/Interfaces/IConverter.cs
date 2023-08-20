namespace WebApi.Services.Interfaces
{
    public interface IConverter<T, DTO>
    {
        DTO AsDTO(T obj);
        T FromDTO(DTO obj);
    }
}
