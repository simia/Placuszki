using System;

public class Player
{
	public String name { get; set; }
	public String id { get; set; }
	//TODO: handle to new car
	public GenericInput input;

		public Player(String name)
		{
			this.name = name;
			this.id = Guid.NewGuid().ToString();
			//TODO: spawn a new car
			this.input = new GenericInput();
		}
}
