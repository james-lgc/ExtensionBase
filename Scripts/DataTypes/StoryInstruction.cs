using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//Instruction to control story progress
public struct StoryInstruction
{
	[SerializeField] private int stagePointID;
	public int StagePointID { get { return stagePointID; } }

	[SerializeField] private StoryIndex sIndex;
	public StoryIndex SIndex { get { return sIndex; } }

	public enum InstructionEnum { None, ActivateStory, ActivateThread, StageUp, StagePointUp };
	[SerializeField] private InstructionEnum instruction;
	public InstructionEnum Instruction { get { return instruction; } }

	public override bool Equals(object obj)
	{
		if (obj == null) return false;
		if (obj.GetType() != this.GetType()) return false;
		StoryInstruction sentInstruction = (StoryInstruction)obj;
		return GetHashCode() == sentInstruction.GetHashCode();
	}

	public override int GetHashCode()
	{
		int hashCode = 53;
		hashCode = hashCode * 59 + stagePointID;
		hashCode = hashCode * 59 + sIndex.GetHashCode();
		hashCode = hashCode * 59 + instruction.GetHashCode();
		return hashCode;
	}

	public static bool operator ==(StoryInstruction instruction1, StoryInstruction instruction2)
	{
		return instruction1.Equals(instruction2);
	}

	public static bool operator !=(StoryInstruction instruction1, StoryInstruction instruction2)
	{
		return !instruction1.Equals(instruction2);
	}
}
