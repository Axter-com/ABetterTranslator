This version of GTranslate has changes which the original GTranslate author did not approve into the main GTranslate branch.

Some of the changes includes the following:
* Additional languages.
* Removal much of the GTranslate alias language code.
* Extra try-catch code.

There are more changes to come which will not go into the main branch.
This includes the following:

* Moving the language details out of the source code and into either an XML file or a SQLite DB.
  * That will make it so the source code doesn't have to be changed every time there are additional languages.
* Have the language details only include the key and language name.
  * Remove native-name because it's not needed to perform the translation.
  * Remove iso6391 because it's misleading since many languages don't have an ISO639-1 2 letter tag.
  * Remove iso6393 because it's misleading since some languages don't have an ISO639-3 3 letter tag.
* Removal much of the GTranslate alias language code.
  * Keep only tag alias that are exact match.  Example: ["sr-Cyrl"] = "sr"
* Move the remaining alias code to XML file. 
  * That will allow users to include or exclude what they want from the alias list.
* Use SQLite DB to cache translations. 
  * The DB will store strings that were previously translated, so-as to avoid calling translation service for the same string and target language.
* Add logic to handle translations which exceed the character limit for the translation service, by dividing the translation request into multiple request which are below the limit.
  * That code is currently in ABetterTranslator source code.
