﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
namespace GTranslate;

/// <summary>
/// Represents the default language dictionary used in GTranslate. It contains all the supported languages across all the included translators.
/// </summary>
public sealed class LanguageDictionary : ILanguageDictionary<string, Language>
{
    internal LanguageDictionary()
    {
        Aliases = new ReadOnlyDictionary<string, string>(BuildLanguageAliases());
        ValidateLanguageSets(); // The implementation is only called in Debug mode.
    }
    [ConditionalAttribute("DEBUG")]
    private void ValidateLanguageSets() // Implementation is only called in Debug mode
    {   // Validate that the language set all have unique keys, Names, and ISO6391
        System.Collections.Generic.SortedList<string, GTranslate.Language> SortedLanguageKeys = new();
        System.Collections.Generic.SortedList<string, GTranslate.Language> SortedLanguageLangNames = new();
        System.Collections.Generic.SortedList<string, GTranslate.Language> SortedLanguageISO6391s = new();
        foreach ( var language in _languages )
        {
            try
            {
                SortedLanguageKeys.Add(language.Key, language.Value); // This will fail if there's a duplicate key
            }
            catch(Exception e)
            {
                string Msg = $"Error: Duplicate key found in language list. See {language.Key} - {language.Value.Name} - {language.Value.ISO6391} ; ExceptionMsg={e.Message}";
                Debug.WriteLine(Msg);
                throw new Exception(Msg);
            }

            try
            {
                SortedLanguageLangNames.Add(language.Value.Name, language.Value); // This will fail if there's a duplicate language name
            }
            catch ( Exception e )
            {
                string Msg = $"Error: Duplicate language name found in language list. See {language.Key} - {language.Value.Name} - {language.Value.ISO6391} ; ExceptionMsg={e.Message}";
                Debug.WriteLine(Msg);
                throw new Exception(Msg);
            }

            try
            {
                SortedLanguageISO6391s.Add(language.Value.ISO6391, language.Value); // This will fail if there's a duplicate ISO6391
            }
            catch ( Exception e )
            {
                string Msg = $"Error: Duplicate language ISO6391 found in language list. See {language.Key} - {language.Value.Name} - {language.Value.ISO6391} ; ExceptionMsg={e.Message}";
                Debug.WriteLine(Msg);
                throw new Exception(Msg);
            }
        }
    }
    /// <inheritdoc />
    public IEnumerator<KeyValuePair<string, Language>> GetEnumerator()
        => _languages.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => _languages.GetEnumerator();

    /// <inheritdoc />
    public int Count => _languages.Count;

    /// <inheritdoc />
    public Language this[string key] => _languages[key];

    /// <inheritdoc />
    public IEnumerable<string> Keys => _languages.Keys;

    /// <inheritdoc />
    public IEnumerable<Language> Values => _languages.Values;

    /// <inheritdoc />
    public bool ContainsKey(string key) => _languages.ContainsKey(key);

    /// <inheritdoc/>
#if NETCOREAPP3_0_OR_GREATER
    public bool TryGetValue(string key, [MaybeNullWhen(false)] out Language value) => _languages.TryGetValue(key, out value);
#else
    public bool TryGetValue(string key, out Language value) => _languages.TryGetValue(key, out value!);
#endif

    /// <summary>
    /// Gets a language from a language code, name or alias.
    /// </summary>
    /// <param name="code">The language name or code. It can be a ISO 639-1 code, a ISO 639-3 code, a language name or a language alias.</param>
    /// <returns>The language, or an exception if the language was not found.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="code"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the language was not found.</exception>
    public Language GetLanguage(string code)
    {
        TranslatorGuards.NotNull(code);

        if (TryGetValue(code, out var language))
        {
            return language;
        }

        return Aliases.TryGetValue(code, out var iso) ? _languages[iso] : throw new ArgumentException($"Unknown language \"{code}\".", nameof(code));
    }

