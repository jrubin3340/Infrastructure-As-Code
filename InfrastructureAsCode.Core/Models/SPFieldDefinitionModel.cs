﻿using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace InfrastructureAsCode.Core.Models
{
    /// <summary>
    /// Field Definition concrete class
    /// </summary>
    public class SPFieldDefinitionModel
    {
        public SPFieldDefinitionModel()
        {
            this.FieldGuid = new System.Guid();
            this.RestrictedMode = false;
            this.GroupName = "CustomDevelopment";
            this.RichTextField = false;
            this.HiddenField = false;
            this.AddToDefaultView = false;
            this.NumLines = 0;
            this.ChoiceFormat = ChoiceFormatType.Dropdown;
            this.DateFieldFormat = DateTimeFieldFormatType.DateTime;
            this.FieldChoices = new List<SPChoiceModel>();
        }

        public SPFieldDefinitionModel(FieldType fType) : this()
        {
            this.fieldType = fType;
        }

        /// <summary>
        /// Unique Identifier
        /// </summary>
        public System.Guid FieldGuid { get; set; }

        /// <summary>
        /// The custom group name in which this field will display
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Internal name for the field definition
        /// </summary>
        public string InternalName { get; set; }

        /// <summary>
        /// Converts an SP field with property internal name structure
        /// </summary>
        public string DisplayNameMasked
        {
            get
            {
                if (!string.IsNullOrEmpty(DisplayName))
                {
                    return this.DisplayName.Replace(" ", "_x0020_");
                }
                return InternalName;
            }
        }

        public string DisplayName { get; set; }

        public FieldType fieldType { get; set; }

        public bool AddToDefaultView { get; set; }

        public bool HiddenField { get; set; }

        public string Description { get; set; }

        public int NumLines { get; set; }

        /// <summary>
        /// Optional Boolean. TRUE if the field displays rich text formatting.
        /// </summary>
        public bool RichTextField { get; set; }

        /// <summary>
        /// Optional Boolean. TRUE to not allow the Note field to contain enhanced rich text formatting, such as pictures, tables, or hyperlinks, nor to allow pasting formatted text into the field.
        /// </summary>
        public bool RestrictedMode { get; set; }

        public bool AppendOnly { get; set; }

        /// <summary>
        /// DateOnly   Display (and allow edits to) only the date portion (the time portion is set to 12:00 A.M. for all edited values).
        /// DateTime Display and edit both date and time of day(default).
        /// ISO8601 Display date and time in ISO8601 time format converted to Coordinated Universal Time(UTC) format: YYYY-MM-DDTHH:MM:SSZ.This is the format used for document properties in 2007 Microsoft Office system as well as for the standard interchange format used in SharePoint Foundation between New and Edit forms and the server.
        /// ISO8601Basic Use the abbreviated form of 8601 date/time formats: YYYYMMDDTHHMMSSZ.This is the format used for vCard/iCal.
        /// </summary>
        public DateTimeFieldFormatType? DateFieldFormat { get; set; }

        /// <summary>
        /// Optional Boolean. TRUE if the column is indexed for use in view filters.
        /// </summary>
        public bool FieldIndexed { get; set; }

        public List<SPChoiceModel> FieldChoices { get; set; }

        public string ChoiceDefault
        {
            get
            {
                if (FieldChoices.Count > 0)
                {
                    var sel = FieldChoices.FirstOrDefault(s => s.DefaultChoice.HasValue && s.DefaultChoice.Value);
                    if (sel != null)
                    {
                        return sel.Choice.Trim();
                    }
                }
                return string.Empty;
            }
        }

        public ChoiceFormatType ChoiceFormat { get; set; }

        public bool MultiChoice { get; set; }

        /// <summary>
        /// Optional Text. Specifies a scope for selecting user names in a user field on an item form. If the value is 0, there is no restriction to a SharePoint group. If the value is not null, user selection is restricted to members of the SharePoint group whose ID is queried based on the value that is specified.
        /// </summary>
        public string PeopleGroupName { get; set; }

        /// <summary>
        /// Optional Bool. Specifies whether only the names of individual users can be selected in a user field on an item form, or whether the names of both individuals and groups can be selected. The following values are possible:
        /// true - Only the names of individuals can be selected.
        /// false - The names of both individuals and groups can be selected.
        /// </summary>
        public bool PeopleOnly { get; set; }

        /// <summary>
        /// Optional Text. Specified the specific internal name of the field to be presented in views
        /// </summary>
        public string PeopleLookupField { get; set; }

        public int? MaxLength { get; set; }

        public bool Required { get; set; }

        /// <summary>
        /// The JS Link URL
        /// </summary>
        public string JSLink { get; set; }

        /// <summary>
        /// If configure serialize the json file for choices
        /// </summary>
        public string LoadFromJSON { get; set; }


        public string SchemaXml { get; set; }
        public string LookupListName { get; set; }
        public string LookupListFieldName { get; set; }

        /// <summary>
        /// Project the field defintion into the expected provisioning CSOM object
        /// </summary>
        /// <returns></returns>
        public FieldCreationInformation ToCreationObject()
        {
            var finfo = new FieldCreationInformation(this.fieldType);
            finfo.Id = this.FieldGuid;
            finfo.InternalName = this.InternalName;
            finfo.DisplayName = this.DisplayName;
            finfo.Group = this.GroupName;
            finfo.AddToDefaultView = this.AddToDefaultView;
            finfo.Required = this.Required;
            finfo.AdditionalAttributes = new List<KeyValuePair<string, string>>();

            return finfo;
        }
    }
}
