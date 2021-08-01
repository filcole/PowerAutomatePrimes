using Newtonsoft.Json;
using System;
using System.Collections;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrimeCount
{
    public class PrimeSieve
    {
        private readonly int SieveSize = 0;
        private readonly BitArray bitArray; //making it readonly so we tell the compiler that the variable reference cant change. around 5% increase in performance

        public PrimeSieve(int size)
        {
            SieveSize = size;
            bitArray = new BitArray(((this.SieveSize + 1) / 2), true);
        }

        public int CountPrimes()
        {
            int count = 0;
            for (int i = 0; i < this.bitArray.Count; i++)
                if (bitArray[i])
                    count++;
            return count;
        }

        private bool GetBit(int index)
        {
            System.Diagnostics.Debug.Assert(index % 2 == 1);

            return bitArray[index >> 1];
        }

        private void ClearBit(int index)
        {
            System.Diagnostics.Debug.Assert(index % 2 == 1);

            bitArray[index / 2] = false;
        }

        // primeSieve
        //
        // Calculate the primes up to the specified limit

        public void RunSieve()
        {
            int factor = 3;
            int q = (int)Math.Sqrt(SieveSize);

            while (factor <= q)
            {
                for (int num = factor; num <= q; num += 2)
                {
                    if (GetBit(num))
                    {
                        factor = num;
                        break;
                    }
                }

                int increment = factor * 2;

                for (int num = factor * 3; num <= SieveSize; num += increment)
                    ClearBit(num);

                factor += 2;
            }
        }
    }

    public class CountPrimesRequest
    {
        public int UpTo { get; set; }
    }

    public class CountPrimesResponse
    {
        public int NumPrimes { get; set; }
    }

    public class Script : ScriptBase
    {
        public override async Task<HttpResponseMessage> ExecuteAsync()
        {
            var request = JsonConvert.DeserializeObject<CountPrimesRequest>(await this.Context.Request.Content.ReadAsStringAsync());

            var sieve = new PrimeSieve(request.UpTo);
            sieve.RunSieve();

            var response = new CountPrimesResponse
            {
                NumPrimes = sieve.CountPrimes()
            };

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = CreateJsonContent(JsonConvert.SerializeObject(response))
            };
        }
    }
}