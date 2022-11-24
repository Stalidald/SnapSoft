using SnapSoft.Models;

namespace SnapSoft.Services
{
    public interface ICalculateService
    {
        public List<BaseModel> ListAll(Filter? filter);

        public RequestBody GetA(RequestBody requestBody);
        public RequestBody GetB(RequestBody requestBody);
        public RequestBody GetC(RequestBody requestBody);
    }
}
