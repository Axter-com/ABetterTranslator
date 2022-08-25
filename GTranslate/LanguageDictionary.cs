﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.ConstrainedExecution;

namespace GTranslate;

public class LanguageServiceDetails
{
    private LanguageDictionary languagedictionary = new();
    public IReadOnlyDictionary<string, Language> Languages { get { return languagedictionary.Languages; } } // Allow other classes to access the language pool

}

/// <summary>
/// Represents the default language dictionary used in GTranslate. It contains all the supported languages across all the included translators.
/// </summary>
public sealed class LanguageDictionary : ILanguageDictionary<string, Language>
{
    public IReadOnlyDictionary<string, Language> Languages { get { return _languages; } } // Allow other classes to access the language pool
    internal LanguageDictionary() => Aliases = new ReadOnlyDictionary<string, string>(BuildLanguageAliases());

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

        if ( TryGetValue(code, out Language? language) )
        {
            return language;
        }

        return Aliases.TryGetValue(code, out string? iso) ? _languages[iso] : throw new ArgumentException($"Unknown language \"{code}\".", nameof(code));
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
        Dictionary<string, string> aliases = new(StringComparer.InvariantCultureIgnoreCase)
        {
            ["chinese"] = "zh-CN",
        };

        foreach ( KeyValuePair<string, Language> kvp in _languages )
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
    // Some language names were incorrect.
    // See following link for correct name reference: https://en.wikipedia.org/wiki/List_of_ISO_639-1_codes
    private readonly IReadOnlyDictionary<string, Language> _languages = new ReadOnlyDictionary<string, Language>(new Dictionary<string, Language>(StringComparer.OrdinalIgnoreCase)
    {
        ["af"] = new Language("Afrikaans", "Afrikaans", "af", "afr"),
        ["sq"] = new("Albanian", "Shqip", "sq", "sqi"),
        ["am"] = new("Amharic", "አማርኛ", "am", "amh"),
        ["ar"] = new("Arabic", "العربية", "ar", "ara"),
        ["hy"] = new("Armenian", "Հայերեն", "hy", "hye"),
        ["az"] = new("Azerbaijani", "Azərbaycan", "az", "aze"),
        ["eu"] = new("Basque", "Euskara", "eu", "eus"),
        ["be"] = new("Belarusian", "беларуская", "be", "bel", TranslationServices.Google | TranslationServices.Yandex),
        ["bn"] = new("Bengali", "বাংলা", "bn", "ben"),
        ["bs"] = new("Bosnian", "bosanski", "bs", "bos"),
        ["bg"] = new("Bulgarian", "Български", "bg", "bul"),
        ["my"] = new("Burmese", "မြန်မာ", "my", "mya"),
        ["ca"] = new("Catalan", "Català", "ca", "cat"),
        ["ceb"] = new("Cebuano", "Binisaya", "ceb", "ceb", TranslationServices.Google | TranslationServices.Yandex),
        ["ny"] = new("Chichewa", "Nyanja", "ny", "nya", TranslationServices.Google),
        ["zh-CN"] = new("Chinese (Simplified)", "中文 (简体)", "zh-CN", "zho-CN"),
        ["zh-TW"] = new("Chinese (Traditional)", "繁體中文 (繁體)", "zh-TW", "zho-TW", TranslationServices.Google | TranslationServices.Bing | TranslationServices.Microsoft),
        ["co"] = new("Corsican", "Corsu", "co", "cos", TranslationServices.Google),
        ["hr"] = new("Croatian", "Hrvatski", "hr", "hrv"),
        ["cs"] = new("Czech", "Čeština", "cs", "ces"),
        ["da"] = new("Danish", "Dansk", "da", "dan"),
        ["nl"] = new("Dutch", "Nederlands", "nl", "nld"),
        ["en"] = new("English", "English", "en", "eng"),
        ["eo"] = new("Esperanto", "Esperanto", "eo", "epo", TranslationServices.Google | TranslationServices.Yandex),
        ["et"] = new("Estonian", "Eesti", "et", "est"),
        ["fi"] = new("Finnish", "Suomi", "fi", "fin"),
        ["fr"] = new("French", "Français", "fr", "fra"),
        ["fy"] = new("Frisian", "Frysk", "fy", "fry", TranslationServices.Google),
        ["gl"] = new("Galician", "Galego", "gl", "glg"),
        ["ka"] = new("Georgian", "ქართული", "ka", "kat"),
        ["de"] = new("German", "Deutsch", "de", "deu"),
        ["el"] = new("Greek", "Ελληνικά", "el", "ell"),
        ["gu"] = new("Gujarati", "ગુજરાતી", "gu", "guj"),
        ["ht"] = new("Haitian Creole", "Kreyòl ayisyen", "ht", "hat"),
        ["ha"] = new("Hausa", "Hausa", "ha", "hau", TranslationServices.Google),
        ["haw"] = new("Hawaiian", "ʻŌlelo Hawaiʻi", "haw", "haw", TranslationServices.Google),
        ["he"] = new("Hebrew", "עברית", "he", "heb"),
        ["hi"] = new("Hindi", "हिन्दी", "hi", "hin"),
        ["hmn"] = new("Hmong", "Hmong", "hmn", "hmn", TranslationServices.Google),
        ["hu"] = new("Hungarian", "Magyar", "hu", "hun"),
        ["is"] = new("Icelandic", "Íslenska", "is", "isl"),
        ["ig"] = new("Igbo", "Igbo", "ig", "ibo", TranslationServices.Google),
        ["id"] = new("Indonesian", "Indonesia", "id", "ind"),
        ["ga"] = new("Irish", "Gaeilge", "ga", "gle"),
        ["it"] = new("Italian", "Italiano", "it", "ita"),
        ["ja"] = new("Japanese", "日本語", "ja", "jpn"),
        ["jv"] = new("Javanese", "Jawa", "jv", "jav", TranslationServices.Google | TranslationServices.Yandex),
        ["kn"] = new("Kannada", "ಕನ್ನಡ", "kn", "kan"),
        ["kk"] = new("Kazakh", "Қазақ Тілі", "kk", "kaz"),
        ["km"] = new("Khmer", "ខ្មែរ", "km", "khm"),
        ["rw"] = new("Kinyarwanda", "Kinyarwanda", "rw", "kin", TranslationServices.Google),
        ["ko"] = new("Korean", "한국어", "ko", "kor"),
        ["ku"] = new("Kurdish", "Kurdî", "ku", "kur", TranslationServices.Google | TranslationServices.Bing | TranslationServices.Microsoft),
        ["ky"] = new("Kyrgyz", "Kyrgyz", "ky", "kir", TranslationServices.Google | TranslationServices.Yandex | TranslationServices.Microsoft),
        ["lo"] = new("Lao", "ລາວ", "lo", "lao"),
        ["la"] = new("Latin", "Latina", "la", "lat", TranslationServices.Google | TranslationServices.Yandex),
        ["lv"] = new("Latvian", "Latviešu", "lv", "lav"),
        ["lt"] = new("Lithuanian", "Lietuvių", "lt", "lit"),
        ["lb"] = new("Luxembourgish", "Lëtzebuergesch", "lb", "ltz", TranslationServices.Google | TranslationServices.Yandex),
        ["mk"] = new("Macedonian", "Македонски", "mk", "mkd"),
        ["mg"] = new("Malagasy", "Malagasy", "mg", "mlg"),
        ["ms"] = new("Malay", "Melayu", "ms", "msa"),
        ["ml"] = new("Malayalam", "മലയാളം", "ml", "mal"),
        ["mt"] = new("Maltese", "Malti", "mt", "mlt"),
        ["mi"] = new("Maori", "Te Reo Māori", "mi", "mri"),
        ["mr"] = new("Marathi", "मराठी", "mr", "mar"),
        ["mn"] = new("Mongolian", "Монгол хэл", "mn", "mon"),
        ["ne"] = new("Nepali", "नेपाली", "ne", "nep"),
        ["no"] = new("Norwegian", "Norsk", "no", "nor"),
        ["or"] = new("Odia", "ଓଡ଼ିଆ", "or", "ori", TranslationServices.Google | TranslationServices.Bing | TranslationServices.Microsoft),
        ["ps"] = new("Pashto", "پښتو", "ps", "pus", TranslationServices.Google | TranslationServices.Bing | TranslationServices.Microsoft),
        ["fa"] = new("Persian", "فارسی", "fa", "fas"),
        ["pl"] = new("Polish", "Polski", "pl", "pol"),
        ["pt"] = new("Portuguese", "Português", "pt", "por"),
        ["pa"] = new("Punjabi", "ਪੰਜਾਬੀ", "pa", "pan"),
        ["ro"] = new("Romanian", "Română", "ro", "ron"),
        ["ru"] = new("Russian", "Русский", "ru", "rus"),
        ["sm"] = new("Samoan", "Gagana Sāmoa", "sm", "smo", TranslationServices.Google | TranslationServices.Bing | TranslationServices.Microsoft),
        ["gd"] = new("Scottish Gaelic", "Gàidhlig", "gd", "gla", TranslationServices.Google | TranslationServices.Yandex),
        ["sr"] = new("Serbian (Cyrillic)", "Српски", "sr", "srp"),
        ["st"] = new("Sotho", "Sotho", "st", "sot", TranslationServices.Google),
        ["sn"] = new("Shona", "chiShona", "sn", "sna", TranslationServices.Google),
        ["sd"] = new("Sindhi", "سنڌي", "sd", "snd", TranslationServices.Google),
        ["si"] = new("Sinhala", "සිංහල", "si", "sin", TranslationServices.Google | TranslationServices.Yandex),
        ["sk"] = new("Slovak", "Slovenčina", "sk", "slk"),
        ["sl"] = new("Slovenian", "Slovenščina", "sl", "slv"),
        ["so"] = new("Somali", "Af Soomaali", "so", "som", TranslationServices.Google),
        ["es"] = new("Spanish", "Español", "es", "spa"),
        ["su"] = new("Sundanese", "Basa Sunda", "su", "sun", TranslationServices.Google | TranslationServices.Yandex),
        ["sw"] = new("Swahili", "Kiswahili", "sw", "swa"),
        ["sv"] = new("Swedish", "Svenska", "sv", "swe"),
        ["tl"] = new("Tagalog", "Filipino", "tl", "tgl", TranslationServices.Google | TranslationServices.Yandex),
        ["tg"] = new("Tajik", "тоҷикӣ", "tg", "tgk", TranslationServices.Google | TranslationServices.Yandex),
        ["ta"] = new("Tamil", "தமிழ்", "ta", "tam"),
        ["tt"] = new("Tatar", "Татар", "tt", "tat"),
        ["te"] = new("Telugu", "తెలుగు", "te", "tel"),
        ["th"] = new("Thai", "ไทย", "th", "tha"),
        ["tr"] = new("Turkish", "Türkçe", "tr", "tur"),
        ["tk"] = new("Turkmen", "Türkmen Dili", "tk", "tuk", TranslationServices.Google | TranslationServices.Bing | TranslationServices.Microsoft),
        ["uk"] = new("Ukrainian", "Українська", "uk", "ukr"),
        ["ur"] = new("Urdu", "اردو", "ur", "urd"),
        ["ug"] = new("Uighur", "ئۇيغۇرچە", "ug", "uig", TranslationServices.Google | TranslationServices.Bing | TranslationServices.Microsoft),
        ["uz"] = new("Uzbek", "Uzbek", "uz", "uzb"),
        ["vi"] = new("Vietnamese", "Tiếng Việt", "vi", "vie"),
        ["cy"] = new("Welsh", "Cymraeg", "cy", "cym"),
        ["xh"] = new("Xhosa", "isiXhosa", "xh", "xho", TranslationServices.Google | TranslationServices.Yandex),
        ["yi"] = new("Yiddish", "ייִדיש", "yi", "yid", TranslationServices.Google | TranslationServices.Yandex),
        ["yo"] = new("Yoruba", "Èdè Yorùbá", "yo", "yor", TranslationServices.Google),
        ["zu"] = new("Zulu", "Isi-Zulu", "zu", "zul"),

        ["as"] = new("Assamese", "অসমীয়া", "as", "asm", TranslationServices.Bing | TranslationServices.Microsoft),
        ["yue"] = new("Cantonese", "粵語", "yue", "yue", TranslationServices.Bing | TranslationServices.Microsoft),
        ["lzh"] = new("Chinese (Literary)", "中文 (文言文)", "lzh", "lzh", TranslationServices.Bing | TranslationServices.Microsoft),
        ["prs"] = new("Dari", "دری", "prs", "prs", TranslationServices.Bing | TranslationServices.Microsoft),
        ["dv"] = new("Divehi", "ދިވެހިބަސް", "dv", "div", TranslationServices.Bing | TranslationServices.Microsoft),
        ["fj"] = new("Fijian", "Na Vosa Vakaviti", "fj", "fij", TranslationServices.Bing | TranslationServices.Microsoft),
        ["fil"] = new("Filipino", "Tagalog", "fil", "fil", TranslationServices.Bing | TranslationServices.Microsoft),
        ["fr-CA"] = new("French (Canada)", "Français (Canada)", "fr-CA", "fr-CA", TranslationServices.Bing | TranslationServices.Microsoft),
        ["mww"] = new("Hmong Daw", "Hmong Daw", "mww", "mww", TranslationServices.Bing | TranslationServices.Microsoft),
        ["ikt"] = new("Inuinnaqtun", "Inuinnaqtun", "ikt", "ikt", TranslationServices.Bing | TranslationServices.Microsoft),
        ["iu"] = new("Inuktitut", "ᐃᓄᒃᑎᑐᑦ", "iu", "iku", TranslationServices.Bing | TranslationServices.Microsoft),
        ["iu-Latn"] = new("Inuktitut (Latin)", "Inuktitut (Latin)", "iu-Latn", "iu-Latn", TranslationServices.Bing | TranslationServices.Microsoft),
        ["mn-Mong"] = new("Mongolian (Traditional)", "ᠮᠣᠩᠭᠣᠯ ᠬᠡᠯᠡ", "mn-Mong", "mn-Mong", TranslationServices.Bing | TranslationServices.Microsoft),
        ["pt-PT"] = new("Portuguese (Portugal)", "Português (Portugal)", "pt-PT", "pt-PT", TranslationServices.Bing | TranslationServices.Microsoft),
        ["otq"] = new("Querétaro Otomi", "Hñähñu", "otq", "otq", TranslationServices.Bing | TranslationServices.Microsoft),
        ["sr-Latn"] = new("Serbian (Latin)", "Srpski (latinica)", "sr-Latn", "srp-Latn", TranslationServices.Bing | TranslationServices.Microsoft),
        ["ty"] = new("Tahitian", "Reo Tahiti", "ty", "tah", TranslationServices.Bing | TranslationServices.Microsoft),
        ["bo"] = new("Tibetan", "བོད་སྐད་", "bo", "bod", TranslationServices.Bing | TranslationServices.Microsoft),
        ["ti"] = new("Tigrinya", "ትግር", "ti", "tir", TranslationServices.Bing | TranslationServices.Microsoft),
        ["to"] = new("Tongan", "Lea Fakatonga", "to", "ton", TranslationServices.Bing | TranslationServices.Microsoft),
        ["tlh"] = new("Klingon", "tlhIngan Hol", "tlh", "tlh", TranslationServices.Bing | TranslationServices.Microsoft),
        ["kmr"] = new("Kurdish (Northern)", "Kurdî (Bakur)", "kmr", "kmr", TranslationServices.Bing | TranslationServices.Microsoft),
        ["hsb"] = new("Upper Sorbian", "Hornjoserbšćina", "hsb", "hsb", TranslationServices.Bing | TranslationServices.Microsoft),
        ["yua"] = new("Yucatec Maya", "Yucatec Maya", "yua", "yua", TranslationServices.Bing | TranslationServices.Microsoft),

        ["ba"] = new("Bashkir", "Bashkir", "ba", "bak", TranslationServices.Bing | TranslationServices.Microsoft | TranslationServices.Yandex),
        ["cv"] = new("Chuvash", "Чӑвашла", "cv", "chv", TranslationServices.Yandex),
        ["mhr"] = new("Eastern Mari", "олык марий", "mhr", "mhr", TranslationServices.Yandex),
        ["emj"] = new("Emoji", "Emoji", "emj", "emj", TranslationServices.Yandex), // Not present in Yandex.Cloud
        ["kazlat"] = new("Kazakh (Latin)", "qazaqşa", "kazlat", "kazlat", TranslationServices.Yandex),
        ["pap"] = new("Papiamento", "Papiamento", "pap", "pap", TranslationServices.Yandex),
        ["sjn"] = new("Sindarin", "Eledhrim", "sjn", "sjn", TranslationServices.Yandex), // Not present in Yandex.Cloud
        ["udm"] = new("Udmurt", "Удмурт кыл", "udm", "udm", TranslationServices.Yandex),
        ["uzbcyr"] = new("Uzbek (Cyrillic)", "Ўзбекча", "uzbcyr", "uzbcyr", TranslationServices.Yandex),
        ["mrj"] = new("Western Mari", "Мары йӹлмӹ", "mrj", "mrj", TranslationServices.Yandex),
        ["sah"] = new("Yakut", "Саха тыла", "sah", "sah", TranslationServices.Yandex),

        ["tlh-Piqd"] = new("Klingon (pIqaD)", "Klingon (pIqaD)", "tlh-Piqd", "tlh-Piqd", TranslationServices.Bing | TranslationServices.Microsoft), // For some reason Bing stopped supporting this language, ty Microsoft

        // Newly added by David Maisonave aka Axter (www.axter.com)
        ["pt-BR"] = new("Portuguese (Brazil)", "Portuguese (Brazil)", "pt", "por", TranslationServices.Google),
        ["fo"] = new("Faroese", "Faroese", "fo", "fao", TranslationServices.Bing | TranslationServices.Microsoft),
        ["en-GB"] = new("English (UK)", "English (UK)", "en-GB", "en-GB", TranslationServices.Google),
        ["es-MX"] = new("Spanish (Mexico)", "Spanish (Mexico)", "es", "es-MX", TranslationServices.Bing | TranslationServices.Microsoft),
        ["gd-GB"] = new("Gaelic", "Gaelic", "gd", "gla",                               TranslationServices.Google),
        ["gn"] = new("Guarani", "Guarani", "gn", "grn",                                 TranslationServices.Google),
        ["ay"] = new("Aymara", "Aymara", "ay", "aym",                                 TranslationServices.Google),
        ["bm"] = new("Bambara", "Bambara", "bm", "bam",                                 TranslationServices.Google),
        ["dv"] = new("Dhivehi", "Dhivehi", "dv", "div",                                 TranslationServices.Google),
        ["ee"] = new("Ewe", "Ewe", "ee", "ewe",                                 TranslationServices.Google),
        ["ln"] = new("Lingala", "Lingala", "ln", "lin",                                 TranslationServices.Google),
        ["om"] = new("Oromo", "Oromo", "om", "orm",                                 TranslationServices.Google),
        ["sa"] = new("Sanskrit", "Sanskrit", "sa", "san",                                 TranslationServices.Google),
        ["tl"] = new("Tsonga", "Tsonga", "tl", "tgl",                                 TranslationServices.Google),
        ["tw"] = new("Twi", "Twi", "tw", "twi",                                 TranslationServices.Google),
        ["bho"] = new("Bhojpuri", "Bhojpuri", "bho", "bho",                                 TranslationServices.Google),
        ["dgo"] = new("Dogri", "Dogri", "dgo", "dgo",                                 TranslationServices.Google),
        ["ilo"] = new("Ilocano", "Iloko", "ilo", "ilo",                                 TranslationServices.Google),
        ["mai"] = new("Maithili", "Maithili", "mai", "mai",                                 TranslationServices.Google),
        ["nso"] = new("Sepedi", "Sepedi", "nso", "nso",                                 TranslationServices.Google),
        ["qu"] = new("Quechua", "Quechua", "qu", "que",                          TranslationServices.Google),

        // ToDo: Delete or move the below entries after testing
        //["chr-CHER-US"] = new("Cherokee", "Cherokee", "chr-CHER-US", "chr-CHER-US",                                 TranslationServices.Google),
        //["chr"] = new("Cherokee", "Cherokee", "chr", "chr",                                 TranslationServices.Google),
        //["kok"] = new("Konkani (India)", "Konkani (India)", "kok", "kok",           TranslationServices.Yandex),
        //["kok-IN"] = new("Konkani (India)", "Konkani (India)", "kok-IN", "kok-IN",          TranslationServices.Google),
        //["nn-NO"] = new("Norwegian Nynorsk", "Norwegian Nynorsk", "nn-NO", "nn-NO",         TranslationServices.Google),
        //["nn"] = new("Norwegian Nynorsk", "Norwegian Nynorsk", "nn", "nno",         TranslationServices.Google),
        //["xx"] = new("xxxx", "xxxx", "xx", "xxx",                                 TranslationServices.Google),
    });
}