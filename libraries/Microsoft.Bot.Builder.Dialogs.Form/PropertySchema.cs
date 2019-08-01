﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Microsoft.Bot.Builder.Dialogs.Form
{
    /// <summary>
    /// Represents a property found in a JSON schema.
    /// </summary>
    public class PropertySchema
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertySchema"/> class.
        /// </summary>
        /// <param name="path">Path to this property.</param>
        /// <param name="schema">JSON schema fragment for this property.</param>
        /// <param name="children">Children properties if any.</param>
        public PropertySchema(string path, JObject schema, List<PropertySchema> children = null)
        {
            Path = path;
            Schema = schema;
            if (children != null)
            {
                Children = children;
                foreach (var child in Children)
                {
                    child.Parent = this;
                }
            }
            else
            {
                Children = new List<PropertySchema>();
            }
        }

        /// <summary>
        /// Gets path to schema with [] for arrays and otherwise . for path accessors.
        /// </summary>
        /// <value>
        /// Path to schema with [] for arrays and otherwise . for path accessors.
        /// </value>
        public string Path { get; }

        /// <summary>
        /// Gets JSON Schema object for this property.
        /// </summary>
        /// <value>
        /// JSON Schema object for this property.
        /// </value>
        public JObject Schema { get; }

        public PropertySchema Parent { get; private set; }

        public string Type => Schema["type"].Value<string>();

        public bool IsArray => Schema?.Parent?.Parent["type"]?.Value<string>() == "array";

        public JArray Mappings
        {
            get
            {
                var mappings = Schema["$mappings"]?.Value<JArray>();
                if (mappings == null)
                {
                    mappings = new JArray($"@@{Path}");
                }

                return mappings;
            }
        }

        /// <summary>
        /// Gets child properties if there are any.
        /// </summary>
        /// <value>
        /// Child properties if there are any.
        /// </value>
        public IReadOnlyList<PropertySchema> Children { get; }

        public override string ToString() => $"PropertySchema({Path})";
    }
}