![](ABetterTranslator/Images/BannerWithNameAndSubLabel.png)

# Translates Resx Resource Files
[![All Releases](https://img.shields.io/github/downloads/David-Maisonave/ABetterTranslator/total.svg)](https://github.com/David-Maisonave/ABetterTranslator/releases/latest)
[![GitHub issues](https://img.shields.io/github/issues/David-Maisonave/ABetterTranslator.svg)](https://GitHub.com/David-Maisonave/ABetterTranslator/issues/)
[![Open Source? Yes!](https://badgen.net/badge/Open%20Source%20%3F/Yes%21/blue?icon=github)](https://github.com/Naereen/badges/)
[![MIT license](https://img.shields.io/badge/License-MIT-blue.svg)](https://lbesson.mit-license.org/)
[![csharp](https://img.shields.io/badge/--019733?logo=csharp)](https://www.vim.org/)
[![.NET](https://img.shields.io/badge/--512BD4?logo=.net&logoColor=ffffff)](https://dotnet.microsoft.com/)
[![Visual Studio](https://badgen.net/badge/icon/visualstudio?icon=visualstudio&label)](https://visualstudio.microsoft.com)
[![Windows](https://badgen.net/badge/icon/windows?icon=windows&label)](https://microsoft.com/windows/)


Translates Resx files to over 130 different languages in less then 60 seconds.

## What does it do?
The GUI allows the user to select multiple languages from a list of over 130 different languages.
A file (Resx) is created for each language, with the associated language tag in the file name.
The code is optimized so that over 130 languages can be tranlsated in less then 60 seconds. 
![](ABetterTranslator/Docs/screenshots/ABetterTranslatorScreenshot_(512_x_512).png)


# Content

[Features](README.md#Features)
-  [Filter](README.md#Filter)
-  [Sortable Language List](README.md#Sortable-Language-List)
-  [Max Threads](README.md#Max-Threads)
-  [Max Translation Len](README.md#Max-Translation-Len)
-  [Strings-Per-Translation-Req](README.md#Strings-Per-Translation-Req)

[Author](README.md#Author)

[License](README.md#License)


## Features

#### Filter

This option allows user to use keywords to find or filter the list to only items having the keyword(s).

#### Sortable Language List

The list view has multiple columns, and the list can be sorted by any column by clicking on the column header.

#### Max Threads

By default the programs uses the ProcessorCount to determine the maximum threads to use.  This options allows the end user to override that option.  The minimum value is 4, and the maximum value is 400.

#### Max Translation Len

The translation length is used when the program translates many strings in a single translation request.
This value is set to 10000 by default.  The minimum value is 255, and the maximum value is 10,000.

#### Strings-Per-Translation-Req

This options determines if one string is used per Resx translation request, or if many strings are used per Resx translation request. The following are the possible options to select from the combobox window.

-  [Auto]

		This is the default option. It automatically sets the best method depending on the totoal number of strings and the maximum thread settings.

-  [One]

		Only one string is sent per Resx translation request. Use this option if any of the strings contains the {next-line} character.

-  [Multiple]

		Multiple strings are sent per Resx translation request. This is the perferred option if total translation is less then 10,000 characters.


## Screenshots
### Main Tab
![](ABetterTranslator/Docs/screenshots/ABetterTranslatorScreenshot.png)

### Translate Text Tab 
#### This tab translates text on the fly as the user is typing.
![](ABetterTranslator/Docs/screenshots/screenshot_translate_text_tab.png)

### Advance Options Tab
![](ABetterTranslator/Docs/screenshots/screenshot_advance_options_tab.png)

### Group Language Selection
#### The user can select groups of languages based on region, popularity, and business usage.
![](ABetterTranslator/Docs/screenshots/screenshot_group_languages_selections.png)

#### The selections for top spoken languages, are based on 2022 statistics.

### Language Display Set
![](ABetterTranslator/Docs/screenshots/screenshot_LanguageDisplaySetSelection.png)

### Language Filter
#### The language list can displays well over 130 different languages. To help quickly find a language, the GUI has a filter field.
![](ABetterTranslator/Docs/screenshots/screenshot_language_filter_usage.png)

### Resizeable Window
#### The program window can be shrunk or expanded to user's preferred size.
![](ABetterTranslator/Docs/screenshots/screenshot_ABetterTranslator_long_window.png)


# Author

* **David Maisonave** - [David-Maisonave](https://github.com/David-Maisonave)


# License

-  This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
