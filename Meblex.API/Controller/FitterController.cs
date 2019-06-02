using Meblex.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Meblex.API.Controller
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class FitterController : ControllerBase
    {
        public readonly IFitterService _fitterService;
        public FitterController(IFitterService fitterService)
        {
            _fitterService = fitterService;
        }

        /*
          wymiary w metrach, wieć trzeba przekształcić meble z cm n m
          roomSize w metrach 4mx5m - [4, 5]
          furnitureSizes np. [[3,4], [6, 1]]
        */
        public bool is_FITT(float[] roomSize, float[] furnitureSizes)
        {
            foreach (furniture in furnitureSizes)
                sort(furniture);
            sort(furnitureSizes);
            sort(roomSize);

            float level = roomSize[0];
            foreach (furniture in furnitureSizes)
            {
                if (furniture[0] > level)
                {
                    return false;
                }
                else
                {
                    level = roomSize[0] - furniture[0];
                }
            }
            return true;
        }


        static void sort(int[] arr)
        {
            int n = arr.Length;

            // One by one move boundary of unsorted subarray 
            for (int i = 0; i < n - 1; i++)
            {
                // Find the max element in unsorted array 
                int max_idx = i;
                for (int j = i + 1; j < n; j++)
                    if (arr[j] > arr[max_idx])
                        max_idx = j;

                // Swap the found max element with the first element 
                int temp = arr[max_idx];
                arr[max_idx] = arr[i];
                arr[i] = temp;
            }
        }
    }
}