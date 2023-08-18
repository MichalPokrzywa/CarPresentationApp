using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

public class UpdateDescription : MonoBehaviour {
	TMP_Text description;
	public bool flag = false;
	LocalizeStringEvent localizeStringEvent;
	LocalizedString localizedString;
    // Start is called before the first frame update
    public void LoadText() {
	    localizeStringEvent = GetComponent<LocalizeStringEvent>();
	    localizedString = new LocalizedString(LocalizationSettings.StringDatabase.DefaultTable, "ConfigurationDescriptionLabel");
	    localizeStringEvent.StringReference = localizedString;
	    description = GetComponent<TMP_Text>();
		flag = true;
    }
    public void ChangeVersion(Version version)
    {
	    switch (version) {
		    case Version.Label:
			    AssignNewValue("ConfigurationDescriptionLabel");
			    description.text = LocalizationSettings.StringDatabase.GetLocalizedString(
				    localizedString.TableEntryReference, LocalizationSettings.Instance.GetSelectedLocale());

			    break;
			case Version.Sharp:
				AssignNewValue("ConfigurationDescriptionSharp");
				description.text = LocalizationSettings.StringDatabase.GetLocalizedString(
					localizedString.TableEntryReference, LocalizationSettings.Instance.GetSelectedLocale());
				break;
			case Version.Unity:
				AssignNewValue("ConfigurationDescriptionUnity");
				description.text = LocalizationSettings.StringDatabase.GetLocalizedString(
					localizedString.TableEntryReference, LocalizationSettings.Instance.GetSelectedLocale());
				break;
	    }
    }

    void AssignNewValue(string value) {
	    localizedString.TableEntryReference= value;
	    localizeStringEvent.StringReference = localizedString;
    }
}
