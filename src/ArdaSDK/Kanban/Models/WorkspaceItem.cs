// Code generated by Microsoft (R) AutoRest Code Generator 1.0.1.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace ArdaSDK.Kanban.Models
{
    using ArdaSDK.Kanban;
    using Newtonsoft.Json;
    using System.Linq;

    public partial class WorkspaceItem
    {
        /// <summary>
        /// Initializes a new instance of the WorkspaceItem class.
        /// </summary>
        public WorkspaceItem()
        {
          CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the WorkspaceItem class.
        /// </summary>
        public WorkspaceItem(System.Guid? id = default(System.Guid?), string title = default(string), string summary = default(string), int? itemState = default(int?), string description = default(string), System.DateTime? startDate = default(System.DateTime?), System.DateTime? endDate = default(System.DateTime?), string createdBy = default(string), System.DateTime? createdDate = default(System.DateTime?))
        {
            Id = id;
            Title = title;
            Summary = summary;
            ItemState = itemState;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            CreatedBy = createdBy;
            CreatedDate = createdDate;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Id")]
        public System.Guid? Id { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Title")]
        public string Title { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Summary")]
        public string Summary { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "ItemState")]
        public int? ItemState { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "StartDate")]
        public System.DateTime? StartDate { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "EndDate")]
        public System.DateTime? EndDate { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "CreatedBy")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "CreatedDate")]
        public System.DateTime? CreatedDate { get; set; }

    }
}