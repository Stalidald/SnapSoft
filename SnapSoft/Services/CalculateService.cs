using SnapSoft.DataHandler;
using SnapSoft.Models;

namespace SnapSoft.Services
{
    public class CalculateService : ICalculateService
    {
        SnapSoftDataContext _context;
        public CalculateService(SnapSoftDataContext context)
        {
            _context = context;
        }

        public RequestBody GetA(RequestBody requestBody)
        {
            int[] array = requestBody.array;

            int[] returnArray = new int[array.Length];

            int sum = 1;
            foreach (var item in array) { sum *= item; }

            for (int i = 0; i < returnArray.Length; i++)
            {
                returnArray[i] = sum / array[i];
            }

            RequestBody returnBody = new RequestBody() { array = returnArray, comment = requestBody.comment };
            return returnBody;
        }

        public RequestBody GetB(RequestBody requestBody)
        {

            int[] returnArray = new int[requestBody.array.Length];

            for (int i = 0; i < returnArray.Length; i++)
            {
                int sum = 1;
                for (int j = 0; j < returnArray.Length; j++)
                {
                    if (j == i) continue;
                    else sum *= requestBody.array[j];
                }
                returnArray[i] = sum;
            }

            RequestBody returnBody = new RequestBody() { array = returnArray, comment = requestBody.comment };
            return returnBody;
        }

        public RequestBody GetC(RequestBody requestBody)
        {
            int n = requestBody.array.Length;

            var left = new int[n];
            var right = new int[n];
            var array = new int[n];

            left[0] = 1;
            right[n - 1] = 1;
            
            for(int i = 1; i < n; i++)
            {
                left[i] = requestBody.array[i - 1] * left[i - 1];
            }

            for(int i = n - 2; i >= 0; i--)
            {
                right[i] = requestBody.array[i + 1] * right[i + 1];
            }

            for(int i = 0; i < n; i++)
            {
                array[i] = left[i] * right[i];
            }

            return new RequestBody() { array = array, comment = requestBody.comment };
        }

        public List<BaseModel> ListAll(Filter? filter)
        {
            List<BaseModel> filteredValue = new List<BaseModel>();

            if (filter != null)
            {
                if (filter.RequestId != Guid.Empty)
                {
                    var entities = _context.RequestsAndResponses.Where(r => r.RequestId == filter.RequestId || r.Id == filter.RequestId).ToList();
                    filteredValue.AddRange(entities);
                }
                else
                {
                    var entities = _context.RequestsAndResponses.ToList();
                    filteredValue.AddRange(entities);
                }
                    
                
                if (filter.From != DateTime.MinValue)       
                    filteredValue = filteredValue.Where(x => x.TimeStamp > filter.From).ToList();
                
                if (filter.To != DateTime.MinValue)   
                    filteredValue = filteredValue.Where(x => x.TimeStamp < filter.To).ToList();           
            }
            return filteredValue;
        }
    }
}
