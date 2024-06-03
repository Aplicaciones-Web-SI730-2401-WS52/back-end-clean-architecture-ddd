namespace Application.Test;

public class CalculatorUnitTest
{
    [Fact]
    public void Sum_ValidInput_ReturnsSum()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Sum(1, 2);

        // Assert
        Assert.Equal(3, result);
    }
}