    /// <summary>
    /// Tries to get a language from a language code, name or alias.
    /// </summary>
    /// <param name="code">The language name or code. It can be a ISO 639-1 code, a ISO 639-3 code, a language name or a language alias.</param>
    /// <param name="language">The language, if found.</param>
    /// <returns><see langword="true"/> if the language was found, otherwise <see langword="false"/>.</returns>
    public bool TryGetLanguage(string code, [MaybeNullWhen(false)] out Language language)
    {
        language = default;

        if ( string.IsNullOrEmpty(code) )
        {
            return false;
        }

        if ( TryGetValue(code, out language) )
        {
            return true;
        }

        if ( !Aliases.TryGetValue(code, out string? iso) )
        {
            return false;
        }

        language = _languages[iso];
        return true;
    }

    /// <summary>
    /// Gets a read-only dictionary containing language aliases and their corresponding ISO 639-1 codes.
    /// </summary>
    public IReadOnlyDictionary<string, string> Aliases { get; }

    private Dictionary<string, string> BuildLanguageAliases()
    {
        var aliases = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
        {
            ["chinese"] = "zh-CN",
        };

        foreach (var kvp in _languages)
        {
            aliases[kvp.Value.Name] = kvp.Key;
            aliases[kvp.Value.NativeName] = kvp.Key;
            aliases[kvp.Value.ISO6393] = kvp.Key;
        }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
        aliases.TrimExcess();
#endif

        return aliases;
    }

