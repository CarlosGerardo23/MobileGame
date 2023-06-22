
namespace GameDev
{

    public class DevMath 
    {
        public static float ConvertRange(float initialStart, float initialEnd, float desiredStart, float desiredEnd, float value)
        {
           
            // Primero, ajustamos el valor en el rango inicial al rango estándar de 0 a 1.
            float normalizedValue = (value - initialStart) / (initialEnd - initialStart);

            // Luego, mapeamos el valor normalizado al rango deseado.
            float convertedValue = desiredStart + (normalizedValue * (desiredEnd - desiredStart));

            return convertedValue;
        }
    }
}
