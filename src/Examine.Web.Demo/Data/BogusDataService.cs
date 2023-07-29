using Bogus;
using Bogus.DataSets;

namespace Examine.Web.Demo.Controllers
{
    public class BogusDataService
    {
        /// <summary>
        /// Return a ton of people
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ValueSet> GenerateData(int count)
        {
            return Enumerable.Range(1, count)
                 .Select(x => new Person())
                 .Select((person, index) => new ValueSet(
                                index.ToString(),
                                "person",
                                PersonValues(person)));
        }

        private IDictionary<string, IEnumerable<object>> PersonValues(Person person)
        {
            var values = new Dictionary<string, IEnumerable<object>>
            {
                [nameof(person.FullName)] = new List<object>(1) { person.FullName },
                [nameof(person.Email)] = new List<object>(1) { person.Email },
                [nameof(person.Phone)] = new List<object>(1) { person.Phone },
                [nameof(person.Website)] = new List<object>(1) { person.Website },
                [$"{nameof(person.Company)}{nameof(person.Company.Name)}"] = new List<object>(1) { person.Company.Name },
                [$"{nameof(person.Company)}{nameof(person.Company.CatchPhrase)}"] = new List<object>(1) { person.Company.CatchPhrase },
                [$"{nameof(person.Address)}{nameof(person.Address.City)}"] = new List<object>(1) { person.Address.City },
                [$"{nameof(person.Address)}{nameof(person.Address.State)}"] = new List<object>(1) { person.Address.State },
                [$"{nameof(person.Address)}{nameof(person.Address.Street)}"] = new List<object>(1) { person.Address.Street }
            };
            return values;
        }


        /// <summary>
        /// Return a ton of people
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ValueSet> GenerateFacetData(int count)
        {
            return Enumerable.Range(1, count)
                .Select(x => (new Person(), new Commerce()))
                .Select((person, index) => new ValueSet(
                               index.ToString(),
                               "person",
                               FacetPersonValues(person)));
        }

        private IDictionary<string, IEnumerable<object>> FacetPersonValues((Person person, Commerce commerce) personCommerce)
        {
            var person = personCommerce.person;
            var commerce = personCommerce.commerce;
            var values = new Dictionary<string, IEnumerable<object>>
            {
                [nameof(person.FullName)] = new List<object>(1) { person.FullName },
                [nameof(person.Email)] = new List<object>(1) { person.Email },
                [nameof(person.Phone)] = new List<object>(1) { person.Phone },
                [nameof(person.Website)] = new List<object>(1) { person.Website },
                [$"{nameof(person.Company)}{nameof(person.Company.Name)}"] = new List<object>(1) { person.Company.Name },
                [$"{nameof(person.Company)}{nameof(person.Company.CatchPhrase)}"] = new List<object>(1) { person.Company.CatchPhrase },
                [$"{nameof(person.Address)}{nameof(person.Address.City)}"] = new List<object>(1) { person.Address.City },
                [$"{nameof(person.Address)}{nameof(person.Address.State)}"] = new List<object>(1) { person.Address.State },
                [$"{nameof(person.Address)}{nameof(person.Address.Street)}"] = new List<object>(1) { person.Address.Street },
                [$"{nameof(person.Address)}{nameof(person.Address.State)}{nameof(person.Address.City)}"] = new List<object>(2) {
                    new object[] { person.Address.City, new string[] { person.Address.State, person.Address.City }}
                },
                [$"Tags"] = commerce.Categories(3),
            };
            return values;
        }
    }
}
