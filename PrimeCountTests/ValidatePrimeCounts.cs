using Xunit;

namespace PrimeCount.Tests
{
    public class ValidatePrimeCounts
    {
        // Historical data for validating our results - the number of primes
        // to be found under some limit, such as 168 primes under 1000
        [Theory]
        [InlineData(10, 4)]
        [InlineData(100, 25)]
        [InlineData(1000, 168)]
        [InlineData(10000, 1229)]
        [InlineData(100000, 9592)]
        [InlineData(1000000, 78498)]
        [InlineData(10000000, 664579)]
        [InlineData(100000000, 5761455)]
        public void Number_of_primes_upto_given_number_should_be_as_expected(int upTo, int expected)
        {
            var sieve = new PrimeCount.PrimeSieve(upTo);
            sieve.RunSieve();
            Assert.Equal(expected, sieve.CountPrimes());
        }
    }
}