using System.Collections.Generic;
using UnityEngine;
// <summary>
// showcase of inheritance, interfaces and polymorphism
// </summary>
public class Zoo : MonoBehaviour
{
	List<Animal> animals;

	private void Start()
	{
		animals = new List<Animal>{new bird(), new Dog(), new Elephant(), new Bee()};

		foreach (Animal dier in animals)
		{
			dier.Move();
			dier.Eat();
			dier.AAAAA();
			dier.kick();
		}
	}
//public static void print(object message) => Debug.Log(message);
}

interface Ileg
{
	public void kick();
}

public abstract class Animal : Ileg
{
	public virtual void Eat() => Debug.Log("Eat");
	public abstract void Move();
	 public void AAAAA() => Debug.Log("AAAAA");
	 public abstract void kick();

}

public class bird : Animal
{
	public override void Eat() => Debug.Log("Eat Seed");
	public override void Move() => Debug.Log("Fly");
	public override void kick() => Debug.Log("kick");
}

public class Dog : Animal
{
	public override void Eat() => Debug.Log("Eat Bone");
	public override void Move() => Debug.Log("Run");
	public override void kick() => Debug.Log("kick");
}

public class Elephant : Animal
{
	public override void Eat() => Debug.Log("Eat Leaf");
	public override void Move() => Debug.Log("Walk");
	public override void kick() => Debug.Log("kick");
}

public class Bee : Animal
{
	public override void Move() => Debug.Log("Fly");
	public override void kick() => Debug.Log("kick");
}