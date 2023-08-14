namespace WebApi.Services.Interfaces
{
    public interface IConverter<T, DTO, DTOid>
    {
        DTO AsDTO(T obj);
        DTOid AsDTOid(T obj);
        T FromDTO(DTO obj);
    }
}
