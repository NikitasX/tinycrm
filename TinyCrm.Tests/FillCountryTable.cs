using System.Collections.Generic;
using System.IO;
using System.Linq;
using TinyCrm.Core.Model;
using Xunit;

namespace TinyCrm.Tests
{
    public class FillCountryTable : IClassFixture<TinyCrmFixture>
    {

        private readonly TinyCrm.Core.Data.TinyCrmDbContext context_;

        public FillCountryTable(TinyCrmFixture fixture)
        {
            context_ = fixture.DbContext;
        }

        [Fact]
        public void FillOutCountryTableSuccess()
        {
            var Lines = File.ReadAllLines(@"C:\Users\KCA4\devel\TinyCrm\CountryCodes.csv");

            var i = 0;

            foreach (var line in Lines) {
                if (i == 0) {
                    i++;
                    continue;
                }

                var countryProperties = line.Split(',');

                var checker = context_
                    .Set<Country>()
                    .Where(c => c.CountryId.Equals(countryProperties[1]))
                    .SingleOrDefault();

                if(checker != null) {
                    context_.Add(new Country()
                    {
                        CountryId = countryProperties[1],
                        CountryFullName = countryProperties[0]
                    });
                }
            }
            context_.SaveChanges();
        }
    }
}
