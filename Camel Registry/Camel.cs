using System.ComponentModel.DataAnnotations;

public class Camel
{
	public int Id { get; set; }
	public required string Name { get; set; }
	public uint Color { get; set; }
	[Range(1, 2)]
	public int HumpCount { get; set; }
	public DateTime LastFed {  get; set; }
}
