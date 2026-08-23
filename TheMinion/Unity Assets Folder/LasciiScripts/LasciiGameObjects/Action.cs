using System;
using UnityEngine;
using LasciiMain;

namespace LasciiGameObjects
{
    public class Action
    {
        //FIELDS
        private static readonly bool ACTION_CLASS_DEBUG_TOGGLE = false;
        private String description;
        private Page destination;

        private TraitRequirement[] tr = null;
        private bool hasTraitRequirements = false, hideActionOnLacking = true, hasEffects = false;
        private Effect[] effects;

        //CONSTRUCTORS
        public Action(String description, Page destination)
        {
            if (ACTION_CLASS_DEBUG_TOGGLE) Debug.Log("#DEBUG# Action(String :" + description + ":, Page :" + destination.GetFullIndex() + ":) - Created Action. #");
            this.description = description;
            this.destination = destination;
            this.SetHideOnLacking();
        }
        public Action(String description, TraitRequirement[] tr, bool showActionOnLacking, Page destination) : this(description, destination)
        {
            if (ACTION_CLASS_DEBUG_TOGGLE) Debug.Log("#DEBUG# Action(String :" + description + ":, TraitRequirement[" + tr.Length + "], bool (showAction) :" + showActionOnLacking + ":, Page :" + destination.GetFullIndex() + ":) - Created Action. #");
            if (tr.Length > 0)
            {
                this.tr = tr;
                this.hasTraitRequirements = true;
            }
            this.SetHideOnLacking();
        }
        public Action(String description, Effect[] effects, Page destination) : this(description, destination)
        {
            if (ACTION_CLASS_DEBUG_TOGGLE) Debug.Log("#DEBUG# Action(String :" + description + ":, Effect[" + effects.Length + "], Page :" + destination.GetFullIndex() + ":) - Created Action. #");
            if (effects.Length > 0)
            {
                this.effects = effects;
                this.hasEffects = true;
            }
            this.SetHideOnLacking();
        }
        public Action(String description, TraitRequirement[] tr, bool showActionOnLacking, Effect[] effects, Page destination) : this(description, destination)
        {
            if (ACTION_CLASS_DEBUG_TOGGLE)
                Debug.Log(
                  "#DEBUG# Action(String :" + description + ":,"
                + " TraitRequirement[" + tr.Length + (tr.Length > 0 ? " :" + tr[0].GetTitle() + ":" : " : null") + "],"
                + " bool (showAction) :" + showActionOnLacking + ":,"
                + " Effect[" + effects.Length + (effects.Length > 0 ? " :" + effects[0].GetTitle() + ":" : " : null") + "],"
                + " Page :" + destination.GetFullIndex() + ":)"
                + " - Created Action. #");
            if (tr.Length > 0)
            {
                this.tr = tr;
                this.hasTraitRequirements = true;
            }
            if (effects.Length > 0)
            {
                this.effects = effects;
                this.hasEffects = true;
            }
            this.SetHideOnLacking();
        }
        //PROPERTIES
        //METHODS
        private void SetHideOnLacking()
        {
            bool methodDebug = false;
            this.hideActionOnLacking = true;
            if (!this.hasTraitRequirements)
            {
                this.hideActionOnLacking = false;
                if (methodDebug) Debug.Log("#DEBUG# Action.setHideOnLacking() - Hide this Action :" + hideActionOnLacking + ": because of Lacking TRs. #");
                return;
            }
            else
            {
                foreach (TraitRequirement TR in this.tr)
                {
                    if (!PlayerTraits.HasTraitCalled(TR.GetTitle()))
                    {
                        if (methodDebug) Debug.Log("#DEBUG# Action.setHideOnLacking() - Hide this Action :" + hideActionOnLacking + ": Because Player Lacks Trait of title :" + TR.GetTitle() + ":. #");
                        return;
                    }
                    else
                    {
                        Trait pt = PlayerTraits.GetTraitCalled(TR.GetTitle());
                        if (methodDebug) pt.DEBUG_TRAIT();
                        if (!TR.Compare(pt))
                        {

                            if (methodDebug) Debug.Log("#DEBUG# Action.setHideOnLacking() - Hide this Action :" + hideActionOnLacking + ": Because Player Failed Comparison on Trait :" + TR.GetTitle() + ":");
                            return;
                        }
                    }
                }
                this.hideActionOnLacking = false;
                if (methodDebug) Debug.Log("#DEBUG# Action.setHideOnLacking() - Hide this Action :" + hideActionOnLacking + ": Because Player passed all TRs[" + this.tr.Length + "]");
                return;
            }
        }
        public bool CanShowAction()
        {
            bool methodDebug = false;
            if (methodDebug || ACTION_CLASS_DEBUG_TOGGLE) Debug.Log("#DEBUG# Action.CanShowAction() - Begin #");
            int output = 0, trIndex = -1;
            foreach (TraitRequirement TR in this.tr)
            {
                String title = TR.GetTitle();
                trIndex++;
                bool playerHasTrait = PlayerTraits.HasTraitCalled(TR.GetTitle());
                bool showAnyways = TR.ShowActionRegardlessOfLackingTrait();
                if (methodDebug || ACTION_CLASS_DEBUG_TOGGLE) Debug.Log("#DEBUG# Action.CanShowAction() - TR[" + trIndex + "] {" + title + "} - Player Has the Trait :" + playerHasTrait + ":. #");
                if (!playerHasTrait)
                {
                    //PT does not contain a Trait of the proper title
                    if (methodDebug || ACTION_CLASS_DEBUG_TOGGLE) Debug.Log("#DEBUG# Action.CanShowAction() - TR[" + trIndex + "] {" + title + "} - Should Show without having Trait :" + showAnyways + ":. #");
                    if (showAnyways)
                    {
                        continue;
                    }
                    else
                    {
                        output = -1;
                        break;
                    }
                }
                bool Compares = TR.Compare(PlayerTraits.GetTraitCalled(title));
                if (methodDebug || ACTION_CLASS_DEBUG_TOGGLE) Debug.Log("#DEBUG# Action.CanShowAction() - TR[" + trIndex + "] {" + title + "} - Title :" + title + ":, Compares To Requirement :" + Compares + ":. #");
                if (Compares)
                {
                    //Trait Compares to the TR
                    continue; //Check the rest
                }

                if (TR.ShowActionRegardlessOfLackingTrait())
                {
                    if (methodDebug || ACTION_CLASS_DEBUG_TOGGLE) Debug.Log("#DEBUG# Action.CanShowAction() - TR[" + trIndex + "] {" + title + "} - Should Show despite failing comparison :" + showAnyways + ":. #");
                    continue;
                }
                else
                {
                    output = -1;
                    break;
                }
            }
            if (output == 0) output = 1;
            if (methodDebug || ACTION_CLASS_DEBUG_TOGGLE) Debug.Log("#DEBUG# Action.CanShowAction() - Returning :" + output + "::" + (output == -1 ? false : true) + ":. #");
            if (output == -1) return false;
            else return true;
        }
        public bool CanTakeAction()
        {
            if (!this.hasTraitRequirements) return true; //You can take the action if there is nothing stopping you.
            else
            {
                foreach (TraitRequirement TR in tr)
                {
                    if (PlayerTraits.HasTraitCalled(TR.GetTitle()))
                    {
                        Trait pt = PlayerTraits.GetTraitCalled(TR.GetTitle()); //This will return a Trait if called.
                        if (TR.Compare(pt)) //Note: The TraitRequirement.Compare(Trait) method does have a couple of conditions underwhich it will return false beyond not matching the comparison.
                        {
                            continue; //Don't return true here, only if all of the traits match their Requirements
                        }
                        else return false;
                    }
                    else return false;
                }
                return true; //Return true if all of the traits match their Requirements
            }
        }
        public void DEBUG()
        {
            String output = "";
            output += "\n\t\t\tDescription :" + this.description + ":\n\t\t\tDestination :" + this.destination.GetFullIndex() + ":";
            if (this.hasTraitRequirements && (this.tr[0] != null))
            {
                output += "\n\t\t\tTraitRequirements ";
                for (int i = 0; i < this.tr.Length; i++)
                {
                    output += "\n\t\t\t\t:\n\t\t\t\t" + this.tr[i].DEBUG(true) + "\n\t\t\t\t:";
                }
                output += "\n\t\t\tHide Action :" + this.hideActionOnLacking + ":";
            }
            else
                output += "\n\t\t\tNo TraitRequirements";

            if (this.hasEffects)
            {
                output += "\n\t\t\tEfects :";
                for (int i = 0; i < this.effects.Length; i++)
                {
                    output += "\n\t\t\t\t" + this.effects[i].DEBUG(true);
                }
            }
            else output += "\n\t\t\tNo Effects";
            output += "\n\t\t";
            Debug.Log(output);
        }
        public string DEBUG(bool unused)
        {
            String output = "";
            output += "\n\t\t\tDescription :" + this.description + ":\n\t\t\tDestination :" + this.destination.GetFullIndex() + ":";
            if (this.hasTraitRequirements && (this.tr[0] != null))
            {
                output += "\n\t\t\tTraitRequirements ";
                for (int i = 0; i < this.tr.Length; i++)
                {
                    output += "\n\t\t\t\t:\n\t\t\t\t" + this.tr[i].DEBUG(true) + "\n\t\t\t\t:";
                }
                output += "\n\t\t\tHide Action :" + this.hideActionOnLacking + ":";
            }
            else
                output += "\n\t\t\tNo TraitRequirements";

            if (this.hasEffects)
            {
                output += "\n\t\t\tEfects :";
                for (int i = 0; i < this.effects.Length; i++)
                {
                    output += "\n\t\t\t\t" + this.effects[i].DEBUG(true);
                }
            }
            else output += "\n\t\t\tNo Effects";
            output += "\n\t\t";
            return (output);
        }
        public bool ApplyEffects()
        {
            bool output = true;
            foreach (Effect e in this.effects)
            {
                if (!e.ApplyEffect(false))
                {
                    output = false;
                }
            }
            return output;
        }
        public void DebugEffects()
        {
            foreach (Effect e in effects)
            {
                e.DEBUG();
            }
        }
        public bool HasTraitRequirements() { return this.hasTraitRequirements; }
        public string GetDescription() { return this.description; }
        public Page GetDestination() { return this.destination; }
        public TraitRequirement[] GetTR() { return this.tr; }
        public bool HasEffects() { return this.hasEffects; }
        public Effect[] GetEffects() { return this.effects; }
    }
}