    private readonly IReadOnlyDictionary<string, Language> _languages = new ReadOnlyDictionary<string, Language>(new Dictionary<string, Language>(StringComparer.OrdinalIgnoreCase)
    {
        ["af"] = new ("Afrikaans", "Afrikaans", "af", "afr"),
        ["am"] = new("Amharic", "አማርኛ", "am", "amh"),
        ["ar"] = new("Arabic", "العربية", "ar", "ara"),
        ["as"] = new("Assamese", "অসমীয়া", "as", "asm", TranslationServices.Bing | TranslationServices.Microsoft),
        ["ay"] = new("Aymara", "aymar aru [ajmaɾ ˈaɾu]", "ay", "aym", TranslationServices.Google),
        ["az"] = new("Azerbaijani", "Azərbaycan", "az", "aze"),
        ["ba"] = new("Bashkir", "Bashkir", "ba", "bak", TranslationServices.Bing | TranslationServices.Microsoft | TranslationServices.Yandex),
        ["be"] = new("Belarusian", "беларуская", "be", "bel", TranslationServices.Google | TranslationServices.Yandex),
        ["bg"] = new("Bulgarian", "Български", "bg", "bul"),
        ["bho"] = new("Bhojpuri", "भोजपुरी", "bho", "bho", TranslationServices.Google),
        ["bm"] = new("Bambara", "ߓߊߡߊߣߊ߲ߞߊ߲", "bm", "bam", TranslationServices.Google),
        ["bn"] = new("Bengali", "বাংলা", "bn", "ben"),
        ["bo"] = new("Tibetan", "བོད་སྐད་", "bo", "bod", TranslationServices.Bing | TranslationServices.Microsoft),
        ["bs"] = new("Bosnian", "bosanski", "bs", "bos"),
        ["ca"] = new("Catalan", "Català", "ca", "cat"),
        ["ceb"] = new("Cebuano", "Binisaya", "ceb", "ceb", TranslationServices.Google | TranslationServices.Yandex),
        ["co"] = new("Corsican", "Corsu", "co", "cos", TranslationServices.Google),
        ["cs"] = new("Czech", "Čeština", "cs", "ces"),
        ["cv"] = new("Chuvash", "Чӑвашла", "cv", "chv", TranslationServices.Yandex),
        ["cy"] = new("Welsh", "Cymraeg", "cy", "cym"),
        ["da"] = new("Danish", "Dansk", "da", "dan"),
        ["de"] = new("German", "Deutsch", "de", "deu"),
        ["dgo"] = new("Dogri", "डोगरी (ḍogrī) ڈوگرى", "dgo", "dgo", TranslationServices.Google),
        ["dv"] = new("Divehi", "ދިވެހިބަސް", "dv", "div", TranslationServices.Bing | TranslationServices.Microsoft),
        ["ee"] = new("Ewe", "Eʋegbe", "ee", "ewe", TranslationServices.Google),
        ["el"] = new("Greek", "Ελληνικά", "el", "ell"),
        ["emj"] = new("Emoji", "Emoji", "emj", "emj", TranslationServices.Yandex), // Not present in Yandex.Cloud
        ["en"] = new("English", "English", "en", "eng"),
        ["en-GB"] = new("English (UK)", "English (UK)", "en-GB", "en-GB", TranslationServices.Google),
        ["eo"] = new("Esperanto", "Esperanto", "eo", "epo", TranslationServices.Google | TranslationServices.Yandex),
        ["es"] = new("Spanish", "Español", "es", "spa"),
        ["es-MX"] = new("Spanish (Mexico)", "español (México)", "es-MX", "es-MX", TranslationServices.Bing | TranslationServices.Microsoft),
        ["et"] = new("Estonian", "Eesti", "et", "est"),
        ["eu"] = new("Basque", "Euskara", "eu", "eus"),
        ["fa"] = new("Persian", "فارسی", "fa", "fas"),
        ["fi"] = new("Finnish", "Suomi", "fi", "fin"),
        ["fil"] = new("Filipino", "Tagalog", "fil", "fil", TranslationServices.Bing | TranslationServices.Microsoft),
        ["fj"] = new("Fijian", "Na Vosa Vakaviti", "fj", "fij", TranslationServices.Bing | TranslationServices.Microsoft),
        ["fo"] = new("Faroese", "føroyskt mál", "fo", "fao", TranslationServices.Bing | TranslationServices.Microsoft),
        ["fr"] = new("French", "Français", "fr", "fra"),
        ["fr-CA"] = new("French (Canada)", "Français (Canada)", "fr-CA", "fr-CA", TranslationServices.Bing | TranslationServices.Microsoft),
        ["fy"] = new("Frisian", "Frysk", "fy", "fry", TranslationServices.Google),
        ["ga"] = new("Irish", "Gaeilge", "ga", "gle"),
        ["gd"] = new("Gaelic", "Gàidhlig", "gd", "gla", TranslationServices.Google | TranslationServices.Yandex),
        ["gl"] = new("Galician", "Galego", "gl", "glg"),
        ["gn"] = new("Guarani", "avañeʼẽ", "gn", "grn", TranslationServices.Google),
        ["gu"] = new("Gujarati", "ગુજરાતી", "gu", "guj"),
        ["ha"] = new("Hausa", "Hausa", "ha", "hau", TranslationServices.Google),
        ["haw"] = new("Hawaiian", "ʻŌlelo Hawaiʻi", "haw", "haw", TranslationServices.Google),
        ["he"] = new("Hebrew", "עברית", "he", "heb"),
        ["hi"] = new("Hindi", "हिन्दी", "hi", "hin"),
        ["hmn"] = new("Hmong", "Hmong", "hmn", "hmn", TranslationServices.Google),
        ["hr"] = new("Croatian", "Hrvatski", "hr", "hrv"),
        ["hsb"] = new("Upper Sorbian", "Hornjoserbšćina", "hsb", "hsb", TranslationServices.Bing | TranslationServices.Microsoft),
        ["ht"] = new("Haitian Creole", "Kreyòl ayisyen", "ht", "hat"),
        ["hu"] = new("Hungarian", "Magyar", "hu", "hun"),
        ["hy"] = new("Armenian", "Հայերեն", "hy", "hye"),
        ["id"] = new("Indonesian", "Indonesia", "id", "ind"),
        ["ig"] = new("Igbo", "Igbo", "ig", "ibo", TranslationServices.Google),
        ["ikt"] = new("Inuinnaqtun", "Inuinnaqtun", "ikt", "ikt", TranslationServices.Bing | TranslationServices.Microsoft),
        ["ilo"] = new("Ilocano", "Iloko", "ilo", "ilo", TranslationServices.Google),
        ["is"] = new("Icelandic", "Íslenska", "is", "isl"),
        ["it"] = new("Italian", "Italiano", "it", "ita"),
        ["iu"] = new("Inuktitut", "ᐃᓄᒃᑎᑐᑦ", "iu", "iku", TranslationServices.Bing | TranslationServices.Microsoft),
        ["iu-Latn"] = new("Inuktitut (Latin)", "Inuktitut (Latin)", "iu-Latn", "iu-Latn", TranslationServices.Bing | TranslationServices.Microsoft),
        ["ja"] = new("Japanese", "日本語", "ja", "jpn"),
        ["jv"] = new("Javanese", "Jawa", "jv", "jav", TranslationServices.Google | TranslationServices.Yandex),
        ["ka"] = new("Georgian", "ქართული", "ka", "kat"),
        ["kazlat"] = new("Kazakh (Latin)", "qazaqşa", "kazlat", "kazlat", TranslationServices.Yandex),
        ["kk"] = new("Kazakh", "Қазақ Тілі", "kk", "kaz"),
        ["km"] = new("Khmer", "ខ្មែរ", "km", "khm"),
        ["kmr"] = new("Kurdish (Northern)", "Kurdî (Bakur)", "kmr", "kmr", TranslationServices.Bing | TranslationServices.Microsoft),
        ["kn"] = new("Kannada", "ಕನ್ನಡ", "kn", "kan"),
        ["ko"] = new("Korean", "한국어", "ko", "kor"),
        ["ku"] = new("Kurdish", "Kurdî", "ku", "kur", TranslationServices.Google | TranslationServices.Bing | TranslationServices.Microsoft),
        ["ky"] = new("Kyrgyz", "Kyrgyz", "ky", "kir"),
        ["la"] = new("Latin", "Latina", "la", "lat", TranslationServices.Google | TranslationServices.Yandex),
        ["lb"] = new("Luxembourgish", "Lëtzebuergesch", "lb", "ltz", TranslationServices.Google | TranslationServices.Yandex),
        ["ln"] = new("Lingala", "Lingála", "ln", "lin", TranslationServices.Google),
        ["lo"] = new("Lao", "ລາວ", "lo", "lao"),
        ["lt"] = new("Lithuanian", "Lietuvių", "lt", "lit"),
        ["lv"] = new("Latvian", "Latviešu", "lv", "lav"),
        ["lzh"] = new("Chinese (Literary)", "中文 (文言文)", "lzh", "lzh", TranslationServices.Bing | TranslationServices.Microsoft),
        ["mai"] = new("Maithili", "Macushi", "mai", "mai", TranslationServices.Google),
        ["mg"] = new("Malagasy", "Malagasy", "mg", "mlg"),
        ["mhr"] = new("Eastern Mari", "олык марий", "mhr", "mhr", TranslationServices.Yandex),
        ["mi"] = new("Maori", "Te Reo Māori", "mi", "mri"),
        ["mk"] = new("Macedonian", "Македонски", "mk", "mkd"),
        ["ml"] = new("Malayalam", "മലയാളം", "ml", "mal"),
        ["mn"] = new("Mongolian", "Монгол хэл", "mn", "mon"),
        ["mn-Mong"] = new("Mongolian (Traditional)", "ᠮᠣᠩᠭᠣᠯ ᠬᠡᠯᠡ", "mn-Mong", "mn-Mong", TranslationServices.Bing | TranslationServices.Microsoft),
        ["mr"] = new("Marathi", "मराठी", "mr", "mar"),
        ["mrj"] = new("Western Mari", "Мары йӹлмӹ", "mrj", "mrj", TranslationServices.Yandex),
        ["ms"] = new("Malay", "Melayu", "ms", "msa"),
        ["mt"] = new("Maltese", "Malti", "mt", "mlt"),
        ["mww"] = new("Hmong Daw", "Hmong Daw", "mww", "mww", TranslationServices.Bing | TranslationServices.Microsoft),
        ["my"] = new("Burmese", "မြန်မာ", "my", "mya"),
        ["ne"] = new("Nepali", "नेपाली", "ne", "nep"),
        ["nl"] = new("Dutch", "Nederlands", "nl", "nld"),
        ["no"] = new("Norwegian", "Norsk", "no", "nor"),
        ["nso"] = new("Sepedi", "Sesotho sa Leboa", "nso", "nso", TranslationServices.Google),
        ["ny"] = new("Chichewa", "Nyanja", "ny", "nya", TranslationServices.Google),
        ["om"] = new("Oromo", "Afaan Oromoo [ʌfɑːn ɔrɔmoː] Oromiff", "om", "orm", TranslationServices.Google),
        ["or"] = new("Odia", "ଓଡ଼ିଆ", "or", "ori", TranslationServices.Google | TranslationServices.Bing | TranslationServices.Microsoft),
        ["otq"] = new("Querétaro Otomi", "Hñähñu", "otq", "otq", TranslationServices.Bing | TranslationServices.Microsoft),
        ["pa"] = new("Punjabi", "ਪੰਜਾਬੀ", "pa", "pan"),
        ["pap"] = new("Papiamento", "Papiamento", "pap", "pap", TranslationServices.Yandex),
        ["pl"] = new("Polish", "Polski", "pl", "pol"),
        ["prs"] = new("Dari", "دری", "prs", "prs", TranslationServices.Bing | TranslationServices.Microsoft),
        ["ps"] = new("Pashto", "پښتو", "ps", "pus", TranslationServices.Google | TranslationServices.Bing | TranslationServices.Microsoft),
        ["pt"] = new("Portuguese", "Português", "pt", "por"),
        ["pt-BR"] = new("Portuguese (Brazil)", "Português (Brazil)", "pt-BR", "pt-BR", TranslationServices.Google),
        ["qu"] = new("Quechua", "Kechua", "qu", "que", TranslationServices.Google),
        ["ro"] = new("Romanian", "Română", "ro", "ron"),
        ["ru"] = new("Russian", "Русский", "ru", "rus"),
        ["rw"] = new("Kinyarwanda", "Kinyarwanda", "rw", "kin", TranslationServices.Google),
        ["sa"] = new("Sanskrit", "संस्कृत", "sa", "san", TranslationServices.Google),
        ["sah"] = new("Yakut", "Саха тыла", "sah", "sah", TranslationServices.Yandex),
        ["sd"] = new("Sindhi", "سنڌي", "sd", "snd", TranslationServices.Google),
        ["si"] = new("Sinhala", "සිංහල", "si", "sin", TranslationServices.Google | TranslationServices.Yandex),
        ["sjn"] = new("Sindarin", "Eledhrim", "sjn", "sjn", TranslationServices.Yandex), // Not present in Yandex.Cloud
        ["sk"] = new("Slovak", "Slovenčina", "sk", "slk"),
        ["sl"] = new("Slovenian", "Slovenščina", "sl", "slv"),
        ["sm"] = new("Samoan", "Gagana Sāmoa", "sm", "smo", TranslationServices.Google | TranslationServices.Bing | TranslationServices.Microsoft),
        ["sn"] = new("Shona", "chiShona", "sn", "sna", TranslationServices.Google),
        ["so"] = new("Somali", "Af Soomaali", "so", "som", TranslationServices.Google | TranslationServices.Bing | TranslationServices.Microsoft),
        ["sq"] = new("Albanian", "Shqip", "sq", "sqi"),
        ["sr"] = new("Serbian (Cyrillic)", "Српски", "sr", "srp"),
        ["sr-Latn"] = new("Serbian (Latin)", "Srpski (latinica)", "sr-Latn", "srp-Latn", TranslationServices.Bing | TranslationServices.Microsoft),
        ["st"] = new("Sotho", "Sotho", "st", "sot", TranslationServices.Google),
        ["su"] = new("Sundanese", "Basa Sunda", "su", "sun", TranslationServices.Google | TranslationServices.Yandex),
        ["sv"] = new("Swedish", "Svenska", "sv", "swe"),
        ["sw"] = new("Swahili", "Kiswahili", "sw", "swa"),
        ["ta"] = new("Tamil", "தமிழ்", "ta", "tam"),
        ["te"] = new("Telugu", "తెలుగు", "te", "tel"),
        ["tg"] = new("Tajik", "тоҷикӣ", "tg", "tgk", TranslationServices.Google | TranslationServices.Yandex),
        ["th"] = new("Thai", "ไทย", "th", "tha"),
        ["ti"] = new("Tigrinya", "ትግር", "ti", "tir", TranslationServices.Bing | TranslationServices.Microsoft),
        ["tk"] = new("Turkmen", "Türkmen Dili", "tk", "tuk", TranslationServices.Google | TranslationServices.Bing | TranslationServices.Microsoft),
        ["tl"] = new("Tagalog", "Filipino", "tl", "tgl", TranslationServices.Google | TranslationServices.Yandex),
        ["tlh"] = new("Klingon", "tlhIngan Hol", "tlh", "tlh", TranslationServices.Bing | TranslationServices.Microsoft),
        ["tlh-Piqd"] = new("Klingon (pIqaD)", "Klingon (pIqaD)", "tlh-Piqd", "tlh-Piqd", TranslationServices.Microsoft), // For some reason Bing stopped supporting this language, ty Microsoft
        ["to"] = new("Tongan", "Lea Fakatonga", "to", "ton", TranslationServices.Bing | TranslationServices.Microsoft),
        ["tr"] = new("Turkish", "Türkçe", "tr", "tur"),
        ["ts"] = new("Tsonga", "Xitsonga", "ts", "tso", TranslationServices.Google),
        ["tt"] = new("Tatar", "Татар", "tt", "tat"),
        ["tw"] = new("Twi", "Twi", "tw", "twi", TranslationServices.Google),
        ["ty"] = new("Tahitian", "Reo Tahiti", "ty", "tah", TranslationServices.Bing | TranslationServices.Microsoft),
        ["udm"] = new("Udmurt", "Удмурт кыл", "udm", "udm", TranslationServices.Yandex),
        ["ug"] = new("Uighur", "ئۇيغۇرچە", "ug", "uig", TranslationServices.Google | TranslationServices.Bing | TranslationServices.Microsoft),
        ["uk"] = new("Ukrainian", "Українська", "uk", "ukr"),
        ["ur"] = new("Urdu", "اردو", "ur", "urd"),
        ["uz"] = new("Uzbek", "Uzbek", "uz", "uzb"),
        ["uzbcyr"] = new("Uzbek (Cyrillic)", "Ўзбекча", "uzbcyr", "uzbcyr", TranslationServices.Yandex),
        ["vi"] = new("Vietnamese", "Tiếng Việt", "vi", "vie"),
        ["xh"] = new("Xhosa", "isiXhosa", "xh", "xho", TranslationServices.Google | TranslationServices.Yandex),
        ["yi"] = new("Yiddish", "ייִדיש", "yi", "yid", TranslationServices.Google | TranslationServices.Yandex),
        ["yo"] = new("Yoruba", "Èdè Yorùbá", "yo", "yor", TranslationServices.Google),
        ["yua"] = new("Yucatec Maya", "Yucatec Maya", "yua", "yua", TranslationServices.Bing | TranslationServices.Microsoft),
        ["yue"] = new("Cantonese", "粵語", "yue", "yue", TranslationServices.Bing | TranslationServices.Microsoft),
        ["zh-CN"] = new("Chinese (Simplified)", "中文 (简体)", "zh-CN", "zho-CN"),
        ["zh-TW"] = new("Chinese (Traditional)", "繁體中文 (繁體)", "zh-TW", "zho-TW", TranslationServices.Google | TranslationServices.Bing | TranslationServices.Microsoft),
        ["zu"] = new("Zulu", "Isi-Zulu", "zu", "zul"),

        // Templates for adding new languages
        //["xx"] = new("xxxx", "xxxx", "xx", "xxx"),
        //["xx"] = new("xxxx", "xxxx", "xx", "xxx",                                 TranslationServices.Google),
        //["xx"] = new("xxxx", "xxxx", "xx", "xxx",                                 TranslationServices.Bing | TranslationServices.Microsoft),
        //["xx"] = new("xxxx", "xxxx", "xx", "xxx",                                 TranslationServices.Yandex),
    });
}

