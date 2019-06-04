using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Meblex.API.Context;
using Meblex.API.Interfaces;
using Microsoft.Extensions.Localization;

namespace Meblex.API.Services
{
    public class FitterService:IFitterService
    {
        private readonly MeblexDbContext _context;
        private readonly IStringLocalizer<FitterService> _localizer;

        public FitterService(MeblexDbContext context, IStringLocalizer<FitterService> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        /*
          wymiary w metrach, wieć trzeba przekształcić meble z cm n m
          roomSize w metrach 4mx5m - [4, 5]
          furnitureSizes np. [[3,4], [6, 1]]
        */
        public bool is_FITT(float[] roomSize, float[] furnitureSizes)
        {
            //            foreach (var furniture in furnitureSizes)
            //                sort(furniture);
            //            sort(furnitureSizes);
            //            sort(roomSize);
            //
            //            float level = roomSize[0];
            //            foreach (var furniture in furnitureSizes)
            //            {
            //                if (furniture[0] > level)
            //                {
            //                    return false;
            //                }
            //                else
            //                {
            //                    level = roomSize[0] - furniture[0];
            //                }
            //            }
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
