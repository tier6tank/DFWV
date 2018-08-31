using System.Globalization;
using PluralizationService;
using PluralizationService.English;

namespace DFWV {
    internal static class Pluralizer {
        private static readonly IPluralizationApi Api = Initialize();
        private static readonly CultureInfo CultureInfo = new CultureInfo("en-US");

        private static IPluralizationApi Initialize() {
            var builder = new PluralizationApiBuilder();
            builder.AddEnglishProvider();
            return builder.Build();
        }

        internal static string Pluralize(this string word) {
            return Api.Pluralize(word, CultureInfo);
        }

        internal static string Singularize(this string word) {
            return Api.Singularize(word);
        }

        internal static bool IsPlural(this string word) {
            return Api.IsPlural(word);
        }
    }
}