/*      **** Keeping a list of failed language tags, so as to avoid repeating attempt to include them again. ****
        ["chr"] = new("Cherokee", "Cherokee", "chr", "chr", TranslationServices.Bing | TranslationServices.Microsoft),
        ["chr-CHER-US"] = new("Cherokee", "Cherokee", "chr-CHER-US", "chr-CHER-US",                                 TranslationServices.Google),
        ["doi-Deva"] = new("Dogri doi-Deva TestEntry", "Dogri", "doi-Deva", "doi-Deva",                                 TranslationServices.Google),
        ["doi-Deva-IN"] = new("Dogri India TestEntry", "doi-Deva-IN", "doi-Deva-IN", "doi-Deva-IN",                                 TranslationServices.Google),
        ["kk-KZ"] = new("Kazakh kk-KZ TestEntry", "kk-KZ", "kk-KZ", "kk-KZ",                                 TranslationServices.Google),
        ["kok"] = new("Konkani (India)", "Konkani (India)", "kok", "kok",           TranslationServices.Yandex),
        ["kok-IN"] = new("Konkani (India)", "Konkani (India)", "kok-IN", "kok-IN",          TranslationServices.Google),
        ["ku-Arab"] = new("ku-Arab TestEntry", "ku-Arab", "ku-Arab", "ku-Arab",                                 TranslationServices.Google),
        ["ku-Arab-IQ"] = new("ku-Arab-IQ TestEntry", "ku-Arab-IQ", "ku-Arab-IQ", "ku-Arab-IQ",                                 TranslationServices.Google),
        ["ku-Arab-IR"] = new("ku-Arab-IR TestEntry", "ku-Arab-IR", "ku-Arab-IR", "ku-Arab-IR",                                 TranslationServices.Google),
        ["nn"] = new("Norwegian Nynorsk", "Norwegian Nynorsk", "nn", "nno",         TranslationServices.Google),
        ["nn-NO"] = new("Norwegian Nynorsk", "Norwegian Nynorsk", "nn-NO", "nn-NO",         TranslationServices.Google),
        ["quz"] = new("Quechua quz TestEntry", "Quechua", "quz", "quz",                                 TranslationServices.Google),
        ["syr"] = new("Syriac", "leššānā Suryāyā", "syr", "syr",                                 TranslationServices.Google),
        ["uz-Arab"] = new("Uzbek uz-Arab TestEntry", "Uzbek", "uz-Arab", "uz-Arab",                                 TranslationServices.Google),
        ["uz-Cyrl"] = new("Uzbek uz-Cyrl TestEntry", "Uzbek", "uz-Cyrl", "uz-Cyrl",                                 TranslationServices.Google),
        ["uz-Cyrl-UZ"] = new("Uzbek uz-Cyrl-UZ TestEntry", "Uzbek", "uz-Cyrl-UZ", "uz-Cyrl-UZ",                                 TranslationServices.Google),
        ["uz-Latn"] = new("Uzbek uz-Latn TestEntry", "Uzbek", "uz-Latn", "uz-Latn",                                 TranslationServices.Google),
*/