/* ***************************************************************************
 * Copyright © 2022 David Maisonave	(https://axter.com)	All rights reserved.
 * ***************************************************************************
 * The is free software. You can redistribute it and/or modify it under the 
 * terms of the MIT License. For more information, please see License file 
 * distributed with this package.
 * ***************************************************************************
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTranslate.Translators;
namespace ABetterTranslator
{
    internal class LanguageCodes
    {
        // ToDo: Consider moving this logic to GTranslate\LanguageDictionary.cs
        // Missing languages Bhojpuri, Dogri, Ilocano, Krio, Luganda, Maithili, Meiteilon (Manipuri), Mizo, Sepedi, Sorani Kurdish
        // Could not find tags for Krio, Luganda, Meiteilon (Manipuri), Mizo, Sorani Kurdish
        // The language names in LanguageCodesAndAliases are based on the ISO-639-1 name. See source https://en.wikipedia.org/wiki/List_of_ISO_639-1_codes
        public static readonly string[,] LanguageCodesAndAliases = // ToDo: Make sure the list displays only 2 letter ISO-639-1 in the first row, unless language/region is not in ISO-639-1 
        {   //{[Tag AKA Language-Code], [Language/region], [Name and aliases] , [SupportedTranslator] }
             #region LanguageCodesAndAliases
            {"aa", "Afar", "Djibouti;Eritrea;Ethiopia"},
            {"ab", "Abkhazian", "Abkhaz"},
            {"ae", "Avestan", "Zend"},
            {"af", "Afrikaans", "South Africa"},
            {"ak", "Akan", ""},
            {"am", "Amharic", "Ethiopia"},
            {"an", "Aragonese", ""},
            {"ar", "Arabic", ""},
            {"as", "Assamese", "India"},
            {"av", "Avaric", "Avar"},
            {"ay", "Aymara", ""},
            {"az", "Azerbaijani", "Azeri"},
            {"ba", "Bashkir", ""},
            {"be", "Belarusian", ""},
            {"bg", "Bulgarian", "Bulgaria"},
            {"bi", "Bislama", ""},
            {"bm", "Bambara", ""},
            {"bn", "Bengali", "Bangla;India"},
            {"bo", "Tibetan", "Standard Tibetan"},
            {"br", "Breton", ""},
            {"bs", "Bosnian", "Bosnian (Latin)"},
            {"ca", "Catalan", "Valencian"},
            {"ce", "Chechen", ""},
            {"ch", "Chamorro", ""},
            {"chr", "Cherokee", ""},
            {"co", "Corsican", ""},
            {"cr", "Cree", ""},
            {"cs", "Czech", "Czech Republic"},
            {"cu", "Church Slavic", "Old Slavonic;Church Slavonic;Old Bulgarian;Old Church Slavonic;Eastern Orthodox Church"},
            {"cv", "Chuvash", ""},
            {"cy", "Welsh", "Great Britain"},
            {"da", "Danish", "Denmark"},
            {"de", "German", "Germany"},
            {"dv", "Divehi", "Dhivehi;Maldivian"},
            {"dz", "Dzongkha", ""},
            {"ee", "Ewe", ""},
            {"el", "Greek", "Greece;Modern (1453–)"},
            {"en", "English", "US;Canada;Australia;Britain;United States"},
            {"en-GB", "English (UK)", "UK;Great Britain;British;"},
            {"eo", "Esperanto", ""},
            {"es", "Spanish", "Castilian;Spain;Latin"},
            {"es-MX",  "Spanish (Mexico)", "Mexican"},
            {"et", "Estonian", "Estonia"},
            {"eu", "Basque", ""},
            {"fa", "Persian", "Farsi"},
            {"ff", "Fulah", "Fulani;Fula;Fula-Wolof"},
            {"fi", "Finnish", "Finland"},
            {"fj", "Fijian", ""},
            {"fo", "Faroese", ""},
            {"fr", "French", "France"},
            {"fy", "Western Frisian", "Frisian"},
            {"ga", "Irish", "Ireland"},
            {"gd", "Gaelic", "Scottish Gaelic"},
            {"gl", "Galician", ""},
            {"gn", "Guarani", ""},
            {"gu", "Gujarati", "India"},
            {"gv", "Manx", ""},
            {"ha", "Hausa", "Nigeria"},
            {"he", "Hebrew", "Israel;Modern Hebrew"},
            {"hi", "Hindi", "India"},
            {"hmn", "Hmong", "China, Laos, Thailand, Vietnam"},
            {"ho", "Hiri Motu", ""},
            {"hr", "Croatian", "Croatia"},
            {"ht", "Haitian", "Haitian Creole"},
            {"hu", "Hungarian", "Hungary"},
            {"hy", "Armenian", "Armenia"},
            {"hz", "Herero", ""},
            {"ia", "Interlingua", "International Auxiliary Language Association"},
            {"id", "Indonesian", "Indonesia"},
            {"ie", "Interlingue", "Occidental"},
            {"ig", "Igbo", "Nigeria"},
            {"ii", "Sichuan Yi", "Nuosu"},
            {"ik", "Inupiaq", ""},
            {"ikt", "Inuinnaqtun", ""},
            {"io", "Ido", ""},
            {"is", "Icelandic", "Iceland"},
            {"it", "Italian", "Italy"},
            {"iu", "Inuktitut", ""},
            {"ja", "Japanese", "Japan"},
            {"jv", "Javanese", ""},
            {"ka", "Georgian", "Georgia"},
            {"kg", "Kongo", ""},
            {"ki", "Kikuyu", "Gikuyu"},
            {"kj", "Kuanyama", "Kwanyama"},
            {"kk", "Kazakh", "Kazakhstan"},
            {"kl", "Kalaallisut", "Greenlandic"},
            {"km", "Khmer", "Central Khmer;Cambodia"},
            {"kn", "Kannada", "India"},
            {"ko", "Korean", "Korea"},
            {"kr", "Kanuri", ""},
            {"ks", "Kashmiri", ""},
            {"ku", "Kurdish", ""},
            {"kv", "Komi", ""},
            {"kw", "Cornish", ""},
            {"ky", "Kirghiz", "Kyrgyz"},
            {"la", "Latin", ""},
            {"lb", "Luxembourgish", "Letzeburgesch;Luxembourg"},
            {"lg", "Ganda", ""},
            {"li", "Limburgan", "Limburger;Limburgish"},
            {"ln", "Lingala", ""},
            {"lo", "Lao", "Laos"},
            {"lt", "Lithuanian", "Lithuania"},
            {"lu", "Luba-Katanga", "Luba-Shaba"},
            {"lv", "Latvian", "Latvia"},
            {"mg", "Malagasy", "Madagascar"},
            {"mh", "Marshallese", ""},
            {"mi", "Maori", "New Zealand;Māori"},
            {"mk", "Macedonian", "FYROM"},
            {"ml", "Malayalam", "India"},
            {"mn", "Mongolian", ""},
            {"mr", "Marathi", "Marāṭhī;India"},
            {"ms", "Malay", "Malaysia;Brunei;Singapore"},
            {"mt", "Maltese", "Malta"},
            {"my", "Burmese", "Myanmar"},
            {"na", "Nauru", "Nauruan"},
            {"nb", "Norwegian Bokmål", "Norway"},
            {"nd", "North Ndebele", "Northern Ndebele;Ndebele"},
            {"ne", "Nepali", "Nepal;Federal Democratic Republic of Nepal"},
            {"ng", "Ndonga", ""},
            {"nl", "Dutch", "Flemish;Netherlands;Belgium"},
            {"nn", "Norwegian Nynorsk", "Norway;Nynorsk"},
            {"no", "Norwegian", "Norway"},
            {"nr", "South Ndebele", "Southern Ndebele;Ndebele"},
            {"nv", "Navajo", "Navaho"},
            {"ny", "Chichewa", "Chewa;Nyanja"},
            {"oc", "Occitan", ""},
            {"oj", "Ojibwa", "Ojibwe"},
            {"om", "Oromo", ""},
            {"or", "Oriya", "Odia;India"},
            {"os", "Ossetian", "Ossetic"},
            {"pa", "Punjabi", "Panjabi;India"},
            {"pi", "Pali", "Pāli"},
            {"pl", "Polish", "Poland"},
            {"ps", "Pashto", "Pushto"},
            {"pt", "Portuguese", "Portugal;Brazil"},
            {"pt-BR", "Portuguese (Brazil)", "Brazil"},
            {"qu", "Quechua", "Peru"},
            {"rm", "Romansh", ""},
            {"rn", "Rundi", "Kirundi"},
            {"ro", "Romanian", "Romania;Moldavian;Moldovan"},
            {"ru", "Russian", "Russia"},
            {"rw", "Kinyarwanda", ""},
            {"sa", "Sanskrit", ""},
            {"sc", "Sardinian", ""},
            {"sd", "Sindhi", ""},
            {"se", "Northern Sami", ""},
            {"sg", "Sango", ""},
            {"si", "Sinhala", "Sinhalese"},
            {"sk", "Slovak", "Slovakia"},
            {"sl", "Slovenian", "Slovene;Slovenia"},
            {"sm", "Samoan", ""},
            {"sn", "Shona", ""},
            {"so", "Somali", ""},
            {"sq", "Albanian", "Albania"},
            {"sr", "Serbian", "Serbia"},
            {"sr-Cyrl", "Serbian (Cyrillic)", "Serbian (Cyrillic, Bosnia and Herzegovina)"},
            {"sr-Latn", "Serbian (Latin)", "Serbian (Cyrillic, Serbia)"},
            {"ss", "Swati", "Swazi"},
            {"st", "Southern Sotho", "Sesotho"},
            {"su", "Sundanese", ""},
            {"sv", "Swedish", "Sweden"},
            {"sw", "Swahili", "Kiswahili"},
            {"ta", "Tamil", "India"},
            {"te", "Telugu", "India"},
            {"tg", "Tajik", ""},
            {"th", "Thai", "Thailand"},
            {"ti", "Tigrinya", ""},
            {"tk", "Turkmen", ""},
            {"tl", "Tagalog", "Filipino;Philippines"},
            {"tn", "Tswana", ""},
            {"to", "Tonga", "Tonga (Islands);Tongan"},
            {"tr", "Turkish", "Turkey"},
            {"ts", "Tsonga", "Xitsonga"},
            {"tt", "Tatar", "Russia"},
            {"tw", "Twi", ""},
            {"ty", "Tahitian", ""},
            {"ug", "Uighur", "Uyghur"},
            {"uk", "Ukrainian", "Ukraine"},
            {"ur", "Urdu", ""},
            {"uz", "Uzbek", "Uzbek (Latin)"},
            {"ve", "Venda", ""},
            {"vi", "Vietnamese", "Vietnam"},
            {"vo", "Volapük", ""},
            {"wa", "Walloon", ""},
            {"wo", "Wolof", ""},
            {"xh", "Xhosa", ""},
            {"yi", "Yiddish", ""},
            {"yo", "Yoruba", ""},
            {"yua", "Yucatec Maya", ""},
            {"yue", "Cantonese (Traditional)", ""},
            {"za", "Zhuang", "Chuang"},
            {"zh",     "Chinese", "Chinese (PRC);Chinese (Simplified);Mandarin;China"},
            {"zh-CHT", "Chinese (Traditional)", "Mandarin;China"},
            {"zh-CN",  "Chinese (Simplified)", "Chinese;Chinese (PRC);Mandarin;China"},
            {"zh-TW",  "Chinese (Taiwan)", "Mandarin;Taiwan"},
            {"zu", "Zulu", ""},
            #endregion LanguageCodesAndAliases
        };
        //	Windows10Plus_LanguagePacks source: https://docs.microsoft.com/en-us/windows-hardware/manufacture/desktop/available-language-packs-for-windows?view=windows-11 
        public static readonly string[,] Windows10Plus_LanguagePacks = /*Windows 10/11 Language Packs*/
        {	//{[Language-Code], [Language/region] }
			#region Windows10Plus_LanguagePacks
			{"ar-SA", "Arabic"},                   // Arabic(Saudi Arabia)  :  
            {"bg-BG", "Bulgarian"},                    // Bulgarian(Bulgaria)  :  
            {"ca-ES", "Catalan"},                                //   :  
            {"cs-CZ", "Czech"},                  //  Czech(Czech Republic) :  
            {"da-DK", "Danish"},                        // Danish(Denmark)  :  
            {"de-DE", "German"},                        // German(Germany)  :  
            {"el-GR", "Greek"},                          // Greek(Greece)  :  
            {"en-GB", "English (UK)"},                // English(United Kingdom)  :  
            {"en-US", "English"},                 // English(United States)  :  
            {"es-ES", "Spanish"},                         // Spanish(Spain)  :  
            {"es-MX", "Spanish (Mexico)"},                        // Spanish(Mexico)  :  
            {"et-EE", "Estonian"},                      // Estonian(Estonia)  :  
            {"eu-ES", "Basque"},                         // Basque(Basque)  :  
            {"fi-FI", "Finnish"},                       // Finnish(Finland)  :  
            {"fr-CA", "French (Canada)"},                         // French(Canada)  :  
            {"fr-FR", "French"},                         // French(France)  :  
            {"gl-ES", "Galician"},                               // Galician  :  
            {"he-IL", "Hebrew"},                         // Hebrew(Israel)  :  
            {"hr-HR", "Croatian"},                      // Croatian(Croatia)  :  
            {"hu-HU", "Hungarian"},                     // Hungarian(Hungary)  :  
            {"id-ID", "Indonesian"},                  // Indonesian(Indonesia)  :  
            {"it-IT", "Italian"},                         // Italian(Italy)  :  
            {"ja-JP", "Japanese"},                        // Japanese(Japan)  :  
            {"ko-KR", "Korean"},                          // Korean(Korea)  :  
            {"lt-LT", "Lithuanian"},                  // Lithuanian(Lithuania)  :  
            {"lv-LV", "Latvian"},                        // Latvian(Latvia)  :  
            {"nb-NO", "Norwegian"},                      // Norwegian(Norway)  :  
            {"nl-NL", "Dutch"},                     // Dutch(Netherlands)  :  
            {"pl-PL", "Polish"},                         // Polish(Poland)  :  
            {"pt-BR", "Portuguese (Brazil)"},                     // Portuguese(Brazil)  :  
            {"pt-PT", "Portuguese"},                   // Portuguese(Portugal)  :  
            {"ro-RO", "Romanian"},                      // Romanian(Romania)  :  
            {"ru-RU", "Russian"},                        // Russian(Russia)  :  
            {"sk-SK", "Slovak"},                       // Slovak(Slovakia)  :  
            {"sl-SI", "Slovenian"},                    // Slovenian(Slovenia)  :  
            {"sr-Latn-RS", "Serbian (Latin)"},            // Serbian(Latin, Serbia)  :  
            {"sv-SE", "Swedish"},                        // Swedish(Sweden)  :  
            {"th-TH", "Thai"},                         // Thai(Thailand)  :  
            {"tr-TR", "Turkish"},                        // Turkish(Turkey)  :  
            {"uk-UA", "Ukrainian"},                     // Ukrainian(Ukraine)  :  
            {"vi-VN", "Vietnamese"},                             // Vietnamese  :
            {"zh-CN",  "Chinese (Simplified)"},         //
			#endregion Windows10Plus_LanguagePacks
		};
        //	Windows10Plus_LanguageInterfacePacks source: https://docs.microsoft.com/en-us/windows-hardware/manufacture/desktop/available-language-packs-for-windows?view=windows-11 
        public static readonly string[,] Windows10Plus_LanguageInterfacePacks =  /*Windows 10/11 LIPs*/
		{	//{[Language-Code], [Language/region], [Primary Base language/region]}
			#region Windows10Plus_LanguageInterfacePacks
            {"vi-VN", "Vietnamese"},
            {"uz-Latn-UZ", "Uzbek"},
            {"ur-PK", "Urdu"},
            {"ug-CN", "Uighur"},
            {"tt-RU", "Tatar"},
            {"te-IN", "Telugu"},
            {"ta-IN", "Tamil"},
            {"sr-Cyrl-RS", "Serbian (Latin)"}, // aka sr-Latn-RS
            {"sr-Cyrl-BA", "Serbian (Cyrillic)"},
			{"sq-AL", "Albanian"},
            {"quz-PE", "Quechua"}, // -- No available translator supports this language 
            {"pa-IN", "Punjabi"},
            {"or-IN", "Odia"},
            {"nn-NO", "Norwegian Nynorsk"},
            {"ne-NP", "Nepali"},
            {"mt-MT", "Maltese"},
            {"ms-MY", "Malay"},
            {"mr-IN", "Marathi"},
            {"ml-IN", "Malayalam"},
            {"mk-MK", "Macedonian"},
            {"mi-NZ", "Maori"},
            {"lo-LA", "Lao"},
            {"lb-LU", "Luxembourgish"},
            {"kok-IN", "Konkani (India)"}, // -- No available translator supports this language
            {"kn-IN", "Kannada"},
            {"km-KH", "Khmer"},
            {"kk-KZ", "Kazakh"},
            {"ka-GE", "Georgian"},
            {"is-IS", "Icelandic"},
            {"id-ID", "Indonesian"},
            {"hy-AM", "Armenian"},
            {"hi-IN", "Hindi"},
            {"gu-IN", "Gujarati"},
            {"gl-ES", "Galician"},
            {"gd-GB", "Gaelic"},
            {"ga-IE", "Irish"},
            {"fil", "Filipino"},
            {"fa-IR", "Persian"},
            {"eu-ES", "Basque"},
            {"cy-GB", "Welsh"},
            {"chr-CHER-US", "Cherokee"}, // Suppose to be supported by Google Translate via code chr, but it's not working.
            {"ca-ES-valencia", "Catalan"},
            {"ca-ES", "Catalan"},
            {"bs-Latn-BA", "Bosnian"},
            {"bn-IN", "Bengali"},
            {"be-BY", "Belarusian"},
            {"az-Latn-AZ", "Azerbaijani"},
            {"as-IN", "Assamese"},
            {"am-ET", "Amharic"},
			{"af-ZA", "Afrikaans"},
			#endregion Windows10Plus_LanguageInterfacePacks
		};
        // ISO639_1_To_ISO639_3 only contains languages that are in ISO-639-1. See source https://iso639-3.sil.org/sites/iso639-3/files/downloads/iso-639-3.tab
        public static readonly string[,] ISO639_1_To_ISO639_3 =
        {
            #region ISO639_1_To_ISO639_3
            {"aa", "aar"}, // Afar
            {"ab", "abk"}, // Abkhazian
            {"ae", "ave"}, // Avestan
            {"af", "afr"}, // Afrikaans
            {"ak", "aka"}, // Akan
            {"am", "amh"}, // Amharic
            {"an", "arg"}, // Aragonese
            {"ar", "ara"}, // Arabic
            {"as", "asm"}, // Assamese
            {"av", "ava"}, // Avaric
            {"ay", "aym"}, // Aymara
            {"az", "aze"}, // Azerbaijani
            {"ba", "bak"}, // Bashkir
            {"be", "bel"}, // Belarusian
            {"bg", "bul"}, // Bulgarian
            {"bi", "bis"}, // Bislama
            {"bm", "bam"}, // Bambara
            {"bn", "ben"}, // Bengali
            {"bo", "bod"}, // Tibetan
            {"br", "bre"}, // Breton
            {"bs", "bos"}, // Bosnian
            {"ca", "cat"}, // Catalan
            {"ce", "che"}, // Chechen
            {"ch", "cha"}, // Chamorro
            {"co", "cos"}, // Corsican
            {"cr", "cre"}, // Cree
            {"cs", "ces"}, // Czech
            {"cu", "chu"}, // Church Slavic
            {"cv", "chv"}, // Chuvash
            {"cy", "cym"}, // Welsh
            {"da", "dan"}, // Danish
            {"de", "deu"}, // German
            {"dv", "div"}, // Dhivehi
            {"dz", "dzo"}, // Dzongkha
            {"ee", "ewe"}, // Ewe
            {"el", "ell"}, // Modern Greek (1453-)
            {"en", "eng"}, // English
            {"eo", "epo"}, // Esperanto
            {"es", "spa"}, // Spanish
            {"et", "est"}, // Estonian
            {"eu", "eus"}, // Basque
            {"fa", "fas"}, // Persian
            {"ff", "ful"}, // Fulah
            {"fi", "fin"}, // Finnish
            {"fj", "fij"}, // Fijian
            {"fo", "fao"}, // Faroese
            {"fr", "fra"}, // French
            {"fy", "fry"}, // Western Frisian
            {"ga", "gle"}, // Irish
            {"gd", "gla"}, // Scottish Gaelic
            {"gl", "glg"}, // Galician
            {"gn", "grn"}, // Guarani
            {"gu", "guj"}, // Gujarati
            {"gv", "glv"}, // Manx
            {"ha", "hau"}, // Hausa
            {"he", "heb"}, // Hebrew
            {"hi", "hin"}, // Hindi
            {"ho", "hmo"}, // Hiri Motu
            {"hr", "hrv"}, // Croatian
            {"ht", "hat"}, // Haitian
            {"hu", "hun"}, // Hungarian
            {"hy", "hye"}, // Armenian
            {"hz", "her"}, // Herero
            {"ia", "ina"}, // Interlingua (International Auxiliary Language Association)
            {"id", "ind"}, // Indonesian
            {"ie", "ile"}, // Interlingue
            {"ig", "ibo"}, // Igbo
            {"ii", "iii"}, // Sichuan Yi
            {"ik", "ipk"}, // Inupiaq
            {"io", "ido"}, // Ido
            {"is", "isl"}, // Icelandic
            {"it", "ita"}, // Italian
            {"iu", "iku"}, // Inuktitut
            {"ja", "jpn"}, // Japanese
            {"jv", "jav"}, // Javanese
            {"ka", "kat"}, // Georgian
            {"kg", "kon"}, // Kongo
            {"ki", "kik"}, // Kikuyu
            {"kj", "kua"}, // Kuanyama
            {"kk", "kaz"}, // Kazakh
            {"kl", "kal"}, // Kalaallisut
            {"km", "khm"}, // Khmer
            {"kn", "kan"}, // Kannada
            {"ko", "kor"}, // Korean
            {"kok", "kok-IN"}, // Konkani (India) -- No available translator supports this language
            {"kr", "kau"}, // Kanuri
            {"ks", "kas"}, // Kashmiri
            {"ku", "kur"}, // Kurdish
            {"kv", "kom"}, // Komi
            {"kw", "cor"}, // Cornish
            {"ky", "kir"}, // Kirghiz
            {"la", "lat"}, // Latin
            {"lb", "ltz"}, // Luxembourgish
            {"lg", "lug"}, // Ganda
            {"li", "lim"}, // Limburgan
            {"ln", "lin"}, // Lingala
            {"lo", "lao"}, // Lao
            {"lt", "lit"}, // Lithuanian
            {"lu", "lub"}, // Luba-Katanga
            {"lv", "lav"}, // Latvian
            {"mg", "mlg"}, // Malagasy
            {"mh", "mah"}, // Marshallese
            {"mi", "mri"}, // Maori
            {"mk", "mkd"}, // Macedonian
            {"ml", "mal"}, // Malayalam
            {"mn", "mon"}, // Mongolian
            {"mr", "mar"}, // Marathi
            {"ms", "msa"}, // Malay (macrolanguage)
            {"mt", "mlt"}, // Maltese
            {"my", "mya"}, // Burmese
            {"na", "nau"}, // Nauru
            {"nb", "nob"}, // Norwegian Bokmål
            {"nd", "nde"}, // North Ndebele
            {"ne", "nep"}, // Nepali (macrolanguage)
            {"ng", "ndo"}, // Ndonga
            {"nl", "nld"}, // Dutch
            {"nn", "nno"}, // Norwegian Nynorsk
            {"no", "nor"}, // Norwegian
            {"nr", "nbl"}, // South Ndebele
            {"nv", "nav"}, // Navajo
            {"ny", "nya"}, // Nyanja
            {"oc", "oci"}, // Occitan (post 1500)
            {"oj", "oji"}, // Ojibwa
            {"om", "orm"}, // Oromo
            {"or", "ori"}, // Oriya (macrolanguage)
            {"os", "oss"}, // Ossetian
            {"pa", "pan"}, // Panjabi
            {"Part1", "Id"}, // Ref_Name
            {"pi", "pli"}, // Pali
            {"pl", "pol"}, // Polish
            {"ps", "pus"}, // Pushto
            {"pt", "por"}, // Portuguese
            {"qu", "que"}, // Quechua
            {"rm", "roh"}, // Romansh
            {"rn", "run"}, // Rundi
            {"ro", "ron"}, // Romanian
            {"ru", "rus"}, // Russian
            {"rw", "kin"}, // Kinyarwanda
            {"sa", "san"}, // Sanskrit
            {"sc", "srd"}, // Sardinian
            {"sd", "snd"}, // Sindhi
            {"se", "sme"}, // Northern Sami
            {"sg", "sag"}, // Sango
            {"sh", "hbs"}, // Serbo-Croatian
            {"si", "sin"}, // Sinhala
            {"sk", "slk"}, // Slovak
            {"sl", "slv"}, // Slovenian
            {"sm", "smo"}, // Samoan
            {"sn", "sna"}, // Shona
            {"so", "som"}, // Somali
            {"sq", "sqi"}, // Albanian
            {"sr", "srp"}, // Serbian
            {"ss", "ssw"}, // Swati
            {"st", "sot"}, // Southern Sotho
            {"su", "sun"}, // Sundanese
            {"sv", "swe"}, // Swedish
            {"sw", "swa"}, // Swahili (macrolanguage)
            {"ta", "tam"}, // Tamil
            {"te", "tel"}, // Telugu
            {"tg", "tgk"}, // Tajik
            {"th", "tha"}, // Thai
            {"ti", "tir"}, // Tigrinya
            {"tk", "tuk"}, // Turkmen
            {"tl", "tgl"}, // Tagalog
            {"tn", "tsn"}, // Tswana
            {"to", "ton"}, // Tonga (Tonga Islands)
            {"tr", "tur"}, // Turkish
            {"ts", "tso"}, // Tsonga
            {"tt", "tat"}, // Tatar
            {"tw", "twi"}, // Twi
            {"ty", "tah"}, // Tahitian
            {"ug", "uig"}, // Uighur
            {"uk", "ukr"}, // Ukrainian
            {"ur", "urd"}, // Urdu
            {"uz", "uzb"}, // Uzbek
            {"ve", "ven"}, // Venda
            {"vi", "vie"}, // Vietnamese
            {"vo", "vol"}, // Volapük
            {"wa", "wln"}, // Walloon
            {"wo", "wol"}, // Wolof
            {"xh", "xho"}, // Xhosa
            {"yi", "yid"}, // Yiddish
            {"yo", "yor"}, // Yoruba
            {"za", "zha"}, // Zhuang
            {"zh", "zho"}, // Chinese
            {"zu", "zul"}, // Zulu
            #endregion ISO639_1_To_ISO639_3
        };
        // Use following list to temporarily disable languages causing issues.
        public static readonly string[] LanguagesTagsWithIssues = {
            "foofoo",
            //"ay", "bho", "cv", "dgo", "emj", "fj", "hmn", "ht", "ikt", "ilo", "kazlat", "kmr", "lzh", "mhr", "mrj", "mww", "ny", "otq", "qu", "sjn", "sm", "tlh", "tw", "ty", "udm", "uzbcyr", "yua", "yue"
        };

        // ToDo: Delete the following variable after troubleshooting issues.
        public static readonly string[] LanguageTagsNotRecongnizedByCompiler = {
           "ay", "bho", "cv", "dgo", "emj", "fj", "hmn", "ht", "ikt", "ilo", "kazlat", "kmr", "lzh", "mhr", "mrj", "mww", "ny", "otq", "qu", "sjn", "sm", "tlh", "tw", "ty", "udm", "uzbcyr", "yua", "yue"
        };
    }
}
