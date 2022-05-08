/// <summary>
///     Maybe useful in the future to use ref params when is not possible. 
/// </summary>
/// <typeparam name="T">The type.</typeparam>
public class Ref<T>
{
    private T backing;
    public T Value {get{return backing;}}
    public Ref(T reference)
    {
        backing = reference;
    }
}