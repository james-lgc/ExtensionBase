using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//Used to track story progress
public struct StoryIndex
{
	[SerializeField] private int mythID;
	public int MythID { get { return mythID; } }

	[SerializeField] private int storyID;
	public int StoryID { get { return storyID; } }

	[SerializeField] private int threadID;
	public int ThreadID { get { return threadID; } }

	[SerializeField] private int stageID;
	public int StageID { get { return stageID; } }

	public int[] IDs { get { return new int[] { mythID, storyID, threadID, stageID }; } }

	//Create instance from an array of ints
	public StoryIndex(int[] sentIDs)
	{
		if (sentIDs.Length != 4)
		{
			mythID = 0;
			storyID = 0;
			threadID = 0;
			stageID = 0;
		}
		mythID = sentIDs[0];
		storyID = sentIDs[1];
		threadID = sentIDs[2];
		stageID = sentIDs[3];
	}

	//Compare to another StoryIndex by hashcode
	public override bool Equals(object obj)
	{
		if (obj == null) return false;
		if (obj.GetType() != this.GetType()) return false;
		StoryIndex sentIndex = (StoryIndex)obj;
		return GetHashCode() == sentIndex.GetHashCode();
	}

	public override int GetHashCode()
	{
		int hashCode = 31;
		hashCode = hashCode * 37 + mythID;
		hashCode = hashCode * 37 + storyID;
		hashCode = hashCode * 37 + threadID;
		hashCode = hashCode * 37 + stageID;
		return hashCode;
	}

	public static bool operator ==(StoryIndex index1, StoryIndex index2)
	{
		return index1.Equals(index2);
	}

	public static bool operator !=(StoryIndex index1, StoryIndex index2)
	{
		return !index1.Equals(index2);
	}

	public static bool operator <(StoryIndex index1, StoryIndex index2)
	{
		if (index1.MythID != index2.MythID) return false;
		if (index1.StoryID != index2.StoryID) return false;
		if (index1.ThreadID != index2.ThreadID) return false;
		if (index1.StageID < index2.StageID) return true;
		return false;
	}

	public static bool operator >(StoryIndex index1, StoryIndex index2)
	{
		if (index1.MythID != index2.MythID) return false;
		if (index1.StoryID != index2.StoryID) return false;
		if (index1.ThreadID != index2.ThreadID) return false;
		if (index1.StageID > index2.StageID) return true;
		return false;
	}

	public static bool operator <=(StoryIndex index1, StoryIndex index2)
	{
		if (index1.MythID != index2.MythID) return false;
		if (index1.StoryID != index2.StoryID) return false;
		if (index1.ThreadID != index2.ThreadID) return false;
		if (index1.StageID <= index2.StageID) return true;
		return false;
	}

	public static bool operator >=(StoryIndex index1, StoryIndex index2)
	{
		if (index1.MythID != index2.MythID) return false;
		if (index1.StoryID != index2.StoryID) return false;
		if (index1.ThreadID != index2.ThreadID) return false;
		if (index1.StageID >= index2.StageID) return true;
		return false;
	}
}

