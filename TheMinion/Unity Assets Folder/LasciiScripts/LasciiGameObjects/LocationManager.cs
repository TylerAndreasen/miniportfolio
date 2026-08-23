using System;
using UnityEngine;
using LasciiUtility;

namespace LasciiGameObjects
{
    public class LocationManager
    {
        //FIELDS
        private readonly bool LOCATION_MANAGER_CLASS_DEBUG_TOGGLE = true;
        private string locationTitle, currentObject;
        private Response[] responses;
        private string[] validItems;
        private bool filled = false;
        //CONSTRUCTOR
        public LocationManager(String locationTitle, Response[] responses)
        {
            if (LOCATION_MANAGER_CLASS_DEBUG_TOGGLE) Debug.Log($"#DEBUG# LocationManager(String :{locationTitle}:, Response[{responses.Length}]) - Creating. #");
            this.locationTitle = locationTitle;
            this.responses = responses;
            this.validItems = this.GenerateValidObjects();
        }
        //METHODS
        private String[] GenerateValidObjects()
        {
            bool methodDebug = false;
            if (methodDebug || LOCATION_MANAGER_CLASS_DEBUG_TOGGLE) Debug.Log($"#DEBUG# LocationManager.GenerateValidObjects() - Creating list of Valid Objects. #");
            string[] output = new string[this.responses.Length];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = responses[i].GetObject();
            }
            if (methodDebug || LOCATION_MANAGER_CLASS_DEBUG_TOGGLE) Debug.Log($"#DEBUG# LocationManager.GenerateValidObjects() - Created List of :{output.Length}: Valid Objects. #");
            return output;
        }
        public bool IsFilled() { return this.filled; }
        public string GetCurrentObject() { return this.currentObject; }
        public string PushItem(string itemName)
        {
            bool methodDebug = true;
            if (methodDebug || LOCATION_MANAGER_CLASS_DEBUG_TOGGLE) Debug.Log($"#DEBUG# LocationManager.PushItem(string :{itemName}:) - Begin - Location :{this.locationTitle}: is filled {this.filled}. #");
            if (this.filled)
            {
                
                if (methodDebug || LOCATION_MANAGER_CLASS_DEBUG_TOGGLE) Debug.Log($"#ERROR# LocationManager.PullItem(string :{itemName}:) - Location :{this.locationTitle}: Already filled {this.filled}, Cannot add new item. #");
                return $"The {this.currentObject} is alread in the {this.locationTitle}";
            }
            int index = LasciiBasicTypeUtilities.IndexOf(this.validItems, itemName);
            if (index == -1)
            {
                if (methodDebug || LOCATION_MANAGER_CLASS_DEBUG_TOGGLE) Debug.Log($"#DEBUG# LocationManager.PullItem(string :{itemName}:) - Location :{this.locationTitle}: Item not recognized :{itemName}:. #");
                return $"#ERROR# Invalid input to LocationManager.PushItem(String :{itemName}:)";
            } else
            {
                if (methodDebug || LOCATION_MANAGER_CLASS_DEBUG_TOGGLE) Debug.Log($"#DEBUG# LocationManager.PullItem(string :{itemName}:) - Added Location :{this.locationTitle}: is filled :{this.filled}: Current :{this.currentObject}:. #");
                return responses[index].GetInsert(itemName, out this.filled, out this.currentObject);
            }
        }
        public string PullItem(string itemName)
        {
            bool methodDebug = true;
            if (methodDebug || LOCATION_MANAGER_CLASS_DEBUG_TOGGLE) Debug.Log($"#DEBUG# LocationManager.PullItem(string :{itemName}:) - Begin - Location :{this.locationTitle}: is filled {this.filled}. #");
            if (!this.filled)
            {
                if (methodDebug || LOCATION_MANAGER_CLASS_DEBUG_TOGGLE) Debug.Log($"#ERROR# LocationManager.PullItem(string :{itemName}:) - Location :{this.locationTitle}: NOT filled {this.filled}, Cannot remove item. #");
                return $"#ERROR# LocationManager.PullItem(string :{itemName}:) - Called on LM :{this.locationTitle}: with no Item within. #";
            }
            int index = LasciiBasicTypeUtilities.IndexOf(this.validItems, itemName);
            if (index == -1)
            {
                if (methodDebug || LOCATION_MANAGER_CLASS_DEBUG_TOGGLE) Debug.Log($"#DEBUG# LocationManager.PullItem(string :{itemName}:) - Location :{this.locationTitle}: Item not recognized :{itemName}:. #");
                return $"#ERROR# Invalid input to LocationManager.PullItem(String :{itemName}:)";
            } else
            {
                if (methodDebug || LOCATION_MANAGER_CLASS_DEBUG_TOGGLE) Debug.Log($"#DEBUG# LocationManager.PullItem(string :{itemName}:) - REMOVED Location :{this.locationTitle}: is filled :{this.filled}: Current :{this.currentObject}:. #");
                return responses[index].GetRemove(itemName, currentObject, out this.filled, out this.currentObject);
            }
        }
    }
}