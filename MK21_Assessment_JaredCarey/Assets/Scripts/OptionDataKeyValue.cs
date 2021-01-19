﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TMPro
{
    /// <summary>
    /// An extended Class to store the text and/or image of a single option in the dropdown list. 
    /// This version of the Class includes a key to be associated with the text and/or image.
    /// </summary>
    public class OptionDataKeyValue
    {
        // TODO: Figure out why I cannot 
        //              A. Inherit properties and methods from the inner class TMP_Dropdown.OptionData into this class
        //              B. Update TMP_Dropdown.OptionData to have a new property and constructors 
        //
        // Because of the issues listed above I went with this solution to workaround my roadblock

        [SerializeField]
        private string m_Key;

        /// <summary>
        /// The key that is associated with the text and/or image of the option.
        /// </summary>
        public string key { get { return m_Key; } set { m_Key = value; } }

        /// <summary>
        /// The text associated with the option.
        /// </summary>
        public string text { get { return m_OptionData.text; } set { m_OptionData.text = value; } }

        /// <summary>
        /// The image associated with the option.
        /// </summary>
        public Sprite image { get { return m_OptionData.image; } set { m_OptionData.image = value; } }

        TMP_Dropdown.OptionData m_OptionData;

        public OptionDataKeyValue() { }

        public OptionDataKeyValue(string key, string text)
        {
            this.key = key;
            m_OptionData = new TMP_Dropdown.OptionData(text);
        }

        public OptionDataKeyValue(string key, Sprite image)
        {
            this.key = key;
            m_OptionData = new TMP_Dropdown.OptionData(image);
        }

        /// <summary>
        /// Create an object representing a single option for the dropdown list.
        /// </summary>
        /// <param name="key">Optional key for the option.</param>
        /// <param name="text">Optional text for the option.</param>
        /// <param name="image">Optional image for the option.</param>
        public OptionDataKeyValue(string key, string text, Sprite image)
        {
            this.key = key;
            m_OptionData = new TMP_Dropdown.OptionData(image);
        }
    }
}

