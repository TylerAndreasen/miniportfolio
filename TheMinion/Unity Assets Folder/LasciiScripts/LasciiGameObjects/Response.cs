using System;
using UnityEngine;

namespace LasciiGameObjects
{
    public class Response
    {
        //FIELDS
        private readonly bool RESPONSE_CLASS_DEBUG = false;
        private string hideableObject, insertLine, removeLine;
        private bool placeableInLocaiton;
        //CONSTRUCTOR
        public Response(string hideableObject, bool placeableInLocaiton, string insertLine, string removeLine) 
        {
            //if (RESPONSE_CLASS_DEBUG) Debug.Log($"#DEBUG# Response(string :{hideableObject}:, bool :{placeableInLocaiton}:, string :{insertLine}:, string :{removeLine}:) . #");
            this.hideableObject = hideableObject;
            this.placeableInLocaiton = placeableInLocaiton;
            this.insertLine = insertLine;
            this.removeLine = removeLine;
        }
        //METHODS
        public string GetObject() { return this.hideableObject; }
        public bool GetPlaceable() { return this.placeableInLocaiton; }
        public string GetRAWInsertLine() { return this.insertLine; }
        public string GetRAWRemoveLine() { return this.removeLine; }
        public string GetInsert(string objectName, out bool filledFlag, out string objectTitle)
        {
            if (RESPONSE_CLASS_DEBUG) Debug.Log($"#DEBUG# Response.GetInsert(string :{objectName}:, out, out) - Begin. #");
            if (objectName != hideableObject)
            {
                filledFlag = false;
                objectTitle = null;
                if (RESPONSE_CLASS_DEBUG) Debug.Log($"#DEBUG# Response.GetInsert(string :{objectName}:, out, out) - Incorrect object passed to Response :{this.hideableObject}:. #");
                return $"#ERROR# Response.GetInsert(String :{objectName}:) - Does not match Hideable :{this.hideableObject}:. #";
            } else if (!placeableInLocaiton)
            {
                filledFlag = false;
                objectTitle = null;
                if (RESPONSE_CLASS_DEBUG) Debug.Log($"#DEBUG# Response.GetInsert(string :{objectName}:, out, out) - Object does not fit in location. #");
                return this.insertLine;
            } else
            {
                filledFlag = true;
                objectTitle = this.hideableObject;
                if (RESPONSE_CLASS_DEBUG) Debug.Log($"#DEBUG# Response.GetInsert(string :{objectName}:, out, out) - Object Fits, returns insertion and fills the location. #");
                return this.insertLine;
            }
        }
        public string GetRemove(string removeObject, string currentObject, out bool filledFlag, out string objectTitle)
        {
            if (removeObject != hideableObject)
            {
                filledFlag = true;
                objectTitle = currentObject;
                if (RESPONSE_CLASS_DEBUG) Debug.Log($"#DEBUG# Response.GetInsert(string :{removeObject}:, out, out) - Incorrect object passed to Response :{this.hideableObject}:. #");
                return $"#ERROR# Response.GetInsert(String :{removeObject}:) - Does not match Hideable :{this.hideableObject}:. #";
            } else if (!placeableInLocaiton)
            {
                filledFlag = false;
                objectTitle = null;
                if (RESPONSE_CLASS_DEBUG) Debug.Log($"#DEBUG# Response.GetInsert(string :{removeObject}:, out, out) - Object does not fit in location. #");
                return this.removeLine;
            } else
            {
                filledFlag = true;
                objectTitle = this.hideableObject;
                if (RESPONSE_CLASS_DEBUG) Debug.Log($"#DEBUG# Response.GetInsert(string :{removeObject}:, out, out) - Object Fits, returns removes object and empties location. #");
                return this.removeLine;
            }
        }
    }
}