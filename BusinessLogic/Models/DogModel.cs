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
                throw new ArgumentOutOfRangeException("tail length cant be negative");
            }
            _length = value;
        }
    }

    [Required]
    public int Weight
    {
        get => _weight; set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException("Weight cant be equal zero or less");
            }
            _weight = value;
        }
    }


}
