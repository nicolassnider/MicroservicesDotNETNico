using Mango.web.Models;

namespace Mango.web.Service.Iservice
{
    public interface IBaseService
    {
        Task <ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer=true);
    }
}
