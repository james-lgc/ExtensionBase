using UnityEngine;

[System.Serializable]
//Data version of Unity's Transform class
//Used to store or pass transform information in cases where
//scene may be unloaded or object destoryed
public struct TransformValue
{
	//3D position values
	[Header("Position")] [SerializeField] private float xPosition;
	[SerializeField] private float yPosition;
	[SerializeField] private float zPosition;
	public Vector3 Position { get { return new Vector3(xPosition, yPosition, zPosition); } }

	//Quaternion rotation values
	[Header("Rotation")] [SerializeField] private float wRotation;
	[SerializeField] private float xRotation;
	[SerializeField] private float yRotation;
	[SerializeField] private float zRotation;
	public Quaternion Rotation { get { return new Quaternion(xRotation, yRotation, zRotation, wRotation); } }

	//Create instance from Transform
	public TransformValue(Transform sentTrans)
	{
		xPosition = sentTrans.position.x;
		yPosition = sentTrans.position.y;
		zPosition = sentTrans.position.z;
		xRotation = sentTrans.rotation.x;
		yRotation = sentTrans.rotation.y;
		zRotation = sentTrans.rotation.z;
		wRotation = sentTrans.rotation.w;
	}

	//Create instance from TransformValue
	public TransformValue(TransformValue sentTransValue)
	{
		xPosition = sentTransValue.Position.x;
		yPosition = sentTransValue.Position.y;
		zPosition = sentTransValue.Position.z;
		xRotation = sentTransValue.Rotation.x;
		yRotation = sentTransValue.Rotation.y;
		zRotation = sentTransValue.Rotation.z;
		wRotation = sentTransValue.Rotation.w;
	}

	//Compare hashcodes to determine if equal
	public override bool Equals(object obj)
	{
		if (obj == null) return false;
		if (obj.GetType() != this.GetType()) return false;
		TransformValue sentTrans = (TransformValue)obj;
		return GetHashCode() == sentTrans.GetHashCode();
	}

	//Use primes to generate a hashcode
	public override int GetHashCode()
	{
		int hashCode = 43;
		hashCode = hashCode * 47 + xPosition.GetHashCode();
		hashCode = hashCode * 47 + yPosition.GetHashCode();
		hashCode = hashCode * 47 + zPosition.GetHashCode();
		hashCode = hashCode * 47 + xRotation.GetHashCode();
		hashCode = hashCode * 47 + yRotation.GetHashCode();
		hashCode = hashCode * 47 + zRotation.GetHashCode();
		hashCode = hashCode * 47 + wRotation.GetHashCode();
		return hashCode;
	}

	//Compare to another TransformValue
	public static bool operator ==(TransformValue trans1, TransformValue trans2)
	{
		return trans1.Equals(trans2);
	}

	public static bool operator !=(TransformValue trans1, TransformValue trans2)
	{
		return !trans1.Equals(trans2);
	}

	//Compare to a vector3
	public static bool operator ==(TransformValue trans1, Vector3 trans2)
	{
		return trans1.Position == trans2;
	}

	public static bool operator !=(TransformValue trans1, Vector3 trans2)
	{
		return trans1.Position == trans2;
	}

	//Compare to a Quaternion
	public static bool operator ==(TransformValue trans1, Quaternion trans2)
	{
		return trans1.Rotation == trans2;
	}

	public static bool operator !=(TransformValue trans1, Quaternion trans2)
	{
		return trans1.Rotation == trans2;
	}

	//Compare to a Transform
	public static bool operator ==(TransformValue trans1, Transform trans2)
	{
		return !(trans1.Position == trans2.position && trans1.Rotation == trans2.rotation);
	}

	public static bool operator !=(TransformValue trans1, Transform trans2)
	{
		return !(trans1.Position == trans2.position && trans1.Rotation == trans2.rotation);
	}
}
