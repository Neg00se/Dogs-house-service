using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Models;

public class DogModel
{
    private int _length;
    private int _weight;

    [Required]
    public string Name { get; set; }

    [Required]
    public string Color { get; set; }

    [Required]
    public int TailLength
    {
        get { return _length; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(TailLength), "tail length cant be negative");
            }
            _length = value;
        }
    }

    [Required]
    public int Weight
    {
        get { return _weight; }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(Weight), "Weight cant be equal zero or less");
            }
            _weight = value;
        }
    }


}
